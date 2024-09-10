import CreateEventDto from "../types/CreateEventDto";
import CreateRegisterDto from "../types/CreateRegisterDto";
import EventDto from "../types/EventDto";
import LoginDto from "../types/LoginDto";
import LoginResponse from "../types/LoginResponse";

const apiBaseUrl = 'https://localhost:7230/api/';

export async function getEvents(): Promise<EventDto[]> {
      const response = await fetch(apiBaseUrl + "events");
      const data: EventDto[] = await response.json();
      
      return data;
}

export async function createRegistration(dto: CreateRegisterDto): Promise<string> {
      const response = await fetch(apiBaseUrl + "events/register", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
      })

    if (response.ok) {
        return 'Registreerimine õnnestus';
    }
    
    return "Registreerimine ebaõnnestus";
}

export async function logIn(dto: LoginDto): Promise<LoginResponse | null> {
      const response = await fetch(apiBaseUrl + "users/login", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
      })

    if (response.ok) {
        const data: LoginResponse = await response.json();
        return data;
    }
    
    return null
  }

export async function createEvent(dto: CreateEventDto, token: string | null): Promise<boolean> {  
  const dateSplit = dto.date.split("-");
  const timeSplit = dto.time.split(":");
  
  const dateTime = new Date(
    Number(dateSplit[2]),
    Number(dateSplit[1]),
    Number(dateSplit[0]),
    Number(timeSplit[0]),
    Number(timeSplit[1]))
  dto.date = dateTime.toISOString();
  
  const response = await fetch(apiBaseUrl + "events/create", {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token,
    },
    body: JSON.stringify(dto)
  })
    
  return response.ok;
}