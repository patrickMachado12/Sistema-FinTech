import api from './api';

function obterTodas() {
	return api.get('/naturezaLancamento/obterTodas')
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorId(id) {
	return api.get(`/naturezaLancamento/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function cadastrar(naturezaLancamento) {
	return api.post(`/naturezaLancamento`, naturezaLancamento)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function atualizar(naturezaLancamento) {
	return api.put(`/naturezaLancamento/${naturezaLancamento.id}`, naturezaLancamento)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function deletar(id) {
	return api.delete(`/naturezaLancamento/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

export default {
	obterTodas,
	obterPorId,
	cadastrar,
	atualizar,
	deletar
}