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
    path: '/recipe/create',
    name: 'CreateRecipe',
    component: function () {
      return import(/* webpackChunkName: "createRecipe" */ '../views/Recipes/Create.vue')
    }
  },
  {
    path: '/recipe',
    name: 'ListRecipe',
    component: function () {
      return import(/* webpackChunkName: "recipe" */ '../views/Recipes/Index.vue')
    }
  },
  {
    path: '/recipe/edit/:id',
    name: 'EditRecipe',
    component: function () {
      return import(/* webpackChunkName: "editRecipe" */ '../views/Recipes/Edit.vue')
    }
  },
  {
    path: '/recipe/details/:id',
    name: 'DetailsRecipe',
    component: function () {
      return import(/* webpackChunkName: "detailsRecipe" */ '../views/Recipes/Details.vue')
    }
  },

  {
    path: '/food/create',
    name: 'CreateFood',
    component: function () {
      return import(/* webpackChunkName: "createFood" */ '../views/Foods/Create.vue')
    }
  },
  {
    path: '/food',
    name: 'ListFood',
    component: function () {
      return import(/* webpackChunkName: "food" */ '../views/Foods/Index.vue')
    }
  },
  {
    path: '/food/edit/:id',
    name: 'EditFood',
    component: function () {
      return import(/* webpackChunkName: "editFood" */ '../views/Foods/Edit.vue')
    }
  },
  {
    path: '/food/details/:id',
    name: 'DetailsFood',
    component: function () {
      return import(/* webpackChunkName: "detailsFood" */ '../views/Foods/Details.vue')
    }
  },

  {
    path: '/user/create',
    name: 'CreateUser',
    component: function () {
      return import(/* webpackChunkName: "createUser" */ '../views/Users/Create.vue')
    }
  },
  {
    path: '/user',
    name: 'ListUser',
    component: function () {
      return import(/* webpackChunkName: "user" */ '../views/Users/Index.vue')
    }
  },
  {
    path: '/user/edit/:id',
    name: 'EditUser',
    component: function () {
      return import(/* webpackChunkName: "editUser" */ '../views/Users/Edit.vue')
    }
  },
  {
    path: '/user/details/:id',
    name: 'DetailsUser',
    component: function () {
      return import(/* webpackChunkName: "detailsUser" */ '../views/Users/Details.vue')
    }
  },
]

const router = new VueRouter({
  mode: "history",
  routes
})

export default router
