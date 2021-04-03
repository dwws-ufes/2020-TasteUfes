<template>
  <div class="list">
    <h1>Users</h1>
    <v-btn elevation="2" :to="{ name: 'CreateUser' }">Create</v-btn>
    <v-data-table 
      :headers="headers"
      :items="users"
      :items-per-page="10"
      class="elevation-1"
    ></v-data-table>
  </div>
</template>

<script>
import user from "@/assets/json/user.json";

export default {
  data() {
    return {
      headers: [
        {
          text: "ID",
          align: "start",
          sortable: false,
          value: "id",
        },
        {
          text: "First Name",
          value: 'first_name'
        },
        {
          text: "Last Name",
          value: 'last_name'
        },
        {
          text: "Username",
          value: 'username'
        },
        {
          text: "Email",
          value: 'email'
        },
      ],
      users: [
        {
          id: user[0].id,
          first_name: user[0].first_name,
          last_name: user[0].last_name,
          username: user[0].username,
          email: user[0].email,
        },
        {
          id: user[1].id,
          first_name: user[1].first_name,
          last_name: user[1].last_name,
          username: user[1].username,
          email: user[1].email,
        },
      ],
    };
  },
};
</script>