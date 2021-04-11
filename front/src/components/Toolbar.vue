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
    ...mapGetters(["auth"]),
    menuList() {
      return [
        {
          name: "Recipe",
          icon: null,
          options: [
            {
              name: "Create Recipe",
              routeName: "CreateRecipe",
              action: false,
            },
            {
              name: "List Recipe",
              routeName: "ListRecipe",
              action: false,
            },
          ],
        },
        {
          name: "Food",
          icon: null,
          options: [
            {
              name: "Create Food",
              routeName: "CreateFood",
              action: false,
            },
            {
              name: "List Food",
              routeName: "ListFood",
              action: false,
            },
          ],
        },
        {
          name: this.$store.state.user.first_name,
          icon: "mdi-account-circle",
          options: [
            {
              name: "Create User",
              routeName: "CreateUser",
              action: false,
            },
            {
              name: "List User",
              routeName: "ListUser",
              action: false,
            },
            {
              name: "Logout",
              routeName: "Logout",
              action: true,
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