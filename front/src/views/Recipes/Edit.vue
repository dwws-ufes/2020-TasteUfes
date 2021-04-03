<template>
  <v-form ref="form">
    <div class="form-group">
      <h1>Edit Recipe</h1>
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

      <v-btn elevation="2">Save</v-btn>

      <v-btn elevation="2">Back</v-btn>
    </div>
  </v-form>
</template>