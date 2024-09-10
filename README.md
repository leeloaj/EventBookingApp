# Ürituste registreerimise rakendus

Rakendus koosneb backendist ja frontendist.

Backend on tehtud .NET8 rakendusena. Andmebaas jookseb localhosti peal ja migreerub rakenduse käivitamisel.

Frontend on tehtud Reacti ja typescriptiga.

Backendi käimapanekuks on launchsettings.json failis defineeritud profiil, mis laseb rakendust käivitada IDEs nupuvajutusega või terminalis kui cd `EventBookingApp\WebApi` kausta ja kasutada käsku `dotnet run`

Fronti projekti jooksutamiseks tuleb terminalis minna `EventBookingApp\ReactFrontend\react-app>` kausta ja joosta käsklusi `npm i` ja `npm run dev`

API testimiseks on rakenduses swagger, frontendis saab üritusi näha ja nendele registreeruda ning sisse logida adminia. Admini sisselogimiandmed on appsettings.json failis. Adminina sisse logides saab üritusi luua.