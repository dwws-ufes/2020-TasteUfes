import Vue from 'vue';
import Vuex from 'vuex';
import { createAuthAPI, deleteAuthAPI, getUser } from '@/api';

Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    auth: false,
    token: '',
    tokenType: '',
    userId: '',
    user: { first_name: '' },
    ingredients_measures: [
      {
        'name': 'G',
        'id': 1
      },
      {
        'name': 'MG',
        'id': 2
      },
      {
        'name': 'KG',
        'id': 3
      },
      {
        'name': 'L',
        'id': 4
      },
      {
        'name': 'ML',
        'id': 5
      },
      {
        'name': 'UN',
        'id': 6
      },
      {
        'name': 'KCAL',
        'id': 7
      },
    ],
    nutrition_facts_measures: [
      {
        'name': 'G',
        'id': 1
      },
      {
        'name': 'MG',
        'id': 2
      },
      {
        'name': 'KG',
        'id': 3
      },
      {
        'name': 'L',
        'id': 4
      },
      {
        'name': 'ML',
        'id': 5
      },
    ],
  },

  mutations: {
    login: state => state.auth = state.auth = true,
    logout: state => state.auth = false,
    setTokenType: (state, tokenType) => state.tokenType = tokenType,
    setToken: (state, token) => state.token = token,
    setUserId: (state, userId) => state.userId = userId,
    setUser: (state, user) => state.user = user,
  },

  getters: {
    auth: state => state.auth,
  },

  actions: {
    async loadApplication({ dispatch }) {
      let accessToken = localStorage.getItem('access_token');
      let userId = localStorage.getItem('user_id');

      if (userId != null) {
        await getUser(userId).then((user) => {
          Promise.all([
            dispatch('ActionSetToken', accessToken),
            dispatch('ActionSetUser', user.data),
          ]).finally(() => {
            // resolve();
          });

        })
      }
    },

    async load({ dispatch }, { userId, accessToken }) {
      // return new Promise((resolve, reject) => {
      await getUser(userId).then((user) => {
        Promise.all([
          dispatch('ActionSetToken', accessToken),
          dispatch('ActionSetUser', user.data),
        ]).finally(() => {
          // resolve();
        });
        // })
        //   .catch((error) => {
        //     console.log(error.response);
        //     reject(error.response);
        //   });

      })
    },

    ActionSetUser: ({ commit }, payload) => {
      commit('setUser', payload)
    },
    ActionSetUserId: ({ commit }, payload) => commit('setUserId', payload),
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
      dispatch('loginAuth');
      dispatch('ActionSetToken', access.access_token);
      dispatch('ActionSetUserId', access.user_id);
    },

    doLogout: ({ dispatch }) => {
      localStorage.clear();
      deleteAuthAPI();
      dispatch('logoutAuth');
      window._Vue.$router.push({ name: 'Home' }).catch(() => { });
    },

    loadSession: ({ dispatch }, { token_type, access_token }) => {
      if (access_token != null) {
        createAuthAPI(token_type, access_token);
        dispatch('ActionSetToken', access_token);
        dispatch('loginAuth');
      } else {
        dispatch('doLogout');
      }
    }
  }
});

export { store }