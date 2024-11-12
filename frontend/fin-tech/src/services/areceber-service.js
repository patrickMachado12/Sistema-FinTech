import api from './api';

function obterTodos() {
	return api.get('/titulos-areceber')
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorId(id) {
	return api.get(`/titulos-areceber/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function cadastrar(aReceber) {
	return api.post(`/titulos-areceber`, aReceber)
		.then(response => response.data)
		.catch(error => {
			throw error;
		});
}

function atualizar(aReceber) {
	return api.put(`/titulos-areceber/${aReceber.id}`, aReceber)
		.then(response => response.data)
		.catch(error => {
			throw error;
		});
}

function deletar(id) {
	return api.delete(`/titulos-areceber/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorPeriodo(dataInicial, datafinal) {
	return api.get(`/titulos-areceber/periodo?dataInicial=${dataInicial}&dataFinal=${datafinal}`)
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
