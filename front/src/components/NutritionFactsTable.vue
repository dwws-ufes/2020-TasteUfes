<template>
  <v-container>
    <v-card-title>Nutrition Facts</v-card-title>
    <v-row>
      <v-col>
        <v-card-text>
          <v-divider class="mb-4" />
          <span class="py-1"
            >{{ servings }} serving{{ servings != 1 ? "s" : "" }} per
            container</span
          >
          <h3 class="py-1">
            Serving size
            <span class="float-right"
              >{{ data.serving_size
              }}{{ getMeasureName(data.serving_size_unit) }}</span
            >
          </h3>

          <v-divider class="my-4" />
          <v-row>
            <v-col>
              <h3 class="py-1">Amount per serving</h3>
              <h2 class="py-1">
                Calories
                <span class="float-right">{{
                  parseInt(data.serving_energy)
                }}</span>
              </h2>
              <h2 class="py-1">
                Daily Value
                <span class="float-right"
                  >{{ getDailyValue(data.daily_value) }}%</span
                >
              </h2>
            </v-col>
          </v-row>
          <v-divider class="my-4" />
          <v-row>
            <v-col class="pb-0">
              <span class="float-right"><h5>% Daily Value*</h5></span>
            </v-col>
            <v-col cols="12 pb-5">
              <div
                v-for="nutrition_facts_nutrient in data.nutrition_facts_nutrients"
                :key="nutrition_facts_nutrient.id"
              >
                <h5>
                  <v-divider />
                  {{ nutrition_facts_nutrient.nutrient.name }}
                  {{ parseInt(nutrition_facts_nutrient.amount_per_serving)
                  }}{{
                    getMeasureName(
                      nutrition_facts_nutrient.amount_per_serving_unit
                    )
                  }}
                  <span class="float-right">
                    {{ getDailyValue(nutrition_facts_nutrient.daily_value) }}%
                  </span>
                </h5>
              </div>
            </v-col>
          </v-row>
        </v-card-text>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  props: {
    servings: Number,
    data: {
      nutrition_facts_nutrients: [
        {
          nutrient: Object,
        },
      ],
    },
  },
  methods: {
    getMeasureName(id) {
      if (id > 0)
        return this.$store.state.ingredients_measures.find(
          (measure) => measure.id == id
        ).name;
    },
    getDailyValue(daily_value) {
      return (daily_value * 100).toFixed(2);
    },
  },
};
</script>

<style lang="scss" scoped>
.v-card {
  &__text {
    padding-top: 0;
    padding-bottom: 0;
  }
}
</style>