<template>
  <div>
    <v-dialog v-if="!auth" v-model="dialog" persistent max-width="400px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="white" light v-bind="attrs" v-on="on"> Login </v-btn>
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
              :rules="[rules.required]"
              :type="'password'"
              label="Password"
              class="form-control"
            />
            <v-card-actions>
              <v-row justify="end">
                <v-btn
                  type="submit"
                  elevation="2"
                  color="primary"
                  :disabled="!valid"
                >
                  Login
                </v-btn>
              </v-row>
            </v-card-actions>
          </v-form>
        </v-container>
      </v-card>
    </v-dialog>
    <v-btn v-else @click="logout" > Logout </v-btn>
  </div>
</template>

<script>
import { AUTH } from "@/api/data"
import { login } from "@/api/data";

export default {
  data() {
    return {
      dialog: false,
      valid: false,
      auth: AUTH,
      user: {
        username: "",
        password: "",
      },
      rules: {
        required: (value) => !!value || "Required.",
      },
    };
  },

  methods: {
    onSubmit: function () {
      login(this.user)
        .then((result) => {
          console.log("Sucesso: ", result);
          let now = new Date().getTime();
          let access_token = result.data.access_token;
          sessionStorage.setItem("access_token", access_token);
          sessionStorage.setItem("expires_in", result.data.expires_in);
          sessionStorage.setItem("timeout", now);
          // axios.interceptors.response.use(function (config) {
          //   config.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;
          //   return config;
          // });
          // axios.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;

          // console.log(axios.defaults.headers.common['Authorization']);
          window.location.reload();
        })
        .catch((error) => {
          console.log("Erro: ", error);
        });
    },

    logout: function(){
      console.log("Logout")
      sessionStorage.clear();
      window.location.reload();
    }
  },
};
</script>