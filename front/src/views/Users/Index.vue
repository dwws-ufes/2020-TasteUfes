<template>
  <div class="list">
    <h1>Users</h1>
    <v-btn elevation="2" :to="{ name: 'CreateUser' }" color="primary" dark
      >Create User</v-btn
    >
    <v-data-table
      :headers="headers"
      :items="userList"
      :items-per-page="10"
      class="elevation-1"
    >
      <template v-slot:item.actions="{ item }">
        <v-row>
          <DetailsButton :id="item.id" name="DetailsUser" />
          <EditButton :id="item.id" name="EditUser" />
          <DeleteButton :id="item.id" :name="item.first_name" />
        </v-row>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import users from "@/assets/json/user.json";
import EditButton from "@/components/buttons/EditButton.vue";
import DetailsButton from "@/components/buttons/DetailsButton.vue";
import DeleteButton from "@/components/buttons/DeleteButton.vue";

export default {
  data() {
    return {
      headers: [
        {
          text: "ID",
          align: "start",
          sortable: false,
          value: "id",
          class: "primary",
        },
        {
          text: "First Name",
          value: "first_name",
          class: "primary",
        },
        {
          text: "Last Name",
          value: "last_name",
          class: "primary",
        },
        {
          text: "Username",
          value: "username",
          class: "primary",
        },
        {
          text: "Email",
          value: "email",
          class: "primary",
        },
        {
          text: "Actions",
          value: "actions",
          class: "primary",
        },
      ],
      userList: [],
    };
  },

  components: {
    EditButton,
    DetailsButton,
    DeleteButton,
  },

  created: function () {
    this.getUsers();
  },

  methods: {
    getUsers: function () {
      users.forEach((user) => {
        this.userList.push({
          id: user.id,
          first_name: user.first_name,
          last_name: user.last_name,
          username: user.username,
          email: user.email,
        });
      });
    },
  },
};
</script>