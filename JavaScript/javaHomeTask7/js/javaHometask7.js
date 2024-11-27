//////////TASK 1
// const input = document.getElementById("username");

// input.addEventListener("input", (event) => {
//   const filteredValue = event.target.value.replace(/\d/g, "");

//   if (event.target.value !== filteredValue) {
//     event.target.value = filteredValue;
//   }
// });

///////////////////////////TASK 2

// const openModalButton = document.getElementById("open-modal");
// const closeModalButton = document.getElementById("close-modal");
// const modal = document.getElementById("modal");

// openModalButton.addEventListener("click", () => {
//   modal.style.display = "flex";
// });

// closeModalButton.addEventListener("click", () => {
//   modal.style.display = "none";
// });

// modal.addEventListener("click", (event) => {
//   if (event.target === modal) {
//     modal.style.display = "none";
//   }
// });

///////////////TASK 3
// const field = document.getElementById("field");
// const ball = document.getElementById("ball");

// const ballSize = 100;

// field.addEventListener("click", (event) => {
//   const fieldRect = field.getBoundingClientRect();
//   const clickX = event.clientX - fieldRect.left;
//   const clickY = event.clientY - fieldRect.top;

//   let ballX = clickX - ballSize / 2;
//   let ballY = clickY - ballSize / 2;

//   ballX = Math.max(0, Math.min(ballX, fieldRect.width - ballSize));
//   ballY = Math.max(0, Math.min(ballY, fieldRect.height - ballSize));

//   ball.style.left = `${ballX}px`;
//   ball.style.top = `${ballY}px`;
// });

//
//////////////////////////TASK 4

// const redLight = document.querySelector(".traffic-light__red");
// const orangeLight = document.querySelector(".traffic-light__orange");
// const greenLight = document.querySelector(".traffic-light__green");
// const button = document.querySelector(".traffic-light__btn");

// let count = 1;

// button.addEventListener("click", () => {
//   if (count === 1) {
//     redLight.style.backgroundColor = "red";
//     orangeLight.style.backgroundColor = "grey";
//     greenLight.style.backgroundColor = "grey";
//     count += 1;
//   } else if (count === 2) {
//     redLight.style.backgroundColor = "grey";
//     orangeLight.style.backgroundColor = "orange";
//     greenLight.style.backgroundColor = "grey";
//     count += 1;
//   } else if (count === 3) {
//     redLight.style.backgroundColor = "grey";
//     orangeLight.style.backgroundColor = "grey";
//     greenLight.style.backgroundColor = "green";
//     count = 1;
//   }
// });

//////////////////////////////////TASK 5

// const list = document.querySelectorAll(".list-elements li");
// let book = null;
// list.forEach((element) => {
//   element.addEventListener("click", () => {
//     if (book) {
//       book.style.backgroundColor = "white";
//     }
//     book = element;
//     element.style.backgroundColor = "orange";
//   });
// });

//////////////////////////////////TASK 6
document.querySelectorAll(".button").forEach((button) => {
  button.addEventListener("mouseover", () => {
    const tooltip = button.querySelector(".tooltip");
    const rect = tooltip.getBoundingClientRect();

    if (rect.top < 0) {
      tooltip.classList.remove("top");
      tooltip.classList.add("bottom");
    } else {
      tooltip.classList.remove("bottom");
      tooltip.classList.add("top");
    }
  });
});
