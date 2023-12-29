# Projekt: TodoApplication

TodoApplication är ett web API med integrerad frontend och gör det möjligt för användaren att skapa och hantera en lista på saker som ska göras. Användaren kan lägga till, ta bort och markera anteckning som färdig.

## Teknologier

- **Frontend:** HTML, CSS, JavaScript
- **Backend:** ASP.NET Core Web API 6.0, xUnit 6.0

## Struktur och arkitektur

- **Frontend:** "index.html", "style.css", "script.js"
- **Backend:** ApiController ("NoteProcessor"), Models

## Frontend-komponenter

### Startsidan

Innehåller en lista som användaren kan fylla på med anteckningar. Användaren har möjlighet att lägga till, ta bort och markera sina anteckningar. JavaScript-filen innehåller logiken för att hantera användarens interaktion med applikationen. Den använder Fetch API för att göra ett API-anrop för att skapa, markera och ta bort anteckningar.

#### Exempel på API-anrop:

```javascript
addForm.onsubmit = async event => {
    event.preventDefault();

    const noteText = addInput.value;
    if (noteText.trim() !== '') {
        
        addInput.value = '';

        const note = {
            text: noteText,
            isDone: false,
        };
        await fetch(api + '/notes', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(note),
        });

        loadNotes();
    }
};
```

## Backend-funktionalitet

### ApiController

Innehåller logik för att hantera API-anrop och dirigerar dem till rätt metod i applikationen. Den inkluderar HTTP-metoder som GET, POST, PUT och DELETE och hanterar användares anteckningar.

#### Exempel på en ApiController-metod:

```csharp
[HttpPost]
public void Post(Note post)
{
    if (post == null)
    {
        throw new ArgumentNullException(nameof(post));
    }

    post.Id = counter++;
    notes.Add(post);
}
````
### xUnit-tester
Syfte med att använda xUnit är att säkerhetsställa att korrekt funktionalitet och upptäcka potentiella problem i applikationen. Ett separat xUnit-test har lagts till i solution-filen för att hantera enhetstester.

#### Exempel på xUnit-test:

```csharp
[Fact]
public void Should_Post_One_Note()
{
	//ARRANGE
	var post = new Note
	{
		Text = "Shop",
		IsDone = false
	};

	//ACT
	_processor.Post(post);

	List<Note> postNote = _processor.GetAllNotes();

	//ASSERT
	Assert.NotNull(postNote);
	Assert.Single(postNote);
}

