import { useState } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import Layout from "./components/Layout";
import Home from "./pages/Home";
import Profile from "./pages/Profile";
import About from "./pages/About";
import Authorize from "./pages/Authorize";
import Dispatcher from "./pages/Dispatcher";
import Changes from "./pages/Changes";
import AdminPanel from "./pages/AdminPanel";

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/about" element={<About />} />
          <Route path="/auth" element={<Authorize />} />
          <Route path="/load" element={<Dispatcher />} />
          <Route path="/changes" element={<Changes />} />
          <Route path="/admin" element={<AdminPanel />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
