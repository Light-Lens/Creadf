partial class Creadf
{
    private (int, int) DiffTokenIdx = (0, 0);
    private string RenderedTextBuffer = "";
    private Tokenizer tokenizer;
    // White points those points on which the renderer has to render the tokens with white color.
    // Such as the very first token is to be rendered in white color as well as tokens which successed the semicolon.
    // This doesn't include whitespaces and only applies on renderable tokens.
    private List<int> WhitePoints = [];

    private void UpdateBuffer(bool RenderSuggestions=true)
    {
        if (TextBuffer == RenderedTextBuffer)
            return;

        ClearTextBuffer();
        RenderTextBuffer();

        if (!this.Config.ToggleAutoComplete || !RenderSuggestions)
            return;

        // If the cursor has reached the bottom of the window, then move it up by one point.
        // To ensure that the cursor is not going beyond the window which will crash the program.
        if (CursorVec.Y >= Console.WindowHeight - 1)
        {
            CursorVec.Y--;
            SetCursorPosition(CursorVec.X);
            Console.WriteLine();
        }

        ClearSuggestionBuffer();
        RenderSuggestionBuffer();
    }

    // Clear changed text buffer
    private void ClearTextBuffer()
    {
        // Find the position where text buffer and rendered text buffer differ at.
        DiffTokenIdx = GetTokenDiff(TextBuffer, RenderedTextBuffer);
        // Get starting point of the difference to start clearing the screen from.
        int DiffStart = string.Join("", tokenizer.tokens[..DiffTokenIdx.Item1].SelectMany(x => x.Name)).Length + DiffTokenIdx.Item2;
        int TotalDist = Config.LeftCursorStartPos + DiffStart;

        SetCursorPosition(TotalDist);
        Console.Write(new string(' ', RenderedTextBuffer[DiffStart..].Length));
        SetCursorPosition(TotalDist);
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

        else if (Config.ToggleColorCoding && WhitePoints.Any(x => token_idx == x))
            Terminal.Print(Token, ConsoleColor.White, false);

        // Otherwise update text after cursor normally.
        else
            Console.Write(Token);
    }

    // Get all the white points
    private void GetWhitePoints()
    {
        Tokenizer.TokenType[] tokens = tokenizer.tokens.Select(x => x.Type).ToArray();
        bool PauseAddingPoints = false;
        WhitePoints = [];

        for (int i = 0; i < tokens.Length; i++)
        {
            Tokenizer.TokenType TokType = tokens[i];
            if (TokType == Tokenizer.TokenType.EOL)
            {
                WhitePoints.Add(i);
                break;
            }

            else if (TokType != Tokenizer.TokenType.WHITESPACE && TokType != Tokenizer.TokenType.SEMICOLON && !PauseAddingPoints)
            {
                WhitePoints.Add(i);
                PauseAddingPoints = true;
            }

            else if (TokType == Tokenizer.TokenType.SEMICOLON && PauseAddingPoints)
                PauseAddingPoints = false;
        }
    }

    // Render the updated input with syntax highlighting
    private void RenderTextBuffer()
    {
        // Get all the white points
        GetWhitePoints();

        // Loop through each token starting from first different token
        RenderToken(DiffTokenIdx.Item1, DiffTokenIdx.Item2);
        for (int i = DiffTokenIdx.Item1 + 1; i < tokenizer.tokens.Count; i++)
            RenderToken(i, 0);

        RenderedTextBuffer = TextBuffer;
    }
}
