<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="6" d-flex justify-center>
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)"><v-icon>mdi-chevron-left</v-icon> Back</span>
        </div>
        <v-card>
          <v-card-title
            >{{ user.first_name }} {{ user.last_name }}</v-card-title
          >
          <v-divider class="mx-4"></v-divider>
          <v-card-text>
            <div class="my-2"><b>Username:</b> {{ user.username }}</div>
            <div class="my-2"><b>Email:</b> {{ user.email }}</div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import users from "@/assets/json/user.json";

export default {
  name: "DetailsUser",

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