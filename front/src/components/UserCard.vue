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
              <span><b>User</b></span>
            </v-col>
            <v-col cols="12" class="d-flex justify-center pb-0">
              <span>{{ user.email }}</span>
            </v-col>
            <v-col
              v-if="username != null"
              cols="12"
              class="d-flex justify-center"
            >
              <span>Username: {{ user.username }}</span>
            </v-col>
          </v-row>
        </v-card-text>
      </v-row>
    </v-card-title>
    <v-card-actions>
    <v-row>
      <v-col class="d-flex justify-flex-end">
        <v-btn class="primary" :to="{ name: 'EditUser', params: { id: user.id } }"><v-icon small class="mr-2">mdi-pencil</v-icon> Edit</v-btn>
      </v-col>
    </v-row>

    </v-card-actions>
  </v-container>
</template>

<script>
export default {
  props: {
    user: {
      roles: [],
    },
    username: String,
  },
};
</script>

<style>
</style>