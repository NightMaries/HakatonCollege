import React from "react"
import { Outlet } from 'react-router-dom';
import Header from "./Header";
import Footer from "./Footer";
import './Layout.scss'

const Layout = () => {
  return (
    <>
      <Header />
      <div className="main">
        <Outlet />
      </div>
      <Footer />
    </>
  )
};

export default Layout;
