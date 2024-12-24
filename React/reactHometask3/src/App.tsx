import "./App.css";
import { TextInputs } from "./components/TextInputs";
import Checkbox from "./components/Checkbox";
import { RadioButtons } from "./components/RadioButtons";
import Dropdowns from "./components/Dropdowns";
import Buttons from "./components/Buttons";

function App() {
  return (
    <form action="" className="form">
      <TextInputs />
      <Checkbox />
      <RadioButtons />
      <Dropdowns />
      <Buttons />
    </form>
  );
}

export default App;
