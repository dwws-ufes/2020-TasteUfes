<template>
  <div class="nav">
    <v-app-bar dark color="green accent-5">
      <v-toolbar-title class="text-no-wrap">
        <router-link
          class="text-decoration-none title-link"
          :to="{ name: 'Home' }"
        >
          TasteUfes
        </router-link>
      </v-toolbar-title>

      <v-spacer />

      <MenuOption v-for="menu in menuList" :key="menu.name" :menu="menu" />

      <v-dialog v-model="dialog" persistent max-width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn color="white" light v-bind="attrs" v-on="on"> Login </v-btn>
        </template>
        <v-card>
          <v-container>
            <v-row>
              <v-spacer />
              <v-btn elevation="0" color="white" @click="dialog = false"
                ><v-icon>mdi-close</v-icon></v-btn
              >
            </v-row>
            <Login />
            <v-card-actions>
              <v-row justify="end">
                <v-btn elevation="2" color="green accent-5" dark>Login</v-btn>
              </v-row>
            </v-card-actions>
          </v-container>
        </v-card>
      </v-dialog>
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
      dialog: false,
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

<style lang="scss" >
  .theme--light.v-btn--active::before {
    opacity: 0 !important;
  }
  
  .v-list-item--active::before{
    opacity: 0 !important;
  }
</style>

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