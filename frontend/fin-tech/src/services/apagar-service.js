import api from './api';

function obterTodos(){
    return api.get('/APagar/obterTodos')
        .then(response => (response))
        .catch(error => {
            throw error;
        });
}

function obterPorId(id){
    return api.get(`/APagar/id/${id}`)
        .then(response => (response))
        .catch(error => {
            throw error;
        });
}

function cadastrar(aPagar){
    return api.post(`/APagar`, aPagar)
        .then(response => (response))
        .catch(error => {
            throw error;
        });
}

function atualizar(aPagar){
    return api.put(`/APagar/${aPagar.id}`, aPagar)
        .then(response => (response))
        .catch(error => {
            throw error;
        });
}

function deletar(id){
    return api.delete(`/APagar/${id}`)
        .then(response => (response))
        .catch(error => {
            throw error;
        });  
}

function obterPorPeriodo(dataInicial, datafinal) {
    return api.get(`/APagar/periodo?dataInicial=${dataInicial}&dataFinal=${datafinal}`)
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