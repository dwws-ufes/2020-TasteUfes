<template>
  <v-card elevation="2" class="card-form">
    <v-form
      ref="form"
      lazy-validation
      @submit.prevent="onSubmit"
      v-model="valid"
    >
      <h1>{{ $vuetify.lang.t('$vuetify.create') }} {{ $vuetify.lang.t('$vuetify.ingredient') }}</h1>
      <div class="form-group">
        <v-card class="mx-auto" elevation="0" outlined>
          <v-container>
            <v-autocomplete
              :search-input.sync="search"
              v-model="searchLDFood"
              :items="LDFoods"
              item-text="name"
              class="search"
              append-outer-icon="mdi-magnify"
              :label="$vuetify.lang.t('$vuetify.search') + ' ' + $vuetify.lang.t('$vuetify.ingredient')"
              hide-details
              return-object
              :loading="loadSearch"
              no-data-text="No food found"
              hide-no-data
              @keyup="getFoodsFromLD"
              @change="completeFood"
            ></v-autocomplete>
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined>
          <v-container>
            <v-text-field
              v-model="food.name"
              :rules="[rules.required]"
              :label="this.$vuetify.lang.t('$vuetify.name') + '*'"
              hide-details="auto"
              class="form-control"
            />
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined>
          <v-container>
            <v-col class="d-flex justify-content-between">
              <h2>{{ $vuetify.lang.t('$vuetify.nutrition_facts') }}</h2>
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
                    :label="this.$vuetify.lang.t('$vuetify.serving_size')"
                    hide-details="auto"
                    :rules="[rules.required, rules.limitMax, rules.limitMin]"
                    class="form-control"
                  />
                </v-col>
                <v-col>
                  <v-select
                    v-model="food.nutrition_facts.serving_size_unit"
                    :items="nutrition_facts_measures"
                    item-text="name"
                    item-value="id"
                    :label="this.$vuetify.lang.t('$vuetify.select_a') + ' ' + this.$vuetify.lang.t('$vuetify.measure') + '*'"
                    :rules="[rules.required]"
                    return-value
                  />
                </v-col>
              </v-row>
            </div>
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined v-if="nutritionFacts">
          <v-container>
            <v-col class="d-flex justify-content-between">
              <h2>{{ this.$vuetify.lang.t('$vuetify.nutrients') }}</h2>
              <div>
                <v-btn
                  v-if="
                    this.food.nutrition_facts.nutrition_facts_nutrients
                      .length == 0
                  "
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
              :key="nut_facts_nut.id"
              class="nutrition_facts_nutrients"
            >
              <v-card outlined shaped>
                <v-container>
                  <v-row>
                    <v-col
                      cols="12"
                      sm="2"
                      class="d-flex align-center justify-center"
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
                      <v-row>
                        <v-col>
                          <v-select
                            v-model="nut_facts_nut.nutrient_id"
                            :items="nutrients"
                            item-text="name"
                            item-value="id"
                            :label="$vuetify.lang.t('$vuetify.select_o') + ' ' + $vuetify.lang.t('$vuetify.nutrient') + '*'"
                            :rules="[rules.required]"
                            return-value
                          />
                        </v-col>
                        <v-col>
                          <v-text-field
                            v-model.number="nut_facts_nut.amount_per_serving"
                            type="number"
                            :label="$vuetify.lang.t('$vuetify.amount') + '*'"
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
                    </v-col>
                  </v-row>
                </v-container>
              </v-card>
            </div>
            <v-row
              v-if="
                this.food.nutrition_facts.nutrition_facts_nutrients.length > 0
              "
            >
              <v-col class="d-flex justify-end">
                <v-btn
                  class="mx-1 my-0"
                  fab
                  x-small
                  color="primary"
                  @click="addNutrientField"
                >
                  <v-icon dark> mdi-plus </v-icon>
                </v-btn>
              </v-col>
            </v-row>
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
              <span> {{ $vuetify.lang.t('$vuetify.create') }} </span>
            </v-btn>
            <v-btn
              v-else
              color="primary"
              class="submit"
              loading
              :disabled="!valid"
            >
            </v-btn>
            <v-btn elevation="2" @click="$router.go(-1)">{{ $vuetify.lang.t('$vuetify.back') }}</v-btn>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>

<script>
import { createFood, getNutrients, getLDFood } from "@/api";
import { store } from "@/auth";

export default {
  name: "CreateFood",
  data() {
    return {
      search: "",
      searchLDFood: "",
      valid: false,
      submit: false,
      loadSearch: false,
      nutritionFacts: false,
      food: {
        name: "",
        nutrition_facts: null,
      },
      LDFoods: [],
      nutrition_facts_measures: [],
      nutrients: [],
      rules: {
        required: (value) => !!value || this.$vuetify.lang.t('$vuetify.required') + '.',
        valRequired: (value) => !!value || value >= 0 || "Required value.",
        limitMax: (value) => value < 10000 || this.$vuetify.lang.t('$vuetify.too_big') + '.',
        limitMin: (value) => value >= 0 || this.$vuetify.lang.t('$vuetify.neg_zero') + '.',
      },
    };
  },

  methods: {
    getFoodsFromLD() {
      if (this.search && this.search.length == 3) {
        this.loadSearch = true;
        getLDFood(this.search)
          .then((result) => {
            this.LDFoods = result.data;
            this.loadSearch = false;
          })
          .catch((error) => {
            this.loadSearch = false;
          });
      }
      if (this.search.length < 3) {
        this.LDFoods = [];
      }
    },

    completeFood() {
      this.food = this.searchLDFood;
      this.food.nutrition_facts
        ? (this.nutritionFacts = true)
        : (this.nutritionFacts = false);
    },

    changeNutritionFacts: function () {
      if (!this.nutritionFacts) {
        this.food = {
          name: this.food.name,
          nutrition_facts: {
            serving_size_unit: null,
            nutrition_facts_nutrients: [],
          },
        };
        this.nutritionFacts = true;
      } else {
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
        createFood(this.food)
          .then((result) => {
            this.$store.dispatch("setSnackbar", {
              text: `${this.$vuetify.lang.t('$vuetify.ingredient')} ${this.food.name} ${this.$vuetify.lang.t('$vuetify.created')}.`,
              color: "success",
            });
            this.$router.push({ name: "ListFood" });
          })
          .catch((error) => {
            error.response.data.errors.map((error) => {
              this.$store.dispatch("setSnackbar", {
                text: `${error.message}`,
                color: "error",
              });
            });
            this.submit = false;
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
  },
  created() {
    this.nutrition_facts_measures = store.state.nutrition_facts_measures;
    this.getNutrients();
  },
};
</script>