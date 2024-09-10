import { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Context } from "../App";

function Navbar() {
  const navigate = useNavigate();
  const context = useContext(Context);

  const handleLogOut = () => {
    context?.setToken(null);
    context?.setEmail(null);
    localStorage.clear();
    navigate("/");
  };

  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary">
      <div className="container-fluid">
        <Link to="/" className="navbar-brand">
          Menüü
        </Link>
        {context?.token ? (
          <>
            <div>Tere {context.email}!</div>
            <button
              className="btn btn-sm btn-outline-secondary"
              onClick={handleLogOut}
            >
              Logi välja
            </button>
            <Link to="/createEvent" className="navbar-brand">
              Loo Üritus
            </Link>
          </>
        ) : (
          <Link to="/login" className="btn btn-sm btn-outline-secondary">
            Logi sisse
          </Link>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
