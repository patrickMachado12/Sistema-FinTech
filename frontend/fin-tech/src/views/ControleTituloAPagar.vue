<template>
  <v-container fluid>
    <MensagemSucesso ref="successMessage" :message="messagem"/>
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <h2 class="titulo">Contas a Pagar</h2>
        <v-divider></v-divider>
      </v-col>
      <!-- Tela de formulário do cadastro / edição./ -->
      <v-dialog
      v-model="dialog"
      persistent
      max-width="1100px">
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
                <v-col cols="6">
                  <v-text-field
                    v-model="editedItem.descricao"
                    label="Descrição*"
                  ></v-text-field>
                </v-col>
                <v-col cols="2">
                  <CampoMonetario
                    v-model="editedItem.valorAPagar"
                    label="Valor*"
                    type="number"
                    @input="'valorAPagar'"
                  />
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataEmissao"
                    label="Data emissão*"
                    @input="formatarData($event, 'dataEmissao')"
                    :rules="[dataRegra]"
                  />
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataVencimento"
                    label="Data vencimento*"
                    @input="formatarData($event, 'dataVencimento')"
                    :rules="[dataRegra]"
                  />
                </v-col>
                <v-col cols="4">
                  <v-autocomplete id="select-natureza-lancamento"
                    :items="naturezasLancamento"
                    v-model="editedItem.idNaturezaLancamento"
                    label="Natureza de lançamento*"
                    required
                  ></v-autocomplete>
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataReferencia"
                    label="Data referência"
                    @input="formatarData($event, 'dataReferencia')"
                    :rules="[dataRegra]"
                  />
                </v-col>
                <v-col cols="2">
                  <CampoMonetario
                    v-model="editedItem.valorPago"
                    label="Valor pago"
                    type="number"
                    @input="'valorPago'"
                  />
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataPagamento"
                    label="Data pagamento"
                    @input="formatarData($event, 'dataPagamento')"
                    :rules="[dataRegra]"
                  />
                </v-col>
                <v-col cols="8">
                  <v-text-field
                    v-model="editedItem.observacao"
                    label="Observação"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-container>
            <small>* Indica campos obrigatórios</small>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              id="btn-gravar"
              text
              @click="gravar"
            >
              Gravar
            </v-btn>
            <v-btn
              id="btn-fechar"
              color="var(--cor-primaria)"
              text
              @click="dialog = false"
            >
              Fechar
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
          <template #[`item.valorAPagar`]="{ item: { valorAPagar } }">
            {{ valorAPagar | formatarPreco }}
          </template>
          <template #[`item.valorPago`]="{ item: { valorPago } }">
            {{ valorPago | formatarPreco }}
          </template>
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
            <v-icon small class="mr-2" @click="editarItem(item)">
              mdi-pencil
            </v-icon>
            <v-icon small @click="excluirItem(item)"> mdi-delete </v-icon>
          </template>
        </v-data-table>        
      </v-col>
    </v-row>    
  </v-container>
</template>

<script>
import APagar from "@/models/APagar.js";
import aPagarService from "@/services/apagar-service.js";
import naturezaLacamentoService from "@/services/natureza-lancamento-service.js";
import NaturezaLancamento from '@/models/NaturezaLancamento.js';
import moment from "moment";
import conversorData  from '@/utils/conversor-data.js';
import CampoMonetario from '@/components/monetario/CampoMonetario.vue';
import CampoData from '@/components/date/CampoData.vue';
import MensagemSucesso from "../components/alerts/MensagemSucesso.vue";

export default {
  name: "ControleAPagar",
  components: {
    CampoMonetario,
    CampoData,
    MensagemSucesso,
  },

  filters: {
    dataFormatada(data) {
      if (!data) return '';
      return moment(data).format("DD/MM/YYYY");
    },

    formatarPreco(valor) {
      if (valor == null) return '-';
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      }).format(valor);
    }
  },

  data() {
    return {
      items: [
        {
          text: "Controle a pagar",
          disabled: false,
          href: "controle-apagar",
        },
      ],
      naturezasLancamento: [],
      titulosAPagar: [],
      searchQuery: '',
      dialog: false,
      editedIndex: -1,
      editedItem: {
        idNaturezaLancamento: 0,
        valorAPagar: 0,
        dataVencimento: "",
        descricao: "",
        dataEmissao: "",
        valorPago: 0,
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
        { text: "Valor", value: "valorAPagar" },
        { text: "Descrição", value: "descricao" },
        { text: "Data emissão", value: "dataEmissao" },
        {
          text: "Natureza de lançamento",
          align: "start",
          sortable: true,
          value: "naturezaLancamento.descricao",
        },
        { text: "Data vencimento", value: "dataVencimento" },
        { text: "Ações", value: "actions", sortable: false },        
      ],
      messagem: "",
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
      val || this.fechar();
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
      const sucessoHandler = (mensagem) => {
        this.snackbar = true;
        this.messagem = mensagem;
        this.fechar();
        this.atualizarListaTitulosAPagar();
      };

      const erroHandler = (erro) => {
        console.error(erro);
      };

      if (this.editedIndex > -1) {
        // Atualizar item existente
        aPagarService.atualizar(this.editedItem)
          .then(() => {
            Object.assign(this.titulosAPagar[this.editedIndex], this.editedItem);
            sucessoHandler("Título editado com sucesso!");
            this.$refs.successMessage.show();
          })
        .catch(erroHandler);
      } else {
        // Cadastrar novo item
        aPagarService.cadastrar(this.editedItem)
          .then((response) => {
            this.titulosAPagar.push(response.data);
            sucessoHandler("Título cadastrado com sucesso!");
            this.$refs.successMessage.show();
          })
        .catch(erroHandler);
      }
    },

    atualizarListaTitulosAPagar() {
      aPagarService.obterTodos()
        .then(response => {
          this.titulosAPagar = response.data;
        })
        .catch(erro => {
          console.error(erro);
        });
    },

    editarItem(item) {
      this.editedIndex = this.titulosAPagar.indexOf(item);
      // Chama o serviço para obter os detalhes do item a ser editado
      aPagarService.obterPorId(item.id)
        .then(response => {
          this.editedItem = new APagar(response.data);
          this.dialog = true;
        });
    },

    fechar() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },

    snackbarAlter(bool){
      this.snackbar = bool;
    },

    excluirItem(item) {
      const index = this.titulosAPagar.indexOf(item);
      confirm("Deseja excluir este título a pagar?") &&
        this.titulosAPagar.splice(index, 1);

      aPagarService
        .deletar(item.id)
        .then(() => {
          this.snackbar = true;
          this.messagem = "Título excluído com sucesso!";
          this.$refs.successMessage.show();
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
    
    formatarData(event, campo) {
      this.editedItem[campo] = event;

      this.date = conversorData.validarData(this.editedItem[campo]) 
        ? new Date(`${this.editedItem[campo].substring(6, 10)}-${this.editedItem[campo].substring(3, 5)}-${this.editedItem[campo].substring(0, 2)}`) 
        : null;
    },

    dataRegra(value) {
      if (!value) return true;
      return conversorData.validarData(value) || 'Data inválida';
    },    
  },
};
</script>

<style scoped>
  #btn-cadastrar {
    background-color: var(--cor-primaria);
    margin-left: 12px;
  }

  #btn-gravar {
    background-color: var(--cor-primaria);
    color:#ffffff;
  }

  .v-btn{
    text-transform: none;
  }
</style>
