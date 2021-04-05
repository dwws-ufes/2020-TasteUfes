<template>
  <router-link
    class="text-decoration-none title-link"
    :to="{ name: name, params: { id: id } }"
  >
    <v-icon small class="mr-2"> mdi-format-list-bulleted </v-icon>
  </router-link>
</template>

<script>
export default {
  name: "DetailsButton",
  props: {
    id: String,
    name: String,
  }
};
</script>

<style lang="scss" scoped>
  .v-icon {
    color: black;
  }
</style>