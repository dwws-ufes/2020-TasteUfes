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
        <v-text-field
          v-model="food.name"
          :rules="[this.rules.required]"
          label="Name"
          hide-details="auto"
          class="form-control"
        />

        <v-text-field
          v-model.number="food.nutrition_facts.serving_size"
          :rules="[this.rules.required]"
          type="number"
          label="Serving Size"
          hide-details="auto"
          class="form-control"
        />

        <v-select
          v-model="food.nutrition_facts.serving_size_unit"
          :items="nutrition_facts_measures"
          item-text="name"
          item-value="id"
          label="Select a Measure"
          :rules="[(value) => !!value || 'Required.']"
          return-value
          single-line
        />
        <div
          v-for="(nut_facts_nut, i) in this.food.nutrition_facts
            .nutrition_facts_nutrients"
          :key="i"
          class="nutrition_facts_nutrients"
        >
          <v-select
            v-model="nut_facts_nut.nutrient_id"
            :items="nutrients"
            item-text="name"
            item-value="id"
            label="Select a Nutrient"
            :rules="[(value) => !!value || 'Required.']"
            return-value
            single-line
          />
          <v-text-field
            v-model.number="nut_facts_nut.amount_per_serving"
            :rules="[(value) => !!value || 'Required.']"
            type="number"
            label="Amount per serving (g)"
            hide-details="auto"
            class="form-control"
          />
          <v-btn @click="removeNutrientField(i)">-</v-btn>
        </div>
        <v-btn @click="addNutrientField">+ Nutrient</v-btn>
        <v-card-actions>
          <v-row justify="center">
            <v-btn
              class="submit"
              type="submit"
              elevation="2"
              color="primary"
              :disabled="!valid"
            >
              <span v-if="!submit"> Save </span>
              <v-progress-circular
                v-else
                indeterminate
                color="white"
              ></v-progress-circular>
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
      foodId: this.$route.params.id,
      valid: false,
      submit: false,
      food: {
        name: "",
        nutrition_facts: {
          nutrition_facts_nutrients: [],
        },
      },
      nutrition_facts_measures: [],
      nutrients: [],
      rules: {
        required: (value) => !!value || "Required.",
      },
    };
  },

  methods: {
    addNutrientField: function () {
      this.food.nutrition_facts.nutrition_facts_nutrients.push({});
    },

    removeNutrientField: function (index) {
      this.food.nutrition_facts.nutrition_facts_nutrients.splice(index, 1);
    },

    onSubmit: function () {
      this.submit = true;
      this.food.nutrition_facts.nutrition_facts_nutrients.map((nut) => {
        if (!nut.amount_per_serving_unit) {nut.amount_per_serving_unit = 1; console.log(nut)};
        delete nut.nutrient;
        delete nut.id;
      });
      
      updateFood(this.foodId, this.food)
        .then((result) => {
          console.log(result);
          this.$router.push({ name: "ListFood" });
        })
        .catch((error) => {
          this.submit = false;
          console.log(error.response);
        });
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
        })
        .catch((error) => {
          console.log(error.response);
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