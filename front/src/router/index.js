import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: function () {
      return import(/* webpackChunkName: "home" */ '../views/Home.vue')
    }
  },
  {
    path: '/recipes',
    name: 'Recipes',
    component: function () {
      return import(/* webpackChunkName: "recipes" */ '../views/Recipes/Index.vue')
    }
  },
  {
    path: '/recipes/create',
    name: 'CreateRecipe',
    component: function () {
      return import(/* webpackChunkName: "createRecipe" */ '../views/Recipes/Create.vue')
    }
  },

  {
    path: '/foods',
    name: 'Foods',
    component: function () {
      return import(/* webpackChunkName: "foods" */ '../views/Foods/Index.vue')
    }
  },
  {
    path: '/Foods/create',
    name: 'CreateFood',
    component: function () {
      return import(/* webpackChunkName: "createFood" */ '../views/Foods/Create.vue')
    }
  }
]

const router = new VueRouter({
  mode: "history",
  routes
})

export default router
