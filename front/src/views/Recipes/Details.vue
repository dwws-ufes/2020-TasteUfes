<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="6" d-flex justify-center>
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)"
            ><v-icon>mdi-chevron-left</v-icon> Back</span
          >
        </div>
        <v-card>
          <v-card-title>{{ recipe.name }}</v-card-title>
          <v-divider class="mx-4"></v-divider>
          <v-card-text>
            <div class="my-2"><b>Servings:</b> {{ recipe.servings }}</div>
            <div class="my-2" v-if="this.recipe.preparation != null">
              <b>Total steps:</b> {{ this.recipe.preparation.steps.length }}
            </div>
            <div class="my-2" v-if="this.recipe.preparation != null">
              <b>Total time:</b> {{ this.recipe.preparation.preparation_time }}
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getRecipe } from "@/api";

export default {
  name: "DetailsRecipe",

  data() {
    return {
      recipeId: this.$route.params.id,
      recipe: [],
    };
  },

  created: function () {
    this.getData();
  },

  methods: {
    getData: function () {
      getRecipe(this.recipeId)
        .then((result) => {
          this.recipe = result.data;
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
  },
};
</script>