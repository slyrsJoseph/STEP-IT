/////////////////////TASK 1
/////////////////////

function createProductCard(productName, price, imageURL) {
  const cardContainer = document.createElement("div");
  cardContainer.style.width = "300px";
  cardContainer.style.backgroundColor = "white";
  cardContainer.style.marginBottom = "20px";

  const cardImage = document.createElement("img");
  cardImage.setAttribute("src", imageURL);
  cardImage.style.width = "100%";
  cardImage.style.borderRadius = "4px";

  const cardProductName = document.createElement("h3");
  cardProductName.textContent = productName;

  const cardPrice = document.createElement("p");
  cardPrice.textContent = price;

  cardContainer.appendChild(cardImage);
  cardContainer.appendChild(cardProductName);
  cardContainer.appendChild(cardPrice);

  document.body.appendChild(cardContainer);
}

function makeComment(author, message, timestamp) {
  const msgContainer = document.createElement("div");
  msgContainer.style.width = "300px";
  msgContainer.style.backgroundColor = "white";
  msgContainer.style.textAlign = "left";
  msgContainer.style.marginBottom = "20px";

  const msgAuthor = document.createElement("h4");
  msgAuthor.textContent = author + ":";
  msgAuthor.style.marginBottom = "5px";

  const msgText = document.createElement("p");
  msgText.textContent = message;

  const msgTimestamp = document.createElement("small");
  msgTimestamp.textContent = timestamp;
  msgTimestamp.style.textAlign = "right";
  msgTimestamp.style.color = "black";

  msgContainer.appendChild(msgAuthor);
  msgContainer.appendChild(msgText);
  msgContainer.appendChild(msgTimestamp);

  document.body.appendChild(msgContainer);
}

createProductCard(
  "Toyota Supra",
  "10000$",
  "https://cdn.motor1.com/images/mgl/PKZQL/s3/1997-toyota-supra-sold-for-176-000-at-auction.jpg"
);
makeComment("Ahmed Ahmedov", "Rent", "15:05:22");

function createMenuItem(name, price, description) {
  const menuItemContainer = document.createElement("div");
  menuItemContainer.style.width = "300px";
  menuItemContainer.style.backgroundColor = "white";
  menuItemContainer.style.padding = "10px";

  const menuItemName = document.createElement("h3");
  menuItemName.textContent = name;

  const menuItemPrice = document.createElement("p");
  menuItemPrice.textContent = `Price: ${price}`;

  const menuItemDescription = document.createElement("p");
  menuItemDescription.textContent = description;
  menuItemDescription.style.color = "black";

  menuItemContainer.appendChild(menuItemName);
  menuItemContainer.appendChild(menuItemPrice);
  menuItemContainer.appendChild(menuItemDescription);

  return menuItemContainer;
}

const menuItem = createMenuItem(
  "Toyota Supra",
  "10000$",
  "Legendary Japanese sports car known for its speed and reliability."
);

document.body.appendChild(menuItem);
