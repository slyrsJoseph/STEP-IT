let secretNumber = Math.floor(Math.random() * 100) + 1;
let attempts = 0;

export function checkGuess(userGuess) {
  attempts++;
  if (userGuess < secretNumber) {
    return { result: "Больше", attempts };
  } else if (userGuess > secretNumber) {
    return { result: "Меньше", attempts };
  } else {
    return { result: "Угадали!", attempts };
  }
}

export function resetGame() {
  secretNumber = Math.floor(Math.random() * 100) + 1;
  attempts = 0;
}

export function getAttempts() {
  return attempts;
}
