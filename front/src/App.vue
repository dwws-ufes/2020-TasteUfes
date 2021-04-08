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
import Toolbar from "@/components/Toolbar.vue";
import { mapActions } from "vuex";

export default {
  name: "App",

  components: {
    Toolbar,
  },

  data: () => ({
    access_token: localStorage.getItem("access_token"),
  }),

  created() {
    if(this.access_token != null)
      this.loadSession(this.access_token);
  },

  methods: {
    ...mapActions(["loadSession"]),
  },

  updated: function () {
    this.$nextTick(function () {
      this.access_token = localStorage.getItem("access_token");
      let expiresIn = localStorage.getItem("expires_in");
      let now = localStorage.getItem("now");
      if (now && new Date().getTime() - now > expiresIn * 1000) {
        localStorage.clear();
      }
      if(this.access_token == null)
        this.loadSession(this.access_token);
    });
  },
};
</script>

<style lang="scss">
@import "@/assets/scss/_style.scss";
</style>