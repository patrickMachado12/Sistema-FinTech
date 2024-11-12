import api from './api';

function obterTodos() {
	return api.get('/titulos-apagar')
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorId(id) {
	return api.get(`/titulos-apagar/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function cadastrar(aPagar) {
	return api.post(`/titulos-apagar`, aPagar)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function atualizar(aPagar) {
	return api.put(`/titulos-apagar/${aPagar.id}`, aPagar)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function deletar(id) {
	return api.delete(`/titulos-apagar/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorPeriodo(dataInicial, datafinal) {
	return api.get(`/titulos-apagar/periodo?dataInicial=${dataInicial}&dataFinal=${datafinal}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

export default {
	obterTodos,
	obterPorId,
	cadastrar,
	atualizar,
	deletar,
	obterPorPeriodo,
}
