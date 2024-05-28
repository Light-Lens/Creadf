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

            if (Loop) SetCursorPosition(CursorVec.X);
        }

        return TextBuffer;
    }

    private void SetCursorPosition(int TotalDist)
    {
        // Properly set cursor in the terminal
        (int x, int y) = CalcXYCordinates(TotalDist);
        y += CursorVec.Y;

        if (y >= Console.WindowHeight - 1 && x >= Console.WindowWidth - 1)
        {
            y--;
            CursorVec.Y--;
            Console.WriteLine();
        }

        Console.SetCursorPosition(x, y);
    }
}
