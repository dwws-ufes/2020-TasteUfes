<template>
  <v-card elevation="2" class="card-form">
    <v-form
      ref="form"
      lazy-validation
      @submit.prevent="onSubmit"
      v-model="valid"
    >
      <h1>Create Recipe</h1>
      <div class="form-group">
        <v-card class="mx-auto" elevation="0" outlined>
          <v-container>
            <v-text-field
              v-model="recipe.name"
              :rules="[this.rules.required]"
              label="Name"
              hide-details="auto"
              class="form-control"
            />
            <v-text-field
              v-model.number="recipe.servings"
              :rules="[this.rules.required]"
              type="number"
              label="Servings"
              hide-details="auto"
              class="form-control"
            />
            <v-text-field
              v-model.number="prepTime"
              :rules="[(value) => !!value || 'Required.']"
              label="Preparation time (in minutes)"
              type="number"
              hide-details="auto"
              class="form-control"
            />
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined>
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
                        <v-select
                          v-model="ingredient.food_id"
                          :items="foods"
                          item-text="name"
                          item-value="id"
                          label="Select a food"
                          :rules="[(value) => !!value || 'Required.']"
                          return-value
                          @change="showFields(ingredient)"
                        />
                        <v-select
                          v-model="ingredient.quantity_unit"
                          :items="ingredient.measures"
                          item-text="name"
                          item-value="id"
                          label="Select a Measure"
                          :rules="[(value) => !!value || 'Required.']"
                          return-value
                          v-if="ingredient.nutrition_facts_fields"
                        />
                        <v-text-field
                          v-model.number="ingredient.quantity"
                          :rules="[(value) => !!value || 'Required.']"
                          type="number"
                          label="Quantity Food"
                          hide-details="auto"
                          class="form-control"
                          v-if="ingredient.nutrition_facts_fields"
                        />
                      </v-container>
                    </v-col>
                  </v-row>
                </v-card>
              </v-container>
            </div>
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined>
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
                          label="Description"
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
                  :disabled="!valid"
                >
                  <span v-if="!submit"> Create </span>
                  <v-progress-circular
                    v-else
                    indeterminate
                    color="white"
                  ></v-progress-circular>
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
import { createRecipe, getFoods } from "@/api";
import { store } from "@/auth";

export default {
  name: "CreateFood",
  data() {
    return {
      valid: false,
      submit: false,
      prepTime: null,
      nutrition_facts_fields: false,
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
        });
    },
    onSubmit: function () {
      this.submit = true;
      if (this.$refs.form.validate()) {
        this.recipe.preparation.steps.map((step, i) => {
          step.step = i + 1;
        });
        this.recipe.preparation.steps.sort((step1, step2) => {
          if (step1.step < step2.step) return -1;
          else return 1;
        });
        this.recipe.preparation.preparation_time = this.convertMinutesInTimestamp(
          this.prepTime
        );
        createRecipe(this.recipe)
          .then((result) => {
            console.log(result);
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
  },

  created() {
    this.getAllFoods();
    this.measures = store.state.ingredients_measures;
  },
};
</script>