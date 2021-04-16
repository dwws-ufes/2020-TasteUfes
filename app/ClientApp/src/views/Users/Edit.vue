<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form" @submit.prevent="onSubmit" v-model="valid">
      <h1>Edit User</h1>
      <div class="form-group">
        <v-card class="mx-auto" elevation="0" outlined>
          <v-sheet v-if="load" :color="`grey lighten-4`" class="pa-3">
            <v-skeleton-loader
              class="mx-auto"
              type="article"
            ></v-skeleton-loader>
            <v-skeleton-loader
              class="mx-auto"
              type="article"
            ></v-skeleton-loader>
          </v-sheet>
          <v-container v-else>
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
import { getUser, getRoles, updateUser } from "@/api";
import { mapActions, mapGetters } from "vuex";

export default {
  name: "EditUser",

  data() {
    return {
      load: true,
      userId: this.$route.params.id,
      valid: false,
      submit: false,
      user: {
        username: "",
        email: "",
        first_name: "",
        last_name: "",
        password: "",
        roles: [
          {
            id: "",
          },
        ],
      },
      roles: [],
      roleId: "00000000-0000-0000-0000-000000000000",
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
    ...mapGetters(["getUserId", "isAdmin"]),
  },

  methods: {
    ...mapActions(["ActionSetUser"]),
    onSubmit: function () {
      this.submit = true;
      if (this.$refs.form.validate()) {
        if (this.user.roles.length > 0) this.user.roles[0].id = this.roleId;
        else
          this.user.roles = [
            {
              id: this.roleId,
            },
          ];
        updateUser(this.userId, this.user)
          .then((result) => {
            console.log(result.data);
            if (result.data.id === this.getUserId) {
              this.ActionSetUser(result.data);
            }
            if (this.isAdmin) {
              this.$router.push({ name: "ListUser" });
            } else {
              this.$router.push({ name: "Home" });
            }
          })
          .catch((error) => {
            console.log(error.response);
          });
      } else {
        this.submit = false;
      }
    },
    getUser() {
      getUser(this.userId)
        .then((result) => {
          this.user = result.data;
          delete this.user.password;
          if (this.user.roles.length > 0) {
            this.roleId = this.user.roles[0].id;
          }
        })
        .catch((error) => {
          console.log(error.response);
        })
        .finally(() => {
          this.load = false;
        });
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
    this.getUser();
    this.getRoles();
  },
};
</script>