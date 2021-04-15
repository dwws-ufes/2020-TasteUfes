<template>
  <v-card class="mx-auto" max-width="500">
    <v-list>
      <v-list-item-group v-model="model">
        <v-list-item v-for="(item, i) in items" :key="i">
          <v-list-item-icon>
            <v-icon color="primary" v-text="item.icon"></v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="item.text"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
  </v-card>
</template>

<script>
export default {
  data: () => ({
    items: [
      {
        icon: "mdi-note-text",
        text: "Recipes",
      },
      {
        icon: "mdi-food",
        text: "Foods",
      },
    ],
    model: 1,
  }),
};
</script>

<style lang="scss" scoped>
.v-card {
  margin-top: 100px;
}
</style>