<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="6" d-flex justify-center>
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)"><v-icon>mdi-chevron-left</v-icon> Back</span>
        </div>
        <v-card>
          <v-card-title>{{ food.name }}</v-card-title>
          <v-divider class="mx-4"></v-divider>
          <v-card-text>
            <div class="my-2">
              <b>Nutriction Facts:</b> {{ food.nutrition_facts_id }}
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import foods from "@/assets/json/food.json";

export default {
  name: "DetailsFood",

  data() {
    return {
      foodId: this.$route.params.id,
    };
  },

  computed: {
    food() {
      let fd = foods.find((food) => food.id === this.foodId);
      return {
        id: fd.id,
        name: fd.name,
        nutrition_facts_id: fd.nutrition_facts_id,
      };
    },
  },
};
</script>