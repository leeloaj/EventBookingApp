import { ChangeEvent, FormEvent, useContext, useState } from "react";
import CreateEventDto from "../types/CreateEventDto";
import { createEvent } from "../apiRequests/apiRequests";
import { Context } from "../App";
import { useNavigate } from "react-router";

function CreateEventForm() {
  const context = useContext(Context);
  const navigate = useNavigate();

  const [formData, setFormData] = useState<CreateEventDto>({
    name: "",
    date: "",
    maxParticipants: 0,
    time: "",
  });

  const [message, setMessage] = useState<string>("");

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!formData.name) {
      setMessage("Ürituse nimi puudu!");
      return;
    }
    if (!formData.date) {
      setMessage("Ürituse toimumise aeg puudu!");
      return;
    }
    if (!formData.time) {
      setMessage("Ürituse toimumise kellaaeg puudu!");
      return;
    }
    if (!formData.maxParticipants) {
      setMessage("Ürituse maksimaalsete osalejate arv puudu!");
      return;
    }

    const ok = await createEvent(formData, context.token);
    if (ok) navigate("/");
    if (!ok) setMessage("Loomine ebaõnnestus!");
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
        <label htmlFor="name">Ürituse nimi</label>
        <input
          type="text"
          className="form-control"
          id="name"
          name="name"
          placeholder="Sisesta ürituse nimi"
          onChange={handleChange}
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="date">Ürituse toimumisaeg</label>
        <input
          type="date"
          className="form-control"
          id="date"
          name="date"
          onChange={handleChange}
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="time">Ürituse toimumise kellaaeg</label>
        <input
          type="time"
          className="form-control"
          id="time"
          name="time"
          onChange={handleChange}
        />
      </div>
      <div className="form-group mt-2">
        <label htmlFor="maxParticipants">Maksimaalne osalejate arv</label>
        <input
          type="number"
          className="form-control"
          id="maxPparticipants"
          name="maxParticipants"
          placeholder="Sisesta maksimaalne osalejate arv"
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

export default CreateEventForm;
