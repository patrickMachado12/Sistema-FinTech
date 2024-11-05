import api from './api';

function obterTodos() {
	return api.get('/pessoa/obterTodos')
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function obterPorId(id) {
	return api.get(`/pessoa/${id}`)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function cadastrar(pessoa) {
	return api.post(`/pessoa`, pessoa)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function atualizar(pessoa) {
	return api.put(`/pessoa/${pessoa.id}`, pessoa)
		.then(response => (response))
		.catch(error => {
			throw error;
		});
}

function deletar(id) {
	return api.delete(`/pessoa/${id}`)
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
	deletar
}
