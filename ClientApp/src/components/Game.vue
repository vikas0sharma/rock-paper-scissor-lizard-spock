<template>
  <div>
    <div class="main">
      <h1>Round: {{ result.round }}</h1>
      <h2>Winner: {{ result.winner ? result.winner : "" }}</h2>
      <h3>{{ result.win }}</h3>
      <div
        style="
          margin-right: 50px;
          display: flex;
          align-items: center;
          width: 100%;
          justify-content: flex-end;
        "
      >
        <Copy/>
      </div>
    </div>
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
        <div class="player">Opponents</div>
        <ul>
          <li v-for="player of opponents" :key="player.id">
            <div class="card shadow-soft opponent">
              <h2>{{ player.name }}</h2>
              <hr />
              <span class="fs-2">Score: {{ getScore(player.id) }}</span>
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
import { Result } from "../models/result";
import { getPlayers, updatePlayerChoice } from "../services/game-service";
import Copy from "./Copy.vue";

export default Vue.extend({
  components: { Copy },
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
    result: <Result>{},
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
    onWinnerChanged: function (result: Result) {
      this.result = result;
    },
    updateChoice: async function (choice: Choice) {
      this.you.choice = choice;
      await updatePlayerChoice(this.gameId, this.you);
    },
    playerColor: function () {
      const rnd = Math.floor(Math.random() * this.colors.length);
      return this.colors[rnd];
    },
    getScore: function (id: string) {
      // @ts-ignore
      return this.result.choices ? this.result.choices[id].score : 0;
    },
    getChoice: function (id: string) {
      // @ts-ignore
      return this.result.choices ? this.result.choices[id].choice : 0;
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
  justify-self: center;
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
.main {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 20px;
}
</style>