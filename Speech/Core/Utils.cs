using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System;
using System.Configuration;
using System.Globalization;

namespace JG.Speech.Core
{
    internal class Utils
    {
        internal static SpeechRecognitionEngine engine;
        internal static SpeechSynthesizer speechSender = new SpeechSynthesizer();
        internal static string[] wordsList = ConfigurationManager.AppSettings["STORY"].Split(';');

        public static void SetGrammar()
        {
            try
            {
                engine = new SpeechRecognitionEngine(new CultureInfo(ConfigurationManager.AppSettings["CULTURE_INFO"]));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to integrate the language. Error message {ex.Message}");
            }

            var grammar = new Choices();
            grammar.Add(wordsList);

            var gb = new GrammarBuilder();
            gb.Append(grammar);

            try
            {
                var g = new Grammar(gb);

                try
                {
                    engine.RequestRecognizerUpdate();
                    engine.LoadGrammarAsync(g);
                    engine.SetInputToDefaultAudioDevice();
                    speechSender.SetOutputToDefaultAudioDevice();
                    engine.RecognizeAsync(RecognizeMode.Multiple);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error to create the Engine. Error message {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to create the Grammar. Error message {ex.Message}");
            }
        }

    }
}
