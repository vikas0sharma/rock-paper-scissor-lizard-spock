<template>
  <div id="app">
    <div class="header">
      <span class="txt"> Rock Paper Scissor Lizard Spock</span>
      <div class="menu__items">
        <span v-if="!isHome" v-on:click="gotToHome" class="btn material-icons"
          >home</span
        >
        <Copy v-if="isGameOn" />
      </div>
    </div>
    <router-view></router-view>
  </div>
</template>

<script lang="ts" >
import Vue from "vue";
import Copy from "./components/Copy.vue";

export default Vue.extend({
  components: { Copy },
  name: "App",
  data: () => ({
    isGameOn: false,
    isHome: true,
  }),
  mounted: function () {
    this.isGameOn = this.$route.name === "game";
    this.isHome = this.$route.name === "home";
  },
  watch: {
    $route: function (route) {
      this.isGameOn = route && route.name === "game";
      this.isHome = route && route.name === "home";
    },
  },
  methods: {
    gotToHome: function () {
      this.$router.push({
        name: "home",
      });
    },
  },
});
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  width: 100% !important;
}
.header {
  border-radius: 10px;
  background: #e0e0e0;
  box-shadow: 8px 8px 16px #bebebe, -8px -8px 16px #ffffff;
  color: #babecc;
  text-shadow: 2px 2px 2px #fff;
  height: 65px;
  display: flex;
  justify-content: space-around;
  align-items: center;
}

.btn {
  padding: 0.5rem 1.5rem;
  border: 2px solid rgba(255, 255, 255, 0.3);
  box-shadow: 4px 4px 6px 0 rgba(0, 0, 0, 0.1), -4px -4px 6px white;
  border-radius: 10px;
  font-family: "Roboto", sans-serif;
  cursor: pointer;
  transition: color 0.2s ease-out, transform 0.2s ease-out;
  color: dimgray;
  margin-left: 15px;
}
.btn:hover {
  transform: scale(1.05);
  box-shadow: -2px -2px 5px #fff, 2px 2px 5px #babecc;
}
.btn:focus {
  outline: none;
  transform: scale(0.95);
  box-shadow: inset 1px 1px 2px #babecc, inset -1px -1px 2px #fff;
}
.btn:hover,
.btn:focus {
  color: #ae1100;
}
.txt {
  font-size: 25px;
}
.menu__items {
  display: flex;
  align-items: center;
  justify-items: left;
}
</style>
