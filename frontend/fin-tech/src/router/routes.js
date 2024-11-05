import Login from '@/views/Login.vue'
import ControleTituloAPagar from '@/views/ControleTituloAPagar.vue';
import ControleTituloAReceber from '@/views/ControleTituloAReceber.vue';
import ControleDePessoas from '@/views/ControlePessoas.vue';
import ControleNaturezaLancamento from '@/views/ControleNaturezaLancamento.vue';
import ControleUsuario from '@/views/ControleUsuario.vue';
import ControleFluxoCaixa from '@/views/ControleFluxoCaixa.vue';

const routes = [
	{
		path: '/login',
		name: 'Login',
		component: Login,
		title: 'Login',
		meta: { requiredAuth: false }
	},
	{
		path: '/',
		name: 'Login',
		component: Login,
		title: 'Login',
		meta: { requiredAuth: false }
	},
	{
		path: '/fluxo-caixa',
		name: 'FluxoCaixa',
		component: ControleFluxoCaixa,
		title: 'Fluxo de caixa',
		meta: { requiredAuth: true }
	},
	{
		path: '/controle-areceber',
		name: 'ControleAReceber',
		component: ControleTituloAReceber,
		title: 'A receber',
		meta: { requiredAuth: true }
	},
	{
		path: '/controle-apagar',
		name: 'ControleAPagar',
		component: ControleTituloAPagar,
		title: 'A pagar',
		meta: { requiredAuth: true }
	},
	{
		path: '/pessoas',
		name: 'ControleDePessoas',
		component: ControleDePessoas,
		title: 'Cadastro de Pessoas',
		meta: { requiredAuth: true }
	},
	{
		path: '/natureza-lancamento',
		name: 'ControleNaturezaLancamento',
		component: ControleNaturezaLancamento,
		title: 'Cadastro de Natureza de Lançamento',
		meta: { requiredAuth: true }
	},
	{
		path: '/usuario',
		name: 'ControleUsuario',
		component: ControleUsuario,
		title: 'Cadastro de Usuario',
		meta: { requiredAuth: true }
	},
];


export default routes;
