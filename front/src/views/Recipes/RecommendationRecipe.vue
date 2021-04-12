<template>
  <v-container>
    <v-row>
      <v-col col="12" sm="4">
        <v-card>
          <v-container>
            <v-card-title>
              <h2>Recommendation Recipe</h2>
            </v-card-title>
            <v-form ref="form" @submit.prevent="onSubmit" v-model="valid">
              <div class="form-group">
                <v-select
                  v-model="selectFood"
                  :items="foods"
                  item-text="name"
                  item-value="id"
                  label="Select a food"
                  :rules="[(value) => !!value || 'Required.']"
                  multiple
                  return-value
                />
              </div>
              <v-row>
                <v-col class="d-flex justify-end">
                  <v-btn
                    class="submit justify-end"
                    type="submit"
                    elevation="2"
                    color="primary"
                    :disabled="!valid"
                  >
                    <span v-if="!submit"> Submit </span>
                    <v-progress-circular
                      v-else
                      indeterminate
                      color="white"
                    ></v-progress-circular>
                  </v-btn>
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </v-card>
      </v-col>
      <v-col col="12" sm="8">
        <v-container>
          <v-row>
            <v-col>
              <v-card-title>
                <h2>Recipe List</h2>
              </v-card-title>
            </v-col>
            <v-col cols="12">
              <v-row v-if="showRecipes">
                <v-col
                  v-for="recipe in recipeList"
                  :key="recipe.name"
                  justify="space-around"
                >
                  <router-link
                    class="text-decoration-none title-link"
                    :to="{ name: 'DetailsRecipe', params: { id: recipe.id } }"
                  >
                    <v-card>
                      <v-card-title>{{ recipe.name }}</v-card-title>
                      <v-divider class="mx-4"></v-divider>
                      <v-card-text>
                        <div class="my-2">
                          <b>Servings:</b> {{ recipe.servings }}
                        </div>
                        <div class="my-2" v-if="recipe.preparation != null">
                          <b>Total steps:</b>
                          {{ recipe.preparation.steps.length }}
                        </div>
                        <div class="my-2" v-if="recipe.preparation != null">
                          <b>Total time:</b>
                          {{ recipe.preparation.preparation_time }}
                        </div>
                        <div class="my-2">
                          <b>Author:</b> {{ recipe.user.first_name }}
                          {{ recipe.user.last_name }}
                        </div>
                      </v-card-text>
                    </v-card>
                  </router-link>
                </v-col>
              </v-row>
              <div v-else-if="emptyRecipes">
                <v-alert
                  prominent
                  dense
                  text
                  type="error"
                >
                <v-card-text> No recipes found, try with another foods. </v-card-text>
                </v-alert>
              </div>
              <div v-else>
                <v-alert
                  prominent
                  dense
                  text
                  type="info"
                >
                <v-card-text> Session empty, please select foods. </v-card-text>
                </v-alert>
              </div>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getFoods, recommendationByFoods } from "@/api";
export default {
  name: "RecommendationRecipe",
  data() {
    return {
      valid: false,
      submit: false,
      showRecipes: false,
      emptyRecipes: false,
      foods: [],
      recipeList: [],
      selectFood: [],
      rules: {
        required: (value) => !!value || "Required.",
      },
    };
  },

  methods: {
    onSubmit: function () {
      this.submit = true;
      let foodIds = this.selectFood.map((food) => {
        return { id: food };
      });
      recommendationByFoods(foodIds)
        .then((result) => {
          if (result.data.length > 0) {
            this.recipeList = result.data;
            this.showRecipes = true;
            this.emptyRecipes = false;
          } else {
            this.emptyRecipes = true;
            this.showRecipes = false;
          }
        })
        .catch((error) => {
          console.log(error.response);
          this.submit = false;
        })
        .finally(() => {
          this.selectFood = [];
          this.submit = false;
        });
    },

    getAllFoods: function () {
      getFoods()
        .then((foods) => {
          this.foods = foods.data;
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
  },

  created() {
    this.getAllFoods();
  },
};
</script>