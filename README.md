# Peter Pedals Cykelværksted — Clean Code-case

## Casen

Peter Pedals Cykelværksted har fået et nyt system. Hvor mekanikeren Sofia før skrev sine fund på en lap papir og lagde den på Peters kontor, registrerer hun nu fundene **direkte i systemet**.

Systemet er skrevet — men det er skrevet i hast, og ingen har ryddet op i det siden. Det er jeres opgave at gøre noget ved det.

## Kom i gang

1. Åbn en terminal.

2. Tjek at du har en .NET SDK installeret:
   ```
   dotnet --list-sdks
   ```

   Projektet targeter `net8.0`, men kan bygges og køres med SDK 8, 9 eller 10.

3. Naviger til projektmappen og kør programmet:
   ```
   cd PeterPedal
   dotnet run
   ```


4. Se hvad programmet printer i terminalen, **før du ændrer noget** — det er Egons sag,
   spillet igennem fra indlevering til afhentning.

## Reglen: adfærden må ikke ændre sig

Der er ingen tests i denne øvelse. Jeres sikkerhedsnet er, at `dotnet run` skal give
**præcis det samme output** før og efter jeres ændringer. Kør programmet ofte undervejs.
Ændrer outputtet sig, har I ændret noget mere end strukturen — og det er ikke længere en
refaktorering.

## Sådan arbejder vi

### Fase 1 — Find code smells (to og to)

Læs `PeterPedal/Program.cs`. I skal **ikke** rette noget endnu — kun læse og skrive ned.

Lav en liste med, for hver smell I ser:
- Hvilket princip fra dagens forelæsning bryder det med?
- Hvad er problemet?
- Hvor er det (fil og linje)?

Der er **13 forskellige code smells** i koden (derudover er der også nogle C#-konventionsbrud i navngivning og formatering — de skal også på listen).

### Fase 2 — Refaktorér (individuelt)

Nu arbejder I hver for sig i jeres eget repo.

1. Klik **Use this template** øverst på dette repo på GitHub, og opret dit eget repo
2. `git clone` dit nye repo
3. Opret et issue for hvert fund fra jeres fælles liste (se konvention nedenfor)
4. Opret en branch, ret én ting ad gangen, commit med conventional commits
5. `git push`

I skal **ikke** åbne eller merge en pull request i dag — det er næste uges stof. Jeres
branches og commits skal bare stå klar i jeres repo.

### Issue-konvention

- **Ét issue pr. fund**
- **Titlen beskriver problemet, ikke løsningen** — fx `Magic numbers i prisberegningen`,
  ikke `Lav konstanter`

### Commit-konvention

Følg conventional commits, som vi gennemgik i dag:

<type>[optional scope]: <description>

[optional body]

[optional footer(s)]


## Ekstraopgave

1. **Udskil klasserne i hver sin fil.** Lige nu ligger alt i `Program.cs`. Når koden er
   ryddet op, er det tydeligt hvad der hører sammen.
2. **Tilføj en rabat:** Peter vil give 20 % rabat på reservedele til faste kunder. Læg
   mærke til: hvor mange steder i koden skal du rette, for at det virker overalt?
