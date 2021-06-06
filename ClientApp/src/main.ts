import Vue from "vue";
import App from './App.vue';
import router from './router';
import 'material-icons/iconfont/material-icons.css';
import './resources/sass/main.scss';

Vue.config.productionTip = false;


new Vue({
  render: h => h(App),
  router
}).$mount("#app");
