<template>
  <v-card elevation="2" class="card-form">
    <v-form
      ref="form"
      lazy-validation
      @submit.prevent="onSubmit"
      v-model="valid"
    >
      <h1>Edit Recipe</h1>
      <div class="form-group">
        <v-card class="mx-auto" elevation="0" outlined>
          <v-sheet v-if="load" :color="`grey lighten-4`" class="pa-3">
            <v-skeleton-loader
              class="mx-auto"
              type="article"
            ></v-skeleton-loader>
          </v-sheet>
          <v-container v-else>
            <v-text-field
              v-model="recipe.name"
              :rules="[this.rules.required]"
              label="Name*"
              hide-details="auto"
              class="form-control"
            />
            <v-text-field
              v-model.number="recipe.servings"
              :rules="[this.rules.required]"
              type="number"
              label="Servings*"
              hide-details="auto"
              class="form-control"
            />
            <v-text-field
              v-model.number="prepTime"
              :rules="[(value) => !!value || 'Required.']"
              label="Preparation time (in minutes)*"
              type="number"
              hide-details="auto"
              class="form-control"
            />
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined v-if="!load">
          <v-container>
            <v-col class="d-flex justify-content-between">
              <h2>Food</h2>
              <v-btn
                class="mx-1 my-0"
                fab
                x-small
                color="primary"
                @click="addFoodField"
              >
                <v-icon dark> mdi-plus </v-icon>
              </v-btn>
            </v-col>
            <div
              v-for="(ingredient, i) in this.recipe.ingredients"
              class="foods"
            >
              <v-container>
                <v-card outlined shaped>
                  <v-row>
                    <v-col
                      cols="12"
                      sm="2"
                      class="d-flex align-center justify-flex-end"
                    >
                      <v-btn
                        fab
                        x-small
                        dark
                        color="red"
                        class="mx-0"
                        @click="removeFoodField(i)"
                      >
                        <v-icon dark>mdi-minus</v-icon>
                      </v-btn>
                    </v-col>
                    <v-col cols="12" sm="10" class="pl-0">
                      <v-container>
                        <v-autocomplete
                          v-model="ingredient.food_id"
                          :items="foods"
                          item-text="name"
                          item-value="id"
                          label="Select a food*"
                          :rules="[(value) => !!value || 'Required.']"
                          return-value
                          @change="showFields(ingredient)"
                        />
                        <v-row>
                          <v-col>
                            <v-text-field
                              v-model.number="ingredient.quantity"
                              type="number"
                              label="Quantity Food*"
                              :rules="[(value) => !!value || 'Required.']"
                              hide-details="auto"
                              class="form-control"
                              v-if="ingredient.nutrition_facts_fields"
                            />
                          </v-col>
                          <v-col>
                            <v-select
                              v-model="ingredient.quantity_unit"
                              :items="ingredient.measures"
                              item-text="name"
                              item-value="id"
                              label="Select a Measure*"
                              :rules="[(value) => !!value || 'Required.']"
                              return-value
                              v-if="ingredient.nutrition_facts_fields"
                            />
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-col>
                  </v-row>
                </v-card>
              </v-container>
            </div>
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined v-if="!load">
          <v-container>
            <v-col class="d-flex justify-content-between">
              <h2>Step</h2>
              <div>
                <v-btn
                  class="mx-1 my-0"
                  fab
                  x-small
                  color="primary"
                  @click="addStepField"
                >
                  <v-icon dark> mdi-plus </v-icon>
                </v-btn>
              </div>
            </v-col>
            <div
              v-for="(preparation, i) in this.recipe.preparation.steps"
              :key="i"
              class="foods"
            >
              <v-container>
                <v-card outlined shaped>
                  <v-row>
                    <v-col
                      cols="12"
                      sm="2"
                      class="d-flex align-center justify-flex-end"
                    >
                      <v-btn
                        fab
                        x-small
                        dark
                        color="red"
                        class="mx-0"
                        @click="removeStepField(i)"
                      >
                        <v-icon dark>mdi-minus</v-icon>
                      </v-btn>
                    </v-col>
                    <v-col cols="12" sm="10" class="pl-0">
                      <v-container>
                        <v-text-field
                          v-model="preparation.description"
                          :rules="[(value) => !!value || 'Required.']"
                          label="Description*"
                          hide-details="auto"
                          class="form-control"
                        />
                      </v-container>
                    </v-col>
                  </v-row>
                </v-card>
              </v-container>
            </div>
          </v-container>
        </v-card>
        <v-card-actions>
          <v-row justify="center">
            <v-card-actions>
              <v-row justify="center">
                <v-btn
                  class="submit"
                  type="submit"
                  elevation="2"
                  color="primary"
                  v-if="!submit"
                  :disabled="!valid"
                >
                  <span> Save </span>
                </v-btn>
                <v-btn
                  v-else
                  color="primary"
                  class="submit"
                  loading
                  :disabled="!valid"
                >
                </v-btn>
                <v-btn elevation="2" @click="$router.go(-1)">Back</v-btn>
              </v-row>
            </v-card-actions>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>

