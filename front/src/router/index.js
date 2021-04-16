import Vue from 'vue'
import VueRouter from 'vue-router'
import beforeEach from '@/router/beforeEach'
import { store } from '@/auth';

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
    },
    // beforeEnter: (to, from, next) => {
    //   if (store.getters["auth"]) next()
    //   else if (to.name !== 'Home') {
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
  {
    path: '/recipe',
    name: 'ListRecipe',
    component: function () {
      return import(/* webpackChunkName: "recipe" */ '../views/Recipes/Index.vue')
    },
    // beforeEnter: (to, from, next) => {
    //   if (store.getters["auth"]) next()
    //   else if (to.name !== 'Home') {
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
  {
    path: '/recipe/edit/:id',
    name: 'EditRecipe',
    component: function () {
      return import(/* webpackChunkName: "editRecipe" */ '../views/Recipes/Edit.vue')
    },
    // beforeEnter: (to, from, next) => {
    //   if (store.getters["auth"]) next()
    //   else if (to.name !== 'Home') {
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
  {
    path: '/recipe/details/:id',
    name: 'DetailsRecipe',
    component: function () {
      return import(/* webpackChunkName: "detailsRecipe" */ '../views/Recipes/Details.vue')
    }
  },
  {
    path: '/recipe/anonymous',
    name: 'AnonymousRecipe',
    component: function () {
      return import(/* webpackChunkName: "anonymousRecipe" */ '../views/Recipes/AnonymousRecipe.vue')
    },
  },

  {
    path: '/recipe/recommend',
    name: 'RecommendationRecipe',
    component: function () {
      return import(/* webpackChunkName: "recommendationRecipe" */ '../views/Recipes/RecommendationRecipe.vue')
    },
  },

  {
    path: '/food/create',
    name: 'CreateFood',
    component: function () {
      return import(/* webpackChunkName: "createFood" */ '../views/Foods/Create.vue')
    },
    // beforeEnter: (to, from, next) => {
    //   if (store.getters["isAdmin"]) next()
    //   else if (to.name !== 'Home') {
    //     console.log('dfds')
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
  {
    path: '/food',
    name: 'ListFood',
    component: function () {
      return import(/* webpackChunkName: "food" */ '../views/Foods/Index.vue')
    },
    // beforeEnter: (to, from, next) => {
    //   console.log(store.getters["isAdmin"])
    //   if (store.getters["auth"]) next()
    //   else if (to.name !== 'Home') {
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
  {
    path: '/food/edit/:id',
    name: 'EditFood',
    component: function () {
      return import(/* webpackChunkName: "editFood" */ '../views/Foods/Edit.vue')
    },
    // beforeEnter: (to, from, next) => {
    //   console.log(store.getters["isAdmin"])
    //   if (store.getters["isAdmin"]) next()
    //   else if (to.name !== 'Home') {
    //     console.log('dfds')
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
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
    },
    // beforeEnter: (to, from, next) => {
    //   console.log(store.getters["isAdmin"])
    //   if (store.getters["isAdmin"]) next()
    //   else if (to.name !== 'Home') {
    //     console.log('dfds')
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
  {
    path: '/user/edit/:id',
    name: 'EditUser',
    component: function () {
      return import(/* webpackChunkName: "editUser" */ '../views/Users/Edit.vue')
    },
    // beforeEnter: (to, from, next) => {
    //   if (store.getters["auth"]) next()
    //   else if (to.name !== 'Home') {
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
  {
    path: '/user/details/:id',
    name: 'DetailsUser',
    component: function () {
      return import(/* webpackChunkName: "detailsUser" */ '../views/Users/Details.vue')
    },
    // beforeEnter: (to, from, next) => {
    //   if (store.getters["auth"]) next()
    //   else if (to.name !== 'Home') {
    //     next({ name: 'Home' });
    //   } else {
    //     next();
    //   }
    // }
  },
]

const router = new VueRouter({
  mode: "history",
  routes
})

router.beforeEach(beforeEach);

export default router
