struct CreadfConfig(int LeftCursorStartPos, int TopCursorStartPos, Dictionary<Creadf.Tokenizer.TokenType, ConsoleColor> SyntaxHighlightCodes, bool ToggleAutoComplete = true, bool ToggleColorCoding = true, List<string> Suggestions = null)
{
    public int LeftCursorStartPos { get; set; } = LeftCursorStartPos;
    public int TopCursorStartPos { get; set; } = TopCursorStartPos;
    public bool ToggleAutoComplete { get; set; } = ToggleAutoComplete;
    public bool ToggleColorCoding { get; set; } = ToggleColorCoding;
    public List<string> Suggestions { get; set; } = Suggestions ?? ([]);
    public Dictionary<Creadf.Tokenizer.TokenType, ConsoleColor> SyntaxHighlightCodes { get; set; } = SyntaxHighlightCodes;
}

class CursorVec3
{
    public int X { get; set; } = 0; // Cursor left
    public int Y { get; set; } = 0; // Cursor top
    public int I { get; set; } = 0; // Cursor pos on text buffer
}
