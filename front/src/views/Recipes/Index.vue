<template>
  <div class="recipe">
    <template v-if="!isTable">
      <div class="list">
        <h1>Recipes</h1>
        <v-btn
          elevation="2"
          :to="{ name: 'CreateRecipe' }"
          color="primary"
          dark
        >
          Create Recipe
        </v-btn>
        <v-data-table
          :headers="headers"
          :items="recipeList"
          :items-per-page="10"
          class="elevation-1"
        >
          <template v-slot:item.actions="{ item }">
            <v-row>
              <DetailsButton :id="item.id" name="DetailsRecipe" />
              <EditButton :id="item.id" name="EditRecipe" />
              <DeleteButton :id="item.id" :name="item.name" />
            </v-row>
          </template>
        </v-data-table>
      </div>
    </template>

    <template v-else>
      <div class="list">
        <v-col class="pb-0">
          <h1>Recipes</h1>
        </v-col>
        <v-container>
          <v-row>
            <v-col
              v-for="recipe in recipeList"
              :key="recipe.name"
              cols="12"
              sm="4"
            >
              <router-link
                class="text-decoration-none title-link"
                :to="{ name: 'DetailsRecipe', params: { id: recipe.id } }"
              >
                <v-card>
                  <v-card-title>{{ recipe.name }}</v-card-title>
                  <v-divider class="mx-4"></v-divider>
                  <v-card-text>
                    <div class="my-2">
                      <b>Servings:</b> {{ recipe.servings }}
                    </div>
                    <div class="my-2">
                      <b>Total steps:</b> {{ recipe.steps }}
                    </div>
                    <div class="my-2"><b>Total time:</b> {{ recipe.time }}</div>
                  </v-card-text>
                </v-card>
              </router-link>
            </v-col>
          </v-row>
        </v-container>
      </div>
    </template>
  </div>
</template>

<script>
import { getRecipes } from "@/api/data";
import EditButton from "@/components/buttons/EditButton.vue";
import DetailsButton from "@/components/buttons/DetailsButton.vue";
import DeleteButton from "@/components/buttons/DeleteButton.vue";

export default {
  data() {
    return {
      headers: [
        {
          text: "ID",
          align: "start",
          sortable: false,
          value: "id",
          class: "primary",
        },
        {
          text: "Name",
          value: "name",
          class: "primary",
        },
        {
          text: "Servings",
          value: "servings",
          class: "primary",
        },
        {
          text: "Steps",
          value: "steps",
          class: "primary",
        },
        {
          text: "Time",
          value: "time",
          class: "primary",
        },
        {
          text: "Actions",
          value: "actions",
          class: "primary",
        },
      ],
      recipeList: [],
    };
  },

  props: {
    isTable: Boolean,
  },

  components: {
    EditButton,
    DetailsButton,
    DeleteButton,
  },

  created: function () {
    this.getData();
  },

  methods: {
    getData: function () {
      const response = getRecipes();
      response.then((result) => {
        this.recipeList = result.data;
      });
    },
  },
};
</script>