<template>
  <v-menu transition="scroll-y-transition" open-on-hover bottom offset-y>
    <template v-slot:activator="{ on, attrs }">
      <v-btn text tile x-large dark v-bind="attrs" v-on="on">
        {{ menu.name }}
      </v-btn>
    </template>

    <v-list>
      <v-list-item v-for="option in menu.options" :key="option.routeName" link :to="{ name: option.routeName }">
        <v-list-item-title>{{ option.name }}</v-list-item-title>
      </v-list-item>
    </v-list>
  </v-menu>
</template>

<script>
export default {
  name: "Menu",
  data() {
    return {
      on: '',
      attrs: '',
    }
  },
  props: {
    fieldName: String,
    menu: {
      name: String,
      options: [{
        name: String,
        routeName: String,
      }],
    },
  },
};
</script>