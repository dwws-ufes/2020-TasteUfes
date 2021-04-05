<template>
  <div class="list">
    <h1>Foods</h1>
    <v-btn elevation="2" :to="{ name: 'CreateFood' }" color="primary" dark
      >Create Food</v-btn
    >
    <v-data-table
      :headers="headers"
      :items="foodList"
      :items-per-page="10"
      class="elevation-1"
    >
      <template v-slot:item.actions="{ item }">
        <v-row>
          <DetailsButton :id="item.id" name="DetailsFood" />
          <EditButton :id="item.id" name="EditFood" />
          <DeleteButton :id="item.id" :name="item.name" />
        </v-row>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import foods from "@/assets/json/food.json";
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

  created: function () {
    this.getFoods();
  },

  components: {
    EditButton,
    DetailsButton,
    DeleteButton,
  },

  methods: {
    getFoods: function () {
      foods.forEach((food) => {
        this.foodList.push({
          id: food.id,
          name: food.name,
          nutrition_facts_id: food.nutrition_facts_id,
        });
      });
    },
  },
};
</script>