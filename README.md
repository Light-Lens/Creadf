# Creadf
Custom and better ReadLine methods for C#.

*NOTE: This project uses [**Dotnet Core 8**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)*

## Example usage
```csharp
string prompt = "> ";

Dictionary<ReadLine.Tokenizer.TokenType, ConsoleColor> SyntaxHighlightCodes = [];
SyntaxHighlightCodes.Add(ReadLine.Tokenizer.TokenType.STRING, ConsoleColor.Yellow);
SyntaxHighlightCodes.Add(ReadLine.Tokenizer.TokenType.EXPR, ConsoleColor.Cyan);
SyntaxHighlightCodes.Add(ReadLine.Tokenizer.TokenType.BOOL, ConsoleColor.Magenta);
SyntaxHighlightCodes.Add(ReadLine.Tokenizer.TokenType.SYMBOL, ConsoleColor.White);
SyntaxHighlightCodes.Add(ReadLine.Tokenizer.TokenType.COMMENT, ConsoleColor.DarkGray);

ReadLineConfig config = new(
    LeftCursorStartPos: prompt.Length,
    TopCursorStartPos: Console.CursorTop,
    Toggle_color_coding: true,
    Toggle_autocomplete: true,
    Suggestions: ["test", "test2"],
    SyntaxHighlightCodes: SyntaxHighlightCodes
);

ReadLine readline = new(Config);
readline.InitDefaultKeyBindings();

Console.Write(prompt);
string output = readline.Readf();
Console.WriteLine(output);
```
