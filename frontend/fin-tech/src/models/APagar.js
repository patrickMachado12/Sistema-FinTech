export default class APagar {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.idUsuario = obj.idUsuario;
    this.idNaturezaLancamento = obj.idNaturezaLancamento;
    this.valorAPagar = obj.valorAPagar;
    this.valorPago = obj.valorPago || 0;
    this.descricao = obj.descricao;
    this.dataEmissao = obj.dataEmissao;
    this.dataVencimento = obj.dataVencimento;
    this.dataPagamento = obj.dataPagamento || null;
    this.dataReferencia = obj.dataReferencia || null;
    this.observacao = obj.observacao || "";
    this.naturezaLancamento = obj.naturezaLancamento ? {
      id: obj.naturezaLancamento.id,
      descricao: obj.naturezaLancamento.descricao
    } : null;
  }
}
