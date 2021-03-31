<template>
  <div class="nav">
    <v-app-bar dark color="green accent-5">
      <v-toolbar-title>
        TasteUfes
      </v-toolbar-title>
      <v-spacer />
      <v-menu
        v-for="menu in menuList"
        :key="menu.slug"
        open-on-hover
        bottom
        offset-y
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            text
            tile
            x-large
            dark
            v-bind="attrs"
            v-on="on"
          >
            {{ menu.name }}
          </v-btn>
        </template>

        <v-list>
          <v-list-item link :to="{ name: menu.createName }">
            <v-list-item-title>Create</v-list-item-title>
          </v-list-item>
          <v-list-item link :to="{ name: menu.listName }">
            <v-list-item-title>List</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>

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
export default {
  name: "Toolbar",
  data() {
    return {
      menuList: [
        {
          name: "Recipe",
          createName: "CreateRecipe",
          listName: "Recipes",
        },
        {
          name: "Food",
          createName: "CreateFood",
          listName: "Foods",
        },
      ],
      dialog: false,
    };
  },
  components: {
    Login,
  },
};
</script>

<style lang="scss" scoped>
.nav {
  color: #ffffff;
}
</style>