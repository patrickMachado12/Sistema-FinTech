export default class Pessoa {
  constructor(obj) {
    obj = obj || {};

    this.id = obj.id;
    this.nome = obj.nome;
    this.telefone = obj.telefone;
    //this.email = obj.email;
    this.dataCadastro = obj.dataCadastro;
    this.dataInativacao = obj.dataInativacao;
  }
}
