export default class Pessoa {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.nome = obj.nome || null;
    this.telefone = obj.telefone || null;
    this.dataCadastro = obj.dataCadastro  || null;
  }
}
