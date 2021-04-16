<template>
  <v-app>
    <Toolbar />
    <v-main>
      <Snackbar />
      <v-container class="container-main">
        <v-row class="row-main">
          <router-view></router-view>
        </v-row>
      </v-container>
    </v-main>
    <Footbar />
  </v-app>
</template>

<script>
import Toolbar from "@/components/Toolbar.vue";
import Snackbar from "@/components/Snackbar.vue";
import Footbar from "@/components/Footbar.vue";
import { mapActions } from "vuex";
import { refreshAuthentication } from "@/api";

export default {
  name: "App",

  components: {
    Toolbar,
    Snackbar,
    Footbar,
  },

  data: () => ({
    access_token: localStorage.getItem("access_token"),
    token_type: localStorage.getItem("token_type"),
  }),

  created() {
    if (this.access_token != null) {
      this.loadSession({
        token_type: this.token_type,
        access_token: this.access_token,
      });
    }
  },

  methods: {
    ...mapActions(["loadSession", "doLogin", "doLogout"]),
  },

  updated: function () {
    this.$nextTick(function () {
      this.accessToken = localStorage.getItem("access_token");
      let expiresIn = localStorage.getItem("expires_in");
      let now = localStorage.getItem("now");
      if (now && new Date().getTime() - now > expiresIn * 1000) {
        let refreshToken = localStorage.getItem("refresh_token");
        refreshAuthentication({
          access_token: this.accessToken,
          refresh_token: refreshToken,
        })
          .then((result) => {
            this.doLogin(result.data);
          })
          .catch(() => {
            this.doLogout();
          });
      }
    });
  },
};
</script>

<style lang="scss">
@import "@/assets/scss/_style.scss";
</style>