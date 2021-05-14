<template>
  <v-container>
    <v-row class="justify-center">
      <v-col cols="12" sm="8">
        <v-card class="card-list">
          <v-container>
            <v-card-title>
              <h2>
                {{ $vuetify.lang.t("$vuetify.recommendation") }}
                {{ $vuetify.lang.t("$vuetify.recipe") }}
              </h2>
            </v-card-title>
            <v-form ref="form" @submit.prevent="onSubmit" v-model="valid">
              <div class="form-group">
                <v-autocomplete
                  v-model="selectFood"
                  :items="foods"
                  item-text="name"
                  item-value="id"
                  :label="
                    $vuetify.lang.t('$vuetify.select_o') +
                    ' ' +
                    $vuetify.lang.t('$vuetify.ingredient')
                  "
                  :rules="[(value) => !!value || $vuetify.lang.t('$vuetify.required') + '.']"
                  multiple
                  clearable
                  deletable-chips
                  chips
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
                    v-if="!submit"
                    :disabled="!valid"
                  >
                    <span> {{ $vuetify.lang.t("$vuetify.submit") }} </span>
                  </v-btn>
                  <v-btn
                    v-else
                    color="primary"
                    class="submit"
                    loading
                    :disabled="!valid"
                  >
                  </v-btn>
                </v-col>
              </v-row>
            </v-form>
          </v-container>
        </v-card>
      </v-col>
      <v-col cols="12" sm="8">
        <v-container>
          <v-row>
            <v-col>
              <v-card-title>
                <h2>{{ $vuetify.lang.t("$vuetify.list_recipe") }}</h2>
              </v-card-title>
            </v-col>
            <v-col cols="12">
              <v-row v-if="showRecipes">
                <v-col
                  v-for="recipe in recipeList"
                  :key="recipe.name"
                  cols="12"
                  xs="12"
                  sm="6"
                  lg="4"
                >
                  <router-link
                    class="text-decoration-none title-link"
                    :to="{ name: 'DetailsRecipe', params: { id: recipe.id } }"
                  >
                    <v-card>
                      <v-card-title class="primary">{{
                        recipe.name
                      }}</v-card-title>
                      <v-container>
                        <v-row>
                          <v-col>
                            <div class="my-2">
                              <b>{{ $vuetify.lang.t("$vuetify.servings") }}:</b>
                              {{ recipe.servings }}
                            </div>
                            <div class="my-2" v-if="recipe.preparation != null">
                              <b
                                >{{
                                  $vuetify.lang.t("$vuetify.preparation_time")
                                }}:</b
                              >
                              {{ recipe.preparation.preparation_time }}
                            </div>
                            <div class="my-2">
                              <b>{{ $vuetify.lang.t("$vuetify.author") }}:</b>
                              {{ recipe.user.first_name }}
                              {{ recipe.user.last_name }}
                            </div>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                  </router-link>
                </v-col>
              </v-row>
              <div v-else-if="emptyRecipes">
                <v-alert prominent dense text type="error">
                  <v-card-text>
                    {{ $vuetify.lang.t("$vuetify.no_recipes") }}
                  </v-card-text>
                </v-alert>
              </div>
              <div v-else>
                <v-alert prominent dense text type="info">
                  <v-card-text>
                    {{ $vuetify.lang.t("$vuetify.session_empty") }}
                  </v-card-text>
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
        required: (value) => !!value || this.$vuetify.lang.t('$vuetify.required') + '.',
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
          error.response.data.errors.map((error) => {
            this.$store.dispatch("setSnackbar", {
              text: `${error.message}`,
              color: "error",
            });
          });
          this.submit = false;
        })
        .finally(() => {
          this.submit = false;
        });
    },

    getAllFoods: function () {
      getFoods()
        .then((foods) => {
          this.foods = foods.data.sort((food1, food2) => {
            if (food1.name < food2.name) return -1;
            else return 1;
          });
        })
        .catch((error) => {
          error.response.data.errors.map((error) => {
            this.$store.dispatch("setSnackbar", {
              text: `${error.message}`,
              color: "error",
            });
          });
        });
    },
  },

  created() {
    this.getAllFoods();
  },
};
</script>

<style lang="scss" scoped>
.card-list {
  padding: 1em;
}
</style>