partial class Terminal
{
    public static string TakeInput(CreadfConfig Config, string Prompt="", ConsoleColor PromptColor=ConsoleColor.Gray, ConsoleColor InputColor=ConsoleColor.Gray)
    {
        ConsoleColor DefaultColor = Console.ForegroundColor;
        Print(Prompt, PromptColor, false);

        // If the prompt contains newlines, then adjust the cursor start pos accordingly.
        if (Prompt.Contains("\n"))
        {
            Config.LeftCursorStartPos = Prompt.Split("\n").LastOrDefault().Length;
            Config.TopCursorStartPos = Console.CursorTop;
        }

        // Here this fixes a bug. Basically in AOs if the prompt length is to high, so much so that it exceeds the Console Window Width.
        // In starting everything works fine until it reaches the end where the below lines throws an error.
        // So yeah, that's why this is here. Although this bug happens in Creadf.
        else if (Prompt.Length >= Console.WindowWidth)
        {
            // Calculate the exact x and y positions to put the cursor at.
            int y = Prompt.Length / Console.WindowWidth;
            int i = y * Console.WindowWidth;

            Config.LeftCursorStartPos = Prompt[i..].Length;
            Config.TopCursorStartPos = Console.CursorTop;
        }

        // Change the foreground color to what the user wants.
        Console.ForegroundColor = InputColor;

        string Output;
        if (Config.ToggleAutoComplete || Config.ToggleColorCoding)
        {
            Creadf readline = new(Config);
            readline.InitDefaultKeyBindings();
            Output = readline.Readf();
        }

        else
            Output = Console.ReadLine();

        // Reset the foreground color to the default color and return the output.
        Console.ForegroundColor = DefaultColor;
        return Output;
    }
}
