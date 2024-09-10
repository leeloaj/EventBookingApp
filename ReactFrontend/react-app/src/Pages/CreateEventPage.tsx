import { useContext, useEffect } from "react";
import { Context } from "../App";
import { useNavigate } from "react-router-dom";
import CreateEventForm from "../Components/CreateEventForm";

function CreateEventPage() {
  const context = useContext(Context);
  const navigate = useNavigate();

  useEffect(() => {
    if (context?.token === null) navigate("/");
  }, []);

  return (
    <>
      <h2>Loo üritus</h2>
      <CreateEventForm />
    </>
  );
}

export default CreateEventPage;
