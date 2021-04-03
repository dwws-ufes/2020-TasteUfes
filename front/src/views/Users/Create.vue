<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form">
      <h1>Create User</h1>
      <div class="form-group">
        <v-text-field
          v-model="firstName"
          :rules="[rules.required]"
          label="FirstName"
          hide-details="auto"
          class="form-control"
          required
        />

        <v-text-field
          v-model="lastName"
          :rules="[rules.required]"
          label="LastName"
          hide-details="auto"
          class="form-control"
          required
        />

        <v-text-field
          v-model="username"
          :rules="[rules.required]"
          label="Username"
          hide-details="auto"
          class="form-control"
          required
        />

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
          label="Password"
          :type="'password'"
        />

        <v-text-field
          v-model="repeatPassword"
          :rules="[rules.required]"
          label="RepeatPassword"
          :type="'password'"
        />

        <v-card-actions>
          <v-row justify="center">
            <v-btn elevation="2" color="primary" dark>Create</v-btn>

            <v-btn elevation="2" :to="{ name: 'ListUser' }">Back</v-btn>
          </v-row>
        </v-card-actions>
        
      </div>
    </v-form>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      firstName: "",
      lastName: "",
      username: "",
      email: "",
      password: "",
      repeatPassword: "",
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

<style lang="scss" scoped>
.theme--light.v-btn--active::before {
  opacity: 0 !important;
}
</style>