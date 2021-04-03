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
    name: 'ListRecipe',
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
    name: 'ListFood',
    component: function () {
      return import(/* webpackChunkName: "foods" */ '../views/Foods/Index.vue')
    }
  },
  {
    path: '/foods/create',
    name: 'CreateFood',
    component: function () {
      return import(/* webpackChunkName: "createFood" */ '../views/Foods/Create.vue')
    }
  },

  {
    path: '/users',
    name: 'ListUser',
    component: function () {
      return import(/* webpackChunkName: "users" */ '../views/Users/Index.vue')
    }
  },
  {
    path: '/users/create',
    name: 'CreateUser',
    component: function () {
      return import(/* webpackChunkName: "createUser" */ '../views/Users/Create.vue')
    }
  }
]

const router = new VueRouter({
  mode: "history",
  routes
})

export default router
