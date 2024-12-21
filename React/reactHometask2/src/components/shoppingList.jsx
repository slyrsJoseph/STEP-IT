///task 1
import { useState } from "react";

function ShoppingList() {
  const [items, setItems] = useState(["Молоко", "Хлеб", "Яблоки"]);
  const [newItem, setNewItem] = useState("");

  const handleAddItem = () => {
    if (newItem.trim() !== "") {
      setItems([...items, newItem]);
      setNewItem("");
    }
  };

  const handleRemoveItem = (index) => {
    setItems(items.filter((_, i) => i !== index));
  };

  return (
    <div className="shopping-list">
      <h1>Список покупок</h1>
      <ul>
        {items.map((item, index) => (
          <li key={index}>
            {item}{" "}
            <button onClick={() => handleRemoveItem(index)}>Удалить</button>
          </li>
        ))}
      </ul>
      <input
        type="text"
        value={newItem}
        onChange={(e) => setNewItem(e.target.value)}
        placeholder="Добавить товар"
      />
      <button onClick={handleAddItem}>Добавить</button>
    </div>
  );
}

export default ShoppingList;
