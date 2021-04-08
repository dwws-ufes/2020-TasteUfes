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

      <Login />
    </v-app-bar>
  </div>
</template>

<script>
import Login from "@/components/Login.vue";
import MenuOption from "@/components/MenuOption.vue";

export default {
  name: "Toolbar",
  data() {
    return {
      menuList: [
        {
          name: "Recipe",
          options: [
            {
              name: "Create Recipe",
              routeName: "CreateRecipe",
            },
            {
              name: "List Recipe",
              routeName: "ListRecipe",
            },
          ],
        },
        {
          name: "Food",
          options: [
            {
              name: "Create Food",
              routeName: "CreateFood",
            },
            {
              name: "List Food",
              routeName: "ListFood",
            },
          ],
        },
        {
          name: "User",
          options: [
            {
              name: "Create User",
              routeName: "CreateUser",
            },
            {
              name: "List User",
              routeName: "ListUser",
            },
            {
              name: "Logout",
              routeName: "Logout",
            },
          ],
        },
      ],
    };
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