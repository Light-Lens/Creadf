partial class Creadf
{
    // (int: x, int: y)
    private (int, int) CalcXYCordinates(int TotalDist)
    {
        // Calculate the exact x and y positions to put the cursor at.
        int y = TotalDist / Console.WindowWidth;
        int x = TotalDist - (y * Console.WindowWidth);

        return (x, y);
    }

    private (int, int) GetTokenDiff(string Text, string Text2)
    {
        // Tokenize the updated input text
        tokenizer = new("") { tokens = [], line = Text };
        tokenizer.Tokenize();

        // Tokenize the updated rendered input text
        Tokenizer _tokenizer = new("") { tokens = [], line = Text2 };
        _tokenizer.Tokenize();

        int SmallestTokenListLen = Math.Min(tokenizer.tokens.Count, _tokenizer.tokens.Count);

        for (int TokenIdx = 0; TokenIdx < SmallestTokenListLen; TokenIdx++)
        {
            string TextToken = tokenizer.tokens[TokenIdx].Name;
            string Text2Token = _tokenizer.tokens[TokenIdx].Name;

            if (TextToken != Text2Token)
                return (TokenIdx, GetTextDiff(TextToken, Text2Token));
        }

        // (token_idx, char_idx)
        return (SmallestTokenListLen - 1, 0);
    }

    private int GetTextDiff(string Text, string Text2)
    {
        for (int CharIdx = 0; CharIdx < Math.Min(Text.Length, Text2.Length); CharIdx++)
        {
            if (Text[CharIdx] != Text2[CharIdx])
                return CharIdx;
        }

        if (Text.Length != Text2.Length)
            return Math.Min(Text.Length, Text2.Length);

        return 0;
    }
}
