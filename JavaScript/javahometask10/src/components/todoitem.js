export function TodoItem(task, onDeleteTask) {
  const li = document.createElement("li");
  li.className = "todo-item";

  const span = document.createElement("span");
  span.textContent = task;

  const button = document.createElement("button");
  button.textContent = "Удалить";
  button.className = "todo-delete-btn";

  button.addEventListener("click", () => onDeleteTask(task));

  li.append(span, button);
  return li;
}
