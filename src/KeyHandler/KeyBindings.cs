partial class Creadf
{
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
}
