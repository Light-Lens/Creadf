partial class Creadf
{
    private string RenderedTextBuffer = "";
    private Tokenizer tokenizer;

    private void UpdateBuffer(bool RenderSuggestions=true)
    {
        ClearTextBuffer();
        RenderTextBuffer();

        if (RenderSuggestions)
        {
            ClearSuggestionBuffer();
            RenderSuggestionBuffer();
        }
    }

    // Clear changed text buffer
    private void ClearTextBuffer()
    {
        // // Find the position where text buffer and rendered text buffer differ at.
        int DiffStart = GetTextDiff(TextBuffer, RenderedTextBuffer);
        int TotalDist = Config.LeftCursorStartPos + DiffStart;

        (int x, int y) = CalcXYCordinate(TotalDist);
        y += CursorVec.Y;

        // Clear the screen.
        Console.SetCursorPosition(x, y);
        Console.Write(new string(' ', RenderedTextBuffer[DiffStart..].Length));
        Console.SetCursorPosition(x, y);
    }

    private void RenderToken(int token_idx, int char_idx)
    {
        Tokenizer.Token token = tokenizer.tokens[token_idx];

        // EOL is useless so don't render it.
        if (token.Type == Tokenizer.TokenType.EOL)
            return;

        // Check if the token is to be highlighted or not. If yes, then highlight.
        string Token = token.Name[char_idx..];
        if (Config.ToggleColorCoding && Config.SyntaxHighlightCodes.TryGetValue(token.Type, out ConsoleColor color))
            Terminal.Print(Token, color, false);

        // Otherwise update text after cursor normally.
        else
            Console.Write(Token);
    }

    // Render the updated input with syntax highlighting
    private void RenderTextBuffer()
    {
        // Get difference between TextBuffer and RenderedTextBuffer
        (int, int) DiffTokenIdx = GetTokenDiff(TextBuffer, RenderedTextBuffer);

        // Loop through each token starting from first different token
        RenderToken(DiffTokenIdx.Item1, DiffTokenIdx.Item2);
        for (int i = DiffTokenIdx.Item1 + 1; i < tokenizer.tokens.Count; i++)
            RenderToken(i, 0);

        RenderedTextBuffer = TextBuffer;
    }
}
