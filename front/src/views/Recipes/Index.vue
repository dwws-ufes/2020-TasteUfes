<template>
  <div class="list">
    <h1>Recipes</h1>
    <v-btn elevation="2" :to="{ name: 'CreateRecipe' }" color="primary" dark>Create Recipe</v-btn>
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
          class: "primary",
        },
        {
          text: "Name",
          value: 'name',
          class: "primary",
        },
        {
          text: "Servings",
          value: 'servings',
          class: "primary",
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