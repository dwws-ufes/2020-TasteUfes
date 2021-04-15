<template>
  <v-card elevation="2" class="card-form">
    <v-form
      ref="form"
      lazy-validation
      @submit.prevent="onSubmit"
      v-model="valid"
    >
      <h1>Create User</h1>
      <div class="form-group">
        <v-card class="mx-auto" elevation="0" outlined>
          <v-container>
            <v-text-field
              v-model="user.first_name"
              :rules="[rules.required]"
              label="FirstName"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.last_name"
              :rules="[rules.required]"
              label="LastName"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.username"
              :rules="[rules.required]"
              label="Username"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.email"
              :rules="[rules.required, rules.email]"
              label="Email"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.password"
              :rules="[rules.required]"
              label="Password"
              :type="'password'"
              class="form-control"
              hide-details="auto"
            />

            <v-text-field
              v-model="repeatPassword"
              :rules="[rules.required, passwordConfirmationRule]"
              label="RepeatPassword"
              :type="'password'"
              class="form-control"
              hide-details="auto"
            />

            <v-select
              v-model="roleId"
              :items="roles"
              :rules="[rules.required]"
              item-text="name"
              item-value="id"
              label="Select a Role"
              return-value
              hide-details="auto"
            />
          </v-container>
        </v-card>
        <v-card-actions>
          <v-row justify="center">
            <v-btn
              class="submit"
              type="submit"
              elevation="2"
              color="primary"
              :disabled="!valid"
            >
              <span v-if="!submit"> Create </span>
              <v-progress-circular
                v-else
                indeterminate
                color="white"
              ></v-progress-circular>
            </v-btn>

            <v-btn elevation="2" @click="$router.go(-1)">Back</v-btn>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>

<script>
import { createUser, getRoles } from "@/api";
export default {
  data() {
    return {
      valid: false,
      submit: false,
      user: {
        username: "",
        email: "",
        first_name: "",
        last_name: "",
        password: "",
        roles: [],
      },
      roles: [],
      roleId: "",
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

  methods: {
    onSubmit: function () {
      this.submit = true;
      if (this.$refs.form.validate()) {
        if (this.roleId != "") this.user.roles = [{ id: this.roleId }];
        createUser(this.user)
          .then((result) => {
            this.$router.push({ name: "ListUser" });
          })
          .catch((error) => {
            console.log(error.response);
          });
      } else {
        this.submit = false;
      }
    },

    getRoles() {
      getRoles()
        .then((result) => {
          this.roles = result.data;
          this.roles.push({
            id: "00000000-0000-0000-0000-000000000000",
            name: "User",
          });
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
  },

  created() {
    this.getRoles();
  },

  computed: {
    passwordConfirmationRule() {
      return () =>
        this.user.password === this.repeatPassword || "Password must match";
    },
  },
};
</script>

<style lang="scss" scoped>
.theme--light.v-btn--active::before {
  opacity: 0 !important;
}
</style>