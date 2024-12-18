import Panel from "./components/navigation/navigation";
import Card from "./components/interface/cards";
import styles from "./App";

const App = () => {
  return (
    <div className={styles.app}>
      <Panel />
      <Card />
    </div>
  );
};

export default App;
