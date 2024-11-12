<template>
  <v-container fluid>
    <MensagemSucesso ref="successMessage" :message="messagem"/>
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <h2 class="titulo">Cadastro de Usuários</h2>
        <v-divider></v-divider>
      </v-col>
      <!-- Tela de formulário do cadastro / edição./ -->
      <v-dialog
      v-model="dialog"
      persistent
      max-width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn 
            id="btn-cadastrar" 
            dark 
            class="mb-2" 
            v-bind="attrs"
            v-on="on">
            Cadastrar
          </v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="text-h5">{{ formTitulo }}</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.email"
                    label="E-mail*"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    type="password"
                    v-model="editedItem.senha"
                    label="Senha*"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>*Indica campos obrigatórios</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="var(--cor-secundaria)"
              text
              @click="dialog = false"
            >
              Fechar
            </v-btn>
            <v-btn
              color="var(--cor-secundaria)"
              text
              @click="gravar"
            >
              Gravar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>
    <!-- lista de usuarios./ -->
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <v-data-table
          :headers="headers"
          :items="usuarios"
          :items-per-page="5"
          class="elevation-1">
          <template #[`item.dataCadastro`]="{ item: { dataCadastro } }">
            {{ dataCadastro | dataFormatada }}
          </template>
          <template #[`item.dataInativacao`]="{ item: { dataInativacao } }">
            {{ dataInativacao | dataFormatada }}
          </template>
          <template #[`item.actions`]="{ item }">
            <v-icon small class="mr-2" @click="editItem(item)">
              mdi-pencil
            </v-icon>
            <v-icon small @click="deleteItem(item)">
              mdi-delete
            </v-icon>
          </template>
        </v-data-table>
        <template v-slot:[`item.actions`]="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">
            mdi-pencil
          </v-icon>
          <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
        </template>
      </v-col>
    </v-row>  
  </v-container>
</template>

<script>
import Usuario from "../models/Usuario.js";
import usuarioService from "../services/usuario-service.js";
import moment from "moment";
import MensagemSucesso from "../components/alerts/MensagemSucesso.vue";

export default {
  name: "ControleUsuarios",

  components: {
    MensagemSucesso
  },

  filters: {
    dataFormatada(data) {
      if (!data) return '';
      return moment(data).format("DD/MM/YYYY");
    }
  },

  data() {
    return {
      items: [
        {
          text: "Controle de usuarios",
          disabled: true,
          href: "usuarios",
        },
      ],
      usuarios: [],
      dialog: false,
      editedIndex: -1,
      editedItem: {
        email: "",
        senha: ""
      },
      headers: [
        {
          text: "Id",
          align: "start",
          sortable: true,
          value: "id",
        },
        { text: "E-mail", value: "email" },
        { text: "Data Cadastro", value: "dataCadastro" },
        { text: "Data Exclusão", value: "dataInativacao" },
        { text: "Ações", value: "actions", sortable: false },
      ],
      messagem: "",
    };
  },

  mounted() {
    this.obterUsuarios();
  },

  computed: {
    formTitulo() {
      return this.editedIndex === -1 ? "Cadastro" : "Edição";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  methods: {
    obterUsuarios() {
      usuarioService
        .obterTodos()
        .then((response) => {
          this.usuarios = response.data.map((p) => new Usuario(p));
        })
        .catch((error) => {
          console.log(error);
        });
    },

    gravar() {
      if (this.editedIndex > -1) {
        usuarioService
          .atualizar(this.editedItem)
          .then(() => {
            Object.assign(this.usuarios[this.editedIndex], this.editedItem);
            this.snackbar = true;
            this.messagem = "Usuario editado com sucesso!";
            this.$refs.successMessage.show();
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        usuarioService
          .cadastrar(this.editedItem)
          .then((response) => {
            this.snackbar = true;
            this.usuarios.push(response.data);
            this.messagem = "Usuario cadastrado com sucesso!";
            this.$refs.successMessage.show();
          })
          .catch((error) => {
            console.log(error);
          });
      }
      this.close();
    },

    editItem(item) {
      this.editedIndex = this.usuarios.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },

    snackbarAlter(bool){
      this.snackbar = bool;
    },

    deleteItem(item) {
      const index = this.usuarios.indexOf(item);
      confirm("Deseja excluir este usuario?") &&
        this.usuarios.splice(index, 1);

      usuarioService
        .deletar(item.id)
        .then(() => {
          this.snackbar = true;
          this.messagem = "Usuario excluído com sucesso!";
          this.$refs.successMessage.show();
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>

<style scoped>
  #btn-cadastrar {
    background-color: var(--cor-primaria);
    margin-left: 12px;
  }
</style>
