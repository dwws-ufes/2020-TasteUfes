<template>
  <div class="list">
    <v-btn elevation="2" :to="{ name: 'CreateRecipe' }">Create</v-btn>
    <v-data-table
      :headers="headers"
      :items="desserts"
      :items-per-page="10"
      class="elevation-1"
    />
  </div>
</template>