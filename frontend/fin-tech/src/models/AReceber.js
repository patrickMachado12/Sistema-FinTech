export default class APagar {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.pessoa = obj.pessoa;
    this.naturezaLancamento = obj.naturezaLancamento;
    this.valorAReceber = obj.valorAReceber;
    this.valorBaixado = obj.valorBaixado;
    this.descricao = obj.descricao;
    this.observacao = obj.observacao;
    this.dataEmissao = obj.dataEmissao;
    this.dataVencimento = obj.dataVencimento;
    this.dataRecebimento = obj.dataRecebimento;
    this.dataReferencia = obj.dataReferencia;
    this.dataExclusao = obj.dataExclusao;
  }
}
