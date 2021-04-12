<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="4" d-flex justify-center>
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)"
            ><v-icon>mdi-chevron-left</v-icon> Back</span
          >
        </div>
        <v-card>
          <v-container>
            <v-card-title><h1>{{ food.name }}</h1></v-card-title>
            <v-divider class="mx-4" />
          </v-container>
          <NutritionFactsTable :data="this.food.nutrition_facts" :servings="this.food.nutrition_facts.serving_size" />
        </v-card>

      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getFood } from "@/api";
import NutritionFactsTable from "@/components/NutritionFactsTable.vue";

export default {
  name: "DetailsFood",

  data() {
    return {
      foodId: this.$route.params.id,
      food: {
        nutrition_facts: []
      },
    };
  },

  created: function () {
    this.getData();
  },

  methods: {
    getData: function () {
      getFood(this.foodId)
        .then((result) => {
          this.food = result.data;
        })
        .catch((error) => {
          console.log(error.response);
        });
    },
  },

  components: {
    NutritionFactsTable,
  }
};
</script>

<style lang="scss" scoped>
  .v-card {
    &__title {
      padding-bottom: 5;
    }
  }
</style>