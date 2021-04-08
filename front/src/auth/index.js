import Vue from 'vue';
import Vuex from 'vuex';
import { createAuthAPI } from "@/api/data"
import { deleteAuthAPI } from "@/api/data"

Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
        auth: false,
        token: localStorage.getItem("access_token"),
    },
    mutations: {
        loginAuth: state => state.auth = !state.auth,
        setToken: (state, token) => state.token = token,
    },
    getters: {
        auth: state => state.auth,
    },
    actions: {
        loginAuth: ({ commit }) => commit('loginAuth'),
        setToken: ({ commit }, payload) => commit('setToken', payload),
        doLogin: ({ dispatch }, payload) => {
            localStorage.setItem("access_token", payload.access_token);
            localStorage.setItem("expires_in", payload.expires_in);
            localStorage.setItem("now", payload.now);
            createAuthAPI(payload.access_token);
            dispatch('setToken', payload.access_token);
            dispatch('loginAuth');
        },
        doLogout: ({ dispatch }) => {
            localStorage.clear();
            deleteAuthAPI();
            dispatch('loginAuth')
            window._Vue.$router.push({ name: "Home" }).catch(()=>{});

        },
        loadSession: ({ dispatch }, access_token) => {
            try {
                createAuthAPI(access_token);
                dispatch('setToken', access_token);
                dispatch('loginAuth');
            } catch(error) {
                dispatch('doLogout');
            }
        }
    }
});

export { store }