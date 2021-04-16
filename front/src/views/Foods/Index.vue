<template>
  <div class="list">
    <template v-if="!isTable">
      <h1>Foods</h1>
      <v-btn elevation="2" :to="{ name: 'CreateFood' }" color="primary" dark>
        Create Food
      </v-btn>
      <v-data-table
        :loading="load"
        loading-text="Loading... Please wait"
        :headers="headers"
        :items="foodList"
        :items-per-page="10"
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
              @delete="deleteFood(item.id)"
            />
          </v-row>
        </template>
      </v-data-table>
    </template>
    <template v-else>
      <div class="list">
        <div>
          <v-col class="pb-0">
            <h1>Foods</h1>
          </v-col>
          <v-container>
            <v-sheet v-if="loadSkeleton" :color="`grey lighten-4`" class="pa-3">
              <v-container>
                <v-row>
                  <v-skeleton-loader class="mx-auto w-100" type="image" />
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
              <v-card-text> No food found. </v-card-text>
            </v-alert>
            <v-row v-else>
              <v-col
                v-for="food in foodList"
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
                    <v-card-title class="primary">{{ food.name }}</v-card-title>
                    <div class="my-2">
                      <v-container>
                        <v-row v-if="food.nutrition_facts">
                          <v-col>
                            <b>Serving Size: </b
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
          </v-container>
        </div>
      </div>
    </template>
  </div>
</template>

<script>
import { getFoods, deleteFood } from "@/api";
import EditButton from "@/components/buttons/EditButton.vue";
import DetailsButton from "@/components/buttons/DetailsButton.vue";
import DeleteButton from "@/components/buttons/DeleteButton.vue";

export default {
  data() {
    return {
      load: true,
      loadSkeleton: true,
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
          text: "Serving Size",
          value: "serving_size",
          class: "primary",
        },
        {
          text: "Actions",
          value: "actions",
          class: "primary",
        },
      ],
      foodList: [],
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
          console.log(error.response);
        });
    },
    getMeasureName: function (id) {
      if (id > 0)
        return this.$store.state.ingredients_measures.find(
          (measure) => measure.id == id
        ).name;
    },

    deleteFood(id) {
      this.changeLoading();
      deleteFood(id)
        .then((result) => {
          console.log(result);
          let foodId = this.foodList.findIndex((food) => food.id === id);
          this.foodList.splice(foodId, 1);
          this.changeLoading();
        })
        .catch((error) => {
          console.log(error.response);
        });
    },

    changeLoading() {
      this.load = !this.load;
    },
  },
};
</script>