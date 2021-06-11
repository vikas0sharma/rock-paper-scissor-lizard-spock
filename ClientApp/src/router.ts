import Vue from 'vue';
import VueRouter from 'vue-router'
import Profile from './components/Profile.vue';
import Game from './components/Game.vue';
import Home from './components/Home.vue';


Vue.use(VueRouter);

const routes = [
	{ path: '/', component: Home, name: 'home' },
	{ path: '/game/:game_id', component: Profile, name: 'create-profile' },
	{ path: '/game/:game_id/players/:player_id', component: Game, name: 'game' },

];

export default new VueRouter({
	routes
});