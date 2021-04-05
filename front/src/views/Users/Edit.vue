<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form">
      <h1>Edit Recipe</h1>
      <div class="form-group">
        <v-text-field
          v-model="user.first_name"
          label="FirstName"
          hide-details="auto"
          class="form-control"
        />

        <v-text-field
          v-model="user.last_name"
          label="LastName"
          hide-details="auto"
          class="form-control"
        />

        <v-text-field
          v-model="user.username"
          label="Username"
          hide-details="auto"
          class="form-control"
        />

        <v-text-field
          v-model="user.email"
          label="Email"
          hide-details="auto"
          class="form-control"
        />

        <v-card-actions>
          <v-row justify="center">
            <v-btn elevation="2" color="primary" dark>Save</v-btn>

            <v-btn elevation="2" @click="$router.go(-1)">Back</v-btn>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>

<script>
import users from "@/assets/json/user.json";

export default {
  name: "EditUser",

  data() {
    return {
      userId: this.$route.params.id,
    };
  },

  computed: {
    user() {
      let usr = users.find((user) => user.id === this.userId);
      return {
        id: usr.id,
        first_name: usr.first_name,
        last_name: usr.last_name,
        username: usr.username,
        email: usr.email,
      };
    },
  },
};
</script>