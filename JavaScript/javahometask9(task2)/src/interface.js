export function updateHint(message) {
  const hintEl = document.getElementById("hint");
  hintEl.textContent = message;
}

export function updateAttempts(attempts) {
  const attemptsEl = document.getElementById("attempts");
  attemptsEl.textContent = attempts;
}

export function clearInput() {
  const inputEl = document.getElementById("guess-input");
  inputEl.value = "";
}
