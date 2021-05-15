<template>
  <v-container class="details">
    <v-row justify="center">
      <v-col cols="12" sm="8" xs="12" d-flex justify-center>
        <div class="d-flex">
          <span class="back-btn" @click="$router.go(-1)"
            ><v-icon>mdi-chevron-left</v-icon> {{ $vuetify.lang.t('$vuetify.back') }}</span
          >
        </div>
        <v-card>
          <UserCard :user="this.user" :username="this.user.username" />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getUser } from "@/api";
import UserCard from "@/components/UserCard.vue";

export default {
  name: "DetailsUser",

  data() {
    return {
      userId: this.$route.params.id,
      user: [],
    };
  },

  created: function () {
    this.getData();
  },

  methods: {
    getData: function () {
      getUser(this.userId)
        .then((result) => {
          this.user = result.data;
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
    UserCard,
  },
};
</script>