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
              label="FirstName*"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.last_name"
              :rules="[rules.required]"
              label="LastName*"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.username"
              :rules="[rules.required]"
              label="Username*"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.email"
              :rules="[rules.required, rules.email]"
              label="Email*"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.password"
              :rules="[rules.required, rules.minPass, rules.maxPass]"
              label="Password*"
              :type="'password'"
              class="form-control"
              hide-details="auto"
            />

            <v-text-field
              v-model="repeatPassword"
              :rules="[
                rules.required,
                passwordConfirmationRule,
                rules.minPass,
                rules.maxPass,
              ]"
              label="RepeatPassword*"
              :type="'password'"
              class="form-control"
              hide-details="auto"
            />

            <v-select
              v-if="isAdmin"
              v-model="roleId"
              :items="roles"
              :rules="[rules.required]"
              item-text="name"
              item-value="id"
              label="Select a Role*"
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
              v-if="!submit"
              :disabled="!valid"
            >
              <span> Create </span>
            </v-btn>
            <v-btn
              v-else
              color="primary"
              class="submit"
              loading
              :disabled="!valid"
            >
            </v-btn>

            <v-btn elevation="2" @click="$router.go(-1)">Back</v-btn>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>

<script>
import {
  createUser,
  registerUser,
  getRoles,
  login,
  createAuthAPI,
} from "@/api";
import { mapGetters, mapActions } from "vuex";
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
      roleId: "00000000-0000-0000-0000-000000000000",
      repeatPassword: "",
      rules: {
        required: (value) => !!value || "Required.",
        minPass: (value) => value.length >= 6 || "Must be minimum length of 6.",
        maxPass: (value) =>
          value.length <= 32 || "Must be maximun length of 32.",
        email: (value) => {
          const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          return pattern.test(value) || "Invalid e-mail.";
        },
      },
    };
  },

  methods: {
    ...mapActions(["doLogin"]),
    onSubmit: function () {
      this.submit = true;
      if (this.$refs.form.validate()) {
        if (this.roleId != "") this.user.roles = [{ id: this.roleId }];
        if (this.isAdmin) {
          createUser(this.user)
            .then((result) => {
              this.$store.dispatch("setSnackbar", {
                text: `User ${this.user.first_name} created.`,
                color: "success",
              });
              this.$router.push({ name: "ListUser" });
            })
            .catch((error) => {
              error.response.data.errors.map((error) => {
                this.$store.dispatch("setSnackbar", {
                  text: `${error.message}`,
                  color: "error",
                });
              });
              this.submit = false;
            });
        } else {
          registerUser(this.user)
            .then((registerResult) => {
              login(this.user)
                .then((result) => {
                  createAuthAPI(
                    result.data.token_type,
                    result.data.access_token
                  );
                  Promise.all([
                    this.$store.dispatch("ActionSetUser", registerResult.data),
                  ]).finally(() => {
                    this.doLogin(result.data);
                    this.$store.dispatch("setSnackbar", {
                      text: `Welcome ${this.user.first_name}.`,
                      color: "success",
                    });
                    this.$router.push({ name: "ListRecipe" });
                  });
                })
                .catch((error) => {
                  error.response.data.errors.map((error) => {
                    this.$store.dispatch("setSnackbar", {
                      text: `${error.message}`,
                      color: "error",
                    });
                  });
                  this.submit = false;
                  // this.valid = true;
                });
            })
            .catch((error) => {
              if (error.response) {
                error.response.data.errors.map((error) => {
                  this.$store.dispatch("setSnackbar", {
                    text: `${error.message}`,
                    color: "error",
                  });
                });
              } else {
                this.$store.dispatch("setSnackbar", {
                  text: `Network error, please contact server administrator.`,
                  color: "error",
                });
              }
              this.submit = false;
            });
        }
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
          error.response.data.errors.map((error) => {
            this.$store.dispatch("setSnackbar", {
              text: `${error.message}`,
              color: "error",
            });
          });
        });
    },
  },

  created() {
    if (this.isAdmin) this.getRoles();
  },

  computed: {
    passwordConfirmationRule() {
      return () =>
        this.user.password === this.repeatPassword || "Password must match";
    },
    ...mapGetters(["isAdmin"]),
  },
};
</script>

<style lang="scss" scoped>
.theme--light.v-btn--active::before {
  opacity: 0 !important;
}
</style>