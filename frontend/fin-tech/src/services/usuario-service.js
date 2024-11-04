import api from "./api";

function login(email, senha) {
  return api.post(`/usuario/login`, { email, senha })
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function logout() {
  return api.delete(`/logout`)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function cadastrar(email, senha) {
  return api.post(`/usuario`, email, senha)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function atualizar(usuario) {
  return api.put(`/usuario/${usuario.id}`, usuario)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function deletar(id) {
  return api.delete(`/usuario/${id}`)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function obterTodos() {
  return api.get(`/usuario/obterTodos`, {})
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function obterPorId(id) {

  return api.get(`/usuario/${id}`)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

export default {
  login,
  logout,
  cadastrar,
  atualizar,
  obterTodos,
  obterPorId,
  deletar,
};
