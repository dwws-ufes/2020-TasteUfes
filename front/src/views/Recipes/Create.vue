<template>
  <v-form ref="form">
    <div class="form-group">
      <v-text-field
        label="Name"
        hide-details="auto"
        class="form-control"
        required
      />

      <v-text-field
        label="Servings"
        hide-details="auto"
        class="form-control"
        required
      />

      <v-btn elevation="2">Create</v-btn>

      <v-btn elevation="2" :to="{ name: 'Recipes' }">Back</v-btn>
    </div>
  </v-form>
</template>