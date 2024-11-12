//import Perfil from "./Perfil";

export default class Usuario {
	constructor(obj) {
		obj = obj || {};
		this.id = obj.id;
		this.email = obj.email;
		this.senha = obj.senha;
		this.dataCadastro = obj.dataCadastro || null;
		this.dataInativacao = obj.dataInativacao || null;
	}

	modeloValidoLogin() {
		return !!(this.email && this.senha);
	}
}
