import {
  startSessionTimer,
  stopSessionTimer,
  getSessionTime,
  getTotalTime,
} from "./activityTimer.js";

const sessionTimeEl = document.getElementById("session-time");
const totalTimeEl = document.getElementById("total-time");
const startBtn = document.getElementById("start-btn");
const stopBtn = document.getElementById("stop-btn");

function updateUI() {
  setInterval(() => {
    sessionTimeEl.textContent = `Current session: ${getSessionTime()} seconds`;
    totalTimeEl.textContent = `Overall time: ${getTotalTime()} seconds`;
  }, 1000);
}

startBtn.addEventListener("click", () => {
  startSessionTimer();
});

stopBtn.addEventListener("click", () => {
  stopSessionTimer();
});

updateUI();
