namespace JG.Speech.Core
{
    internal abstract class Robot: Utils
    {
        internal static void Load()
        {
            speechSender.Volume = 100;
            speechSender.Rate = -1;
            SetGrammar();
            foreach (string s in wordsList)
            {
                speechSender.SpeakAsync(s);
            }
        }
    }
}
