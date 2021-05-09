<template>
  <div class="home w-100">
    <v-container>
      <v-row>
        <v-card-title class="primary border-radius w-100">
          <h3 class="menu-title">Menu</h3>
        </v-card-title>
        <v-tabs grow v-model="tab" class="border-sides">
          <v-tab class="justify-center d-flex border-half">
            <v-icon color="primary" class="mr-1">mdi-note-text</v-icon>
            <span>Recipes</span>
          </v-tab>
          <v-tab class="justify-center d-flex">
            <v-icon color="primary" class="mr-1">mdi-food</v-icon>
            <span>Ingredients</span>
          </v-tab>
        </v-tabs>
        <v-tabs-items v-model="tab">
          <v-tab-item>
            <Recipes :isTable="true" />
          </v-tab-item>
          <v-tab-item>
            <Foods :isTable="true" />
          </v-tab-item>
        </v-tabs-items>
      </v-row>
    </v-container>
  </div>
</template>


<script>
// @ is an alias to /src
import AnonymousRecipe from "@/views/Recipes/AnonymousRecipe.vue";
import RecommendationRecipe from "@/views/Recipes/RecommendationRecipe.vue";
import Recipes from "@/views/Recipes/Index.vue";
import Foods from "@/views/Foods/Index.vue";

export default {
  name: "Home",

  data() {
    return {
      tab: null,
    };
  },
  components: {
    AnonymousRecipe,
    RecommendationRecipe,
    Recipes,
    Foods,
  },
};
</script>

<style lang="scss" scoped>
.border {
  &-radius {
    border-top-left-radius: 4px;
    border-top-right-radius: 4px;
    border: 2px solid;
    border-color: $primary;
    border-bottom: none;
  }

  &-sides {
    &:first-child {
      background: tomato;
    }
    border-color: $primary;
  }
  &-half {
    border-right: 2px solid;
    border-color: $primary;
  }
}
.v-card {
  margin-top: 87px;
  .menu-title {
    padding-right: 100px;
  }
}

.v-window {
  width: 100%;
}
</style>