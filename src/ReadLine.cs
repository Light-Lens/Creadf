partial class Creadf
{
    public bool Loop = true;
    public CursorVec3 CursorVec;
    public Dictionary<(ConsoleKey, ConsoleModifiers), Action> KeyBindings = [];

    private string TextBuffer = "";
    private readonly CreadfConfig Config;

    public Creadf(CreadfConfig Config)
    {
        this.Config = Config;
        CursorVec = new()
        {
            X = this.Config.LeftCursorStartPos,
            Y = this.Config.TopCursorStartPos
        };
    }

    public string Readf()
    {
        while (Loop)
        {
            ConsoleKeyInfo KeyInfo = Console.ReadKey(true);
            (ConsoleKey, ConsoleModifiers) Key = (KeyInfo.Key, KeyInfo.Modifiers);

            if (KeyBindings.TryGetValue(Key, out Action func)) func();
            else KeyPress(KeyInfo);
        }

        return TextBuffer;
    }

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

    public void AddKeyBindings(ConsoleKey key, ConsoleModifiers modifier, Action action)
    {
        KeyBindings.Add((key, modifier), action);
    }

    public void InitDefaultKeyBindings()
    {
        AddKeyBindings(ConsoleKey.Enter, ConsoleModifiers.None, HandleEnter);
        // AddKeyBindings(ConsoleKey.Enter, ConsoleModifiers.Control, HandleCtrlEnter);

        // AddKeyBindings(ConsoleKey.Tab, ConsoleModifiers.None, HandleTab);
        // AddKeyBindings(ConsoleKey.Spacebar, ConsoleModifiers.Control, HandleCtrlSpacebar);

        // AddKeyBindings(ConsoleKey.Escape, ConsoleModifiers.None, HandleEscape);
        // AddKeyBindings(ConsoleKey.Escape, ConsoleModifiers.Shift, HandleShiftEscape);

        // AddKeyBindings(ConsoleKey.Home, ConsoleModifiers.None, HandleHome);
        // AddKeyBindings(ConsoleKey.End, ConsoleModifiers.None, HandleEnd);

        // AddKeyBindings(ConsoleKey.Delete, ConsoleModifiers.None, HandleDelete);
        // AddKeyBindings(ConsoleKey.Delete, ConsoleModifiers.Control, HandleCtrlDelete);

        // AddKeyBindings(ConsoleKey.Backspace, ConsoleModifiers.None, HandleBackspace);
        // AddKeyBindings(ConsoleKey.Backspace, ConsoleModifiers.Control, HandleCtrlBackspace);

        // AddKeyBindings(ConsoleKey.LeftArrow, ConsoleModifiers.None, HandleLeftArrow);
        // AddKeyBindings(ConsoleKey.LeftArrow, ConsoleModifiers.Control, HandleCtrlLeftArrow);

        // AddKeyBindings(ConsoleKey.RightArrow, ConsoleModifiers.None, HandleRightArrow);
        // AddKeyBindings(ConsoleKey.RightArrow, ConsoleModifiers.Control, HandleCtrlRightArrow);
    }
}
