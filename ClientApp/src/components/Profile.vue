<template>
  <form>
    <div class="segment">
      <h1>Create Profile</h1>
    </div>

    <label>
      <input v-model="playerName" type="text" placeholder="Player Name" />
    </label>

    <button class="red input-group" type="button" v-on:click="createPlayer">
      <span class="material-icons">gamepad</span>Create
    </button>
  </form>
</template>

<script lang="ts">
import Vue from "vue";
import { addPlayer } from "../services/game-service";
export default Vue.extend({
  name: "Profile",
  data: () => ({
    playerName: "",
  }),
  methods: {
    createPlayer: async function () {
      if (this.playerName && this.$route.params.game_id) {
        const playerId = await addPlayer(
          this.$route.params.game_id,
          this.playerName
        );
        
        this.$router.push({
          name: "game",
          params: {
            game_id: this.$route.params.game_id,
            player_id: playerId,
          },
        });
      }
    },
  },
});
</script>

<style>
form {
  padding: 16px;
  width: 320px;
  margin: 0 auto;
}

.segment {
  padding: 32px 0;
  text-align: center;
}

.input-group {
  display: flex;
  align-items: center;
  justify-content: flex-start;
}
.input-group label {
  margin: 0;
  flex: 1;
}
</style>