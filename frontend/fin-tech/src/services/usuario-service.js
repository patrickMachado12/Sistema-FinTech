import api from "./api";

function login(email, senha) {
  return api.post(`/usuarios/login`, { email, senha })
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
  return api.post(`/usuarios`, email, senha)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function atualizar(usuario) {
  return api.put(`/usuarios/${usuario.id}`, usuario)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function deletar(id) {
  return api.delete(`/usuarios/${id}`)
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function obterTodos() {
  return api.get(`/usuarios`, {})
    .then(response => (response))
    .catch(error => {
      throw error;
    });
}

function obterPorId(id) {

  return api.get(`/usuarios/${id}`)
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
