<template>
  <div>
    <h1>Winner: {{ winner.name ? winner.name : "" }}</h1>
    <div class="container">
      <div class="game">
        <div class="player">{{ you.name }}</div>
        <div class="game__options">
          <div
            class="option btn btn-primary rounded-bottom"
            v-on:click="updateChoice(1)"
          >
            <img src="../resources/images/rock-emoji.png" height="100" />
          </div>
          <div
            class="option btn btn-primary rounded-bottom"
            v-on:click="updateChoice(2)"
          >
            <img src="../resources/images/pen-paper-icon-1.png" height="100" />
          </div>
          <div
            class="option btn btn-primary rounded-bottom"
            v-on:click="updateChoice(3)"
          >
            <img src="../resources/images/icon-scissors-60.png" height="100" />
          </div>
        </div>
        <div class="game__options">
          <div
            class="option btn btn-primary rounded-bottom"
            v-on:click="updateChoice(4)"
          >
            <img src="../resources/images/lizard-icon-7.png" height="100" />
          </div>
          <div
            class="option btn btn-primary rounded-bottom"
            v-on:click="updateChoice(5)"
          >
            <img src="../resources/images/spock-icon-3.png" height="100" />
          </div>
        </div>
      </div>
      <div class="game">
        <div class="player">?</div>
        <ul>
          <li v-for="player of opponents" :key="player.id">
            <div class="card shadow-soft opponent">
              <h2>{{ player.name }}</h2>
              <hr />
              <span class="fs-2">Score: 5</span>
              <!-- <div class="player-avatar shadow-inset rounded">
              <span
                class="material-icons fs-3"
                v-bind:style="{ color: playerColor() }"
                >face</span
              >
              <span class="fs-3">{{ player.name }}</span>
            </div>
            <div class="shadow-inset player-avatar rounded mt-10 fs-2">
              vikas
            </div>
            <div class="shadow-inset rounded mt-10 fs-2">
              vikas
            </div> -->
            </div>
          </li>
        </ul>
      </div>
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
    winner: <Player>{},
  }),
  mounted: async function () {
    this.gameId = this.$route.params.game_id;
    this.playerId = this.$route.params.player_id;

    await setUpSignalRConnection(
      this.gameId,
      this.onplayersUpdated,
      this.onWinnerChanged
    );
    this.onplayersUpdated(await getPlayers(this.gameId));
    this.you = this.players.find((p) => p.id === this.playerId) as Player;
  },
  methods: {
    onplayersUpdated: function (players: Player[]) {
      this.players = players;
    },
    onWinnerChanged: function (winner: Player) {
      this.winner = winner;
    },
    updateChoice: async function (choice: Choice) {
      this.you.choice = choice;
      await updatePlayerChoice(this.gameId, this.you);
    },
    playerColor: function () {
      const rnd = Math.floor(Math.random() * this.colors.length);
      console.log(this.colors);
      return this.colors[rnd];
    },
  },
  computed: {
    opponents: function () {
      const playerId = this.$route.params.player_id;
      return this.players.filter((p) => p.id !== playerId);
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
  justify-content: space-around;
  justify-self:center;
  flex-flow: wrap;
  align-content: center;
}
.game {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 50px;
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
.player-avatar {
  display: flex;
  align-items: center;
  justify-content: center;
}

.fs-3 {
  font-size: 3em;
}
.fs-2 {
  font-size: 1.5em;
}
.mt-10 {
  margin-top: 10px;
}
</style>