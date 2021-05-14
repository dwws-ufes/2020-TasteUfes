<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-card elevation="0" color="white">
          <v-container>
            <div v-if="auth">
              <v-btn fab small elevation="0" color="primary button-logout" class="mx-2" @click="logout">
                <v-icon>mdi-logout</v-icon>
              </v-btn>
              <router-link
                class="text-decoration-none title-link"
                :to="{ name: 'DetailsUser', params: { id: user.id } }"
              >
                <v-container @click="$emit('closeMenu')">
                  <div>
                    <v-row>
                      <v-col class="d-flex justify-center pb-0">
                        <v-avatar color="primary" size="64">
                          <span class="white--text headline">
                            {{
                              new String(user.first_name)
                                .charAt(0)
                                .toUpperCase()
                            }}{{
                              new String(user.last_name).charAt(0).toUpperCase()
                            }}
                          </span>
                        </v-avatar>
                      </v-col>
                    </v-row>
                    <v-row>
                      <v-col class="d-flex justify-center">
                        <span>{{ user.first_name }} {{ user.last_name }}</span>
                      </v-col>
                    </v-row>
                  </div>
                </v-container>
              </router-link>
            </div>
            <div v-else class="d-flex justify-center">
              <div v-if="!auth">
                <v-btn
                  outlined
                  color="primary"
                  :to="{ name: 'CreateUser' }"
                  class="mx-2"
                  @click="$emit('closeMenu')"
                  >{{ $vuetify.lang.t('$vuetify.sign_up') }}</v-btn
                >
              </div>
              <Login
                v-if="!auth"
                @closeMenu="closeMenu()"
                buttonColor="primary"
              />
            </div>
            <div v-for="menu in menuList" :key="menu.name">
              <v-card
                v-if="menu.show"
                class="mx-auto py-3 px-3 my-3 border-primary"
                elevation="0"
                outlined
              >
                <v-card-title
                  v-if="menu.isUser"
                  class="d-flex justify-center pt-0"
                >
                  {{ $vuetify.lang.t('$vuetify.user') }}
                </v-card-title>
                <v-card-title v-else class="d-flex justify-center pt-0">
                  {{ menu.name }}
                </v-card-title>
                <div v-for="option in menu.options" :key="option.routeName">
                  <div v-if="option.show">
                    <router-link
                      class="text-decoration-none title-link"
                      :to="{
                        name: option.routeName,
                        params: { id: option.param },
                      }"
                    >
                        <div
                          v-if="!option.action"
                          @click="$emit('closeMenu')"
                          class="my-3 mx-3 px-0 pb-0 pt-3 menu-option"
                        >
                          <span>{{ option.name }}</span>
                        </div>
                    </router-link>
                  </div>
                </div>
              </v-card>
            </div>
          </v-container>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import Login from "@/components/Login.vue";
import { mapActions, mapGetters } from "vuex";
export default {
  name: "MenuMobile",

  components: {
    Login,
  },

  computed: {
    ...mapGetters(["auth"]),
    user() {
      return this.$store.state.user;
    },
  },

  props: {
    fieldName: String,
    menuList: Array,
  },

  methods: {
    ...mapActions(["doLogout"]),
    logout: function () {
      this.$emit("closeMenu");
      this.doLogout();
    },
    closeMenu() {
      this.$emit("closeMenu");
    },
  },
};
</script>

<style lang="scss" scoped>
.menu-option {
  border-top: 1px solid;
  border-color: $primary;
  span {
    width: 100%;
  }
}

.button-logout {
  right: 0;
  position: absolute;
}

.v-btn--fab.v-size--small .v-icon {
      height: 18px;
    font-size: 18px;
    width: 18px;
}
</style>