<template>
  <div class="list">
    <h1>Recipes</h1>
    <v-btn elevation="2" :to="{ name: 'CreateRecipe' }">Create</v-btn>
    <v-data-table
      :headers="headers"
      :items="recipes"
      :items-per-page="10"
      class="elevation-1"
    />
  </div>
</template>

<script>
import recipe from "@/assets/json/recipe.json";

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
          text: "Servings",
          value: 'servings'
        },
      ],
      recipes: [
        {
          id: recipe.id,
          name: recipe.name,
          servings: recipe.servings,
        },
      ],
    };
  },
};
</script>