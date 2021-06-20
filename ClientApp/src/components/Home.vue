<template>
  <form class="form">
    <div class="rules__form">
      <div v-if="progress" class="progress">
        <div class="progress-bar"></div>
      </div>
      <button v-else class="red" type="button" v-on:click="createGame">
        <span class="material-icons">gamepad</span>
        <span class="btn-icon">Play</span>
      </button>
      <img class="image" src="../resources/images/main.png" />
      <h1>Rules</h1>
      <label class="rules__text">
        "Scissors decapitate lizard, scissors cuts paper, paper covers rock, rock
        crushes lizard, lizard poisons Spock, Spock smashes scissors, scissors
        decapitates lizard, lizard eats paper, paper disproves Spock, Spock
        vaporizes rock, and as it always has, rock crushes scissors."
      </label>
    </div>
  </form>
</template>

<script>
import Vue from "vue";
import { createGame } from "../services/game-service";
export default Vue.extend({
  name: "Home",
  data: () => ({
    progress: false,
  }),
  methods: {
    createGame: async function () {
      this.progress = true;
      try {
        const gameId = await createGame();
        this.$router.push({
          name: "create-profile",
          params: { game_id: gameId },
        });
      } finally {
        this.progress = false;
      }
    },
  },
});
</script>

<style scoped>
.rules__form {
  display: flex;
  align-items: center;
  flex-direction: column;
}
.rules__text {
  margin-right: 8px;
  box-shadow: inset 2px 2px 5px #babecc, inset -5px -5px 10px #fff;
  width: 100%;
  box-sizing: border-box;
  transition: all 0.2s ease-in-out;
  appearance: none;
  -webkit-appearance: none;
  padding: 2em;
}
.rules__text:hover {
  color: #ae1100;
  box-shadow: inset 1px 1px 2px #babecc, inset -1px -1px 2px #fff;
}
.btn-icon {
  font-size: 25px;
}
.image {
  margin: 40px 0px 40px;
  -webkit-animation: spin 4s linear infinite;
  -moz-animation: spin 4s linear infinite;
  animation: spin 4s linear infinite;
  width: 90%;
}
@-moz-keyframes spin {
  100% {
    -moz-transform: rotate(360deg);
  }
}
@-webkit-keyframes spin {
  100% {
    -webkit-transform: rotate(360deg);
  }
}
@keyframes spin {
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
.progress {
  margin-top: 50px;
  width: 10em;
  height: 0.6rem;
  border: 0.0625rem solid #d1d9e6;
  margin-bottom: 1rem;
  overflow: hidden;
  font-size: 0.75rem;
  font-weight: 600;
  box-shadow: inset 2px 2px 5px #b8b9be, inset -3px -3px 7px #fff;
}
.progress-bar {
  height: 0.6rem;
  display: flex;
  flex-direction: column;
  justify-content: center;
  color: #ecf0f3;
  text-align: center;
  white-space: nowrap;
  background-color: #e6e7ee;
  transition: width 0.6s ease;
  box-shadow: none;
  border-radius: 0.55rem;
  background-color: #0056b3 !important;
  animation: 3s ease 0s 1 normal none running animate-positive;
  opacity: 1;
  background-image: linear-gradient(
    45deg,
    rgba(236, 240, 243, 0.15) 25%,
    transparent 25%,
    transparent 50%,
    rgba(236, 240, 243, 0.15) 50%,
    rgba(236, 240, 243, 0.15) 75%,
    transparent 75%,
    transparent
  );
  background-size: 1rem 1rem;
  animation-duration: 1s;
  animation-name: changewidth;
  animation-iteration-count: infinite;
  animation-direction: alternate;
}
@keyframes changewidth {
  from {
    width: 0em;
  }

  to {
    width: 100%;
  }
}
</style>