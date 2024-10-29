export default class NaturezaLancamento {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.idUsuario = obj.idUsuario;
    this.descricao = obj.descricao;
    this.observacao = obj.observacao;
    this.dataCadastro = obj.dataCadastro;
    this.dataInativacao = obj.dataInativacao;
  }
}
