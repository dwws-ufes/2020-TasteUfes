<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="6" d-flex justify-center>
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)"><v-icon>mdi-chevron-left</v-icon> Back</span>
        </div>
        <v-card>
          <v-card-title>{{ recipe.name }}</v-card-title>
          <v-divider class="mx-4"></v-divider>
          <v-card-text>
            <div class="my-2"><b>Servings:</b> {{ recipe.servings }}</div>
            <div class="my-2"><b>Total steps:</b> {{ recipe.steps }}</div>
            <div class="my-2"><b>Total time:</b> {{ recipe.time }}</div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import recipes from "@/assets/json/recipe.json";

export default {
  name: "DetailsRecipe",

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