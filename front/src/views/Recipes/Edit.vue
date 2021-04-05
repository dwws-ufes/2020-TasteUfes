<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form">
      <h1>Edit Recipe</h1>
      <div class="form-group">
        <v-text-field
          v-model="recipe.name"
          label="Name"
          hide-details="auto"
          class="form-control"
        />

        <v-text-field
          v-model="recipe.servings"
          label="Servings"
          hide-details="auto"
          class="form-control"
        />

        <v-card-actions>
          <v-row justify="center">
            <v-btn elevation="2" color="primary" dark>Save</v-btn>

            <v-btn elevation="2" @click="$router.go(-1)">Back</v-btn>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>

<script>
import recipes from "@/assets/json/recipe.json";

export default {
  name: "EditRecipe",

  data() {
    return {
      recipeId: this.$route.params.id,
    };
  },

  computed: {
    recipe() {
      let rcp = recipes.find((recipe) => recipe.id === this.recipeId);
      let preparation = rcp.preparation;
      let time =
        preparation.preparation_time.hours +
        "h " +
        preparation.preparation_time.minutes +
        "m";
      return {
        id: rcp.id,
        name: rcp.name,
        servings: rcp.servings,
        steps: preparation.steps.length,
        time: time,
      };
    },
  },
};
</script>