<template>
  <div class="list">
    <template v-if="!isTable">
      <v-row class="justify-space-between">
        <v-col>
          <h1>{{ $vuetify.lang.t('$vuetify.ingredient') }}</h1>
        </v-col>
        <v-col class="align-flex-end d-flex flex-column">
          <v-btn
            elevation="2"
            :to="{ name: 'CreateFood' }"
            color="primary"
            dark
          >
            <v-icon class="mr-1">mdi-food</v-icon>
            {{ $vuetify.lang.t('$vuetify.create') }}
          </v-btn>
          <v-btn
            v-if="!selected.length"
            outlined
            color="primary"
            @click="redirect"
          >
            {{ $vuetify.lang.t('$vuetify.generate') }} RDF
            <v-icon class="d-inline" small>mdi-open-in-new</v-icon>
          </v-btn>
          <v-btn v-else outlined color="primary" @click="download">
            {{ $vuetify.lang.t('$vuetify.download') }} RDF
          </v-btn>
        </v-col>
      </v-row>
      <v-row class="mb-2">
        <v-col>
          <v-text-field
            v-model="search"
            class="search"
            append-icon="mdi-magnify"
            :label="$vuetify.lang.t('$vuetify.search') + ' ' + $vuetify.lang.t('$vuetify.ingredient')"
            single-line
            hide-details
          ></v-text-field>
        </v-col>
      </v-row>
      <v-data-table
        v-model="selected"
        :loading="load"
        :loading-text="$vuetify.lang.t('$vuetify.loading_wait')"
        :headers="headers"
        :items="foodList"
        :items-per-page="10"
        :search="search"
        show-select
        class="elevation-1"
      >
        <template v-slot:item.name="{ item }">
          <v-row>
            <router-link
              class="text-decoration-none"
              :to="{ name: 'DetailsFood', params: { id: item.id } }"
              >{{ item.name }}</router-link
            >
          </v-row>
        </template>
        <template v-slot:item.serving_size="{ item }">
          <v-row v-if="item.nutrition_facts">
            {{ item.nutrition_facts.serving_size
            }}{{ getMeasureName(item.nutrition_facts.serving_size_unit) }}
          </v-row>
          <v-row v-else> - </v-row>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-row>
            <DetailsButton :id="item.id" name="DetailsFood" />
            <EditButton :id="item.id" name="EditFood" />
            <DeleteButton
              :id="item.id"
              :name="item.name"
              @delete="deleteFood(item.id, item.name)"
            />
          </v-row>
        </template>
      </v-data-table>
    </template>
    <template v-else>
      <div class="list">
        <div>
          <v-col class="pb-0">
            <h1>{{ $vuetify.lang.t('$vuetify.ingredient') }}</h1>
          </v-col>
          <v-container>
            <v-sheet v-if="loadSkeleton" :color="`grey lighten-4`" class="pa-3">
              <v-container>
                <v-skeleton-loader class="mx-auto mb-4" type="text" />
                <v-row>
                  <v-col
                    v-for="index in 9"
                    :key="index"
                    cols="12"
                    xs="12"
                    sm="6"
                    lg="4"
                  >
                    <v-skeleton-loader class="mx-auto" type="image" />
                  </v-col>
                </v-row>
              </v-container>
            </v-sheet>
            <v-alert
              v-else-if="foodList.length == 0"
              prominent
              dense
              text
              type="warning"
            >
              <v-card-text> {{ $vuetify.lang.t('$vuetify.no_ingredient') }}. </v-card-text>
            </v-alert>
            <div v-else>
              <v-row class="mb-2">
                <v-col>
                  <v-text-field
                    v-model="search"
                    class="search"
                    append-icon="mdi-magnify"
                    :label="$vuetify.lang.t('$vuetify.search') + ' ' + $vuetify.lang.t('$vuetify.ingredient')"
                    single-line
                    hide-details
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col
                  v-for="food in visibleFoods"
                  :key="food.name"
                  cols="12"
                  xs="12"
                  sm="6"
                  lg="4"
                >
                  <router-link
                    class="text-decoration-none title-link"
                    :to="{ name: 'DetailsFood', params: { id: food.id } }"
                  >
                    <v-card>
                      <v-card-title class="primary">{{
                        food.name
                      }}</v-card-title>
                      <div class="my-2">
                        <v-container>
                          <v-row v-if="food.nutrition_facts">
                            <v-col>
                              <b>{{ $vuetify.lang.t('$vuetify.serving_size') }}: </b
                              >{{ food.nutrition_facts.serving_size
                              }}{{
                                getMeasureName(
                                  food.nutrition_facts.serving_size_unit
                                )
                              }}
                            </v-col>
                          </v-row>
                          <v-row v-else>
                            <v-col> - </v-col>
                          </v-row>
                        </v-container>
                      </div>
                    </v-card>
                  </router-link>
                </v-col>
              </v-row>
              <v-pagination
                v-model="page"
                :length="Math.ceil(filterFood.length / perPage)"
              ></v-pagination>
            </div>
          </v-container>
        </div>
      </div>
    </template>
  </div>
