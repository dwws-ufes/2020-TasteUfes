<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="12" d-flex justify-center class="py-0">
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)">
            <v-icon>mdi-chevron-left</v-icon> Back
          </span>
        </div>
      </v-col>
      <v-col cols="12" sm="8" d-flex justify-center>
        <v-card>
          <v-card-title
            ><h1>{{ recipe.name }}</h1></v-card-title
          >
          <v-divider class="mx-4" />
          <v-list-item>
            <v-list-item-content>
              <div class="my-2">
                <v-row>
                  <v-col justify="space-around">
                    <v-text-field
                      v-model.number="serv"
                      label="Servings"
                      type="number"
                      :rules="this.limitRule"
                      @change="recalculate()"
                    />
                  </v-col>
                </v-row>
              </div>
              <div class="my-2">
                <b>Preparation Time:</b>
                {{ this.recipe.preparation.preparation_time }}
              </div>
            </v-list-item-content>
          </v-list-item>
          <v-list-item v-if="this.recipe.ingredients.length > 0">
            <v-list-item-content>
              <h3>Ingredients</h3>
              <v-divider class="px-1 pb-3" />

              <v-list-item-content
                v-for="ingredient in this.recipe.ingredients"
                :key="ingredient.id"
              >
                <span>
              <router-link class="text-decoration-none" :to="{ name: 'DetailsFood', params: {id: ingredient.food.id} }"">
                  <b>{{ ingredient.food.name }}:</b>
              </router-link>
                  {{ formatNumber(ingredient.quantity)
                  }}{{ getMeasureName(ingredient.quantity_unit) }}
                </span>
              </v-list-item-content>
            </v-list-item-content>
          </v-list-item>
          <v-list-item v-if="this.recipe.preparation.steps.length > 0">
            <v-list-item-content>
              <h3>Steps</h3>
              <v-divider class="px-1 pb-3" />
              <v-list-item-content
                v-for="step in this.recipe.preparation.steps"
                :key="step.step"
              >
                <ul>
                  <li>
                    <span>
                      <b>Step {{ step.step }}:</b> {{ step.description }}
                    </span>
                  </li>
                </ul>
              </v-list-item-content>
            </v-list-item-content>
          </v-list-item>
        </v-card>
      </v-col>
      <v-col cols="12" sm="4">
        <v-card class="mb-5">
          <UserCard :user="this.recipe.user" :username="null" />
        </v-card>
        <v-card>
          <NutritionFactsTable
            :data="this.recipe.nutrition_facts"
            :servings="this.recipe.servings"
          />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getRecipe, recalculatePerServing } from "@/api";
import NutritionFactsTable from "@/components/details/NutritionFactsTable.vue";
import UserCard from "@/components/UserCard.vue";

export default {
  name: "DetailsRecipe",

  data() {
    return {
      recipeId: this.$route.params.id,
      serv: null,
      limitRule: [
        (value) => value < 10000 || "Value too big",
        (value) => value > 0 || "Value must not be negative or 0",
      ],
      recipe: {
        name: "",
        servings: null,
        preparation: {
          steps: [],
        },
        ingredients: [],
        nutrition_facts: {},
        user: {
          roles: [],
        },
      },
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
          this.recipe.preparation.steps.sort((step1, step2) => {
            if (step1.step < step2.step) return -1;
            else return 1;
          });
          this.serv = this.recipe.servings;
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
    getMeasureName(id) {
      return this.$store.state.ingredients_measures.find(
        (measure) => measure.id == id
      ).name;
    },
    getDailyValue(daily_value) {
      return (daily_value * 100).toFixed(2);
    },

    recalculate() {
      if (this.serv > 0 && this.serv < 10000) {
        recalculatePerServing(this.recipe.id, this.serv)
          .then((result) => {
            this.recipe = result.data;
          })
          .catch((error) => {
            console.log(error.response);
          });
      }
    },
    formatNumber(value) {
      if (parseInt(value) == 0) {
        return value.toFixed(2);
      } else {
        return value;
      }
    },
  },

  components: {
    NutritionFactsTable,
    UserCard,
  },
};
</script>

<style lang="scss" scoped>
.details {
  .v-card {
    margin-top: 0;
  }
}
</style>