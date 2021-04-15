<template>
  <div class="home">
    <v-container>
      <v-row>
        <v-col cols="12" sm="4">
          <LateralMenu />
        </v-col>
        <v-col cols="12" sm="8">
          <Recipes :isTable="true" />
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
// @ is an alias to /src
import Recipes from "@/views/Recipes/Index.vue";
import LateralMenu from "@/components/LateralMenu.vue";

export default {
  name: "Home",
  components: {
    Recipes,
    LateralMenu,
  },
};
</script>
