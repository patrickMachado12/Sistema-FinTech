import api from './api';

function obterTodos(){
    return new Promise((resolve, reject) => {
        return api.get('/APagar/obterTodos')
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function obterPorId(id){
    return new Promise((resolve, reject) => {
        return api.get(`/APagar/${id}`)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function cadastrar(aPagar){
    return new Promise((resolve, reject) => {
        return api.post(`/APagar`, aPagar)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function atualizar(aPagar){
    return new Promise((resolve, reject) => {
        return api.put(`/APagar/${aPagar.id}`, aPagar)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function deletar(id){
    return new Promise((resolve, reject) => {
        return api.delete(`/APagar/${id}`)
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