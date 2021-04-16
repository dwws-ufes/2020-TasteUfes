import Vue from 'vue';
import Vuex from 'vuex';
import { createAuthAPI, deleteAuthAPI, getUser } from '@/api';

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
  },

  getters: {
    auth: state => state.auth,
    isAdmin: state => state.isAdmin,
    getUserId: state => state.userId,
    getUser: state => state.user,
  },

  actions: {
    setSnackbar({ commit }, snackbar) {
      snackbar.show = true,
        commit('setSnackbar', snackbar);
    },
    async loadApplication({ dispatch }) {
      let accessToken = localStorage.getItem('access_token');
      let userId = localStorage.getItem('user_id');
      let tokenType = localStorage.getItem('token_type');
      if (userId != null) {
        createAuthAPI(tokenType, accessToken)
        await getUser(userId).then((user) => {
          dispatch('ActionSetToken', accessToken),
            dispatch('ActionSetUser', user.data),
            dispatch('ActionSetUserId', user.data.id),
            dispatch('ActionSetIsAdmin', user.data),
            dispatch('loginAuth', true)

        })
      }
    },

    ActionSetUser: ({ commit }, payload) => {
      commit('setUser', payload)
    },
    ActionSetUserId: ({ commit }, payload) => commit('setUserId', payload),
    ActionSetIsAdmin: ({ commit }, payload) => commit('setIsAdmin', payload.roles.length > 0),
    loginAuth: ({ commit }) => commit('login'),
    logoutAuth: ({ commit }) => commit('logout'),
    ActionSetToken: ({ commit }, payload) => commit('setToken', payload),
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
        dispatch('loginAuth'),
        dispatch('ActionSetToken', access.access_token),
        dispatch('ActionSetUserId', access.user_id)
      ]).then(() => {})
    },

    doLogout: ({ dispatch }) => {
      localStorage.clear();
      deleteAuthAPI();
      dispatch('logoutAuth');
      window._Vue.$router.push({ name: 'Home' }).catch(() => { });
    },

    loadSession: ({ dispatch }, { token_type, access_token }) => {
      if (access_token != null) {
        Promise.all([
          createAuthAPI(token_type, access_token),
          dispatch('ActionSetToken', access_token),
          dispatch('loginAuth'),
        ]);
      } else {
        dispatch('doLogout');
      }
    }
  }
});

export { store }