<template>
  <div class="delete">
    <v-dialog v-model="dialogDelete" max-width="500px">
      <template v-slot:activator="{ on, attrs }">
        <v-icon medium v-bind="attrs" v-on="on" secondary> mdi-delete </v-icon>
      </template>
      <v-card>
        <v-card-title class="headline">Delete</v-card-title>
        <v-card-text
          >Are you sure you want to delete <b>{{ name }}</b
          >?</v-card-text
        >
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text @click="closeDelete">Cancel</v-btn>
          <v-btn color="red" text @click="deleteItem(id)">OK</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "DeleteButton",
  data: () => ({
    dialogDelete: false,
  }),
  props: {
    id: String,
    name: String,
  },
  methods: {
    deleteItem: function (id) {
      this.$emit("delete", id);
      this.closeDelete();
    },
    closeDelete: function () {
      this.dialogDelete = false;
    },
  },
};
</script>

<style lang="scss" scoped>
.v-icon {
  color: red;
}
</style>