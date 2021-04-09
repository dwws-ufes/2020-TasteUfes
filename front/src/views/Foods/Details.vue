<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="6" d-flex justify-center>
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)"
            ><v-icon>mdi-chevron-left</v-icon> Back</span
          >
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
import { getFood } from "@/api/data";

export default {
  name: "DetailsFood",

  data() {
    return {
      foodId: this.$route.params.id,
      food: {},
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
          console.log(error);
        });
    },
  },
};
</script>