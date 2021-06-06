import Vue from 'vue';
import VueRouter from 'vue-router'
import Profile from './components/Profile.vue';
import Game from './components/Game.vue';
import Home from './components/Home.vue';


Vue.use(VueRouter);

const routes = [
	{ path: '/', component: Home, name: 'home' },
	{ path: '/profile', component: Profile, name: 'profile' },
	{ path: '/game', component: Game, name: 'game' },

];

export default new VueRouter({
	routes
});