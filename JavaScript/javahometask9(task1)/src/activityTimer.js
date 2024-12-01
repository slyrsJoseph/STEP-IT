import { saveData, loadData } from "./storageManager.js";

let sessionTime = 0;
let timerId;

export function startSessionTimer() {
  sessionTime = 0;
  timerId = setInterval(() => {
    sessionTime++;
    updateTotalTime();
  }, 1000);
}

export function stopSessionTimer() {
  clearInterval(timerId);
}

function updateTotalTime() {
  const totalTime = loadData("totalTime") || 0;
  saveData("totalTime", totalTime + 1);
}

export function getSessionTime() {
  return sessionTime;
}

export function getTotalTime() {
  return loadData("totalTime") || 0;
}
