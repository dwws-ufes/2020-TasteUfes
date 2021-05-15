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
            {{ $vuetify.lang.t("$vuetify.edit") }}
            {{ $vuetify.lang.t("$vuetify.ingredient") }}
          </h1>
          <div class="form-group">
            <v-card class="mx-auto" elevation="0" outlined>
              <v-container>
                <v-text-field
                  v-model="food.name"
                  :rules="[rules.required]"
                  :label="$vuetify.lang.t('$vuetify.name') + '*'"
                  hide-details="auto"
                  class="form-control"
                />
              </v-container>
            </v-card>
            <v-card class="mx-auto" elevation="0" outlined>
              <v-container>
                <v-col class="d-flex justify-content-between">
                  <h2>{{ $vuetify.lang.t("$vuetify.nutrition_facts") }}</h2>
                  <div>
                    <v-btn
                      class="mx-0 my-0"
                      fab
                      x-small
                      color="primary"
                      @click="changeNutritionFacts"
                      v-if="!nutritionFacts"
                    >
                      <v-icon dark> mdi-plus </v-icon>
                    </v-btn>
                    <v-btn
                      class="mx-0 my-0"
                      fab
                      x-small
                      dark
                      color="red"
                      @click="changeNutritionFacts"
                      v-else
                    >
                      <v-icon dark> mdi-minus </v-icon>
                    </v-btn>
                  </div>
                </v-col>
                <div v-if="nutritionFacts">
                  <v-row>
                    <v-col>
                      <v-text-field
                        v-model.number="food.nutrition_facts.serving_size"
                        type="number"
                        :label="$vuetify.lang.t('$vuetify.serving_size') + '*'"
                        hide-details="auto"
                        :rules="[
                          rules.required,
                          rules.limitMax,
                          rules.limitMin,
                        ]"
                        class="form-control"
                      />
                    </v-col>
                    <v-col>
                      <v-select
                        v-model="food.nutrition_facts.serving_size_unit"
                        :items="nutrition_facts_measures"
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
                      />
                    </v-col>
                  </v-row>
                </div>
              </v-container>
            </v-card>
            <v-card
              class="mx-auto"
              elevation="0"
              outlined
              v-if="nutritionFacts"
            >
              <v-container>
                <v-col class="d-flex justify-content-between">
                  <h2>Nutritients</h2>
                  <div>
                    <v-btn
                      class="mx-0 my-0"
                      fab
                      x-small
                      color="primary"
                      @click="addNutrientField"
                    >
                      <v-icon dark> mdi-plus </v-icon>
                    </v-btn>
                  </div>
                </v-col>
                <div
                  v-for="(nut_facts_nut, i) in this.food.nutrition_facts
                    .nutrition_facts_nutrients"
                  :key="i"
                  class="nutrition_facts_nutrients"
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
                            @click="removeNutrientField(i)"
                          >
                            <v-icon dark>mdi-minus</v-icon>
                          </v-btn>
                        </v-col>
                        <v-col cols="12" sm="10" class="pl-0">
                          <v-container>
                            <v-row>
                              <v-col>
                                <v-select
                                  v-model="nut_facts_nut.nutrient_id"
                                  :items="nutrients"
                                  item-text="name"
                                  item-value="id"
                                  :label="
                                    $vuetify.lang.t('$vuetify.select_o') +
                                    ' ' +
                                    $vuetify.lang.t('$vuetify.nutrient') +
                                    '*'
                                  "
                                  :rules="[rules.required]"
                                  return-value
                                />
                              </v-col>
                              <v-col>
                                <v-text-field
                                  v-model.number="
                                    nut_facts_nut.amount_per_serving
                                  "
                                  type="number"
                                  :label="
                                    $vuetify.lang.t('$vuetify.amount') + '*'
                                  "
                                  :rules="[
                                    rules.valRequired,
                                    rules.limitMax,
                                    rules.limitMin,
                                  ]"
                                  hide-details="auto"
                                  class="form-control"
                                />
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card>
                </div>
              </v-container>
            </v-card>
            <v-card-actions>
              <v-row justify="center">
                <v-btn
                  class="submit"
                  type="submit"
                  elevation="2"
                  color="primary"
                  :disabled="!valid"
                  v-if="!submit"
                >
                  <span> {{ $vuetify.lang.t("$vuetify.update") }} </span>
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
          </div>
        </v-form>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import { getFood, updateFood, getNutrients } from "@/api";
import { store } from "@/auth";

export default {
  name: "EditFood",

  data() {
    return {
      load: true,
      foodId: this.$route.params.id,
      valid: false,
      submit: false,
      nutritionFacts: false,
      food: {
        name: "",
        nutrition_facts: null,
      },
      nutrition_facts_measures: [],
      nutrition_facts: null,
      nutrients: [],
      rules: {
        required: (value) =>
          !!value || this.$vuetify.lang.t("$vuetify.required") + ".",
        valRequired: (value) => !!value || value >= 0 || "Required value.",
        limitMax: (value) =>
          value < 10000 || this.$vuetify.lang.t("$vuetify.too_big") + ".",
        limitMin: (value) =>
          value >= 0 || this.$vuetify.lang.t("$vuetify.neg_zero") + ".",
      },
    };
  },

  methods: {
    changeNutritionFacts: function () {
      if (!this.nutritionFacts) {
        if (this.nutrition_facts) {
          this.food.nutrition_facts = this.nutrition_facts;
        } else {
          this.food = {
            id: this.food.id,
            name: this.food.name,
            nutrition_facts: {
              serving_size_unit: null,
              nutrition_facts_nutrients: [],
            },
          };
        }
        this.nutritionFacts = true;
      } else {
        this.nutrition_facts = this.food.nutrition_facts;
        this.nutritionFacts = false;
        delete this.food.nutrition_facts;
      }
    },

    addNutrientField: function () {
      this.food.nutrition_facts.nutrition_facts_nutrients.push({});
    },

    removeNutrientField: function (index) {
      this.food.nutrition_facts.nutrition_facts_nutrients.splice(index, 1);
    },

    onSubmit: function () {
      this.submit = true;
      if (this.$refs.form.validate()) {
        if (
          this.food.nutrition_facts &&
          this.food.nutrition_facts.nutrition_facts_nutrients.length > 0
        ) {
          this.food.nutrition_facts.nutrition_facts_nutrients.map((nut) => {
            nut.amount_per_serving_unit = 1;
          });
        }
        updateFood(this.foodId, this.food)
          .then((result) => {
            this.$store.dispatch("setSnackbar", {
              text: `${$vuetify.lang.t("$vuetify.ingredient")} ${
                this.food.name
              } updated.`,
              color: "success",
            });
            this.$router.push({ name: "ListFood" });
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
    getNutrients() {
      getNutrients()
        .then((nutrients) => {
          this.nutrients = nutrients.data;
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
    getFood() {
      getFood(this.foodId)
        .then((result) => {
          this.food = result.data;
          if (this.food.nutrition_facts) this.nutritionFacts = true;
        })
        .catch((error) => {
          error.response.data.errors.map((error) => {
            this.$store.dispatch("setSnackbar", {
              text: `${error.message}`,
              color: "error",
            });
          });
        })
        .finally(() => {
          this.load = false;
        });
    },
  },
  created() {
    this.getFood();
    this.nutrition_facts_measures = store.state.nutrition_facts_measures;
    this.getNutrients();
  },
};
</script>