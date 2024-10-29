export default class APagar {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.pessoa = obj.pessoa;
    this.usuario = obj.usuario;
    this.naturezaLancamento = obj.naturezaLancamento;
    this.valorAPagar = obj.valorAPagar;
    this.valorPago = obj.valorPago;
    this.descricao = obj.descricao;
    this.dataEmissao = obj.dataEmissao;
    this.dataVencimento = obj.dataVencimento;
    this.dataPagamento = obj.dataPagamento;
    this.dataReferencia = obj.dataReferencia;
    this.dataExclusao = obj.dataExclusao;
    this.observacao = obj.observacao;
  }
}
