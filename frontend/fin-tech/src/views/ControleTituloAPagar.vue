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
        <h2 class="titulo">Contas a Pagar</h2>
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
                    placeholder="Nome da Pessoa"
                  ></v-text-field>
                </v-col>
                  
                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.valorAPagar"
                    label="Valor a Pagar*"
                    placeholder="R$ 0,00"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.dataEmissao"
                    label="Data Emissão*"
                    placeholder="DD/MM/AAAA"
                    @input="formatarData($event, 'dataEmissao')"
                    :rules="[dataRegra]"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.dataVencimento"
                    label="Data Vencimento*"
                    placeholder="DD/MM/AAAA"
                    @input="formatarData($event, 'dataVencimento')"
                    :rules="[dataRegra]"
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
                    placeholder="DD/MM/AAAA"
                    @input="formatarData($event, 'dataReferencia')"
                    :rules="[dataRegra]"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.valorPago"
                    label="Valor Baixa"
                    placeholder="R$ 0,00"
                  ></v-text-field>
                </v-col>

                <v-col cols="12">
                  <v-text-field
                    v-model="editedItem.dataPagamento"
                    label="Data Baixa"
                    placeholder="DD/MM/AAAA"
                    @input="formatarData($event, 'dataPagamento')"
                    :rules="[dataRegra]"
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
              color="var(--cor-primaria)"
              text
              @click="dialog = false"
            >
              Fechar
            </v-btn>
            <v-btn
              color="var(--cor-primaria)"
              text
              @click="gravar"
            >
              Gravar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>

    <v-row>
      <v-col cols="12" sm="12" md="12">
        <v-data-table
          :headers="headers"
          :items="titulosAPagar"
          :items-per-page="5"
          class="elevation-1">
          <template #[`item.dataVencimento`]="{ item: { dataVencimento } }">
            {{dataVencimento | dataFormatada}}
          </template>
          <template #[`item.dataPagamento`]="{ item: { dataPagamento } }">
            {{dataPagamento | dataFormatada}}
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
import APagar from "@/models/APagar.js";
import aPagarService from "@/services/apagar-service.js";
import naturezaLacamentoService from "@/services/naturezaLancamento-service.js";
import NaturezaLancamento from '@/models/NaturezaLancamento.js';
import moment from "moment";
import { formatarData, validarData } from '@/utils/conversorData.js';

export default {
  name: "ControleAPagar",
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
          text: "Controle a pagar",
          disabled: true,
          href: "controle-apagar",
        },
      ],
      naturezasLancamento: [],
      titulosAPagar: [],
      dialog: false,
      editedIndex: -1,
      editedItem: {
        idPessoa: "",
        idNaturezaLancamento: "",
        valorAPagar: "",
        dataVencimento: "",
        descricao: "",
        dataEmissao: "",
        valorPago: "",
        dataPagamento: "",
        dataReferencia: "",
        observacao: "",
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
        { text: "Valor", value: "valorAPagar" },
        { text: "Data vencimento", value: "dataVencimento" },
        {
          text: "Natureza de Lançamento",
          align: "start",
          sortable: true,
          value: "id",
        },
        { text: "Descrição", value: "descricao" },
        { text: "Valor pagamento", value: "valorPago" },
        { text: "Data Pagamento", value: "dataPagamento" },
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
      aPagarService
        .obterTodos()
        .then((response) => {
          this.titulosAPagar = response.data.map((p) => new APagar(p));
        })
        .catch((error) => {
          console.log(error);
        });
    },

    gravar() {
      if (this.editedIndex > -1) {
        aPagarService
          .atualizar(this.editedItem)
          .then(() => {
            Object.assign(this.titulosAPagar[this.editedIndex], this.editedItem);
            this.snackbar = true;
            this.messagem = "Título a Pagar editada com sucesso!";
            this.color = "success";
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        aPagarService
          .cadastrar(this.editedItem)
          .then((response) => {
            this.snackbar = true;
            this.titulosAPagar.push(response.data);
            this.messagem = "Título a Pagar cadastrada com sucesso!";
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
      this.editedIndex = this.titulosAPagar.indexOf(item);
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
      const index = this.titulosAPagar.indexOf(item);
      confirm("Deseja excluir este título a Pagar?") &&
        this.titulosAPagar.splice(index, 1);

      aPagarService
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
    
    formatarData(value, campo) {
      this.editedItem[campo] = formatarData(value);

      this.date = validarData(this.editedItem[campo]) 
        ? new Date(`${this.editedItem[campo].substring(6, 10)}-${this.editedItem[campo].substring(3, 5)}-${this.editedItem[campo].substring(0, 2)}`) 
        : null;
    },

    dataRegra(value) {
      return validarData(value) || 'Data inválida';
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