<template>
  <v-container fluid>
    <!-- Mensagem de sucesso./ -->
    <v-snackbar v-model="snackbar" :color="color">
      {{ messagem }}
      <v-btn dark text absolute @click="snackbarAlter(false)"> 
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </v-snackbar> 

    
    <v-row>
      <!-- Título da pagina./ -->
      <v-col cols="12" sm="12" md="12">
        <h2 class="titulo">Pessoa</h2>
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
                    v-model="editedItem.nome"
                    label="Nome*"
                  ></v-text-field>
                </v-col>
                  
                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.telefone"
                    label="Telefone"
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
    
    <!-- Tabela de pessoas./ -->
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <v-data-table
          :headers="headers"
          :items="pessoas"
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
import Pessoa from "../models/Pessoa.js";
import pessoaService from "../services/pessoa-service.js";
import moment from "moment";

export default {
  name: "ControleDePessoas",

  components: {},

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
          text: "Controle de pessoas",
          disabled: true,
          href: "pessoas",
        },
      ],
      pessoas: [],
      dialog: false,
      editedIndex: -1,
      editedItem: {
        nome: "",
        telefone: "",
        //email: "",
      },

      headers: [
        {
          text: "Id",
          align: "start",
          sortable: true,
          value: "id",
        },
        { text: "Nome", value: "nome" },
        { text: "Telefone", value: "telefone" },
        //{ text: "E-mail", value: "email" },
        { text: "Data Cadastro", value: "dataCadastro" },
        { text: "Data Exclusão", value: "dataInativacao" },
        { text: "Ações", value: "actions", sortable: false },
      ],
    };
  },

  mounted() {
    this.obterPessoas();
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
    obterPessoas() {
      pessoaService
        .obterTodos()
        .then((response) => {
          this.pessoas = response.data.map((p) => new Pessoa(p));
        })
        .catch((error) => {
          console.log(error);
        });
    },

    gravar() {
      if (this.editedIndex > -1) {
        pessoaService
          .atualizar(this.editedItem)
          .then(() => {
            Object.assign(this.pessoas[this.editedIndex], this.editedItem);
            this.snackbar = true;
            this.messagem = "Pessoa editada com sucesso!";
            this.color = "success";
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        pessoaService
          .cadastrar(this.editedItem)
          .then((response) => {
            this.snackbar = true;
            this.pessoas.push(response.data);
            this.messagem = "Pessoa cadastrada com sucesso!";
            this.color = "success";
            this.close();
          })
          .catch((error) => {
            console.log(error);
          });
      }
      this.close();
    },

    editItem(item) {
      this.editedIndex = this.pessoas.indexOf(item);
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
      const index = this.pessoas.indexOf(item);
      confirm("Deseja excluir está pessoa?") &&
        this.pessoas.splice(index, 1);

      pessoaService
        .deletar(item.id)
        .then(() => {
          this.snackbar = true;
          this.messagem = "Pessoa excluída com sucesso!";
          this.color = "success";
          this.close();
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
