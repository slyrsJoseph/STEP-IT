////////TASK1
// const links = document.querySelectorAll("a");
// links.forEach((link) => {
//   const href = link.getAttribute("href");
//   if (href && (href.startsWith("http://") || href.startsWith("https://"))) {
//     link.classList.add("external-link");
//   }
// });

/////////TASK 2
// const items = document.querySelectorAll("li");

// items.forEach((item) => {
//   item.addEventListener("click", function (event) {
//     const nestedList = this.querySelector(".nested");
//     if (nestedList) {
//       nestedList.classList.toggle("active");
//     }

//     event.stopPropagation();
//   });
// });

//////////////TASK 3
// const bookList = document.getElementById("book-list");
// let lastClickedIndex = null;

// bookList.addEventListener("click", (event) => {
//   const items = Array.from(bookList.children);
//   const clickedItem = event.target;

//   if (clickedItem.tagName !== "LI") return;

//   const clickedIndex = items.indexOf(clickedItem);

//   if (event.ctrlKey) {
//     clickedItem.classList.toggle("selected");
//   } else if (event.shiftKey && lastClickedIndex !== null) {
//     const start = Math.min(lastClickedIndex, clickedIndex);
//     const end = Math.max(lastClickedIndex, clickedIndex);

//     items.forEach((item, index) => {
//       if (index >= start && index <= end) {
//         item.classList.add("selected");
//       }
//     });
//   } else {
//     items.forEach((item) => item.classList.remove("selected"));

//     clickedItem.classList.add("selected");
//   }

//   lastClickedIndex = clickedIndex;
// });

////////////////TASK4
// const textContainer = document.querySelector(".text-container");
// const text = document.querySelector(".text");
// const textarea = document.querySelector(".textarea");
// document.body.addEventListener("keydown", (event) => {
//   console.log(event);
//   if (event.ctrlKey && event.key == "e") {
//     text.style.display = "none";
//     textarea.style.display = "block";
//     textarea.value = text.textContent;
//     event.preventDefault();
//   } else if (event.ctrlKey && event.key == "s") {
//     text.style.display = "block";
//     textarea.style.display = "none";
//     text.textContent = textarea.value;
//     event.preventDefault();
//   }
// });

//////////////////////TASK5
// function Table(columnIndex, isNumeric = false) {
//   const table = document.querySelector(".table");
//   const tbody = table.querySelector(".table__body");
//   const rows = Array.from(tbody.rows);
//   rows.sort((row1, row2) => {
//     const cell1 = row1.cells[columnIndex].textContent;
//     const cell2 = row2.cells[columnIndex].textContent;

//     if (isNumeric) {
//       return parseFloat(cell2) - parseFloat(cell1);
//     } else {
//       return cell1.localeCompare(cell2);
//     }
//   });
//   rows.forEach((row) => tbody.appendChild(row));
// }

////////////////////////TASK 6
const resizable = document.getElementById("resizable");
const resizeHandle = document.getElementById("resize-handle");
let isResizing = false;

resizeHandle.addEventListener("mousedown", (e) => {
  isResizing = true;

  const startWidth = resizable.offsetWidth;
  const startHeight = resizable.offsetHeight;
  const startX = e.clientX;
  const startY = e.clientY;

  const onMouseMove = (event) => {
    if (isResizing) {
      const newWidth = startWidth + (event.clientX - startX);
      const newHeight = startHeight + (event.clientY - startY);
      resizable.style.width = `${newWidth}px`;
      resizable.style.height = `${newHeight}px`;
    }
  };

  const onMouseUp = () => {
    isResizing = false;
    document.removeEventListener("mousemove", onMouseMove);
    document.removeEventListener("mouseup", onMouseUp);
  };

  document.addEventListener("mousemove", onMouseMove);
  document.addEventListener("mouseup", onMouseUp);
});
