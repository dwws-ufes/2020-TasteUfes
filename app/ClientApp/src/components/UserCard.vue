<template>
  <v-container>
    <v-card-title>
      <v-row>
        <v-col cols="12" class="d-flex justify-center">
          <v-avatar color="primary" size="64">
            <span class="white--text headline">
              {{ new String(user.first_name).charAt(0).toUpperCase()
              }}{{ new String(user.last_name).charAt(0).toUpperCase() }}
            </span>
          </v-avatar>
        </v-col>
        <v-col cols="12" class="d-flex justify-center">
          <span>{{ user.first_name }} {{ user.last_name }}</span>
        </v-col>
        <v-card-text class="d-flex justify-center pt-0">
          <v-row v-if="user.roles">
            <v-col
              v-if="user.roles.length > 0"
              cols="12"
              class="d-flex justify-center pb-0"
            >
              <span
                ><b>{{ user.roles[0].name }}</b></span
              >
            </v-col>
            <v-col v-else cols="12" class="d-flex justify-center pb-0">
              <span><b>{{ $vuetify.lang.t('$vuetify.user') }}</b></span>
            </v-col>
            <v-col cols="12" class="d-flex justify-center pb-0">
              <span>{{ user.email }}</span>
            </v-col>
            <v-col
              v-if="username != null"
              cols="12"
              class="d-flex justify-center"
            >
              <span>{{ $vuetify.lang.t('$vuetify.username') }}: {{ user.username }}</span>
            </v-col>
          </v-row>
        </v-card-text>
      </v-row>
    </v-card-title>
    <v-card-actions>
    <v-row v-if="user.id == getUserId && username != null">
      <v-col class="d-flex justify-center">
        <v-btn class="mr-5" outlined :to="{ name: 'UpdatePassword'}"><v-icon small class="mr-2" color="primary">mdi-key</v-icon>{{ $vuetify.lang.t('$vuetify.update') }}</v-btn>
        <v-btn class="primary" :to="{ name: 'EditUser', params: { id: user.id } }"><v-icon small class="mr-2">mdi-pencil</v-icon>{{ $vuetify.lang.t('$vuetify.edit') }}</v-btn>
      </v-col>
    </v-row>

    </v-card-actions>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
  props: {
    user: {
      roles: [],
    },
    username: String,
  },
  computed: {
    ...mapGetters(['getUserId']),
  },
};
</script>

<style>
</style>