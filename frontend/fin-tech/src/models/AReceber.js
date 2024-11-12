export default class AReceber {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.idUsuario = obj.idUsuario;
    this.idNaturezaLancamento = obj.idNaturezaLancamento;
    this.valorAReceber = obj.valorAReceber;
    this.valorBaixado = obj.valorBaixado || 0;
    this.descricao = obj.descricao;
    this.dataEmissao = obj.dataEmissao;
    this.dataVencimento = obj.dataVencimento;
    this.dataRecebimento = obj.dataRecebimento;
    this.dataReferencia = obj.dataReferencia;
    this.observacao = obj.observacao || "";
    this.naturezaLancamento = obj.naturezaLancamento ? {
      id: obj.naturezaLancamento.id,
      descricao: obj.naturezaLancamento.descricao
    } : null;
  }
}
