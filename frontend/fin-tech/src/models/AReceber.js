export default class AReceber {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.idPessoa = obj.idPessoa;
    this.idUsuario = obj.idUsuario;
    this.idNaturezaLancamento = obj.idNaturezaLancamento;
    this.valorAReceber = obj.valorAReceber;
    this.valorBaixado = obj.valorBaixado || 0;
    this.descricao = obj.descricao;
    this.dataEmissao = obj.dataEmissao;
    this.dataVencimento = obj.dataVencimento;
    this.dataRecebimento = obj.dataRecebimento || null;
    this.dataReferencia = obj.dataReferencia || null;
    this.observacao = obj.observacao || "";
    this.pessoa = obj.pessoa ? {
      id: obj.pessoa.id,
      nome: obj.pessoa.nome
    } : null;
    this.naturezaLancamento = obj.naturezaLancamento ? {
      id: obj.naturezaLancamento.id,
      descricao: obj.naturezaLancamento.descricao
    } : null;
  }
}
