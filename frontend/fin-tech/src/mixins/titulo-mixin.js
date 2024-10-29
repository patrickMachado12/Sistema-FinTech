import conversorMonetario from "../utils/conversor-monetario";
import conversorDeData from "../utils/conversor-data";

let TituloMixin = {
    filters: {
        data(data) {
          return conversorDeData.aplicarMascaraEmDataIso(data);
        },
        real(valor) {
          return conversorMonetario.aplicarMascaraParaRealComPrefixo(valor);
        },
    },
}

export default TituloMixin;