<script>
import { getFoods, updateRecipe, getRecipe } from "@/api";
import { store } from "@/auth";

export default {
  name: "EditRecipe",

  data() {
    return {
      load: true,
      recipeId: this.$route.params.id,
      valid: false,
      submit: false,
      nutrition_facts_fields: false,
      prepTime: null,
      recipe: {
        name: "",
        servings: null,
        preparation: {
          steps: [],
        },
        ingredients: [],
      },
      foods: [],
      measures: [],
      select: {
        id: "",
      },
      rules: {
        required: (value) => !!value || "Required.",
      },
      attrs: {
        class: "mb-6",
        boilerplate: true,
        elevation: 2,
      },
    };
  },

  methods: {
    addFoodField: function () {
      this.recipe.ingredients.push({});
    },

    removeFoodField: function (index) {
      this.recipe.ingredients.splice(index, 1);
    },

    addStepField: function () {
      this.recipe.preparation.steps.push({});
    },

    removeStepField: function (index) {
      this.recipe.preparation.steps.splice(index, 1);
    },

    showFields(ingredient) {
      let foodId = ingredient.food_id;
      let nutrition_facts = this.foods.find((food) => food.id == foodId)
        .nutrition_facts;
      ingredient.measures = [];
      if (nutrition_facts) {
        if (
          this.$store.state.mass_measures_keys.includes(
            nutrition_facts.serving_size_unit
          )
        ) {
          ingredient.measures = this.$store.state.mass_measures;
        } else if (
          this.$store.state.weight_measures_keys.includes(
            nutrition_facts.serving_size_unit
          )
        ) {
          ingredient.measures = this.$store.state.weight_measures;
        } else {
          ingredient.measures = this.$store.state.ingredients_measures;
        }
      } else {
        ingredient.measures = this.$store.state.ingredients_measures;
      }
      ingredient.nutrition_facts_fields = true;
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
          console.log(error.response);
        })
        .finally(() => {
          this.load = false;
        });
    },
    getRecipe() {
      getRecipe(this.recipeId)
        .then((result) => {
          this.recipe = result.data;
          delete this.recipe.user;
          delete this.recipe.user_id;
          delete this.recipe.nutrition_facts;
          if (this.recipe.preparation.steps.length > 0)
            this.recipe.preparation.steps.sort((step1, step2) => {
              if (step1.step < step2.step) return -1;
              else return 1;
            });
          if (this.recipe.ingredients.length > 0) {
            this.recipe.ingredients.map((ingredient) => {
              this.showFields(ingredient);
              ingredient.nutrition_facts_fields = true;
            });
          }
          this.prepTime = this.convertTimestampInMinutes(
            this.recipe.preparation.preparation_time
          );
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
    onSubmit: function () {
      this.submit = true;
      if (this.$refs.form.validate()) {
        this.recipe.preparation.steps.map((step, i) => {
          step.step = i + 1;
        });
        this.recipe.preparation.preparation_time = this.convertMinutesInTimestamp(
          this.prepTime
        );
        updateRecipe(this.recipeId, this.recipe)
          .then((result) => {
            this.$router.push({ name: "ListRecipe" });
          })
          .catch((error) => {
            this.submit = false;
            console.log(error.response);
          });
      } else {
        this.submit = false;
      }
    },
    convertMinutesInTimestamp: function (totalMinutes) {
      var hours = Math.floor(totalMinutes / 60);
      var minutes = totalMinutes % 60;
      return hours + ":" + minutes + ":00";
    },

    convertTimestampInMinutes: function (timestamp) {
      let val = String(timestamp).split(":");
      var hours = parseInt(val[0]);
      var minutes = parseInt(val[1]);
      return hours * 60 + minutes;
    },
  },

  created() {
    this.getRecipe();
    this.getAllFoods();
    this.measures = store.state.ingredients_measures;
  },
};
</script>