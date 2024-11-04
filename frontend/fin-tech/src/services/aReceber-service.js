import api from './api';

function obterTodos() {
	return api.get('/AReceber/obterTodos')
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorId(id) {
	return api.get(`/AReceber/id/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function cadastrar(aReceber) {
	return api.post(`/AReceber`, aReceber)
		.then(response => response.data)
		.catch(error => {
			throw error;
		});
}

function atualizar(aReceber) {
	return api.put(`/AReceber/${aReceber.id}`, aReceber)
		.then(response => response.data)
		.catch(error => {
			throw error;
		});
}

function deletar(id) {
	return api.delete(`/AReceber/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorPeriodo(dataInicial, datafinal) {
	return api.get(`/AReceber/periodo?dataInicial=${dataInicial}&dataFinal=${datafinal}`)
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