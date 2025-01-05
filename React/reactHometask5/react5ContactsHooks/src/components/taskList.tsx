import React, { useEffect, useState } from "react";

type Props = {
  numbers: number[];
  names: string[];
};

const TaskList: React.FC<Props> = (props) => {
  const [firstLoad, setFirstLoad] = useState(false);

  useEffect(() => {
    setFirstLoad(true);
  }, []);

  const deleteTask = (event: React.MouseEvent<HTMLButtonElement>) => {
    const button = event.currentTarget;
    const li = button.parentNode as HTMLElement | null;

    if (!li) return;

    const numbers = JSON.parse(localStorage.getItem("Numbers") || "[]");
    const names = JSON.parse(localStorage.getItem("Names") || "[]");

    const taskText = li.textContent?.trim();
    let index = -1;

    for (let i = 0; i < numbers.length; i += 1) {
      if (taskText && taskText.includes(numbers[i].toString())) {
        index = i;
        break;
      }
    }

    if (index !== -1) {
      numbers.splice(index, 1);
      names.splice(index, 1);
      localStorage.setItem("Numbers", JSON.stringify(numbers));
      localStorage.setItem("Names", JSON.stringify(names));
    }

    li.remove();
  };

  const rendElement = (numbers: number[], names: string[]) => {
    return numbers.map((number, index) => (
      <li key={index}>
        {number} - {names[index]}
        <button onClick={deleteTask}>delete</button>
      </li>
    ));
  };

  const rendFromDB = () => {
    const numbersData = localStorage.getItem("Numbers");
    const namesData = localStorage.getItem("Names");
    if (numbersData && namesData) {
      const numbers = JSON.parse(numbersData);
      const names = JSON.parse(namesData);
      return numbers.map((number: number, index: number) => (
        <li key={index}>
          {number} - {names[index]}
          <button onClick={deleteTask}>delete</button>
        </li>
      ));
    }
  };

  return (
    <ul>
      {firstLoad ? rendFromDB() : rendElement(props.numbers, props.names)}
    </ul>
  );
};

export default TaskList;
