<template>
  <div class="text-center">
    <v-overlay :value="getOverlay" z-index=20>
      <v-progress-circular indeterminate size="64"></v-progress-circular>
    </v-overlay>
  </div>
</template>
<script>
import { mapGetters } from "vuex";
export default {
  computed: {
    ...mapGetters(["getOverlay"]),
  },
};
</script>
