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
      <template v-slot:item.name="{ item }">
        <v-row>
          <router-link
            class="text-decoration-none"
            :to="{ name: 'DetailsUser', params: { id: item.id } }"
            >{{ item.first_name }} {{item.last_name}}</router-link
          >
        </v-row>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-row>
          <DetailsButton :id="item.id" name="DetailsUser" />
          <EditButton :id="item.id" name="EditUser" />
          <DeleteButton
            :id="item.id"
            :name="item.first_name"
            @delete="deleteUser(item.id)"
          />
        </v-row>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import { getUsers, deleteUser } from "@/api";
import EditButton from "@/components/buttons/EditButton.vue";
import DetailsButton from "@/components/buttons/DetailsButton.vue";
import DeleteButton from "@/components/buttons/DeleteButton.vue";

export default {
  data() {
    return {
      load: true,
      headers: [
        {
          text: "Nº",
          align: "start",
          value: "number",
          class: "primary",
        },
        {
          text: "Name",
          value: "name",
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
          text: "Role",
          value: "roles",
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
    this.getData();
  },

  methods: {
    getData: function () {
      const response = getUsers();
      response.then((result) => {
        this.userList = result.data;
        this.userList.map((user) => {
          user.roles.length > 0
            ? (user.roles = user.roles[0].name)
            : (user.roles = "User");
        });
        this.userList.map((user, index) => {
            user.number = index + 1;
          })
        this.changeLoading();
      });
    },

    deleteUser(id) {
      this.changeLoading();
      deleteUser(id)
        .then((result) => {
          console.log(result);
          let userId = this.userList.findIndex((user) => user.id === id);
          this.userList.splice(userId, 1);
          this.changeLoading();
        })
        .catch((error) => {
          console.log(error.response);
        });
    },

    changeLoading() {
      this.load = !this.load;
    },
  },
};
</script>