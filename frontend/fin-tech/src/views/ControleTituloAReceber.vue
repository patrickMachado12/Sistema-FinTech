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
        <h2 class="titulo">Contas a Receber</h2>
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
                    v-model="editedItem.idPessoa"
                    label="Pessoa*"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.valorAReceber"
                    label="Valor a Receber*"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.dataVencimento"
                    label="Data Vencimento*"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.dataEmissao"
                    label="Data Emissão*"
                  ></v-text-field>
                </v-col>
                  
                <v-col cols="12">
                  <v-select
                    :items="naturezasLancamento"
                    v-model="editedItem.idNaturezaLancamento"
                    label="Natureza de Lançamento*"
                    required
                  ></v-select>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.descricao"
                    label="Descrição*"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.dataReferencia"
                    label="Data Referência"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.valorBaixado"
                    label="Valor Baixa*"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.dataRecebimento"
                    label="Data Baixa"
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
              color="blue darken-1"
              text
              @click="dialog = false"
            >
              Fechar
            </v-btn>
            <v-btn
              color="blue darken-1"
              text
              @click="gravar"
            >
              Gravar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>

    
    <!-- Tabela de contas a receber. -->
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <v-data-table
          :headers="headers"
          :items="titulosAReceber"
          :items-per-page="5"
          class="elevation-1">
          <template #[`item.dataRecebimento`]="{ item: { dataRecebimento } }">
            {{dataRecebimento | dataFormatada}}
          </template>
          <template #[`item.dataVencimento`]="{ item: { dataVencimento } }">
            {{dataVencimento | dataFormatada}}
          </template>
          <template #[`item.dataEmissao`]="{ item: { dataEmissao } }">
            {{dataEmissao | dataFormatada}}
          </template>
          <template v-slot:[`item.actions`]="{ item }">
            <v-icon small class="mr-2" @click="editItem(item)">
              mdi-pencil
            </v-icon>
            <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
          </template>
        </v-data-table>        
      </v-col>
    </v-row>

  </v-container>
</template>

<script>
import AReceber from "../models/AReceber.js";
import aReceberService from "../services/aReceber-service.js";
import naturezaLacamentoService from "../services/naturezaLancamento-service.js";
import NaturezaLancamento from '@/models/NaturezaLancamento.js';
import moment from "moment";

export default {
  name: "ControleAReceber",
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
          text: "Controle a receber",
          disabled: true,
          href: "controle-areceber",
        },
      ],
      naturezasLancamento: [],
      titulosAReceber: [],
      dialog: false,
      editedIndex: -1,
      editedItem: {
        idPessoa: "",
        idNaturezaLancamento: "",
        valorAReceber: "",
        valorBaixado: "",
        descricao: "",
        observacao: "",
        dataEmissao: "",
        dataVencimento: "",
        dataRecebimento: "",
        dataReferencia: "",
      },

      headers: [
        {
          text: "Id",
          align: "start",
          sortable: true,
          value: "id",
        },
        {
          text: "Pessoa",
          align: "start",
          sortable: true,
          value: "id",
        },
        { text: "Valor", value: "valorAReceber" },
        { text: "Data vencimento", value: "dataVencimento" },
        {
          text: "Natureza de Lançamento",
          align: "start",
          sortable: true,
          value: "id",
        },
        { text: "Descrição", value: "descricao" },
        { text: "Valor baixado", value: "valorBaixado" },
        { text: "Data Recebimento", value: "dataRecebimento" },
        { text: "Data Emissão", value: "dataEmissao" },
        { text: "Observação", value: "observacao" },
        { text: "Ações", value: "actions", sortable: false },
      ],
    };
  },

  mounted() {
    this.obterTitulos();
    this.obterNaturezasLacamento();
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
    obterTitulos() {
      aReceberService
        .obterTodos()
        .then((response) => {
          this.titulosAReceber = response.data.map((p) => new AReceber(p));
        })
        .catch((error) => {
          console.log(error);
        });
    },

    gravar() {
      if (this.editedIndex > -1) {
        aReceberService
          .atualizar(this.editedItem)
          .then(() => {
            Object.assign(this.titulosAReceber[this.editedIndex], this.editedItem);
            this.snackbar = true;
            this.messagem = "Título a Receber editada com sucesso!";
            this.color = "success";
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        aReceberService
          .cadastrar(this.editedItem)
          .then((response) => {
            this.snackbar = true;
            this.titulosAReceber.push(response.data);
            this.messagem = "Título a Receber cadastrada com sucesso!";
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
      this.editedIndex = this.titulosAReceber.indexOf(item);
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
      const index = this.titulosAReceber.indexOf(item);
      confirm("Deseja excluir este título a Receber?") &&
        this.titulosAReceber.splice(index, 1);

      aReceberService
        .deletar(item.id)
        .then(() => {
          this.snackbar = true;
          this.messagem = "Título excluído com sucesso!";
          this.color = "success";
          this.close();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    obterNaturezasLacamento() {
      naturezaLacamentoService
        .obterTodas()
        .then((response) => {
          this.naturezaLancamento = response.data.map((p) => new NaturezaLancamento(p));
          this.naturezasLancamento = response.data.map((p) => ({
            text: p.descricao,
            value: p.id,
          }))
        })
        .catch((error) => {
          console.log(error)
        })
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