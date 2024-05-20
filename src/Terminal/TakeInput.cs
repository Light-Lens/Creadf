partial class Terminal
{
    public static string TakeInput(CreadfConfig Config, string Prompt="", ConsoleColor PromptColor=ConsoleColor.Gray, ConsoleColor InputColor=ConsoleColor.Gray)
    {
        ConsoleColor DefaultColor = Console.ForegroundColor;
        Print(Prompt, PromptColor, false);

        // Change the foreground color to what the user wants.
        Console.ForegroundColor = InputColor;

        string Output;
        if (!(Config.ToggleAutoComplete && Config.ToggleColorCoding))
            Output = Console.ReadLine();

        else
        {
            Creadf readline = new(Config);
            readline.InitDefaultKeyBindings();
            Output = readline.Readf();
        }

        // Reset the foreground color to the default color and return the output.
        Console.ForegroundColor = DefaultColor;
        return Output;
    }
}
