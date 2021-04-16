<template>
  <v-card elevation="2" class="card-form">
    <v-form
      ref="form"
      lazy-validation
      @submit.prevent="onSubmit"
      v-model="valid"
    >
      <h1>Edit Food</h1>
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
              v-model="food.name"
              :rules="[this.rules.required]"
              label="Name*"
              hide-details="auto"
              class="form-control"
            />
          </v-container>
        </v-card>
        <v-card class="mx-auto" elevation="0" outlined v-if="!load">
          <v-container>
            <v-col class="d-flex justify-content-between">
              <h2>Nutrition Facts</h2>
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
                    label="Serving Size*"
                    hide-details="auto"
                    :rules="[(value) => !!value || 'Required.']"
                    class="form-control"
                  />
                </v-col>
                <v-col>
                  <v-select
                    v-model="food.nutrition_facts.serving_size_unit"
                    :items="nutrition_facts_measures"
                    item-text="name"
                    item-value="id"
                    label="Select a Measure*"
                    :rules="[(value) => !!value || 'Required.']"
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
                            label="Select a Nutrient*"
                            :rules="[(value) => !!value || 'Required.']"
                            return-value
                          />
                        </v-col>
                        <v-col>
                          <v-text-field
                            v-model.number="nut_facts_nut.amount_per_serving"
                            :rules="[(value) => !!value || 'Required.']"
                            type="number"
                            label="Amount per serving (g)*"
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
          </v-container>
        </v-card>
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
      </div>
    </v-form>
  </v-card>
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
        required: (value) => !!value || "Required.",
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
            console.log(result);
            this.$router.push({ name: "ListFood" });
          })
          .catch((error) => {
            this.submit = false;
            console.log(error.response);
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
          console.log(error.response);
        });
    },
    getFood() {
      getFood(this.foodId)
        .then((result) => {
          this.food = result.data;
          if (this.food.nutrition_facts) this.nutritionFacts = true;
        })
        .catch((error) => {
          console.log(error.response);
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