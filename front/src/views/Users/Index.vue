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
    this.getData();
  },

  methods: {
    getData: function () {
      const response = getUsers();
      response.then((result) => {
        this.userList = result.data;
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
          console.log(error);
        });
    },

    changeLoading() {
      this.load = !this.load;
    },
  },
};
</script>