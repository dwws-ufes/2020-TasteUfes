<template>
  <v-app v-if="getLoadingMain">
    <v-container class="loading-main">
      <v-row class="justify-center align-center h-100 flex-column">
        <v-progress-circular
          :size="70"
          :width="7"
          color="primary"
          indeterminate
        ></v-progress-circular>
        <h3 v-if="getLoadingMainError">
          {{ loadingName }}
        </h3>
        <h3 v-else>{{ $vuetify.lang.t("$vuetify.loading") }}</h3>
      </v-row>
    </v-container>
  </v-app>
  <v-app v-else>
    <Overlay />
    <Toolbar />
    <v-main>
      <Snackbar />
      <v-container class="container-main">
        <v-row class="row-main">
          <router-view></router-view>
        </v-row>
        <HelpButton />
      </v-container>
    </v-main>
    <Footbar />
  </v-app>
</template>

<script>
import Toolbar from "@/components/Toolbar.vue";
import Snackbar from "@/components/Snackbar.vue";
import Footbar from "@/components/Footbar.vue";
import Overlay from "@/components/Overlay.vue";
import HelpButton from "@/components/HelpButton.vue";
import { mapActions, mapGetters } from "vuex";
import { refreshAuthentication } from "@/api";

export default {
  name: "App",

  components: {
    Toolbar,
    Snackbar,
    Footbar,
    Overlay,
    HelpButton,
  },

  data: () => ({
    access_token: localStorage.getItem("access_token"),
    token_type: localStorage.getItem("token_type"),
  }),

  computed: {
    ...mapGetters([
      "getLoadingMain",
      "getLoadingMainError",
      "getQuotes",
      "getRandomizeQuote",
    ]),
    loadingName() {
      return this.getQuotes[this.getRandomizeQuote];
    },
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