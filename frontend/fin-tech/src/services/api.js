import axios from "axios";
import utilsStorage from '../utils/storage'

const api = axios.create({
	baseURL: 'https://localhost:5000'
});

api.interceptors.request.use((config) => {

	const token = utilsStorage.obterTokenNaStorage();
	config.headers.Authorization = token;
	return config;
});

export default api;