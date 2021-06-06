<template>
  <form class="form">
    <div class="rules__form">
      <img src="../resources/images/main.png" />
      <h1>Rules</h1>
      <label class="rules__text">
        "Scissors decapitate Scissors cuts paper, paper covers rock, rock
        crushes lizard, lizard poisons Spock, Spock smashes scissors, scissors
        decapitates lizard, lizard eats paper, paper disproves Spock, Spock
        vaporizes rock, and as it always has, rock crushes scissors."
      </label>
    </div>
  </form>
</template>

<script>
import Vue from "vue";
export default Vue.extend({
  name: "Home",
});
</script>

<style>
.form {
  width: 406px !important;
}
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
.rules__text:hover{
	color: #ae1100;
  box-shadow: inset 1px 1px 2px #babecc, inset -1px -1px 2px #fff;
}

</style>