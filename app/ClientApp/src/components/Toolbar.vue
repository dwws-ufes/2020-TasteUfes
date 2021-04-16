<template>
  <div class="nav">
    <v-app-bar dark color="primary">
      <v-toolbar-title class="text-no-wrap">
        <router-link
          class="text-decoration-none title-link d-flex align-center"
          :to="{ name: 'Home' }"
        >
          <v-icon>mdi-home</v-icon>
          <span class="title-name">TasteUfes</span>
        </router-link>
      </v-toolbar-title>
      <v-spacer />
      <div class="menu">
        <MenuOption v-for="menu in menuList" :key="menu.name" :menu="menu" />
      </div>
      <div v-if="!auth">
        <v-btn outlined :to="{ name: 'CreateUser' }" class="mx-2"
          >Sign Up</v-btn
        >
      </div>
      <Login v-if="!auth" />
    </v-app-bar>
  </div>
</template>

<script>
import Login from '@/components/Login.vue';
import MenuOption from '@/components/MenuOption.vue';
import { mapGetters } from 'vuex';

export default {
  name: 'Toolbar',
  computed: {
    ...mapGetters(['auth', 'isAdmin', 'getUserId']),
    showMenu() {
      return this.isAdmin;
    },
    menuList() {
      return [
        {
          name: 'Recipe',
          icon: null,
          show: true,
          options: [
            {
              name: 'Create Recipe',
              routeName: 'CreateRecipe',
              param: null,
              action: false,
              show: this.auth,
            },
            {
              name: 'List Recipe',
              routeName: 'ListRecipe',
              param: null,
              action: false,
              show: this.auth,
            },
            {
              name: 'Create Anonymous Recipe',
              routeName: 'AnonymousRecipe',
              param: null,
              action: false,
              show: true,
            },
            {
              name: 'Recommendation Recipe',
              routeName: 'RecommendationRecipe',
              param: null,
              action: false,
              show: true,
            },
          ],
        },
        {
          name: 'Food',
          icon: null,
          show: this.isAdmin,
          options: [
            {
              name: 'Create Food',
              routeName: 'CreateFood',
              param: null,
              action: false,
              show: this.auth,
            },
            {
              name: 'List Food',
              routeName: 'ListFood',
              param: null,
              action: false,
              show: this.auth,
            },
          ],
        },
        {
          name: this.$store.state.user.first_name,
          icon: 'mdi-account-circle',
          show: this.auth,
          options: [
            {
              name: 'My Account',
              routeName: 'DetailsUser',
              param: this.getUserId,
              action: false,
              show: this.auth,
            },
            {
              name: 'Create User',
              routeName: 'CreateUser',
              param: null,
              action: false,
              show: this.isAdmin,
            },
            {
              name: 'List User',
              routeName: 'ListUser',
              param: null,
              action: false,
              show: this.isAdmin,
            },
            {
              name: 'Logout',
              routeName: 'Logout',
              param: null,
              action: true,
              show: this.auth,
            },
          ],
        },
      ];
    },
  },

  components: {
    Login,
    MenuOption,
  },
};
</script>

<style lang="scss" scoped>
.nav {
  color: #ffffff;
}

.title {
  &-link {
    color: #ffffff;
    font-size: 26px;
  }
}
.v-icon {
  font-size: 28px;
}
</style>