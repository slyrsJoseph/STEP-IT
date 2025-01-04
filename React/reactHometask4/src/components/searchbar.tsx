import React, { useState } from "react";

export const Searchbar: React.FC<{ onSubmit: (query: string) => void }> = ({
  onSubmit,
}) => {
  const [query, setQuery] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(query);
    setQuery("");
  };

  return (
    <header className="searchbar">
      <form className="form" onSubmit={handleSubmit}>
        <button type="submit" className="button">
          <span className="button-label">Search</span>
        </button>
        <input
          className="input"
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Search image"
        />
      </form>
    </header>
  );
};
