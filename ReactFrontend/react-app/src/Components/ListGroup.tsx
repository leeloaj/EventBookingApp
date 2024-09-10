import { useEffect, useState } from "react";
import { getEvents } from "../apiRequests/apiRequests";
import EventDto from "../types/EventDto";
import RegistrationForm from "./RegistrationForm";

function ListGroup() {
  const [events, setEvents] = useState<EventDto[]>([]);

  useEffect(() => {
    const fetchEvents = async () => {
      setEvents(await getEvents());
    };

    fetchEvents();
  }, []);

  const handleButtonClick = (event: EventDto) => {
    event.showForm = !event.showForm;
    const index = events.findIndex((e) => e.id === event.id);
    events[index] = event;
    setEvents([...events]);
  };

  return (
    <>
      <ul className="list-group">
        {events.length ? (
          events.map((event, index) => (
            <div key={index}>
              <li className="list-group-item d-flex justify-content-between align-items-center">
                <div>
                  {event.name} - {new Date(event.date).toLocaleString()}
                </div>
                <div>
                  <div className="mr-2">
                    {event.registrations.length}/{event.maxParticipants}
                  </div>
                  {event.registrations.length !== event.maxParticipants && (
                    <button
                      className="btn btn-primary"
                      onClick={() => handleButtonClick(event)}
                    >
                      Registreeri
                    </button>
                  )}
                </div>
              </li>
              {event.showForm && <RegistrationForm eventId={event.id} />}
            </div>
          ))
        ) : (
          <div className="alert alert-warning">Hetkel ei ole üritusi</div>
        )}
      </ul>
    </>
  );
}

export default ListGroup;
