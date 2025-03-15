import axios from "axios";
import utilsStorage from '../utils/storage'

const api = axios.create({
	baseURL: 'https://fintech-asb6bhe4eyetevgw.brazilsouth-01.azurewebsites.net'
});

api.interceptors.request.use((config) => {

	const token = utilsStorage.obterTokenNaStorage();
	config.headers.Authorization = token;
	return config;
});

export default api;