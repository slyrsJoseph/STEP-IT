import React from "react";
import style from "../styles/Styles.module.css";
import { NavLink } from "react-router-dom";

const NavBar = () => {
  return (
    <nav className={style.nav}>
      <ul>
        <li>
          <NavLink
            to="/foodSpin"
            className={({ isActive }) =>
              isActive ? style["active-link"] : style["inactive-link"]
            }
          >
            <div className={style.logo}>FoodSpin</div>
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/breakfast"
            className={({ isActive }) =>
              isActive ? style["active-link"] : style["inactive-link"]
            }
          >
            Breakfast
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/lunch"
            className={({ isActive }) =>
              isActive ? style["active-link"] : style["inactive-link"]
            }
          >
            Lunch
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/dinner"
            className={({ isActive }) =>
              isActive ? style["active-link"] : style["inactive-link"]
            }
          >
            Dinner
          </NavLink>
        </li>
      </ul>
    </nav>
  );
};

export default NavBar;
