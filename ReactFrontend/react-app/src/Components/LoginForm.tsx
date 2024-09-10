import { ChangeEvent, FormEvent, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import LoginDto from "../types/LoginDto";
import { logIn } from "../apiRequests/apiRequests";
import { Context } from "../App";

function LoginForm() {
  const context = useContext(Context);
  const [formData, setFormData] = useState<LoginDto>({
    email: "",
    password: "",
  });

  const [message, setMessage] = useState<string>("");

  const navigate = useNavigate();

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!formData.email) {
      setMessage("Email puudu!");
      return;
    }
    if (!formData.password) {
      setMessage("Parool puudu!");
      return;
    }

    const response = await logIn(formData);
    if (response === null) {
      setMessage("Sisselogimine ebaõnnestus");
      return;
    }

    context?.setEmail(response.email);
    context?.setToken(response.token);
    localStorage.setItem("email", response.email);
    localStorage.setItem("token", response.token);
    navigate("/");
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="email">Email aadress</label>
        <input
          type="email"
          className="form-control"
          id="email"
          name="email"
          aria-describedby="emailHelp"
          placeholder="Sisesta email"
          value={formData.email}
          onChange={handleChange}
        />
      </div>
      <div className="form-group">
        <label htmlFor="exampleInputPassword1">Parool</label>
        <input
          type="password"
          className="form-control"
          id="password"
          name="password"
          placeholder="Sisesta parool"
          value={formData.password}
          onChange={handleChange}
        />
      </div>
      <br />
      <button type="submit" className="btn btn-primary">
        Logi sisse
      </button>
      {message && <div className="alert alert-warning">{message}</div>}
    </form>
  );
}

export default LoginForm;
