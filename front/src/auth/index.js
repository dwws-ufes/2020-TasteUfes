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
            dispatch('logoutAuth');
            console.log("Deslogou do site")
            window._Vue.$router.push({ name: "Home" }).catch(() => { });
        },
        loadSession: ({ dispatch }, access_token) => {
            // try {
                if (access_token != null) {
                    createAuthAPI(access_token);
                    dispatch('setToken', access_token);
                    dispatch('loginAuth');
                } else {
                    dispatch('doLogout');
                }
            // } catch (error) {
            //     dispatch('doLogout');
            // }
        }
    }
});

export { store }