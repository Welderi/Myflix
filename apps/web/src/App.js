import './App.css';
// import { useEffect, useState } from "react";
import Login from "./components/login/login";
import Register from "./components/register/register";
import Main from "./components/main/main";
import {BrowserRouter as Router, Routes, Route, Navigate} from "react-router-dom";

function App() {
  return (
      <Router>
          <div className="App">
              <Routes>
                  <Route path="/" element={<Main />} />
                  <Route path="/login" element={<Login />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="*" element={<Navigate to="/" replace />} />
              </Routes>
          </div>
      </Router>
  );
}

export default App;
