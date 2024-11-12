<template>
  <v-container fluid>
    <MensagemSucesso ref="successMessage" :message="messagem"/> 
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <h2 class="titulo">Natureza de Lançamento</h2>
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
                    v-model="editedItem.descricao"
                    label="Descrição*"
                    :rules="[descricaoError]"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.observacao"
                    label="Observação"
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
    <!-- lista de naturezas./ -->
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <v-data-table
          :headers="headers"
          :items="naturezas"
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
import NaturezaLancamento from "../models/NaturezaLancamento.js";
import naturezaLancamentoService from "../services/natureza-lancamento-service.js";
import moment from "moment";
import MensagemSucesso from "../components/alerts/MensagemSucesso.vue";

export default {
  name: "ControleNaturezaLancamento",

  components: {
    MensagemSucesso,
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
          text: "Controle de naturezas",
          disabled: true,
          href: "naturezas",
        },
      ],
      naturezas: [],
      dialog: false,
      editedIndex: -1,
      editedItem: {
        descricao: "",
        observacao: "",
      
      },

      headers: [
        {
          text: "Id",
          align: "start",
          sortable: true,
          value: "id",
        },
        { text: "Descrição", value: "descricao" },
        { text: "Observação", value: "observacao" },
        { text: "Data Cadastro", value: "dataCadastro" },
        { text: "Ações", value: "actions", sortable: false },
      ],
      messagem: "",
    };
  },

  mounted() {
    this.obterNaturezas();
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
    obterNaturezas() {
      naturezaLancamentoService
        .obterTodas()
        .then((response) => {
          this.naturezas = response.data.map((p) => new NaturezaLancamento(p));
        })
        .catch((error) => {
          console.log(error);
        });
    },

    gravar() {
      if (this.editedIndex > -1) {
        naturezaLancamentoService
          .atualizar(this.editedItem)
          .then(() => {
            Object.assign(this.naturezas[this.editedIndex], this.editedItem);
            this.snackbar = true;
            this.messagem = "Natureza editada com sucesso!";
            this.$refs.successMessage.show();
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        naturezaLancamentoService
          .cadastrar(this.editedItem)
          .then((response) => {
            this.snackbar = true;
            this.naturezas.push(response.data);
            this.messagem = "Natureza cadastrada com sucesso!";
            this.$refs.successMessage.show();
          })
          .catch((error) => {
            console.log(error);
          });
      }
      this.close();
    },

    editItem(item) {
      this.editedIndex = this.naturezas.indexOf(item);
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
      const index = this.naturezas.indexOf(item);
      confirm("Deseja excluir está natureza?") &&
        this.naturezas.splice(index, 1);

      naturezaLancamentoService
        .deletar(item.id)
        .then(() => {
          this.snackbar = true;
          this.messagem = "Natureza excluída com sucesso!";
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
