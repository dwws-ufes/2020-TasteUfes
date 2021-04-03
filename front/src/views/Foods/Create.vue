<template>
  <v-card elevation="2" class="card-form">
    <v-form ref="form">
      <h1>Create Food</h1>
      <div class="form-group">
        <v-text-field
          label="Name"
          hide-details="auto"
          class="form-control"
          required
        />

        <v-card-actions>
          <v-row justify="center">
            <v-btn elevation="2" color="primary" dark>Create</v-btn>

            <v-btn elevation="2" :to="{ name: 'ListFood' }">Back</v-btn>
          </v-row>
        </v-card-actions>
      </div>
    </v-form>
  </v-card>
</template>