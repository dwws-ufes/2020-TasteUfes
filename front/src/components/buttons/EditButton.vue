<template>
  <router-link
    class="text-decoration-none title-link"
    :to="{ name: name, params: { id: id } }"
  >
    <v-icon small class="mr-2"> mdi-pencil </v-icon>
  </router-link>
</template>

<script>
export default {
  name: "EditButton",
  props: {
    id: String,
    name: String,
  }
};
</script>

<style lang="scss" scoped>
  .v-icon {
    color: $primary;
  }
</style>