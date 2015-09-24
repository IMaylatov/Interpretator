namespace Interpretator
{
    public static class StringHelper
    {
        public static string SkipWord(this string str, string word)
        {
            if (str.StartsWith(word))
            {
                str = str.Remove(0, word.Length).Trim();
            }
            return str;
        }
    }
}
