<template>
  <div class="game">
    <div class="player">Vikas</div>
    <div class="game__options">
      <div class="option neo-btn">
        <img src="../resources/images/rock-emoji.png" height="100" />
      </div>
      <div class="option neo-btn">
        <img src="../resources/images/pen-paper-icon-1.png" height="100" />
      </div>
      <div class="option neo-btn">
        <img src="../resources/images/icon-scissors-60.png" height="100" />
      </div>
    </div>
    <div class="game__options">
      <div class="option neo-btn">
        <img src="../resources/images/lizard-icon-7.png" height="100" />
      </div>
      <div class="option neo-btn">
        <img src="../resources/images/spock-icon-3.png" height="100" />
      </div>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
export default Vue.extend({
  name: "Game",
});
</script>

<style>
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
}
.option {
  flex-grow: 1;
  margin: 10px;
  display: flex;
  justify-content: center;
}
.player {
  color: #babecc;
  text-shadow: 1px 1px 1px #fff;
  font-size: 3em;
}
</style>