<template>
  <v-app>
    <Toolbar />
    <v-main>
      <v-container>
        <router-view></router-view>
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
