<template>
  <div class="nav">
    <v-app-bar dark color="primary">
      <v-toolbar-title class="text-no-wrap">
        <router-link
          class="text-decoration-none title-link"
          :to="{ name: 'Home' }"
        >
          TasteUfes
        </router-link>
      </v-toolbar-title>
      <v-spacer />
      <div v-if="this.$store.state.auth" class="menu">
        <MenuOption v-for="menu in menuList" :key="menu.name" :menu="menu" />
      </div>
      <Login v-if="!auth" />
    </v-app-bar>
  </div>
</template>

<script>
import Login from "@/components/Login.vue";
import MenuOption from "@/components/MenuOption.vue";
import { mapGetters } from "vuex";

export default {
  name: "Toolbar",
  computed: {
    ...mapGetters(["auth", "isAdmin"]),
    showMenu() {
      return this.isAdmin;
    },
    menuList() {
      return [
        {
          name: "Recipe",
          icon: null,
          show: true,
          options: [
            {
              name: "Create Recipe",
              routeName: "CreateRecipe",
              action: false,
              show: true,
            },
            {
              name: "List Recipe",
              routeName: "ListRecipe",
              action: false,
              show: true,
            },
            {
              name: "Create Anonymous Recipe",
              routeName: "AnonymousRecipe",
              action: false,
              show: true,
            },
            {
              name: "Recommendation Recipe",
              routeName: "RecommendationRecipe",
              action: false,
              show: true,
            },
          ],
        },
        {
          name: "Food",
          icon: null,
          show: this.showMenu,
          options: [
            {
              name: "Create Food",
              routeName: "CreateFood",
              action: false,
              show: this.showMenu,
            },
            {
              name: "List Food",
              routeName: "ListFood",
              action: false,
              show: this.showMenu,
            },
          ],
        },
        {
          name: this.$store.state.user.first_name,
          icon: "mdi-account-circle",
          show: true,
          options: [
            {
              name: "Create User",
              routeName: "CreateUser",
              action: false,
              show: this.showMenu,
            },
            {
              name: "List User",
              routeName: "ListUser",
              action: false,
              show: this.showMenu,
            },
            {
              name: "Logout",
              routeName: "Logout",
              action: true,
              show: true,
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
  }
}
</style>