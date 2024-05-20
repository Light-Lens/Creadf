partial class Creadf
{
    private string Suggestion = "";
    private string RenderedSuggestionsBuffer = "";
    private List<string> AllSuggestions = [];
    private int CurrentSuggestionIdx = 0;

    private void ClearSuggestionBuffer()
    {
        if (StrIsEmpty(RenderedSuggestionsBuffer))
            return;

        Console.SetCursorPosition(0, CursorVec3.Y + 1);
        Console.Write(new string(' ', RenderedSuggestionsBuffer.Length));
        Console.SetCursorPosition(CursorVec3.X, CursorVec3.Y);

        RenderedSuggestionsBuffer = "";
    }

    private void GetSuggestion()
    {
        if (StrIsEmpty(TextBuffer))
        {
            AllSuggestions = [];
            return;
        }

        AllSuggestions = Config.Suggestions.Where(x => x.StartsWith(TextBuffer)).ToList();
    }

    private void RenderSuggestionBuffer()
    {
        GetSuggestion();

        if (ArrayIsEmpty(AllSuggestions.ToArray()))
            return;

        else if (CurrentSuggestionIdx < 0 || CurrentSuggestionIdx > AllSuggestions.Count-1)
            CurrentSuggestionIdx = 0;

        // Get the current suggestion
        Suggestion = AllSuggestions[CurrentSuggestionIdx];

        // Move to new line to render suggestions
        Console.WriteLine();

        // Render the suggestions
        string Buffer = "";
        for (int i = 0; i < AllSuggestions.Count; i++)
        {
            Buffer += AllSuggestions[i] + "    ";
            Terminal.Print(AllSuggestions[i] + "    ", Suggestion == AllSuggestions[i] ? ConsoleColor.Blue : ConsoleColor.DarkGray, false);

            if ((i+1) % 12 == 0)
            {
                string whitespace = new(' ', Console.WindowWidth - (Buffer.Length % Console.WindowWidth));
                Buffer += whitespace;
                Console.Write(whitespace);
            }
        }

        // Get only the uncommon part of suggestion
        Suggestion = TextBuffer.Length <= Suggestion.Length ? Suggestion[TextBuffer.Length..] : "";
        RenderedSuggestionsBuffer = Buffer;
    }
}
