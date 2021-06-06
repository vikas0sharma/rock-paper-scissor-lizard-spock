<template>
  <form>
    <div class="segment">
      <h1>Create Profile</h1>
    </div>

    <label>
      <input type="text" placeholder="Player Name" />
    </label>

    <button class="red input-group" type="button">
      <span class="material-icons">gamepad</span>Create
    </button>


  </form>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "Profile",
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