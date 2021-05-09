<template>
  <div class="nav">
    <v-app-bar dark color="primary" elevation="0">
      <v-toolbar-title class="text-no-wrap">
        <router-link
          @click.native="closeMenu()"
          class="text-decoration-none title-link d-flex align-center"
          :to="{ name: 'Home' }"
        >
          <v-icon>mdi-home</v-icon>
          <span class="title-name">TasteUfes</span>
        </router-link>
      </v-toolbar-title>
      <v-spacer />
      <div class="menu">
        <v-btn dark icon @click="toggleMenu()" v-if="this.$vuetify.breakpoint.xs">
          <div id="menu-hamburguer" class="menu-hamburguer">
            <div class="menu-hamburguer__span"></div>
          </div>
        </v-btn>
        <MenuOption v-for="menu in menuList" :key="menu.name" :menu="menu" v-else />
      </div>
        <div v-if="!auth && !this.$vuetify.breakpoint.xs">
          <v-btn outlined :to="{ name: 'CreateUser' }" class="mx-2"
            >Sign Up</v-btn
          >
        </div>
        <Login v-if="!auth && !this.$vuetify.breakpoint.xs" buttonColor="white" />
    </v-app-bar>
    <div id="menu" class="menu-mobile">
      <MenuMobile :menuList="menuList" @closeMenu="closeMenu()" />
    </div>
  </div>
</template>

<script>
import Login from "@/components/Login.vue";
import MenuOption from "@/components/MenuOption.vue";
import MenuMobile from "@/components/MenuMobile.vue";
import { mapGetters } from "vuex";

export default {
  name: "Toolbar",
  data() {
    return {
      menuToogle: false,
    }
  },

  computed: {
    ...mapGetters(["auth", "isAdmin", "getUserId"]),
    showMenu() {
      return this.isAdmin;
    },
    menuList() {
      return [
        {
          name: "Recipe",
          icon: null,
          show: true,
          isUser: false,
          options: [
            {
              name: "Create Recipe",
              routeName: "CreateRecipe",
              param: null,
              action: false,
              show: this.auth,
            },
            {
              name: "List Recipe",
              routeName: "ListRecipe",
              param: null,
              action: false,
              show: this.auth,
            },
            {
              name: "Create Anonymous Recipe",
              routeName: "AnonymousRecipe",
              param: null,
              action: false,
              show: true,
            },
            {
              name: "Recommendation Recipe",
              routeName: "RecommendationRecipe",
              param: null,
              action: false,
              show: true,
            },
          ],
        },
        {
          name: "Ingredient",
          icon: null,
          show: this.isAdmin,
          isUser: false,
          options: [
            {
              name: "Create Ingredient",
              routeName: "CreateFood",
              param: null,
              action: false,
              show: this.auth,
            },
            {
              name: "List Ingredient",
              routeName: "ListFood",
              param: null,
              action: false,
              show: this.auth,
            },
          ],
        },
        {
          name: this.$store.state.user.first_name,
          icon: "mdi-account-circle",
          show: this.auth,
          isUser: true,
          options: [
            {
              name: "My Account",
              routeName: "DetailsUser",
              param: this.getUserId,
              action: false,
              show: this.auth,
            },
            {
              name: "Create User",
              routeName: "CreateUser",
              param: null,
              action: false,
              show: this.isAdmin,
            },
            {
              name: "List User",
              routeName: "ListUser",
              param: null,
              action: false,
              show: this.isAdmin,
            },
            {
              name: "Logout",
              routeName: "Logout",
              param: null,
              action: true,
              show: this.auth,
            },
          ],
        },
      ];
    },
  },

  components: {
    Login,
    MenuOption,
    MenuMobile,
  },

  methods: {
    openMenuMobile() {
      if(this.menuToogle){
        document.querySelector('#menu-hamburguer').classList.add('open');
        document.querySelector('body').style.position = 'fixed';
        document.querySelector('#menu').style.top = "56px"
      } else {
        document.querySelector('#menu-hamburguer').classList.remove('open');
        document.querySelector('body').style.position = 'relative';
        document.querySelector('#menu').style.top = "-100vh";
      }
    },

    toggleMenu() {
      this.menuToogle = !this.menuToogle;
      this.openMenuMobile();
    },

    closeMenu() {
      if(this.$vuetify.breakpoint.xs){
        this.menuToogle = false;
        this.openMenuMobile();
      }
    }
  },
};
</script>

<style lang="scss" scoped>
header {
  z-index: 10;
}

.nav {
  color: #ffffff;
}

.title {
  &-link {
    color: #ffffff;
    font-size: 22px;
  }
}

.v-icon {
  font-size: 24px;
}

.menu {
  &-mobile {
    width: 100%;
    height: 94vh;
    background: $primary;
    top: -100vh;
    z-index: 1;
    transition: top .5s;
    position: absolute;
    overflow: hidden;
    overflow-y: scroll;
  }
}

.menu-hamburguer {
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 80px;
  height: 80px;
  cursor: pointer;
  transition: all .5s ease-in-out;
}
.menu-hamburguer__span {
  width: 20px;
  height: 2px;
  background: #fff;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(255,101,47,.2);
  transition: all .5s ease-in-out;
}
.menu-hamburguer__span::before,
.menu-hamburguer__span::after {
  content: '';
  display: flex;
  position: absolute;
  width: 20px;
  height: 2px;
  background: #fff;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(255,101,47,.2);
  transition: all .5s ease-in-out;
}
.menu-hamburguer__span::before {
  transform: translateY(-5px);
}
.menu-hamburguer__span::after {
  transform: translateY(5px);
}
/* ANIMATION */
.menu-hamburguer.open .menu-hamburguer__span {
  transform: translateX(50px);
  background: transparent;
  box-shadow: none;
}
.menu-hamburguer.open .menu-hamburguer__span::before {
  transform: rotate(45deg) translate(-35px, 35px);
}
.menu-hamburguer.open .menu-hamburguer__span::after {
  transform: rotate(-45deg) translate(-35px, -35px);
}

</style>