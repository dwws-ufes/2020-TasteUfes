import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import { store } from '@/auth';

Vue.config.productionTip = false

if (localStorage.getItem('access_token') != '') {
  Promise.all([
    store.dispatch('loadApplication'),
  ]).then(() => {
    window._Vue = new Vue({
      store,
      vuetify,
      router,
      render: function (h) { return h(App) }
    }).$mount('#app')

  });
} else {
  window._Vue = new Vue({
    store,
    vuetify,
    router,
    render: function (h) { return h(App) }
  }).$mount('#app')
}
