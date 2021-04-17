import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import { store } from '@/auth';

Vue.config.productionTip = false

store.dispatch('verifyConnection');
window._Vue = new Vue({
  store,
  vuetify,
  router,
  render: function (h) { return h(App) }
}).$mount('#app')
