<template>
  <div>
    <v-app-bar color="#295255" dark>
      <v-app-bar-nav-icon @click="drawer = true"></v-app-bar-nav-icon>
      <v-row no-gutters>
        <v-col cols="12">
          <img src="../assets/default-monochrome-white.svg" height="30" />
        </v-col>
      </v-row>
      <v-icon @click="logOff">mdi-exit-to-app</v-icon>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer" absolute temporary>
      <v-list nav dense>
        <v-list-item-group v-model="group" active-class="text--accent-4">
          <v-list-item @click="navegarParaFluxoCaixa">
            <v-list-item-icon>
              <v-icon>mdi-chart-box-multiple-outline</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Fluxo de Caixa</v-list-item-title>
          </v-list-item>
          <v-list-item @click="navegarParaAReceber">
            <v-list-item-icon>
              <v-icon>mdi-cash-multiple</v-icon>
            </v-list-item-icon>
            <v-list-item-title>A Receber</v-list-item-title>
          </v-list-item>
          <v-list-item @click="navegarParaAPagar">
            <v-list-item-icon>
              <v-icon>mdi-cash-minus</v-icon>
            </v-list-item-icon>
            <v-list-item-title>A Pagar</v-list-item-title>
          </v-list-item>
          <v-list-item @click="navegarParaPessoas">
            <v-list-item-icon>
              <v-icon>mdi-account-multiple</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Pessoas</v-list-item-title>
          </v-list-item>
          <v-list-item @click="navegarParaNaturezaLancamento">
            <v-list-item-icon>
              <v-icon>mdi-account-file</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Natureza Lançamento</v-list-item-title>
          </v-list-item>
          <v-list-item @click="navegarParaUsuario">
            <v-list-item-icon>
              <v-icon>mdi-account-multiple-plus</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Usuário</v-list-item-title>
          </v-list-item>

        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  name: "MenuGeral",

  data: () => ({
    drawer: false,
    group: null,
  }),

  methods: {
    navegarParaFluxoCaixa() {
      this.$router.push({ name: "FluxoCaixa" });
    },
    navegarParaAReceber() {
      this.$router.push({ name: "ControleAReceber" });
    },
    navegarParaAPagar() {
      this.$router.push({ name: "ControleAPagar" });
    },
    navegarParaPessoas() {
      this.$router.push({ name: "ControleDePessoas" });
    },
    navegarParaNaturezaLancamento() {
      this.$router.push({ name: "ControleNaturezaLancamento" });
    },
    navegarParaUsuario() {
      this.$router.push({ name: "ControleUsuario" });
    },
    logOff() {
      localStorage.clear();
      this.$router.push({ name: "Login" });
    },
  },
};
</script>

<style scoped>
.v-list-item-group .v-icon {
  font-size: 35px;
}
</style>
