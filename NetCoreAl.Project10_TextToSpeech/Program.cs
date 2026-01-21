using System.Speech.Synthesis;

class Program
{
    static void Main(string[] args)
    {
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();

        speechSynthesizer.Volume = 100;
        speechSynthesizer.Rate = -4;

        Console.WriteLine("Enter text to convert to speech:");
        string input;
        input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input)) { 
        speechSynthesizer.Speak(input);
        }else
        {
            Console.ReadLine();
        }
    }
}