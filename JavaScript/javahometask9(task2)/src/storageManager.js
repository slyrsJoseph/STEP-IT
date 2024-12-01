const ATTEMPTS_KEY = "guessGameAttempts";

export function saveAttempts(attempts) {
  localStorage.setItem(ATTEMPTS_KEY, attempts);
}

export function loadAttempts() {
  return parseInt(localStorage.getItem(ATTEMPTS_KEY)) || 0;
}

export function clearStorage() {
  localStorage.removeItem(ATTEMPTS_KEY);
}
