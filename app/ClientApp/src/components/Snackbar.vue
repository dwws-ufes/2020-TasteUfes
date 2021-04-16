<template>
  <v-snackbar
    v-model="snackbar.show"
    top
    text
    :timeout="timeout"
    :color="snackbar.color"
  >
    {{ snackbar.text }}
    <template v-slot:action="{ attrs }">
      <v-btn
        :color="snackbar.color"
        text
        v-bind="attrs"
        @click="snackbar.show = false"
      >
        Close
      </v-btn>
    </template>
  </v-snackbar>
</template>

<script>
import { mapState } from "vuex";
export default {
  data() {
    return {
      timeout: 5000,
    };
  },

  computed: {
    ...mapState(["snackbar"]),
  },
};
</script>