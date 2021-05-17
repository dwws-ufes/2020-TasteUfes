<template>
  <v-container>
    <v-row>
      <v-col>
        <v-tooltip left>
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="primary"
              fab
              v-bind="attrs"
              v-on="on"
              class="btn-manual"
              @click="openManual"
            >
              ?
            </v-btn>
          </template>
          <span>{{ $vuetify.lang.t("$vuetify.manual") }}</span>
        </v-tooltip>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
    methods: {
        openManual() {
            window.open("./docs/TutorialTasteUFES.pdf", "_blank");
        }
    },
}
</script>

<style lang="scss" scoped>
.btn-manual {
  position: fixed;
  right: 20px;
  bottom: 60px;
}
</style>