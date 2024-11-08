<template>
  <v-app>
    <v-container>
      <v-row>
        <v-col cols="12">
          <h1>Fluxo de Caixa</h1>
          <p>Acompanhe de perto o seu fluxo de caixa pessoal!</p>
        </v-col>
      </v-row>
      <v-row justify="end">
        <v-col cols="12" md="3">
          <v-menu
            ref="menu1"
            v-model="menu1"
            :close-on-content-click="false"
            :nudge-right="40"
            transition="scale-transition"
            offset-y
            min-width="auto"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-text-field
                v-model="formatarDataInicial"
                label="Data Inicial"
                prepend-icon="mdi-calendar"
                readonly
                v-bind="attrs"
                v-on="on"
              ></v-text-field>
            </template>
            <v-date-picker
              v-model="filters.dataInicial"
              no-title
              scrollable
              locale="pt-br"
              @change="onDataInicialChange"
            >
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="menu1 = false">Cancelar</v-btn>
              <v-btn text color="primary" @click="$refs.menu1.save(filters.dataInicial)">OK</v-btn>
            </v-date-picker>
          </v-menu>
        </v-col>
        <v-col cols="12" md="3">
          <v-menu
            ref="menu2"
            v-model="menu2"
            :close-on-content-click="false"
            :nudge-right="40"
            transition="scale-transition"
            offset-y
            min-width="auto"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-text-field
                v-model="formatarDataFinal"
                label="Data Final"
                prepend-icon="mdi-calendar"
                readonly
                v-bind="attrs"
                v-on="on"
              ></v-text-field>
            </template>
            <v-date-picker
              v-model="filters.dataFinal"
              no-title
              scrollable
              locale="pt-br"
              @change="onDataFinalChange"
            >
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="menu2 = false">Cancelar</v-btn>
              <v-btn text color="primary" @click="$refs.menu2.save(filters.dataFinal)">OK</v-btn>
            </v-date-picker>
          </v-menu>
        </v-col>
        <v-col class="col-filtro" cols="12" md="2">
          <v-btn id="btn-filtro" dark @click="filtrarPeriodo">FILTRAR</v-btn>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="3">
          <v-card color="green lighten-2" class="pa-3">
            <v-card-title class="headline">{{ totalRecebido | currency }}
              <v-icon>mdi-arrow-up-bold</v-icon>
            </v-card-title>
            <v-card-subtitle>Total Recebido (R$)</v-card-subtitle>
          </v-card>
        </v-col>
        <v-col cols="12" md="3">
          <v-card color="red lighten-2" class="pa-3">
            <v-card-title class="headline">{{ totalGastos | currency }}
              <v-icon>mdi-arrow-down-bold</v-icon>
            </v-card-title>
            <v-card-subtitle>Total Gastos (R$)</v-card-subtitle>
          </v-card>
        </v-col>
        <v-col cols="12" md="3">
          <v-card color="blue lighten-2" class="pa-3">
            <v-card-title class="headline">{{ receitaDespesa | currency }}</v-card-title>
            <v-card-subtitle>Receita - Despesa (R$)</v-card-subtitle>
          </v-card>
        </v-col>
        <v-col cols="12" md="3">
          <v-card color="purple lighten-2" class="pa-3">
            <v-card-title class="headline">{{ saldoFinal | currency }}</v-card-title>
            <v-card-subtitle>Saldo Final (R$)</v-card-subtitle>
          </v-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <h2 class="positive">Entradas</h2>
          <v-data-table
            :headers="headersEntradas"
            :items="entradas"
            disable-pagination
            hide-default-footer
            class="elevation-1"
          >
            <template v-slot:[`item.dataEmissao`]="{ item }">
              {{ item.dataEmissao | formatarData }}
            </template>
            <template v-slot:[`item.descricao`]="{ item }">
              {{ item.descricao }}
            </template>
            <template v-slot:[`item.valorAReceber`]="{ item }">
              {{ item.valorAReceber | currency }}
            </template>
            <template v-slot:[`item.naturezaLancamento`]="{ item }">
              {{ item.naturezaLancamento }}
            </template>
          </v-data-table>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <h2 class="negative">Saídas</h2>
          <v-data-table
            :headers="headersSaidas"
            :items="saidas"
            disable-pagination
            hide-default-footer
            class="elevation-1"
            :item-class="getRowClass"
          >
            <template v-slot:[`item.dataEmissao`]="{ item }">
              {{ item.dataEmissao | formatarData }}
            </template>
            <template v-slot:[`item.descricao`]="{ item }">
              {{ item.descricao }}
            </template>
            <template v-slot:[`item.valorAPagar`]="{ item }">
              {{ item.valorAPagar | currency }}
            </template>
            <template v-slot:[`item.naturezaLancamento`]="{ item }">
              {{ item.naturezaLancamento }}
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script>
import moment from 'moment';
import AReceber from "../models/AReceber.js";
import aReceberService from "../services/areceber-service.js";
import APagar from "../models/APagar.js";
import aPagarService from "../services/apagar-service.js";

