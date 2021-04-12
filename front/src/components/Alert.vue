<template>
  <v-alert dense text :type="alertType">
    {{ message }}
  </v-alert>
</template>

<script>
export default {
  name: "Alert",
  props: {
    alertType: String,
    message: String,
  },
};
</script>