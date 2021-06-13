<template>
  <div class="container">
    <h1>Winner: {{winner.name ? winner.name : ''}}</h1>
    <div class="game">
      <div class="player">{{ you.name }}</div>
      <div class="game__options">
        <div class="option neo-btn" v-on:click="updateChoice(1)">
          <img src="../resources/images/rock-emoji.png" height="100" />
        </div>
        <div class="option neo-btn" v-on:click="updateChoice(2)">
          <img src="../resources/images/pen-paper-icon-1.png" height="100" />
        </div>
        <div class="option neo-btn" v-on:click="updateChoice(3)">
          <img src="../resources/images/icon-scissors-60.png" height="100" />
        </div>
      </div>
      <div class="game__options">
        <div class="option neo-btn" v-on:click="updateChoice(4)">
          <img src="../resources/images/lizard-icon-7.png" height="100" />
        </div>
        <div class="option neo-btn" v-on:click="updateChoice(5)">
          <img src="../resources/images/spock-icon-3.png" height="100" />
        </div>
      </div>
    </div>
    <div class="game">
      <div class="player">?</div>
      <ul>
        <li v-for="player of opponents" :key="player.id">
          <div class="neo opponent">
            <span class="material-icons" v-bind:style="{ color: playerColor }"
              >face</span
            >
            <span>{{ player.name }}</span>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import setUpSignalRConnection from "../services/game-hub";
import { Choice, Player } from "../models/player";
import { getPlayers, updatePlayerChoice } from "../services/game-service";

export default Vue.extend({
  name: "Game",
  data: () => ({
    players: <Player[]>[],
    colors: [
      "#55efc4",
      "#81ecec",
      "#74b9ff",
      "#a29bfe",
      "#dfe6e9",
      "#b2bec3",
      "#6c5ce7",
      "#fdcb6e",
      "#e17055",
      "#d63031",
      "#e84393",
    ],
    gameId: "",
    playerId: "",
    you: <Player>{},
    winner: <Player>{}
  }),
  mounted: async function () {
    this.gameId = this.$route.params.game_id;
    this.playerId = this.$route.params.player_id;

    await setUpSignalRConnection(this.gameId, this.onplayersUpdated, this.onWinnerChanged);
    this.onplayersUpdated(await getPlayers(this.gameId));
    this.you = this.players.find((p) => p.id === this.playerId) as Player;
  },
  methods: {
    onplayersUpdated: function (players: Player[]) {
      this.players = players;
    },
    onWinnerChanged : function(winner: Player){
      this.winner = winner;
    },
    updateChoice: async function (choice: Choice) {
      this.you.choice = choice;
      await updatePlayerChoice(this.gameId, this.you);
    },
  },
  computed: {
    opponents: function () {
      const playerId = this.$route.params.player_id;
      return this.players.filter((p) => p.id !== playerId);
    },
    playerColor: function () {
      const rnd = Math.floor(Math.random() * this.colors.length);
      console.log(this.colors);
      return this.colors[rnd];
    },
  },
});
</script>

<style scoped>
ul {
  list-style-type: none;
}
.container {
  display: flex;
  justify-content: space-between;
}
.game {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 50px;
  width: 50%;
}
.game__options {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}
.option {
  margin: 10px;
  display: flex;
  justify-content: center;
}
.player {
  color: #babecc;
  text-shadow: 1px 1px 1px #fff;
  font-size: 3em;
}
.opponent {
  width: 300px;
  margin: 10px;
  padding: 10px;
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
}
</style>