export default {
  data() {
    return {
      menu: false,
      filters: {
        dataInicial: null,
        dataFinal: null,
      },
      totalRecebido: 0,
      totalGastos: 0,
      receitaDespesa: 0,
      saldoFinal: 0,
      entradas: [],
      saidas: [],
      headersEntradas: [
        { text: 'Descrição', value: 'descricao' },
        { text: 'Valor', value: 'valorAReceber' },
        { text: 'Data Emissão', value: 'dataEmissao' },
        { text: 'Natureza de lançamento', value: 'naturezaLancamento.descricao' },
      ],
      headersSaidas: [
        { text: 'Descrição', value: 'descricao' },
        { text: 'Valor', value: 'valorAPagar' },
        { text: 'Data Emissão', value: 'dataEmissao' },
        { text: 'Natureza de lançamento', value: 'naturezaLancamento.descricao' },
      ],
      formatarDataInicial: '',
      formatarDataFinal: '',
    };
  },
  computed: {
    saldoClass() {
      return this.saldoFinal >= 0 ? 'positive' : 'negative';
    }
  },
  methods: {
    onDateChange() {
      // Configura o período, ajustando data inicial e final
      this.filters.dataInicial = moment(this.filters.date).startOf('month').format('YYYY-MM-DD');
      this.filters.dataFinal = moment(this.filters.date).endOf('month').format('YYYY-MM-DD');
    },

    filtrarPeriodo() {
      // Verifica se as datas foram definidas antes de realizar a consulta
      if (!this.filters.dataInicial || !this.filters.dataFinal) {
        console.warn("Defina as datas inicial e final para consultar por período.");
        return;
      }

      const consultas = [
        { service: aReceberService, target: 'entradas', mapper: AReceber },
        { service: aPagarService, target: 'saidas', mapper: APagar },
      ];

      consultas.forEach(({ service, target, mapper }) => {
        service.obterPorPeriodo(this.filters.dataInicial, this.filters.dataFinal)
          .then((response) => {
            this[target] = response.data.map((p) => new mapper(p));
            
            // Chama os métodos de cálculo de total recebido e total gastos
            if (target === 'entradas') {
              this.calcularTotalRecebido();
            } else if (target === 'saidas') {
              this.calcularTotalGastos();
            }

            this.atualizarSaldos();
          })
          .catch((error) => {
            console.error(`Erro ao consultar dados por período para ${target}:`, error);
          });
      });
    },

    // Calcula o total recebido somando os valores de entradas
    calcularTotalRecebido() {
      this.totalRecebido = this.entradas.reduce((total, entrada) => {
        return total + (entrada.valorAReceber || 0); // Adiciona 0 para evitar NaN
      }, 0);
    },

    // Calcula o total gastos somando os valores de saídas
    calcularTotalGastos() {
      this.totalGastos = this.saidas.reduce((total, saida) => {
        return total + (saida.valorAPagar || 0); // Adiciona 0 para evitar NaN
      }, 0);
    },

    // Calcula o total dos recebimentos menos os gastos e atualiza o saldo final
    atualizarSaldos() {
      this.receitaDespesa = this.totalRecebido - this.totalGastos;
      this.saldoFinal = this.receitaDespesa;
    },

    formatarData(date) {
      if (!date) return '';
      
      const localDate = new Date(date);
      
      const utcDate = new Date(localDate.getUTCFullYear(), 
                                localDate.getUTCMonth(), 
                                localDate.getUTCDate());
      
      const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
      return utcDate.toLocaleDateString('pt-BR', options);
    },

    onDataInicialChange(date) {
      this.filters.dataInicial = date;
      this.formatarDataInicial = this.formatarData(date);
    },

    onDataFinalChange(date) {
      this.filters.dataFinal = date;
      this.formatarDataFinal = this.formatarData(date);
    },
  },

  watch: {
    'filters.dataInicial': 'onDataInicialChange',
    'filters.dataFinal': 'onDataFinalChange',
  },
  
  filters: {
    currency(value) {
      return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value);
    },
    formatarData(value) {
      return moment(value).format('DD/MM/YYYY');
    }
  }
};
</script>

<style scoped>
.headline {
  font-size: 24px;
  font-weight: bold;
}
.positive {
  color: green;
}
.negative {
  color: red;
}

.col-filtro{
  display: flex;
  align-items: center;
  justify-content: flex-end;
}

#btn-filtro {
  background-color: var(--cor-primaria);
  margin-left: 12px;
}

</style>
