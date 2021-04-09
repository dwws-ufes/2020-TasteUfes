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
        login: state => state.auth = state.auth = true,
        setToken: (state, token) => state.token = token,
        logout: state => state.auth = false,
    },
    getters: {
        auth: state => state.auth,
    },
    actions: {
        loginAuth: ({ commit }) => commit('login'),
        logoutAuth: ({ commit }) => commit('logout'),
        setToken: ({ commit }, payload) => commit('setToken', payload),
        doLogin: ({ dispatch }, payload) => {
            let now = new Date().getTime();
            let access = {
                token_type: payload.token_type,
                access_token: payload.access_token,
                expires_in: payload.expires_in,
                refresh_token: payload.refresh_token,
                now: now,
            };
            localStorage.setItem("token_type", access.token_type);
            localStorage.setItem("access_token", access.access_token);
            localStorage.setItem("expires_in", access.expires_in);
            localStorage.setItem("refresh_token", access.refresh_token);
            localStorage.setItem("now", access.now.toString());
            createAuthAPI(access.access_token);
            dispatch('setToken', access.access_token);
            dispatch('loginAuth');
        },
        doLogout: ({ dispatch }) => {
            localStorage.clear();
            deleteAuthAPI();
            dispatch('logoutAuth');
            window._Vue.$router.push({ name: "Home" }).catch(() => { });
        },
        loadSession: ({ dispatch }, access_token) => {
            if (access_token != null) {
                createAuthAPI(access_token);
                dispatch('setToken', access_token);
                dispatch('loginAuth');
            } else {
                dispatch('doLogout');
            }
        }
    }
});

export { store }