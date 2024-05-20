# Creadf
Custom and better ReadLine methods for C#.

> [!NOTE]
> This project uses [**Dotnet Core 8**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## How to Use?
1. Setting up a prompt. (**optional**)
```csharp
string prompt = "> ";
```



2. Configure everything before reading the line
> [!IMPORTANT]
> It is a must to do the configuration before reading the line, otherwise it won't work.

- Configure everything in the `CreadfConfig` 
```csharp
CreadfConfig config = new(
    LeftCursorStartPos: prompt.Length,
    TopCursorStartPos: Console.CursorTop,
    ToggleColorCoding: true,
    ToggleAutoComplete: true,
    Suggestions: ["test", "test2"],
    SyntaxHighlightCodes: SyntaxHighlightCodes
);
```

```csharp
CreadfConfig config = new(
    LeftCursorStartPos: Console.CursorLeft,
    TopCursorStartPos: Console.CursorTop,
    ToggleColorCoding: false,
    ToggleAutoComplete: false,
    Suggestions: null,
    SyntaxHighlightCodes: null
);
```

Structure of all config options that are available as of now.
```csharp
struct CreadfConfig(int LeftCursorStartPos, int TopCursorStartPos, Dictionary<Creadf.Tokenizer.TokenType, ConsoleColor> SyntaxHighlightCodes, bool ToggleAutoComplete = true, bool ToggleColorCoding = true, List<string> Suggestions = null)
{
    public int LeftCursorStartPos { get; set; } = LeftCursorStartPos;
    public int TopCursorStartPos { get; set; } = TopCursorStartPos;
    public bool ToggleAutoComplete { get; set; } = ToggleAutoComplete;
    public bool ToggleColorCoding { get; set; } = ToggleColorCoding;
    public List<string> Suggestions { get; set; } = Suggestions ?? ([]);
    public Dictionary<Creadf.Tokenizer.TokenType, ConsoleColor> SyntaxHighlightCodes { get; set; } = SyntaxHighlightCodes;
}
```

- Configuring the Syntax color coding
```csharp
Dictionary<Creadf.Tokenizer.TokenType, ConsoleColor> SyntaxHighlightCodes = [];
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.STRING, ConsoleColor.Yellow);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.EXPR, ConsoleColor.Cyan);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.BOOL, ConsoleColor.Magenta);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.SYMBOL, ConsoleColor.White);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.COMMENT, ConsoleColor.DarkGray);
```

All supported TokenTypes by the current Tokenizer of Creadf
```csharp
public enum TokenType
{
    EOL = 0,
    COMMENT,
    WHITESPACE,
    IDENTIFIER,
    STRING,
    BOOL,
    EXPR,
    SYMBOL,
}
```

3. Initialize the `Creadf` object
```csharp
Creadf readline = new(Config);
```



4. Initialize the default keybindings
```csharp
Creadf.InitDefaultKeyBindings();
```

Where the default keybindings are the following:

| Shortcut              | Comment                         |
| --------------------- | --------------------------------|
| `End`                 | Send end of line                |
| `Tab`                 | Change autocomplete suggestions |
| `Home`                | Send start of line              |
| `Escape`              | Clear suggestions               |
| `Delete`              | Delete succeeding character     |
| `Backspace`           | Delete previous character       |
| `LeftArrow`           | Backward one character          |
| `RightArrow`          | Forward one character           |
| `Shift`+`Escape`      | Clear input and suggestions     |
| `Ctrl`+`Enter`        | Accept current suggestion       |
| `Ctrl`+`Spacebar`     | Show current suggestions        |
| `Ctrl`+`Delete`       | Delete succeeding token         |
| `Ctrl`+`Backspace`    | Delete previous token           |
| `Ctrl`+`LeftArrow`    | Backward one token              |
| `Ctrl`+`RightArrow`   | Forward one token               |

However you can add more or other Keybindings by using the below method:

```csharp
public void AddKeyBindings(ConsoleKey key, ConsoleModifiers modifier, Action action)
```

Replace `InitDefaultKeyBindings` by the below code
```csharp
// Creadf.InitDefaultKeyBindings();
AddKeyBindings(ConsoleKey.Enter, ConsoleModifiers.Control, HandleCtrlEnter);
```



5. Start taking input
> [!IMPORTANT]
> If prompt exists, then it is a must to print it before calling the `Readf` method.

```csharp
Console.Write(prompt);
string output = readline.Readf();
Console.WriteLine(output);
```
