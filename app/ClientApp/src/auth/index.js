import Vue from 'vue';
import Vuex from 'vuex';
import { createAuthAPI, deleteAuthAPI, getUser, healthCheck } from '@/api';

Vue.use(Vuex)

const userDefault = () => ({
  first_name: '',
  roles: []
})

const store = new Vuex.Store({
  state: {
    auth: false,
    token: '',
    tokenType: '',
    userId: '',
    isAdmin: false,
    user: userDefault(),
    snackbar: {},
    loadingMain: true,
    loadingMainError: false,
    overlay: true,
    randomizeQuote: 0,
    quotes: [
      "We're poor and waiting for server to wake up...",
      "Wait for a bit, dollar is very high...",
      "If you wanna a faster website, make a donation to us...",
      "Waiting it's the hardest thing...",
    ],
    ingredients_measures: [
      {
        'name': 'g',
        'id': 1
      },
      {
        'name': 'mg',
        'id': 2
      },
      {
        'name': 'kg',
        'id': 3
      },
      {
        'name': 'L',
        'id': 4
      },
      {
        'name': 'ml',
        'id': 5
      },
      {
        'name': 'un',
        'id': 6
      },
    ],
    nutrition_facts_measures: [
      {
        'name': 'g',
        'id': 1
      },
      {
        'name': 'mg',
        'id': 2
      },
      {
        'name': 'kg',
        'id': 3
      },
      {
        'name': 'L',
        'id': 4
      },
      {
        'name': 'ml',
        'id': 5
      },
    ],
    mass_measures: [
      {
        'name': 'g',
        'id': 1
      },
      {
        'name': 'mg',
        'id': 2
      },
      {
        'name': 'kg',
        'id': 3
      },
      {
        'name': 'un',
        'id': 6
      },
    ],
    weight_measures: [
      {
        'name': 'L',
        'id': 4
      },
      {
        'name': 'ml',
        'id': 5
      },
      {
        'name': 'un',
        'id': 6
      },
    ],
    mass_measures_keys: [1, 2, 3],
    weight_measures_keys: [4, 5],
  },

  mutations: {
    login: state => state.auth = state.auth = true,
    logout: state => { state.auth = false; state.isAdmin = false, state.user = userDefault() },
    setTokenType: (state, tokenType) => state.tokenType = tokenType,
    setToken: (state, token) => state.token = token,
    setUserId: (state, userId) => state.userId = userId,
    setIsAdmin: (state, isAdmin) => state.isAdmin = isAdmin,
    setUser: (state, user) => state.user = user,
    setSnackbar: (state, snackbar) => state.snackbar = snackbar,
    setLoadingMain: (state, loadingMain) => state.loadingMain = loadingMain,
    setLoadingMainError: (state, loadingMainError) => state.loadingMainError = loadingMainError,
    setOverlay: (state, overlay) => state.overlay = overlay,
    setRandomizeQuote: (state, randomizeQuote) => state.randomizeQuote = randomizeQuote,
  },

  getters: {
    auth: state => state.auth,
    isAdmin: state => state.isAdmin,
    getUserId: state => state.userId,
    getUser: state => state.user,
    getLoadingMain: state => state.loadingMain,
    getLoadingMainError: state => state.loadingMainError,
    getOverlay: state => state.overlay,
    getQuotes: state => state.quotes,
    getRandomizeQuote: state => state.randomizeQuote,
  },

  actions: {
    setSnackbar({ commit }, snackbar) {
      snackbar.show = true,
        commit('setSnackbar', snackbar);
    },
    tryAgain({ dispatch, commit }) {
      return new Promise((resolve, reject) => {
        for (let index = 0; index < 4; index++) {
          (function (i) {
            setTimeout(function () {
              let quotesLength = store.state.quotes.length;
              let rand = Math.floor(Math.random() * quotesLength);
              commit('setRandomizeQuote', rand);
              healthCheck()
                .then(() => {
                  dispatch('loadApplication');
                  resolve();
                  return null;
                }).
                catch(() => { });
            }, index * 5000);
          })(index);
        }
        setTimeout(reject, 20000);
      });
    },
    verifyConnection({ dispatch }) {
      return new Promise((resolve, reject) => {
        healthCheck()
          .then(() => {
            dispatch('loadApplication');
            dispatch('ActionSetLoadingMain', false);
            resolve();
          }).
          catch(() => {
            dispatch('ActionSetLoadingMainError', true);
            dispatch('tryAgain')
              .then(() => {
                dispatch('ActionSetLoadingMain', false);
                resolve();
              })
              .catch(() => {
                dispatch("setSnackbar", {
                  text: `${this.$vuetify.lang.t('$vuetify.network_error')}.`,
                  color: "error",
                });
                dispatch('ActionSetLoadingMain', false);
                dispatch('ActionSetLoadingMainError', false);
                resolve();
              });
          });
      });
    },

    async loadApplication({ dispatch }) {
      let accessToken = localStorage.getItem('access_token');
      let userId = localStorage.getItem('user_id');
      let tokenType = localStorage.getItem('token_type');
      if (userId != null) {
        createAuthAPI(tokenType, accessToken)
        await getUser(userId).then((user) => {
          dispatch('ActionSetToken', accessToken);
          dispatch('ActionSetUser', user.data);
          dispatch('ActionSetUserId', user.data.id);
          dispatch('ActionSetIsAdmin', user.data);
          dispatch('loginAuth', true);
          dispatch('ActionSetLoadingMain', false);
          dispatch('ActionSetOverlay', false);
        })
          .catch(() => {
            dispatch("setSnackbar", {
              text: `${this.$vuetify.lang.t('$vuetify.network_error')}.`,
              color: "error",
            });
          })
      }
    },
    ActionSetUser: ({ commit }, payload) => {
      commit('setUser', payload)
    },
    ActionSetUserId: ({ commit }, payload) => commit('setUserId', payload),
    ActionSetIsAdmin: ({ commit }, payload) => commit('setIsAdmin', payload.roles.length > 0),
    ActionSetLoadingMain: ({ commit }, payload) => commit('setLoadingMain', payload),
    ActionSetLoadingMainError({ commit }, loadingMainError) { commit('setLoadingMainError', loadingMainError); },
    loginAuth: ({ commit }) => commit('login'),
    logoutAuth: ({ commit }) => commit('logout'),
    ActionSetToken: ({ commit }, payload) => commit('setToken', payload),
    ActionSetOverlay: ({ commit }, payload) => commit('setOverlay', payload),
    async doLogin({ dispatch }, payload) {
      let now = new Date().getTime();
      let access = {
        token_type: payload.token_type,
        access_token: payload.access_token,
        expires_in: payload.expires_in,
        refresh_token: payload.refresh_token,
        now: now,
        user_id: payload.user_id,
      };
      localStorage.setItem('token_type', access.token_type);
      localStorage.setItem('access_token', access.access_token);
      localStorage.setItem('expires_in', access.expires_in);
      localStorage.setItem('refresh_token', access.refresh_token);
      localStorage.setItem('user_id', access.user_id);
      localStorage.setItem('now', access.now.toString());
      Promise.all([
        dispatch('loadApplication'),
      ]).then(() => { })
    },

    doLogout: ({ dispatch }) => {
      localStorage.clear();
      deleteAuthAPI();
      dispatch('logoutAuth');
      dispatch('ActionSetOverlay', false);
      window._Vue.$router.push({ name: 'Home' }).catch(() => { });
    },
  }
});

export { store }