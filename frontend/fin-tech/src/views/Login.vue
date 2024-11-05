<template>
  <v-row class="row-login">
    <v-col class="col-logo" sm="7">
      <v-img
        src="@/assets/FinTech-logo.png"
        alt="Logo"
        max-width="540"
        class="mx-auto mb-5"
      >
      </v-img>
    </v-col>
    <v-col
      class="col-login"
      sm="5"
      justify-content="center"
      aling-items="center">
      <v-col cols="12" sm="8" md="6" lg="9">
        <v-card class="pa-12 text-center">
          <v-card-title>
            <h2>Login</h2>
          </v-card-title>
          <v-card-text>
            <v-form v-model="valid">
              <v-text-field
                label="E-mail"
                v-model="usuario.email"
                prepend-inner-icon="mdi-account"
                autocomplete="off"
                required="true"
              ></v-text-field>
              <v-text-field
                label="Senha"
                v-model="usuario.senha"
                prepend-inner-icon="mdi-lock"
                type="password"
                autocomplete="off"
                required="true"
              ></v-text-field>
            </v-form>
          </v-card-text>

          <v-card-actions class="justify-center">
            <v-btn 
              value="Entrar" 
              color="primary" 
              @click="login"
              @keydown.enter="login" 
              :disabled="!valid"
              :callback="disparar">
              Entrar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-col>
  </v-row>
</template>

<script>
import Usuario from '../models/Usuario.js';
import usuarioService from '../services/usuario-service.js';
import LocalStorage from '../utils/storage.js';

export default {
  name: "TelaLogin",
  data() {
    return {
      usuario: new Usuario()
    };
  },
  mounted() {
    window.addEventListener('keydown', this.handleEnterKey);
  },
  beforeDestroy() {
    window.removeEventListener('keydown', this.handleEnterKey);
  },
  methods: {
    handleEnterKey(event) {
      if (event.key === 'Enter') {
        this.login();
      }
    },
    login() {

      usuarioService.login(this.usuario.email, this.usuario.senha)
      .then(response => {
        this.usuario = new Usuario(response.data.usuario);

        LocalStorage.salvarUsuarioNaStorage(this.usuario);
        LocalStorage.salvarTokenNaStorage(response.data.token);

        this.$router.push({name: 'FluxoCaixa'});
      })
      .catch(error => {
        console.log(error);
      })      
    },
    
  },
};
</script>

<style scoped>
.row-login {
  margin-right: 0;
  height: 100vh;
  width: 100vw;
}

.row-login {
  background-color: rgba(78, 192, 160, 0.2);
}

.col-logo,
.col-login {
  display: flex;
  align-items: center;
  justify-content: center;
}

.fill-height {
  height: 100%;
  padding: 10vh;
}

.v-card-title {
  color: #ffffff;
}

.v-card__title {
  justify-content: center;
}

.v-btn {
  width: 100%;
}
</style>
