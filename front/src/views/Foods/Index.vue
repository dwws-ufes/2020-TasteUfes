<template>
  <div class="list">
    <h1>Foods</h1>
    <v-btn elevation="2" :to="{ name: 'CreateFood' }" color="primary" dark
      >Create Food</v-btn
    >
    <v-data-table
      :loading="load"
      loading-text="Loading... Please wait"
      :headers="headers"
      :items="foodList"
      :items-per-page="10"
      class="elevation-1"
    >
      <template v-slot:item.actions="{ item }">
        <v-row>
          <DetailsButton :id="item.id" name="DetailsFood" />
          <EditButton :id="item.id" name="EditFood" />
          <DeleteButton
            :id="item.id"
            :name="item.name"
            @delete="deleteFood(item.id)"
          />
        </v-row>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import { getFoods } from "@/api";
import { deleteFood } from "@/api";
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
          text: "Name",
          value: "name",
          class: "primary",
        },
        {
          text: "Nutrition Facts ID",
          value: "nutrition_facts_id",
          class: "primary",
        },
        {
          text: "Actions",
          value: "actions",
          class: "primary",
        },
      ],
      foodList: [],
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
      getFoods()
        .then((result) => {
          this.foodList = result.data;
          this.changeLoading();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    deleteFood(id) {
      this.changeLoading();
      deleteFood(id)
        .then((result) => {
          console.log(result);
          let foodId = this.foodList.findIndex((food) => food.id === id);
          this.foodList.splice(foodId, 1);
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