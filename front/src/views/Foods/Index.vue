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
      <template v-slot:item.name="{ item }">
        <v-row>
          <router-link
            class="text-decoration-none"
            :to="{ name: 'DetailsFood' , params: {'id': item.id}}"
            >{{ item.name }}</router-link
          >
        </v-row>
      </template>
      <template v-slot:item.serving_size="{ item }">
        <v-row v-if="item.nutrition_facts">
          {{ item.nutrition_facts.serving_size }}{{ getMeasureName(item.nutrition_facts.serving_size_unit) }}
        </v-row>
        <v-row v-else>
          -
        </v-row>
      </template>
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
import { getFoods, deleteFood } from "@/api";
import EditButton from "@/components/buttons/EditButton.vue";
import DetailsButton from "@/components/buttons/DetailsButton.vue";
import DeleteButton from "@/components/buttons/DeleteButton.vue";

export default {
  computed: {},
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
          text: "Serving Size",
          value: "serving_size",
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
          this.foodList.map((food, index) => {
            food.number = index + 1;
          })
          this.changeLoading();
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
    getMeasureName: function (id) {
      if (id > 0)
        return this.$store.state.ingredients_measures.find(
          (measure) => measure.id == id
        ).name;
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
          console.log(error.response);
        });
    },

    changeLoading() {
      this.load = !this.load;
    },
  },
};
</script>