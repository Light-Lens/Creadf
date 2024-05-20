# Creadf
Custom and better ReadLine methods for C#.

*NOTE: This project uses [**Dotnet Core 8**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)*

## How to Use?
1. Example usage with some basic syntax highlighting and text autocompletion.
```csharp
string prompt = "> ";

Dictionary<Creadf.Tokenizer.TokenType, ConsoleColor> SyntaxHighlightCodes = [];
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.STRING, ConsoleColor.Yellow);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.EXPR, ConsoleColor.Cyan);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.BOOL, ConsoleColor.Magenta);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.SYMBOL, ConsoleColor.White);
SyntaxHighlightCodes.Add(Creadf.Tokenizer.TokenType.COMMENT, ConsoleColor.DarkGray);

CreadfConfig config = new(
    LeftCursorStartPos: prompt.Length,
    TopCursorStartPos: Console.CursorTop,
    ToggleColorCoding: true,
    ToggleAutoComplete: true,
    Suggestions: ["test", "test2"],
    SyntaxHighlightCodes: SyntaxHighlightCodes
);

Creadf readline = new(Config);
Creadf.InitDefaultKeyBindings();

Console.Write(prompt);
string output = readline.Readf();
Console.WriteLine(output);
```

2. Structure of all config options that are available as of now.
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

3. The default keybindings and how to add more
```csharp
public void AddKeyBindings(ConsoleKey key, ConsoleModifiers modifier, Action action)
{
    KeyBindings.Add((key, modifier), action);
}

public void InitDefaultKeyBindings()
{
    AddKeyBindings(ConsoleKey.Enter, ConsoleModifiers.None, HandleEnter);
    AddKeyBindings(ConsoleKey.Enter, ConsoleModifiers.Control, HandleCtrlEnter);

    AddKeyBindings(ConsoleKey.Tab, ConsoleModifiers.None, HandleTab);
    AddKeyBindings(ConsoleKey.Spacebar, ConsoleModifiers.Control, HandleCtrlSpacebar);

    AddKeyBindings(ConsoleKey.Escape, ConsoleModifiers.None, HandleEscape);
    AddKeyBindings(ConsoleKey.Escape, ConsoleModifiers.Shift, HandleShiftEscape);

    AddKeyBindings(ConsoleKey.Home, ConsoleModifiers.None, HandleHome);
    AddKeyBindings(ConsoleKey.End, ConsoleModifiers.None, HandleEnd);

    AddKeyBindings(ConsoleKey.Delete, ConsoleModifiers.None, HandleDelete);
    AddKeyBindings(ConsoleKey.Delete, ConsoleModifiers.Control, HandleCtrlDelete);

    AddKeyBindings(ConsoleKey.Backspace, ConsoleModifiers.None, HandleBackspace);
    AddKeyBindings(ConsoleKey.Backspace, ConsoleModifiers.Control, HandleCtrlBackspace);

    AddKeyBindings(ConsoleKey.LeftArrow, ConsoleModifiers.None, HandleLeftArrow);
    AddKeyBindings(ConsoleKey.LeftArrow, ConsoleModifiers.Control, HandleCtrlLeftArrow);

    AddKeyBindings(ConsoleKey.RightArrow, ConsoleModifiers.None, HandleRightArrow);
    AddKeyBindings(ConsoleKey.RightArrow, ConsoleModifiers.Control, HandleCtrlRightArrow);
}
```

4. All supported TokenTypes by the current Tokenizer of Creadf
```csharp
public partial class Tokenizer
{
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

    public struct Token(string Name, TokenType Type)
    {
        public string Name = Name;
        public TokenType Type = Type;
    }
    ...
}
```
