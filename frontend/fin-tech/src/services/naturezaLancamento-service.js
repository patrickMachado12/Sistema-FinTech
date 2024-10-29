import api from './api';

function obterTodas(){
    return new Promise((resolve, reject) => {
        return api.get('/naturezalancamento/obterTodas')
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function obterPorId(id){
    return new Promise((resolve, reject) => {
        return api.get(`/naturezalancamento/${id}`)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function cadastrar(naturezalancamento){
    return new Promise((resolve, reject) => {
        return api.post(`/naturezalancamento`, naturezalancamento)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function atualizar(naturezalancamento){
    return new Promise((resolve, reject) => {
        return api.put(`/naturezalancamento/${naturezalancamento.id}`, naturezalancamento)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

function deletar(id){
    return new Promise((resolve, reject) => {
        return api.delete(`/naturezalancamento/${id}`)
        .then(response => resolve(response))
        .catch(error => reject(error));
    });
}

export default {
    obterTodas,
    obterPorId,
    cadastrar,
    atualizar,
    deletar
}