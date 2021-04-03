<template>
  <v-form ref="form">
    <h1>Create Food</h1>
    <div class="form-group">
      <v-text-field
        label="Name"
        hide-details="auto"
        class="form-control"
        required
      />

      <v-btn elevation="2">Create</v-btn>

      <v-btn elevation="2" :to="{ name: 'ListFood' }">Back</v-btn>
    </div>
  </v-form>
</template>