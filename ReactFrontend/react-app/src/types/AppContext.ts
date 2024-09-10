export default interface AppContextType {
    token: string | null,
    setToken: (c: string | null) => void
    email: string | null
    setEmail: (c: string | null) => void
}