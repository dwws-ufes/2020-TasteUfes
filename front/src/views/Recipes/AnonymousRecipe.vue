<template>
  <v-container class="d-flex justify-center">

    <v-card elevation="2" class="card-form" v-if="!submit">
      <v-form ref="form" @submit.prevent="onSubmit" v-model="valid">
        <h1>My Recipe</h1>
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

          <v-card-actions>
            <v-row justify="center">
              <v-card-actions>
                <v-row justify="center">
                  <v-btn
                    class="submit"
                    type="submit"
                    elevation="2"
                    color="primary"
                    :disabled="!validBtn"
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
    <v-container class="details" v-else>
      <v-row justify="center">
        <v-col cols="12" sm="12" d-flex justify-center class="py-0">
          <div class="d-flex">
            <span class="back-btn" @click="submit = !submit">
              <v-icon>mdi-chevron-left</v-icon> Back
            </span>
          </div>
        </v-col>
        <v-col cols="12" sm="8" d-flex justify-center>
          <v-card>
            <v-card-title
            ><h1>Anonymous Recipe</h1></v-card-title
          ></v-card-title>
          <v-divider class="mx-4" />
            <v-list-item v-if="this.anonymous.ingredients.length > 0">
              <v-list-item-content>
                <h3>Ingredients</h3>
                <v-divider class="px-1 pb-3" />

                <v-list-item-content
                  v-for="ingredient in this.anonymous.ingredients"
                  :key="ingredient.id"
                >
                  <span>
                    <router-link target="_blank" class="text-decoration-none" :to="{ name: 'DetailsFood', params: {id: ingredient.food.id} }"">
                  <b>{{ ingredient.food.name }}:</b>
              </router-link>
                    {{ ingredient.quantity
                    }}{{ getMeasureName(ingredient.quantity_unit) }}
                  </span>
                </v-list-item-content>
              </v-list-item-content>
            </v-list-item>
          </v-card>
        </v-col>
        <v-col cols="12" sm="4" v-if="this.anonymous.ingredients.length > 0">
          <v-card>
            <NutritionFactsTable
            :data="this.anonymous.nutrition_facts"
            :servings="this.anonymous.servings"
            />
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-container>
</template>

<script>
import { calculateAnonymous, getFoods } from "@/api";
import { store } from "@/auth";
import NutritionFactsTable from "@/components/details/NutritionFactsTable.vue";

export default {
  name: "AnonymousRecipe",
  components: {
    NutritionFactsTable,
  },
  data() {
    return {
      valid: false,
      validBtn: false,
      submit: false,
      prepTime: null,
      nutrition_facts_fields: false,
      recipe: {
        preparation: {},
        ingredients: [{}],
      },

      anonymous: {
        ingredients: [],
        preparation: {},
        nutrition_facts: {
          nutrition_facts_nutrients: {
            nutrient: {},
          },
        },
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
      if(this.recipe.ingredients.length == 0) this.validBtn = false;
    },

    getMeasureName(id) {
      return this.$store.state.ingredients_measures.find(
        (measure) => measure.id == id
      ).name;
    },
    getDailyValue(daily_value) {
      return (daily_value * 100).toFixed(2);
    },

    showFields(ingredient) {
      let foodId = ingredient.food_id;
      let nutrition_facts = this.foods.find((food) => food.id == foodId)
        .nutrition_facts;
      ingredient.measures = [];
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
      ingredient.nutrition_facts_fields = true;
    },

    convertMinutesInTimestamp: function (totalMinutes) {
      var hours = Math.floor(totalMinutes / 60);
      var minutes = totalMinutes % 60;
      return hours + ":" + minutes + ":00";
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
        this.recipe.preparation.preparation_time = this.convertMinutesInTimestamp(
          this.prepTime
        );
        calculateAnonymous(this.recipe)
          .then((result) => {
            this.anonymous = result.data;
            this.anonymous.name = this.recipe.name;
          })
          .catch((error) => {
            this.submit = false;
            console.log(error.response);
          });
      } else {
        this.submit = false;
      }
    },
  },

  created() {
    this.getAllFoods();
    this.measures = store.state.ingredients_measures;
  },
};
</script>

<style lang="scss" scoped>
.card-form {
  max-width: 400px;
}
</style>