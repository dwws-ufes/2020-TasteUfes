<template>
  <div class="home w-100">
    <v-container>
      <v-row >
        <v-tabs vertical hide-slider>
          <v-col>
          <v-card>
            <v-list class="py-0">
              <v-card-title class="primary border-radius">
                <h3 class="menu-title">Menu</h3>
              </v-card-title>
              <v-list-item-group>
                <v-tab class="justify-start d-flex">
                  <v-icon color="primary" class="mr-1">mdi-note-text</v-icon>
                  <span>Recipes</span>
                </v-tab>
                <v-tab class="justify-start d-flex">
                  <v-icon color="primary" class="mr-1">mdi-food</v-icon>
                  <span>Foods</span>
                </v-tab>
              </v-list-item-group>
            </v-list>
          </v-card>
          </v-col>
          <v-tab-item>
            <Recipes :isTable="true" />
          </v-tab-item>
          <v-tab-item>
            <Foods :isTable="true" />
          </v-tab-item>
        </v-tabs>
      </v-row>
    </v-container>
  </div>
</template>

<script>
// @ is an alias to /src
import Recipes from "@/views/Recipes/Index.vue";
import Foods from "@/views/Foods/Index.vue";
import LateralMenu from "@/components/LateralMenu.vue";

export default {
  name: "Home",
  components: {
    Recipes,
    Foods,
    LateralMenu,
  },
};
</script>

<style lang="scss" scoped>
.v-card {
  margin-top: 87px;
  .border-radius {
    border-top-left-radius: 4px;
    border-top-right-radius: 4px;
  }
  .menu-title {
    padding-right: 100px;
  }
}
</style>