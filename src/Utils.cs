partial class Creadf
{
    // Check whether a string is empty.
    bool ArrayIsEmpty(string[] arr) => arr.Length == 0 || arr == null;

    // Check whether a string is empty.
    bool StrIsEmpty(string line) => string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line);
}
