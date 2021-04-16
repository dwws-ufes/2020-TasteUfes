<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="4" d-flex justify-center>
        <div class="d-flex" v-if="isAdmin">
          <router-link
            class="text-decoration-none d-flex"
            :to="{ name: 'ListFood' }"
          >
            <v-icon>mdi-chevron-left</v-icon> Back
          </router-link>
        </div>
        <v-card>
          <v-container class="primary">
            <v-card-title
              ><h1>{{ food.name }}</h1></v-card-title
            >
            <v-divider class="white" />
          </v-container>
          <NutritionFactsTable
            v-if="this.food.nutrition_facts"
            :data="this.food.nutrition_facts"
            :servings="this.food.nutrition_facts.serving_size"
          />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getFood } from "@/api";
import NutritionFactsTable from "@/components/details/NutritionFactsTable.vue";
import { mapGetters } from "vuex";

export default {
  name: "DetailsFood",

  data() {
    return {
      foodId: this.$route.params.id,
      food: {
        nutrition_facts: [],
      },
    };
  },

  computed: {
    ...mapGetters(["isAdmin", "auth"]),
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
          error.response.data.errors.map((error) => {
            this.$store.dispatch("setSnackbar", {
              text: `${error.message}`,
              color: "error",
            });
          });
        });
    },
  },

  components: {
    NutritionFactsTable,
  },
};
</script>

<style lang="scss" scoped>
.v-card {
  &__title {
    padding-bottom: 5;
  }
}
h1 {
  word-break: break-word;
  line-height: 1em;
}
</style>