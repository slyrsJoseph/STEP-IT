export function TodoForm(onAddTask) {
  const form = document.createElement("form");
  form.className = "todo-form";

  const input = document.createElement("input");
  input.type = "text";
  input.placeholder = "Введите задачу";
  input.className = "todo-input";

  const button = document.createElement("button");
  button.type = "submit";
  button.textContent = "Добавить";
  button.className = "todo-add-btn";

  form.append(input, button);

  form.addEventListener("submit", (event) => {
    event.preventDefault();
    if (input.value.trim()) {
      onAddTask(input.value.trim());
      input.value = "";
    }
  });

  return form;
}
