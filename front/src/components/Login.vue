<template>
  <v-form ref="form">
    <v-text-field
      v-model="email"
      :rules="[rules.required, rules.email]"
      label="Email"
      hide-details="auto"
      class="form-control"
    />

    <v-text-field
      v-model="password"
      :rules="[rules.required]"
      :type="'password'"
      label="Password"
    />
  </v-form>
</template>

<script>
export default {
  data() {
    return {
      email: "",
      password: "",
      rules: {
        required: (value) => !!value || "Required.",
        email: (value) => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || "Invalid e-mail.";
        },
      },
    };
  },
};
</script>