import Dashboard from "../interface/board";
import Card from "../interface/cards";
import styles from "./navigation.module.css";

const Panel = () => {
  return (
    <div className={styles.panel}>
      <nav className={styles.navigation}>
        <h1>BankDash</h1>
        <ul>
          <li className={styles.active}>Dashboard</li>
          <li>Transactions</li>
          <li>Accounts</li>
          <li>Investments</li>
          <li>Credit Cards</li>
          <li>Loans</li>
          <li>Services</li>
          <li>My Privileges</li>
          <li>Settings</li>
        </ul>
      </nav>
      <div className={styles.content}>
        <Dashboard />
        <Card />
      </div>
    </div>
  );
};

export default Panel;
