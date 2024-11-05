<template>
  <div :id="id">
    <v-text-field
      :id="'textfield-' + id"
      :label="label"
      v-money="formatoMonetario"
      v-model.lazy="valor"
      @change="emitirAlteracoes"
      @input="onInput"
      @blur="formatarValor"
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
    entrada: { type: Number, default: 0 },
  },
  data() {
    return {
      valor: 0,
      formatoMonetario: {
        decimal: ',',
        thousands: '.',
        prefix: 'R$ ',
        precision: 2,
        masked: false
      }
    };
  },
  methods: {
    emitirAlteracoes() {
      this.$emit("change", parseFloat(this.removerMascara(this.valor)));
    },

    removerMascara(value) {
      if (typeof value == "string") {
        return parseFloat(
          value
            .replace("R$ ", "")
            .replace(/\./g, "")
            .replace(",", ".")
        );
      } else if (typeof value == "number") {
        return value;
      }
      return 0;
    }
  }
};
</script>

<style scoped>

</style>
