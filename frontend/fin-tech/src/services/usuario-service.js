import api from "./api";

function login(email, senha) {
  return new Promise((resolve, reject) => {
    return api.post(`/usuario/login`, { email, senha })
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

function logout() {
  return new Promise((resolve, reject) => {
    return api.delete(`/logout`)
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

function cadastrarUsuario(email, senha) {
  return new Promise((resolve, reject) => {
    return api.post(`/usuario`, { email, senha })
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

function obterTodos() {
  return new Promise((resolve, reject) => {
    return api.get(`/usuario/obterTodos`, {})
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

function obterPorId(id) {
  return new Promise((resolve, reject) => {
    return api.get(`/usuario/${id}`)
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

export default {
  login,
  logout,
  cadastrarUsuario,
  obterTodos,
  obterPorId,
};
