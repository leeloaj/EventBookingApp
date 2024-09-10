import { Outlet } from "react-router-dom";
import Navbar from "./Components/Navbar";
import { createContext, useState } from "react";
import AppContextType from "./types/AppContext";

export const Context = createContext<AppContextType>({
  email: null,
  setEmail: function () {},
  token: null,
  setToken: function () {},
});

function App() {
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("token")
  );
  const [email, setEmail] = useState<string | null>(
    localStorage.getItem("email")
  );

  return (
    <Context.Provider value={{ token, setToken, email, setEmail }}>
      <div className="container">
        <main>
          <Navbar />
          <Outlet />
        </main>
      </div>
    </Context.Provider>
  );
}

export default App;
