<template>
  <v-container fluid>
    <MensagemSucesso ref="successMessage" :message="messagem"/>
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <h2 class="titulo">Contas a Receber</h2>
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
                    v-model="editedItem.valorAReceber"
                    label="Valor a Receber*"
                    type="number"
                    @input="formatarValor('valorAReceber')"/>
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataEmissao"
                    label="Data Emissão*"
                    @input="formatarData($event, 'dataEmissao')"
                    :rules="[dataRegra]"
                  />
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataVencimento"
                    label="Data Vencimento*"
                    @input="formatarData($event, 'dataVencimento')"
                    :rules="[dataRegra]"
                  />
                </v-col>
                <v-col cols="4">
                  <v-select
                    :items="naturezasLancamento"
                    v-model="editedItem.idNaturezaLancamento"
                    label="Natureza de Lançamento*"
                    required
                  ></v-select>
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataReferencia"
                    label="Data Referência"
                    @input="formatarData($event, 'dataReferencia')"
                    :rules="[dataRegra]"
                  />
                </v-col>
                <v-col cols="2">
                  <CampoMonetario
                    v-model="editedItem.valorBaixado"
                    label="Valor Baixa*"
                    type="number"
                    @input="formatarValor('valorBaixado')"
                  />
                </v-col>
                <v-col cols="2">
                  <CampoData
                    v-model="editedItem.dataRecebimento"
                    label="Data Baixa"
                    @input="formatarData($event, 'dataRecebimento')"
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
    <!-- Tabela de contas a receber. -->
    <v-row>
      <v-col cols="12" sm="12" md="12">
        <v-data-table
          :headers="headers"
          :items="titulosAReceber"
          :items-per-page="5"
          class="elevation-1">
          <template #[`item.valorAReceber`]="{ item: { valorAReceber } }">
            {{ valorAReceber | currency }}
          </template>
          <template #[`item.valorBaixado`]="{ item: { valorBaixado } }">
            {{ valorBaixado | currency }}
          </template>
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
import aReceberService from "../services/areceber-service.js";
import naturezaLacamentoService from "../services/natureza-lancamento-service.js";
import NaturezaLancamento from '@/models/NaturezaLancamento.js';
import moment from "moment";
import conversorData  from '@/utils/conversor-data.js';
import CampoMonetario from '@/components/monetario/CampoMonetario.vue';
import CampoData from '@/components/date/CampoData.vue';
import MensagemSucesso from "../components/alerts/MensagemSucesso.vue";

export default {
  name: "ControleAReceber",
  components: {
    CampoMonetario,
    CampoData,
    MensagemSucesso,
  },

  filters: {
    currency(value) {
      if (value == null) return '-';
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      }).format(value);
    },
    dataFormatada(value) {
      if (!value) return '';
      return moment(value).format("DD/MM/YYYY");
    },
  },

  data() {
    return {
      items: [
        {
          text: "Controle a receber",
          disabled: false,
          href: "controle-areceber",
        },
      ],
      naturezasLancamento: [],
      titulosAReceber: [],
      searchQuery: '',
      dialog: false,
      editedIndex: -1,
      editedItem: {
        idNaturezaLancamento: 0,
        valorAReceber: 0,
        valorBaixado: 0,
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
        { text: "Valor", value: "valorAReceber" },
        { text: "Descrição", value: "descricao" },
        { text: "Data Emissão", value: "dataEmissao" },
        {
          text: "Natureza de Lançamento",
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
      const sucessoHandler = (mensagem) => {
        this.snackbar = true;
        this.messagem = mensagem;
        this.fechar();
        this.atualizarListaTitulosAReceber();
      };

      const erroHandler = (erro) => {
        console.error(erro);
      };

      if (this.editedIndex > -1) {
        // Atualizar item existente
        aReceberService.atualizar(this.editedItem)
          .then(() => {
            Object.assign(this.titulosAReceber[this.editedIndex], this.editedItem);
            sucessoHandler("Título editado com sucesso!");
            this.$refs.successMessage.show();
          })
        .catch(erroHandler);
      } else {
        // Cadastrar novo item
        aReceberService.cadastrar(this.editedItem)
          .then((response) => {
            this.titulosAReceber.push(response.data);
            sucessoHandler("Título cadastrado com sucesso!");
            this.$refs.successMessage.show();
          })
        .catch(erroHandler);
      }
    },

    atualizarListaTitulosAReceber() {
      aReceberService.obterTodos()
        .then(response => {
          this.titulosAReceber = response.data;
        })
        .catch(erro => {
          console.error(erro);
        });
    },

    editItem(item) {
      this.editedIndex = this.titulosAReceber.indexOf(item);
      // Chama o serviço para obter os detalhes do item a ser editado
      aReceberService.obterPorId(item.id)
        .then(response => {
          this.editedItem = new AReceber(response.data);
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

    deleteItem(item) {
      const index = this.titulosAReceber.indexOf(item);
      confirm("Deseja excluir este título a receber?") &&
        this.titulosAReceber.splice(index, 1);

      aReceberService
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
</style>
