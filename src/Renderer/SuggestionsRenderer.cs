partial class Creadf
{
    private string Suggestion = "";
    private string RenderedSuggestionsBuffer = "";
    private List<string> AllSuggestions = [];
    private int CurrentSuggestionIdx = 0;

    private void Add_Y_OffsetToCursorPosition()
    {
        /*
        Number of lines to move up by (let k) = RenderedSuggestionsBuffer.Length / Console.WindowWidth
        Therefore, if the current y pos of the cursor is greater than (Console.WindowHeight - 2),
        then move the cursor up by 'k' lines otherwise leave it.

        *NOTE:
        Changing (Console.WindowHeight - 2) to (Console.WindowHeight - 1) will cause a bug, where it is not moving up properly.
        */

        if (CursorVec.Y >= Console.WindowHeight - 2)
            CursorVec.Y -= RenderedSuggestionsBuffer.Length / Console.WindowWidth;

        SetCursorPosition(CursorVec.X);
    }

    private void ClearSuggestionBuffer()
    {
        Console.Write("\n" + new string(' ', RenderedSuggestionsBuffer.Length));
        SetCursorPosition(CursorVec.X);
    }

    private void RenderSuggestionBuffer()
    {
        int SuggestionIdxOnBuffer = MakeSuggestions();

        if (SuggestionIdxOnBuffer == -1)
            return;

        // Render all suggestions
        Console.WriteLine();
        Terminal.Print(RenderedSuggestionsBuffer[..SuggestionIdxOnBuffer], ConsoleColor.DarkGray, false);
        Terminal.Print(Suggestion, ConsoleColor.DarkCyan, false);
        Terminal.Print(RenderedSuggestionsBuffer[(SuggestionIdxOnBuffer + Suggestion.Length)..], ConsoleColor.DarkGray, false);

        Add_Y_OffsetToCursorPosition();

        // Get only the uncommon part of suggestion
        Suggestion = TextBuffer.Length <= Suggestion.Length ? Suggestion[TextBuffer.Length..] : "";
    }

    private int MakeSuggestions()
    {
        // Get all the suggestions based on the current input in the text buffer
        GetSuggestions();

        if (ArrayIsEmpty(AllSuggestions.ToArray()))
            return -1;

        // Get the current suggestion
        Suggestion = AllSuggestions[CurrentSuggestionIdx];

        string SuggestionsBuffer = "";
        int SuggestionIdxOnBuffer = 0;

        for (int i = 0; i < AllSuggestions.Count; i++)
        {
            if (Suggestion == AllSuggestions[i])
                SuggestionIdxOnBuffer = SuggestionsBuffer.Length;

            SuggestionsBuffer += AllSuggestions[i] + "    ";

            if ((i+1) % 12 == 0)
            {
                string Whitespace = new(' ', Console.WindowWidth - (SuggestionsBuffer.Length % Console.WindowWidth));
                SuggestionsBuffer += Whitespace;
            }
        }

        // Set rendered suggestion buffer as the current buffer.
        RenderedSuggestionsBuffer = SuggestionsBuffer.Trim();

        return SuggestionIdxOnBuffer;
    }

    private void GetSuggestions()
    {
        if (StrIsEmpty(TextBuffer))
        {
            AllSuggestions = [];
            return;
        }

        AllSuggestions = Config.Suggestions.Where(x => x.StartsWith(TextBuffer)).Take(10).ToList();
        // Reset the current suggestion index if not properly set.
        if (CurrentSuggestionIdx < 0 || CurrentSuggestionIdx > AllSuggestions.Count-1) CurrentSuggestionIdx = 0;
    }
}
