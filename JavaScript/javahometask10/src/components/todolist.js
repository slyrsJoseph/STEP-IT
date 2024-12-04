import { TodoItem } from "./todoitem.js";

export function TodoList(tasks, onDeleteTask) {
  const ul = document.createElement("ul");
  ul.className = "todo-list";

  tasks.forEach((task) => {
    const item = TodoItem(task, onDeleteTask);
    ul.appendChild(item);
  });

  return ul;
}
