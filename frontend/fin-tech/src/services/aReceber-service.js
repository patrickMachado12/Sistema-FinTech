import api from './api';

function obterTodos(){
    return new Promise((resolve, reject) => {
        return api.get('/AReceber/obterTodos')
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function obterPorId(id){
    return new Promise((resolve, reject) => {
        return api.get(`/AReceber/${id}`)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function cadastrar(aReceber){
    return new Promise((resolve, reject) => {
        return api.post(`/AReceber`, aReceber)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function atualizar(aReceber){
    return new Promise((resolve, reject) => {
        return api.put(`/AReceber/${aReceber.id}`, aReceber)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function deletar(id){
    return new Promise((resolve, reject) => {
        return api.delete(`/AReceber/${id}`)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

export default {
    obterTodos,
    obterPorId,
    cadastrar,
    atualizar,
    deletar
}