import api from './api';

function obterTodas() {
	return api.get('/naturezas-lancamento')
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorId(id) {
	return api.get(`/naturezas-lancamento/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function cadastrar(naturezaLancamento) {
	return api.post(`/naturezas-lancamento`, naturezaLancamento)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function atualizar(naturezaLancamento) {
	return api.put(`/naturezas-lancamento/${naturezaLancamento.id}`, naturezaLancamento)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function deletar(id) {
	return api.delete(`/naturezas-lancamento/${id}`)
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
