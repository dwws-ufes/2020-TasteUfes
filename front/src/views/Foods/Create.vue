<template>
  <v-card elevation="2" class="card-form">
    <!-- <Alert type="success" message="Sucesso internacional" /> -->
    <v-form
      ref="form"
      lazy-validation
      @submit.prevent="onSubmit"
      v-model="valid"
    >
      <h1>Create Food</h1>
      <div class="form-group">
        <v-text-field
          v-model="food.name"
          :rules="[this.rules.required]"
          label="Name"
          hide-details="auto"
          class="form-control"
        />

        <v-card-actions>
          <v-row justify="center">
            <v-btn
              class="submit"
              type="submit"
              elevation="2"
              color="primary"
              :disabled="!valid"
            >
              <span v-if="!submit"> Create </span>
              <v-progress-circular
                v-else
                indeterminate
                color="white"
              ></v-progress-circular>
            </v-btn>

            <v-btn elevation="2" @click="$router.go(-1)">Back</v-btn>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>

<script>
import { createFood } from "@/api/data";
// import Alert from "@/components/Alert.vue";

export default {
  name: "CreateFood",
  data() {
    return {
      valid: false,
      submit: false,
      food: {
        name: "",
      },
      rules: {
        required: (value) => !!value || "Required.",
      },
    };
  },

  components: {
    // Alert,
  },

  methods: {
    onSubmit: function () {
      this.submit = true;
      createFood(this.food)
        .then(() => {
          this.reset();
        })
        .catch((error) => {
          this.submit = false;
          console.log(error);
        });
    },

    reset: function () {
      this.$router.push({ name: "ListFood" });
    },
  },
};
</script>