</template>

<script>
import { getFoods, deleteFood, getRDFById } from "@/api";
import EditButton from "@/components/buttons/EditButton.vue";
import DetailsButton from "@/components/buttons/DetailsButton.vue";
import DeleteButton from "@/components/buttons/DeleteButton.vue";

export default {
  data() {
    return {
      search: "",
      page: 1,
      perPage: 12,
      load: true,
      loadSkeleton: true,
      selected: [],
      linkRDF: process.env.VUE_APP_API_URL + "foods/ld/rdf",
      headers: [
        {
          text: "Nº",
          align: "start",
          value: "number",
          class: "primary",
        },
        {
          text: this.$vuetify.lang.t('$vuetify.name'),
          value: "name",
          class: "primary",
        },
        {
          text: this.$vuetify.lang.t('$vuetify.serving_size'),
          value: "serving_size",
          class: "primary",
        },
        {
          text: this.$vuetify.lang.t('$vuetify.actions'),
          value: "actions",
          class: "primary",
        },
      ],
      foodList: [],
    };
  },

  computed: {
    visibleFoods() {
      return this.filterFood.slice(
        (this.page - 1) * this.perPage,
        this.page * this.perPage
      );
    },
    filterFood() {
      let search = this.search.toString().toLowerCase();
      return this.foodList.filter((food) =>
        Object.keys(food).some(() => food.name.toLowerCase().includes(search))
      );
    },
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
      this.$store.dispatch("ActionSetOverlay", false);
    }, 300);
  },

  mounted() {
    if(!this.isTable){
      document.querySelector("th").classList.add("primary");
    }
  },

  methods: {
    getData: function () {
      getFoods()
        .then((result) => {
          this.foodList = result.data;
          this.foodList.map((food, index) => {
            food.number = index + 1;
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
    },
    getMeasureName: function (id) {
      if (id > 0)
        return this.$store.state.ingredients_measures.find(
          (measure) => measure.id == id
        ).name;
    },

    deleteFood(id, name) {
      this.changeLoading();
      deleteFood(id)
        .then((result) => {
          this.$store.dispatch("setSnackbar", {
            text: `${this.$vuetify.lang.t('$vuetify.ingredient')} ${name} ${this.$vuetify.lang.t('$vuetify.deleted')}.`,
            color: "success",
          });
          let foodId = this.foodList.findIndex((food) => food.id === id);
          this.foodList.splice(foodId, 1);
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

    redirect() {
      window.open(this.linkRDF, "_blank");
    },

    download() {
      let ids = this.selected.map((ingredient) => {
        return ingredient.id;
      })
      getRDFById(ids)
        .then((response) => {
          var fileURL = window.URL.createObjectURL(
            new Blob([response.data])
          );
          var fileLink = document.createElement("a");
          fileLink.href = fileURL;
          fileLink.setAttribute("download", this.$vuetify.lang.t('$vuetify.ingredients') + ".rdf");
          document.body.appendChild(fileLink);
          fileLink.click();
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
};
</script>

<style lang="scss" scoped>
th:first-of-type {
  background-color: $primary;
}
</style>