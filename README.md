# Peter Pedals Cykelværksted — Clean Code-case

## Casen

Peter Pedals Cykelværksted har fået et nyt system. Hvor mekanikeren Sofia før skrev sine fund på en lap papir og lagde den på Peters kontor, registrerer hun nu fundene **direkte i systemet**.

Systemet er skrevet — men det er skrevet i hast, og ingen har ryddet op i det siden. Det er jeres opgave at gøre noget ved det.

## Kom i gang

1. Klik **Use this template** - den grønne knap øverst i højre hjørne, og opret dit eget repo. 

2. Klon dit nye repo ned på din maskine:
   ```
   git clone <URL på dit nye repo>
   ```

3. Åbn en terminal, og tjek at du har en .NET SDK installeret:
   ```
   dotnet --list-sdks
   ```

   Projektet targeter `net8.0`, men kan bygges og køres med SDK 8, 9 eller 10.

4. Naviger til projektmappen og kør programmet:
   ```
   cd PeterPedal
   dotnet run
   ```

5. Se hvad programmet printer i terminalen, **før du ændrer noget** — det er Egons sag, spillet igennem fra indlevering til afhentning.

## Reglen: adfærden må ikke ændre sig

Der er ingen tests i denne øvelse. Jeres sikkerhedsnet er, at `dotnet run` skal give
**præcis det samme output** før og efter jeres ændringer. Kør programmet ofte undervejs.
Ændrer outputtet sig, har I ændret noget mere end strukturen — og det er ikke længere en
refaktorering.

## Opgave

### Del 1 — Find code smells

Læs `PeterPedal/Program.cs`. I skal **ikke** rette noget endnu — kun læse og skrive ned.

Lav en liste med, for hver smell I ser:
- Hvilket princip fra dagens forelæsning bryder det med?
- Hvad er problemet?
- Hvor er det (fil og linje)?

Der er **13 forskellige code smells** i koden (derudover er der også nogle C#-konventionsbrud i navngivning og formatering — de skal også på listen). Bemærk at den samme smell godt kan optræde flere steder i koden.

### Del 2 — Refaktorér 

1. Opret et issue for hvert fund fra listen over liste (se konvention nedenfor)
2. Opret en branch, ret én ting ad gangen, commit med conventional commits

I skal **ikke** åbne eller merge en pull request — det er næste uges stof. Jeres
branches og commits skal bare stå klar i jeres repo.

### Issue-konvention

- **Ét issue pr. fund**
- **Titlen siger, hvad du har tænkt dig at gøre** — skriv den i bydeform, fx
  `Erstat magic numbers i prisberegningen med konstanter`, ikke `Magic numbers i prisberegningen`
- Så bruger issue-titel og commit-besked samme udsagnsord, og kæden hænger sammen:
  issue `Erstat magic numbers...` → commit `refactor(pricing): replace magic numbers...` → `Refs #7`

### Commit-konvention

Følg conventional commits, som vi gennemgik i dag:
```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```


## Ekstraopgave (ikke obligatorisk)

1. **Udskil klasserne i hver sin fil.** Lige nu ligger alt i `Program.cs`. Når koden er clean kan du dele ansvar ud. 

2. **Tilføj en rabat:** Peter vil give 20 % rabat på reservedele til faste kunder. Læg mærke til: hvor mange steder i koden skal du rette, for at det virker overalt?
