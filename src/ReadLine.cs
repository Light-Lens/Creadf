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

            if (Loop)
            {
                (int x, int y) = CalcXYCordinate(CursorVec.X);
                y += CursorVec.Y;

                Console.SetCursorPosition(x, y);
            }
        }

        return TextBuffer;
    }
}
