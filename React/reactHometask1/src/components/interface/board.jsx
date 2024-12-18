import styles from "./board.module.css";

const Dashboard = () => {
  return (
    <div className={styles.dashboard}>
      <h2 className={styles.title}>My Cards</h2>
      <div className={styles.cardsContainer}></div>
    </div>
  );
};
export default Dashboard;
