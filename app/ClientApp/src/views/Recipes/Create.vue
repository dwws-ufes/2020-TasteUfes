<template>
  <v-row class="justify-center">
    <v-col cols="12" lg="8" sm="10" xs="12">
      <v-card elevation="2" class="card-form">
        <v-form
          ref="form"
          lazy-validation
          @submit.prevent="onSubmit"
          v-model="valid"
        >
          <h1>
            {{ $vuetify.lang.t("$vuetify.create") }}
            {{ $vuetify.lang.t("$vuetify.recipe") }}
          </h1>
          <div class="form-group">
            <v-card class="mx-auto" elevation="0" outlined>
              <v-container>
                <v-text-field
                  v-model="recipe.name"
                  :rules="[this.rules.required]"
                  :label="$vuetify.lang.t('$vuetify.name') + '*'"
                  hide-details="auto"
                  class="form-control"
                />
                <v-text-field
                  v-model.number="recipe.servings"
                  :rules="[
                    this.rules.required,
                    this.rules.limitMax,
                    this.rules.limitMin,
                  ]"
                  type="number"
                  :label="this.$vuetify.lang.t('$vuetify.servings') + '*'"
                  hide-details="auto"
                  class="form-control"
                />
                <v-text-field
                  v-model.number="prepTime"
                  :rules="[
                    this.rules.required,
                    this.rules.limitMaxTime,
                    this.rules.limitMin,
                  ]"
                  :label="
                    this.$vuetify.lang.t('$vuetify.preparation_time') + '*'
                  "
                  type="number"
                  hide-details="auto"
                  class="form-control"
                />
              </v-container>
            </v-card>
            <v-card class="mx-auto" elevation="0" outlined>
              <v-container>
                <v-col class="d-flex justify-content-between">
                  <h2>{{ $vuetify.lang.t("$vuetify.ingredient") }}</h2>
                  <v-btn
                    v-if="this.recipe.ingredients.length == 0"
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
                  :key="ingredient.id"
                  class="foods"
                >
                  <v-card outlined shaped>
                    <v-container>
                      <v-row>
                        <v-col
                          cols="12"
                          sm="2"
                          class="d-flex align-center btn-minus"
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
                              :label="
                                $vuetify.lang.t('$vuetify.select_o') +
                                ' ' +
                                $vuetify.lang.t('$vuetify.ingredient') +
                                '*'
                              "
                              :rules="[rules.required]"
                              return-value
                              @change="showFields(ingredient)"
                            />
                            <v-row>
                              <v-col cols="12" sm="6" xs="12">
                                <v-text-field
                                  v-model.number="ingredient.quantity"
                                  :rules="[
                                    rules.required,
                                    rules.limitMax,
                                    rules.limitMin,
                                  ]"
                                  type="number"
                                  :label="
                                    $vuetify.lang.t('$vuetify.quantity') + '*'
                                  "
                                  hide-details="auto"
                                  class="form-control"
                                  v-if="ingredient.nutrition_facts_fields"
                                />
                              </v-col cols="12" sm="6" xs="12">
                              <v-col>
                                <v-select
                                  v-model="ingredient.quantity_unit"
                                  :items="ingredient.measures"
                                  item-text="name"
                                  item-value="id"
                                  :label="
                                    $vuetify.lang.t('$vuetify.select_a') +
                                    ' ' +
                                    $vuetify.lang.t('$vuetify.measure') +
                                    '*'
                                  "
                                  :rules="[rules.required]"
                                  return-value
                                  v-if="ingredient.nutrition_facts_fields"
                                />
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card>
                </div>
                <v-row v-if="this.recipe.ingredients.length > 0">
                  <v-col class="d-flex justify-end">
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
                </v-row>
              </v-container>
            </v-card>
            <v-card class="mx-auto" elevation="0" outlined>
              <v-container>
                <v-col class="d-flex justify-content-between">
                  <h2>{{ $vuetify.lang.t("$vuetify.step") }}</h2>
                  <div>
                    <v-btn
                      v-if="this.recipe.preparation.steps.length == 0"
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
                  <v-card outlined shaped>
                    <v-container>
                      <v-row>
                        <v-col
                          cols="12"
                          sm="2"
                          class="d-flex align-center btn-minus"
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
                              :rules="[
                                (value) =>
                                  !!value ||
                                  $vuetify.lang.t('$vuetify.required') + '.',
                              ]"
                              :label="
                                $vuetify.lang.t('$vuetify.description') + '*'
                              "
                              hide-details="auto"
                              class="form-control"
                            />
                          </v-container>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card>
                </div>
                <v-row v-if="this.recipe.preparation.steps.length > 0">
                  <v-col class="d-flex justify-end">
                    <v-btn
                      class="mx-1 my-0"
                      fab
                      x-small
                      color="primary"
                      @click="addStepField"
                    >
                      <v-icon dark> mdi-plus </v-icon>
                    </v-btn>
                  </v-col>
                </v-row>
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
                      <span> {{ $vuetify.lang.t("$vuetify.create") }} </span>
                    </v-btn>
                    <v-btn
                      v-else
                      color="primary"
                      class="submit"
                      loading
                      :disabled="!valid"
                    >
                    </v-btn>
                    <v-btn elevation="2" @click="$router.go(-1)">{{
                      $vuetify.lang.t("$vuetify.back")
                    }}</v-btn>
                  </v-row>
                </v-card-actions>
              </v-row>
            </v-card-actions>
          </div>
        </v-form>
      </v-card>
    </v-col>
  </v-row>
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
        required: (value) =>
          !!value || this.$vuetify.lang.t("$vuetify.required") + ".",
        limitMax: (value) =>
          value < 10000 || this.$vuetify.lang.t("$vuetify.too_big") + ".",
        limitMaxTime: (value) =>
          value < 1400 || this.$vuetify.lang.t("$vuetify.bit_time") + ".",
        limitMin: (value) =>
          value > 0 || this.$vuetify.lang.t("$vuetify.neg_zero") + ".",
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
          error.response.data.errors.map((error) => {
            this.$store.dispatch("setSnackbar", {
              text: `${error.message}`,
              color: "error",
            });
          });
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
            this.$store.dispatch("setSnackbar", {
              text: `${this.$vuetify.lang.t("$vuetify.recipe")} ${
                this.recipe.name
              } ${this.$vuetify.lang.t("$vuetify.created_a")}.`,
              color: "success",
            });
            this.$router.push({ name: "ListRecipe" });
          })
          .catch((error) => {
            this.submit = false;
            error.response.data.errors.map((error) => {
              this.$store.dispatch("setSnackbar", {
                text: `${error.message}`,
                color: "error",
              });
            });
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