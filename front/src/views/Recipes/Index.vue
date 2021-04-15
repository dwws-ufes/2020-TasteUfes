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
          :loading="load"
          loading-text="Loading... Please wait"
          :headers="headers"
          :items="recipeListTable"
          :items-per-page="10"
          class="elevation-1"
        >
          <template v-slot:item.name="{ item }">
            <v-row>
              <router-link
                class="text-decoration-none"
                :to="{ name: 'DetailsRecipe', params: { id: item.id } }"
              >
                {{ item.name }}
              </router-link>
            </v-row>
          </template>
          <template v-slot:item.actions="{ item }">
            <v-row>
              <DetailsButton :id="item.id" name="DetailsRecipe" />
              <EditButton :id="item.id" name="EditRecipe" />
              <DeleteButton
                :id="item.id"
                :name="item.name"
                @delete="deleteRecipe(item.id)"
              />
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
              xs="12"
              sm="6"
              lg="4"
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
                    <div class="my-2" v-if="recipe.preparation != null">
                      <b>Total steps:</b> {{ recipe.preparation.steps.length }}
                    </div>
                    <div class="my-2" v-if="recipe.preparation != null">
                      <b>Total time:</b>
                      {{ recipe.preparation.preparation_time }}
                    </div>
                    <div class="my-2">
                      <b>Author:</b> {{ recipe.user.first_name }}
                      {{ recipe.user.last_name }}
                    </div>
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
import { getRecipes, deleteRecipe } from "@/api";
import { mapGetters } from "vuex";
import EditButton from "@/components/buttons/EditButton.vue";
import DetailsButton from "@/components/buttons/DetailsButton.vue";
import DeleteButton from "@/components/buttons/DeleteButton.vue";

export default {
  data() {
    return {
      load: true,
      headers: [
        {
          text: "Nº",
          align: "start",
          value: "number",
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
          text: "Time",
          value: "preparation.preparation_time",
          class: "primary",
        },
        {
          text: "User",
          value: "user.first_name",
          class: "primary",
        },
        {
          text: "Actions",
          value: "actions",
          class: "primary",
        },
      ],
      recipeList: [],
      recipeListTable: [],
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

  computed: {
    ...mapGetters(["isAdmin", "getUserId"]),
  },

  methods: {
    deleteRecipe(id) {
      this.changeLoading();
      deleteRecipe(id)
        .then((result) => {
          console.log(result);
          let recipeId = this.recipeListTable.findIndex(
            (recipe) => recipe.id === id
          );
          this.recipeListTable.splice(recipeId, 1);
          this.changeLoading();
        })
        .catch((error) => {
          console.log(error.response);
        });
    },

    changeLoading() {
      this.load = !this.load;
    },

    getData() {
      if (this.$store.state.auth) {
        getRecipes()
          .then((recipes) => {
            this.recipeList = recipes.data;
            let auxList = recipes.data;
            if (!this.isAdmin) {
              auxList.map((recipe) => {
                recipe.user_id == this.getUserId
                  ? this.recipeListTable.push(recipe)
                  : "";
              });
            } else {
              this.recipeListTable = auxList;
            }
            this.recipeListTable.map((rec, index) => {
              rec.number = index + 1;
            });
            this.changeLoading();
          })
          .catch((error) => {
            console.log(error.response);
          });
      } else {
        getRecipes()
          .then((result) => {
            this.recipeList = result.data;
            this.changeLoading();
          })
          .catch((error) => {
            console.log(error.response);
          });
      }
    },
  },
};
</script>