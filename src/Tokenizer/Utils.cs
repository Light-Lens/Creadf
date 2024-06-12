partial class Creadf
{
    public partial class Tokenizer
    {
        public enum TokenType
        {
            EOL = 0,
            SEMICOLON,
            COMMENT,
            WHITESPACE,
            IDENTIFIER,
            STRING,
            BOOL,
            EXPR,
            SYMBOL,
            HIDDEN
        }

        public struct Token(string Name, TokenType Type)
        {
            public string Name = Name;
            public TokenType Type = Type;
        }

        private void MakeString(char StringLiteral)
        {
            int IsEscapeChar = 0;

            i++;
            while (i < line.Length)
            {
                tok += line[i];

                if (line[i] == StringLiteral && IsEscapeChar == 1)
                    IsEscapeChar = 0;

                else if (line[i] == StringLiteral)
                    break;

                if (line[i] == '\\')
                    IsEscapeChar %= IsEscapeChar + 1;

                i++; // Move to next char
            }

            i++;
        }

        private static bool IsValidCharForFilenameInWindows(char ch)
        {
            return "`~!@$^&?()=+-*/%_.[],{}|:<>\\".Contains(ch);
        }

        private bool IsIdentifier(char ch)
        {
            // Check if a char is empty or not, if not then check if the char is a letter or a digit or a symbol.
            // If no in any of these cases then return false, otherwise true.
            if (Utils.String.IsEmpty(ch.ToString()) || !(char.IsLetterOrDigit(ch) || IsValidCharForFilenameInWindows(ch)))
                return false;

            return true;
        }
    }
}
