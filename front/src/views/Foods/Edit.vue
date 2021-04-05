<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form">
      <h1>Edit Food</h1>
      <div class="form-group">
        <v-text-field
          v-model="food.name"
          label="Name"
          hide-details="auto"
          class="form-control"
        />
      </div>
      <v-card-actions>
        <v-row justify="center">
          <v-btn elevation="2" color="primary" dark>Save</v-btn>

          <v-btn elevation="2" @click="$router.go(-1)">Back</v-btn>
        </v-row>
      </v-card-actions>
    </v-form>
  </v-card>
</template>

<script>
import foods from "@/assets/json/food.json";

export default {
  name: "EditFood",

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