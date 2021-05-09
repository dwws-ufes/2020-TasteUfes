<template>
  <div class="recipe">
    <template v-if="!isTable">
      <div class="list">
        <v-row class="justify-space-between">
          <v-col>
            <h1>Recipes</h1>
          </v-col>
          <v-col class="justify-flex-end d-flex">
            <v-btn
              elevation="2"
              :to="{ name: 'CreateRecipe' }"
              color="primary"
              dark
            >
              <v-icon class="mr-1">mdi-note-text</v-icon>
              Create
            </v-btn>
          </v-col>
        </v-row>
        <v-row class="mb-2">
          <v-col>
            <v-text-field
              v-model="search"
              class="search"
              append-icon="mdi-magnify"
              label="Search Recipe"
              single-line
              hide-details
            ></v-text-field>
          </v-col>
        </v-row>
        <v-data-table
          :loading="load"
          loading-text="Loading... Please wait"
          :headers="headers"
          :items="recipeListTable"
          :items-per-page="10"
          :search="search"
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
                @delete="deleteRecipe(item.id, item.name)"
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
          <v-sheet v-if="loadSkeleton" :color="`grey lighten-4`" class="pa-3">
            <v-container>
                <v-skeleton-loader class="mx-auto mb-4" type="text" />
              <v-row>
                <v-col v-for="index in 9" :key="index" 
                cols="12" xs="12" sm="6" lg="4">
                  <v-skeleton-loader class="mx-auto" type="image" />
                </v-col>
              </v-row>
            </v-container>
          </v-sheet>
          <v-alert
            v-else-if="recipeList.length == 0"
            prominent
            dense
            text
            type="warning"
          >
            <v-card-text> No recipe found. </v-card-text>
          </v-alert>
          <div v-else>
            <v-row class="mb-2">
              <v-col>
                <v-text-field
                  v-model="search"
                  class="search"
                  append-icon="mdi-magnify"
                  label="Search Recipe"
                  single-line
                  hide-details
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row class="mb-5 min-height">
              <v-col
                v-for="recipe in visibleRecipes"
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
                    <v-card-title class="primary">{{
                      recipe.name
                    }}</v-card-title>
                    <v-container>
                      <v-row>
                        <v-col>
                          <div class="my-2">
                            <b>Servings:</b> {{ recipe.servings }}
                          </div>
                          <div class="my-2" v-if="recipe.preparation != null">
                            <b>Preparation Time:</b>
                            {{ recipe.preparation.preparation_time }}
                          </div>
                          <div class="my-2">
                            <b>Author:</b> {{ recipe.user.first_name }}
                            {{ recipe.user.last_name }}
                          </div>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card>
                </router-link>
              </v-col>
            </v-row>
            <v-pagination
              v-model="page"
              :length="Math.ceil(filterRecipe.length / perPage)"
            ></v-pagination>
          </div>
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
      page: 1,
      perPage: 9,
      load: true,
      loadSkeleton: true,
      search: "",
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
    setTimeout(() => {
      this.loadSkeleton = false;
    }, 300);
  },

  computed: {
    visibleRecipes() {
      return this.filterRecipe.slice(
        (this.page - 1) * this.perPage,
        this.page * this.perPage
      );
    },
    ...mapGetters(["isAdmin", "getUserId"]),
    filterRecipe() {
      let search = this.search.toString().toLowerCase();
      return this.recipeList.filter((recipe) =>
        Object.keys(recipe).some(() =>
          recipe.name.toLowerCase().includes(search)
        )
      );
    },
  },

  methods: {
    deleteRecipe(id, name) {
      this.changeLoading();
      deleteRecipe(id)
        .then((result) => {
          let recipeId = this.recipeListTable.findIndex(
            (recipe) => recipe.id === id
          );
          this.$store.dispatch("setSnackbar", {
            text: `Recipe ${name} deleted.`,
            color: "success",
          });
          this.recipeListTable.splice(recipeId, 1);
          this.changeLoading();
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
            error.response.data.errors.map((error) => {
              this.$store.dispatch("setSnackbar", {
                text: `${error.message}`,
                color: "error",
              });
            });
          });
      } else {
        getRecipes()
          .then((result) => {
            this.recipeList = result.data;
            this.changeLoading();
          })
          .catch((error) => {
            error.response.data.errors.map((error) => {
              this.$store.dispatch("setSnackbar", {
                text: `${error.message}`,
                color: "error",
              });
            });
          });
      }
    },
  },
};
</script>