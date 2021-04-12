<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form"  @submit.prevent="onSubmit" v-model="valid">
      <h1>Edit User</h1>
      <div class="form-group">
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

        <v-card-actions>
          <v-row justify="center">
            <v-btn
              class="submit"
              type="submit"
              elevation="2"
              color="primary"
              :disabled="!valid"
            >
              <span v-if="!submit"> Save </span>
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
import { getUser, updateUser } from "@/api";

export default {
  name: "EditUser",

  data() {
    return {
      userId: this.$route.params.id,
      valid: false,
      submit: false,
      user: {
        username: "",
        email: "",
        first_name: "",
        last_name: "",
        password: "",
      },
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

  computed: {
    passwordConfirmationRule() {
      return () =>
        this.user.password === this.repeatPassword || "Password must match";
    },
  },

  methods: {
    onSubmit: function () {
      this.submit = true;
      updateUser(this.userId, this.user)
        .then((result) => {
          console.log(result.data);
          this.$router.push({ name: "ListUser" });
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
    getUser() {
      getUser(this.userId)
        .then((result) => {
          this.user = result.data;
          delete this.user.password;
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
  },
    created() {
      this.getUser();
    },
};
</script>