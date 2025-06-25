import './App.css';
import { useEffect, useState } from "react";

function App() {
  const [data, setData] = useState('');

  useEffect(() => {
    fetch("http://localhost:5000/api/tmdb")
        .then(res => res.text())
        .then(data => {
          console.log(data);
          setData(data);
        })
        .catch(err => console.error(err));
  }, []);

  return (
      <div className="App">
        <h1>API:</h1>
        <pre>{data}</pre>
      </div>
  );
}

export default App;
