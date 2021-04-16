<template>
  <div>
    <v-dialog v-model="dialog" persistent max-width="400px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="white" light v-bind="attrs" v-on="on"> Sign In </v-btn>
      </template>
      <v-card>
        <v-container>
          <v-row>
            <v-spacer />
            <v-btn elevation="0" color="white" @click="dialog = false"
              ><v-icon>mdi-close</v-icon></v-btn
            >
          </v-row>
          <v-form ref="form" @submit.prevent="onSubmit" v-model="valid">
            <v-text-field
              v-model="user.username"
              :rules="[rules.required]"
              label="Username"
              hide-details="auto"
              class="form-control"
            />

            <v-text-field
              v-model="user.password"
              :rules="[rules.required, rules.minPass, rules.maxPass]"
              :type="'password'"
              label="Password"
              class="form-control"
              hide-details="auto"
            />
            <v-card-actions>
              <v-container>
                <v-row justify="end">
                  <v-btn
                    type="submit"
                    elevation="2"
                    color="primary"
                    class="submit"
                    :disabled="!valid"
                    v-if="!submit"
                  >
                    <span> Submit </span>
                  </v-btn>
                  <v-btn
                    v-else
                    color="primary"
                    class="submit"
                    loading
                    :disabled="!valid"
                  >
                  </v-btn>
                </v-row>
              </v-container>
            </v-card-actions>
          </v-form>
        </v-container>
      </v-card>
    </v-dialog>
    <!-- <v-btn v-else @click="logout"> Logout </v-btn> -->
  </div>
</template>

<script>
import { login, getUser, createAuthAPI } from "@/api";
import { mapActions } from "vuex";

export default {
  data() {
    return {
      submit: false,
      dialog: false,
      valid: false,
      user: {
        username: "",
        password: "",
      },
      rules: {
        required: (value) => !!value || "Required.",
        minPass: (value) => value.length >= 6 || "Must be minimum length of 6.",
        maxPass: (value) =>
          value.length <= 32 || "Must be maximun length of 32.",
      },
    };
  },

  methods: {
    ...mapActions(["doLogin"]),
    onSubmit() {
      this.submit = true;
      if (this.$refs.form.validate()) {
        login(this.user)
          .then((result) => {
            createAuthAPI(result.data.token_type, result.data.access_token);
            getUser(result.data.user_id).then((user) => {
              Promise.all([
                this.$store.dispatch("ActionSetUser", user.data),
                this.$store.dispatch("ActionSetIsAdmin", user.data),
              ]).finally(() => {
                this.doLogin(result.data);
                this.dialog = false;
                this.$store.dispatch("setSnackbar", {
                  text: `Welcome ${user.data.first_name}.`,
                  color: "success",
                });
              });
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
          });
      } else {
        this.submit = false;
      }
    },
  },
};
</script>