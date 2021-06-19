<template>
  <div class="tooltip">
    <span
      style="color: blue"
      class="material-icons btn btn-primary rounded-bottom"
      v-on:click="copy"
      >share</span
    >
  </div>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  name: "Copy",
  methods: {
    copy: function () {
      let href = window.location.href;
      href = href.substring(0, href.indexOf("players"));
      const textField = document.createElement("textarea");
      textField.innerText = href;
      document.body.appendChild(textField);
      textField.select();
      document.execCommand("copy");
      textField.remove();
    },
  },
});
</script>
<style scoped>
.tooltip-inner {
  padding: 0.25rem 0.5rem;
  color: #262833;
  text-align: center;
  background-color: #ecf0f3;
  border-radius: 0.55rem;
  box-shadow: inset 2px 2px 5px #b8b9be, inset -3px -3px 7px #fff;
  top: 0px;
  left: 0px;
  will-change: transform;
  position: relative;
  z-index: 1070;
  display: block;
  margin: 0;
  font-family: "Nunito Sans", sans-serif;
  font-style: normal;
  line-height: 1.5;
  text-align: left;
  text-align: start;
  text-decoration: none;
  text-shadow: none;
  text-transform: none;
  letter-spacing: normal;
  word-break: normal;
  word-spacing: normal;
  white-space: normal;
  line-break: auto;
  font-size: 0.875rem;
  word-wrap: break-word;
  opacity: 0;
}
.tooltip {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  align-content: center;

}
.tooltip:hover .tooltip-inner {
  opacity: 100;
}
</style>