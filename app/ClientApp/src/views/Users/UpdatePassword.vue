<template>
  <v-card elevation="2" class="card-form">
    <v-form
      ref="form"
      lazy-validation
      @submit.prevent="onSubmit"
      v-model="valid"
    >
      <h1>Update Password</h1>
      <div class="form-group">
        <v-card class="mx-auto" elevation="0" outlined>
          <v-container>
            <v-text-field
              v-model="passwordUpdate.old_password"
              :rules="[rules.required, rules.minPass, rules.maxPass]"
              label="Old Password*"
              :type="'password'"
              class="form-control"
              hide-details="auto"
            />
            <v-text-field
              v-model="passwordUpdate.new_password"
              :rules="[rules.required, rules.minPass, rules.maxPass]"
              label="New Password*"
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
              label="Repeat Password*"
              :type="'password'"
              class="form-control"
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
              <span> Save </span>
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
import { updatePassword } from "@/api";
import { mapGetters } from "vuex";
export default {
  data() {
    return {
      valid: false,
      submit: false,
      passwordUpdate: {
        old_password: "",
        new_password: "",
      },
      repeatPassword: "",
      rules: {
        required: (value) => !!value || "Required.",
        minPass: (value) => value.length >= 6 || "Must be minimum length of 6.",
        maxPass: (value) => value.length <= 32 || "Must be maximun length of 32.",
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
        updatePassword(this.getUserId, this.passwordUpdate)
          .then((result) => {
            this.$store.dispatch("setSnackbar", {
              text: `Password updated.`,
              color: "success",
            });
            this.$router.push({ name: "ListUser" });
          })
          .catch((error) => {
            this.submit = false;
            console.log(error.response)
            error.response.data.errors.map((error) => {
              this.$store.dispatch("setSnackbar", {
                text: `${error.message}`,
                color: "error",
              });
            });
          });
      } else {
        this.submit = false;
      }
    },
  },

  computed: {
    ...mapGetters(['getUserId']),
    passwordConfirmationRule() {
      return () =>
        this.passwordUpdate.new_password === this.repeatPassword || "Password must match";
    },
  },
};
</script>

<style lang="scss" scoped>
.theme--light.v-btn--active::before {
  opacity: 0 !important;
}
</style>