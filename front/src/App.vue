<template>
  <v-app>
    <Toolbar />
    <v-main>
      <v-container class="container-main">
        <v-row class="row-main">
          <router-view></router-view>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import HelloWorld from "./components/HelloWorld";
import Toolbar from "@/components/Toolbar.vue";

export default {
  name: "App",

  components: {
    HelloWorld,
    Toolbar,
  },

  data: () => ({
    //
  }),
};
</script>

<style lang="scss">
  @import "@/assets/scss/_style.scss";
</style>