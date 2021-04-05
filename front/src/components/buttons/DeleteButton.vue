<template>
  <div class="delete">
    <v-icon small @click="deleteItem(id, name)" secondary> mdi-delete </v-icon>
    <v-dialog v-model="dialogDelete" max-width="500px">
      <v-card>
        <v-card-title class="headline"
          >Delete</v-card-title
        >
        <v-card-text>Are you sure you want to delete <b>{{ name }}</b>?</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text @click="closeDelete">Cancel</v-btn>
          <v-btn color="red" text @click="deleteItem(id)"
            >OK</v-btn
          >
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
      // Chamar API delete.
      this.dialogDelete = true;
    },
    closeDelete: function() {
      this.dialogDelete = false;
    }
  },
};
</script>

<style lang="scss" scoped>
  .v-icon {
    color: red;
  }
</style>