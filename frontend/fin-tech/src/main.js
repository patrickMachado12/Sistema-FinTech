import Vue from 'vue'
import App from './App.vue'
import router from './router'
import VueTheMask from 'vue-the-mask'
import money from 'v-money2'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'
import pt from 'vuetify/es5/locale/pt'

Vue.config.productionTip = false
Vue.use(VueTheMask)
Vue.use(money)
Vue.use(Vuetify)

const vuetify = new Vuetify({
  lang: {
    locales: { pt },
    current: 'pt',
  },
})

new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
