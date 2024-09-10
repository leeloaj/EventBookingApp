import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import EventPage from "../Pages/EventPage";
import LogInPage from "../Pages/LogInPage";
import CreateEventPage from "../Pages/CreateEventPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/",
        element: <EventPage />,
      },
      {
        path: "/login",
        element: <LogInPage />,
      },
      {
        path: "/createEvent",
        element: <CreateEventPage />,
      },
    ],
  },
]);

export { router };
