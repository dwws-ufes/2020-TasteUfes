<template>
  <div class="recipe-data">
    <v-card-title v-if="!anonymous"
      ><h1>{{ recipeData.name }}</h1></v-card-title
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
          {{ recipeData.preparation.preparation_time }}
        </div>
      </v-list-item-content>
    </v-list-item>
    <v-list-item v-if="recipeData.ingredients.length > 0">
      <v-list-item-content>
        <h3>Ingredients</h3>
        <v-divider class="px-1 pb-3" />

        <v-list-item-content
          v-for="ingredient in recipeData.ingredients"
          :key="ingredient.id"
        >
          <span>
            <b>{{ ingredient.food.name }}:</b>
            {{ ingredient.quantity
            }}{{ getMeasureName(ingredient.quantity_unit) }}
          </span>
        </v-list-item-content>
      </v-list-item-content>
    </v-list-item>
    <div v-if="!anonymous">
      <v-list-item v-if="recipeData.preparation.steps.length > 0">
        <v-list-item-content>
          <h3>Steps</h3>
          <v-divider class="px-1 pb-3" />
          <v-list-item-content
            v-for="step in recipeData.preparation.steps"
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
    </div>
  </div>
</template>

<script>
import { recalculatePerServing } from "@/api";
export default {
  data() {
    return {
      serv: null,
      limitRule: [
        (value) => value < 10000 || "Value too big",
        (value) => value > 0 || "Value must not be negative or 0",
      ],
      recipeData: null,
    };
  },
  props: {
    recipe: {
      preparation: {
        steps: [],
      },
      ingredients: [],
      nutrition_facts: {},
      user: {
        roles: [],
      },
    },
    anonymous: Boolean,
    servings: Number,
  },

  mounted() {
    this.recipeData = this.recipe;
    this.serv = this.recipe;
  },

  methods: {
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
            this.recipeData = result.data;
            // this.$emit('recalculate', this.recipe.nutrition_facts);
            console.log(result);
          })
          .catch((error) => {
            error.response.data.errors.map((error) => {
              this.$store.dispatch("setSnackbar", {
                text: `${error.message}`,
                color: "error",
              });
            });
          });
      }
    },
  },
};
</script>

<style>
</style>