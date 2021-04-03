<template>
  <div class="list">
    <h1>Foods</h1>
    <v-btn elevation="2" :to="{ name: 'CreateFood' }">Create</v-btn>
    <v-data-table
      :headers="headers"
      :items="foods"
      :items-per-page="10"
      class="elevation-1"
    ></v-data-table>
  </div>
</template>

<script>
import food from "@/assets/json/food.json";

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
          text: "Name",
          value: 'name'
        },
        {
          text: "Nutrition Facts ID",
          value: 'nutrition_facts_id'
        },
      ],
      foods: [
        {
          id: food.id,
          name: food.name,
          nutrition_facts_id: food.nutrition_facts_id,
        },
      ],
    };
  },
};
</script>