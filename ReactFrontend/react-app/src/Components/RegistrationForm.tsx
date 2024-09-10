import { useState, FormEvent, ChangeEvent } from "react";
import { createRegistration } from "../apiRequests/apiRequests";
import CreateRegisterDto from "../types/CreateRegisterDto";

interface Props {
  eventId: number;
}

function RegistrationForm({ eventId }: Props) {
  const [formData, setFormData] = useState<CreateRegisterDto>({
    firstname: "",
    lastname: "",
    idcode: "",
    eventId: eventId,
  });
  const [message, setMessage] = useState<string>("");

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!formData.firstname) {
      setMessage("Eesnimi puudu!");
      return;
    }
    if (!formData.lastname) {
      setMessage("Perekonnanimi puudu!");
      return;
    }
    if (!formData.idcode) {
      setMessage("Isikukood puudu!");
      return;
    }

    setMessage(await createRegistration(formData));
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
      <div className="form-group mt-2">
        <label htmlFor="firstname">Eesnimi</label>
        <input
          type="text"
          className="form-control"
          id="firstname"
          name="firstname"
          placeholder="Sisesta eesnimi"
          onChange={handleChange}
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="lastname">Pererkonnanimi</label>
        <input
          type="text"
          className="form-control"
          id="lastname"
          name="lastname"
          placeholder="Sisesta perekonnanimi"
          onChange={handleChange}
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="idcode">Isikukood</label>
        <input
          type="text"
          className="form-control"
          id="idcode"
          name="idcode"
          placeholder="Sisesta isikukood"
          onChange={handleChange}
        />
      </div>
      <button type="submit" className="btn btn-success mt-3 mb-3">
        Registreeri
      </button>
      {message && <div className="alert alert-warning">{message}</div>}
    </form>
  );
}

export default RegistrationForm;
