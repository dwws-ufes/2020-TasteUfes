<template>
  <v-menu
    v-if="menu.show"
    transition="scroll-y-transition"
    open-on-hover
    bottom
    offset-y
  >
    <template v-slot:activator="{ on, attrs }">
      <v-btn text tile x-large dark v-bind="attrs" v-on="on">
        <v-list-item-icon v-if="menu.icon">
          <v-icon v-if="menu.icon" v-text="menu.icon"></v-icon>
        </v-list-item-icon>
        {{ menu.name }}
      </v-btn>
    </template>

    <v-list v-for="option in menu.options" :key="option.routeName">
      <div v-if="option.show">
        <v-list-item
          color="primary"
          link
          v-if="!option.action"
          :to="{ name: option.routeName, params: { id: option.param } }"
        >
          <v-list-item-title>{{ option.name }}</v-list-item-title>
        </v-list-item>
        <v-list-item link v-else @click="logout">
          <v-list-item-title>{{ option.name }}</v-list-item-title>
        </v-list-item>
      </div>
    </v-list>
  </v-menu>
</template>

<script>
import { mapActions } from "vuex";
export default {
  name: "Menu",
  data() {
    return {
      on: "",
      attrs: "",
    };
  },
  props: {
    fieldName: String,
    menu: {
      name: String,
      options: [
        {
          name: String,
          routeName: String,
        },
      ],
    },
  },
  methods: {
    ...mapActions(["doLogout"]),
    logout: function () {
      this.doLogout();
    },
  },
};
</script>

<style lang="scss" scoped>
.v-list {
  padding: 0;
  &-item__icon {
    margin-right: 10px !important;
  }
}
</style>