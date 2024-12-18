import styles from "./cards.module.css";

const Card = () => {
  return (
    <div className={styles.card}>
      <div className={styles.cardContent}>
        <h3>Balance</h3>
        <p className={styles.amount}>$5,756</p>
        <p className={styles.cardHolder}>CARD HOLDER</p>
        <p className={styles.name}>Eddy Cusuma</p>
        <div className={styles.cardDetails}>
          <p>4098 **** **** 0947</p>
          <p>VALID THRU 12/22</p>
        </div>
      </div>
    </div>
  );
};

export default Card;
