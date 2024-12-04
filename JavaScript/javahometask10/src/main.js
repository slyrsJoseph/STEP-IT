import { Header } from "./components/Header.js";
import { Footer } from "./components/Footer.js";
import { TodoForm } from "./components/TodoForm.js";
import { TodoList } from "./components/TodoList.js";
import { saveTasks, loadTasks } from "./utils/localstorage.js";
import "./styles/styles.css";

const app = document.querySelector("#app");

const tasks = loadTasks();

const header = Header();
const footer = Footer();
const todoListContainer = document.createElement("div");
todoListContainer.className = "todo-list-container";

const onAddTask = (task) => {
  tasks.push(task);
  saveTasks(tasks);
  const todoList = TodoList([task], onDeleteTask);
  todoListContainer.appendChild(todoList.firstChild);
};

const onDeleteTask = (taskToDelete) => {
  const index = tasks.indexOf(taskToDelete);
  if (index > -1) {
    tasks.splice(index, 1);
    saveTasks(tasks);

    const items = todoListContainer.querySelectorAll(".todo-item");
    items.forEach((item) => {
      if (item.querySelector("span").textContent === taskToDelete) {
        todoListContainer.removeChild(item);
      }
    });
  }
};

const form = TodoForm(onAddTask);
const initialTodoList = TodoList(tasks, onDeleteTask);

todoListContainer.appendChild(initialTodoList);
app.append(header, form, todoListContainer, footer);
