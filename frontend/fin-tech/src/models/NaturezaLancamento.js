export default class NaturezaLancamento {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.idUsuario = obj.idUsuario;
    this.descricao = obj.descricao || null;
    this.observacao = obj.observacao || null;
    this.dataCadastro = obj.dataCadastro || null;
  }
}
