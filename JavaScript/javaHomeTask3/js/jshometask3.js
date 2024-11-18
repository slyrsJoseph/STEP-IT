///////////////////////TASK 1
// const purchaseList = [
//   { name: "Monitor", quantity: 1, bought: true },
//   { name: "Graphic Card", quantity: 1, bought: false },
//   { name: "Keyboard", quantity: 1, bought: true },
//   { name: "Notebook", quantity: 1, bought: false },
// ];

// const displayList = (list) => {
//   const sortedList = list.slice().sort((a, b) => a.bought - b.bought);
//   console.log("Purchase list:");
//   sortedList.forEach((item) => {
//     let status;
//     if (item.bought) {
//       status = "Bought";
//     } else {
//       status = "Not bought";
//     }
//     console.log(`${item.name} - Count: ${item.quantity}, Status: ${status}`);
//   });
// };

// const addProduct = (name, quantity) => {
//   let existingItem = null;

//   for (let item of purchaseList) {
//     if (item.name === name) {
//       existingItem = item;
//       break;
//     }
//   }

//   if (existingItem) {
//     existingItem.quantity += quantity;
//   } else {
//     purchaseList.push({ name, quantity, bought: false });
//   }
// };

// function markAsBought(name) {
//   let found = false;

//   for (let item of purchaseList) {
//     if (item.name === name) {
//       item.bought = true;
//       found = true;
//       break;
//     }
//   }

//   if (!found) {
//     console.log(`Product "${name}" has not found in purchase list`);
//   }
// }

// displayList(purchaseList);
// addProduct("Graphic Card", 1);
// addProduct("Mouse", 1);
// markAsBought("Notebook");
// markAsBought("Ipad");
// displayList(purchaseList);
///////////////////////TASK 2
// const shop_Check = [
//   { name: "Bread", quantity: 1, price: 0.5 },
//   { name: "Butter", quantity: 1, price: 1 },
//   { name: "Glass of water", quantity: 1, price: 0.7 },
//   { name: "Alcohol", quantity: 1, price: 50 },
// ];

// const printCheck = (shop_Check) => {
//   console.log("Check : ");
//   shop_Check.forEach((i) => {
//     console.log(
//       `${i.name}: ${i.quantity} x ${i.price} = ${i.quantity * i.price}`
//     );
//   });
// };

// const calculate = (shop_Check) => {
//   return shop_Check.reduce((sum, i) => sum + i.quantity * i.price, 0);
// };

// const mostExpensive = (shop_Check) => {
//   return shop_Check.reduce((mostExpensive, i) => {
//     const currentTotal = i.quantity * i.price;
//     const mostExpensiveTotal = mostExpensive.quantity * mostExpensive.price;
//     if (currentTotal > mostExpensiveTotal) {
//       return i;
//     } else {
//       return mostExpensive;
//     }
//   });
// };

// const averagePrice = (shop_Check) => {
//   const products = shop_Check.reduce(
//     (sum, product) => sum + product.quantity,
//     0
//   );
//   const totalPrice = calculate(shop_Check);
//   return totalPrice / products;
// };
// printCheck(shop_Check);
// console.log("Total sum of purchase : ", calculate(shop_Check));
// console.log("The most expensive product : ", mostExpensive(shop_Check));
// console.log("Average cost of 1 product: ", averagePrice(shop_Check));
////////////////////////////////////////////////TASK 3
// const styles = [
//   { styleName: "color", value: "blue" },
//   { styleName: "font-size", value: "20px" },
//   { styleName: "text-align", value: "center" },
//   { styleName: "text-decoration", value: "underline" },
// ];

// const applyStyles = (styles, text) => {
//   const styleString = styles
//     .map((style) => `${style.styleName}: ${style.value}`)
//     .join("; ");

//   document.write(`<p style="${styleString}">${text}</p>`);
// };

// applyStyles(styles, "Text example.");
/////////////////////////////////////////////////TASK 4
// const auditoriums = [
//   { name: "Physics Lab", seats: 15, faculty: "Science" },
//   { name: "Chemistry Lab", seats: 20, faculty: "Science" },
//   { name: "Math Classroom", seats: 10, faculty: "Mathematics" },
//   { name: "History Room", seats: 18, faculty: "Humanities" },
//   { name: "Biology Lab", seats: 12, faculty: "Science" },
// ];

// const displayAllAuditoriums = (auditoriums) => {
//   console.log("All auditoriums:");
//   auditoriums.forEach(({ name, seats, faculty }) => {
//     console.log(`Name: ${name}, Seats: ${seats}, Faculty: ${faculty}`);
//   });
// };

// const displayFacultyAuditoriums = (auditoriums, facultyName) => {
//   console.log(`Auditoriums for the ${facultyName} faculty:`);
//   auditoriums
//     .filter((auditorium) => auditorium.faculty === facultyName)
//     .forEach(({ name, seats, faculty }) => {
//       console.log(`Name: ${name}, Seats: ${seats}, Faculty: ${faculty}`);
//     });
// };

// const displaySuitableAuditoriums = (auditoriums, group) => {
//   console.log(`Suitable auditoriums for the group '${group.name}':`);
//   auditoriums
//     .filter(
//       (auditorium) =>
//         auditorium.seats >= group.students &&
//         auditorium.faculty === group.faculty
//     )
//     .forEach(({ name, seats, faculty }) => {
//       console.log(`Name: ${name}, Seats: ${seats}, Faculty: ${faculty}`);
//     });
// };

// const sortBySeats = (auditoriums) => {
//   return [...auditoriums].sort((a, b) => a.seats - b.seats);
// };

// const sortByName = (auditoriums) => {
//   return [...auditoriums].sort((a, b) => a.name.localeCompare(b.name));
// };

// displayAllAuditoriums(auditoriums);
// console.log("\n");

// displayFacultyAuditoriums(auditoriums, "Science");
// console.log("\n");

// const group = { name: "First group", students: 14, faculty: "Science" };
// displaySuitableAuditoriums(auditoriums, group);
// console.log("\n");

// console.log("Auditoriums sorted by seats:");
// console.log(sortBySeats(auditoriums));
// console.log("\n");

// console.log("Auditoriums sorted by name:");
// console.log(sortByName(auditoriums));
