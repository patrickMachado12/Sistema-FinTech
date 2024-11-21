<template>
  <div :id="id">
    <v-text-field
      :id="'textfield-' + id"
      :label="label"
      v-money="formatoMonetario"
      v-model="valor"
      @change="emitirAlteracoes"
      clearable
    />
  </div>
</template>

<script>
export default {
  model: {
    prop: 'entrada',
    event: 'change'
  },
  props: {
    id: { type: String, required: false, default: () => 'monetario' },
    label: { type: String, required: true },
    entrada: { type: [Number, String], default: 0 },
  },
  data() {
    return {
      valor: this.formatarValorMonetario(this.entrada),
      formatoMonetario: {
        decimal: ',',
        thousands: '.',
        prefix: 'R$ ',
        precision: 2,
        masked: true
      }
    };
  },
  methods: {
    // Emitir a alteração para o componente pai, removendo a máscara do valor
    emitirAlteracoes() {
      this.$emit("change", this.removerMascara(this.valor));
    },

    // Remover a máscara do valor, caso o valor seja uma string formatada
    removerMascara(value) {
      if (typeof value === "string") {
        return parseFloat(
          value
            .replace("R$ ", "")
            .replace(/\./g, "")
            .replace(",", ".")
        );
      } else if (typeof value === "number") {
        return value;
      }
      return 0;
    },

    // Formatando o valor inicial para o formato monetário
    formatarValorMonetario(valor) {
      if (typeof valor === "number") {
        return valor.toFixed(2).replace('.', ',');
      }
      return valor;
    }
  },
  watch: {
    // Atualizar o valor caso o prop "entrada" seja alterado
    entrada(newValor) {
      this.valor = this.formatarValorMonetario(newValor);
    }
  }
};
</script>
