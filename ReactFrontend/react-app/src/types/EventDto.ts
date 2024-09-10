import Registration from "./RegistrationDto";

export default interface EventDto {
    id: number,
    name: string,
    date: Date,
    maxParticipants: number,
    registrations: Registration[],
    showForm: boolean
}