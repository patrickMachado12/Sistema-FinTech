<template>
  <v-app>
    <Menu v-if="this.$router.currentRoute.name != 'Login'"></Menu>

    <v-main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-main>
  </v-app>
</template>
<script>
import Menu from "../src/components/Menu.vue";

export default {
  name: "App",

  components: {
    Menu,
  },

  data: () => ({}),
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Quicksand:wght@300;400;500;600;700&display=swap");

:root {
  --cor-primaria: #295255;
  --cor-secundaria: #577877;
  --cor-terciaria: #162623;
  --cor-quartenaria: #F0F5F7;
}

body {
  margin: 0;
  padding: 0;
  font-family: "Quicksand", sans-serif;
}

.titulo {
  font-weight: 600;
  color: #162623;
  font-size: 25px;
  margin-top: 10px;
}

.btn-primary,
.btn-primary:active
.btn-primary:focus,
.btn-primary:focus-visible,
.btn-primary:visited,
.btn-primary:not(:disabled):not(.disabled):active{
  color: #fff;
  background-color: var(--cor-primaria);
  border: var(--cor-primaria);
  outline: none;
  box-shadow: none;
  min-width: 90px;
}

.btn-primary:hover{
  color: #fff;
  background-color: var(--cor-secundaria);
  border-color: var(--cor-secundaria);
}
</style>
