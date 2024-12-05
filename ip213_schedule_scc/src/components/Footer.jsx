import React from "react";
import { Link } from "react-router-dom";
import './Footer.scss';


const Footer = () => {
  return (
    <>
      <footer>
        <div className="footer-container">
          <ul className="footer-links">
            
            <li><Link to ='about'>О нас</Link></li>
          </ul>
          <p>© 2024 Все права защищены.</p>
        </div>
      </footer>
    </>
  )
};

export default Footer;
