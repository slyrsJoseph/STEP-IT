import { checkGuess, resetGame, getAttempts } from "./gamelogics.js";
import { saveAttempts, loadAttempts, clearStorage } from "./storageManager.js";
import { updateHint, updateAttempts, clearInput } from "./interface.js";

let totalAttempts = loadAttempts();
updateAttempts(totalAttempts);

document.getElementById("guess-btn").addEventListener("click", () => {
  const inputEl = document.getElementById("guess-input");
  const userGuess = parseInt(inputEl.value);

  if (isNaN(userGuess)) {
    updateHint("Enter the number");
    return;
  }

  const { result, attempts } = checkGuess(userGuess);
  updateHint(result);

  if (result === "Congratulations you have guessed the number") {
    saveAttempts(totalAttempts + attempts);
    totalAttempts += attempts;
    resetGame();
  }

  updateAttempts(totalAttempts + attempts);
  clearInput();
});

document.getElementById("reset-btn").addEventListener("click", () => {
  resetGame();
  clearStorage();
  totalAttempts = 0;
  updateAttempts(0);
  updateHint("Enter the number between 1 - 100");
});
