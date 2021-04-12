<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form" @submit.prevent="onSubmit" v-model="valid">
      <h1>Create Recipe</h1>
      <div class="form-group">
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
        <div v-for="(ingredient, i) in this.recipe.ingredients" class="foods">
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
          <v-text-field
            v-model.number="ingredient.quantity"
            :rules="[(value) => !!value || 'Required.']"
            type="number"
            label="Quantity Food"
            hide-details="auto"
            class="form-control"
            v-if="ingredient.nutrition_facts_fields"
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

          <v-btn @click="removeFoodField(i)">-</v-btn>
        </div>
        <div
          v-for="(preparation, i) in this.recipe.preparation.steps"
          :key="i"
          class="foods"
        >
          <v-text-field
            v-model="preparation.description"
            :rules="[(value) => !!value || 'Required.']"
            label="Description"
            hide-details="auto"
            class="form-control"
          />

          <v-btn @click="removeStepField(i)">-</v-btn>
        </div>
        <v-btn @click="addFoodField">+ Ingredient</v-btn>
        <v-btn @click="addStepField">+ Step</v-btn>

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
// import Alert from '@/components/Alert.vue';

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

  components: {
    // Alert,
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
          this.foods = foods.data;
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
    onSubmit: function () {
      this.submit = true;
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
    },
  },

  created() {
    this.getAllFoods();
    this.measures = store.state.ingredients_measures;
  },
};
</script>