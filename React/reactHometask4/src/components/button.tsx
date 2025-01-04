import React from "react";

export const Button: React.FC<{ onClick: () => void }> = ({ onClick }) => {
  return <button className="button" onClick={onClick}></button>;
};
