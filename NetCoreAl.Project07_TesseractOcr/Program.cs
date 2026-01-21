using Tesseract;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Karakter okuması yapılacak resim yolu:");
        string imagePath = Console.ReadLine();

        string tessDataPath = @"C:\.Net\tessdata";

        try
        {
            using( var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
            {
                using( var img = Pix.LoadFromFile(imagePath))
                {
                    using( var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        Console.WriteLine("Okunan Metin:");
                        Console.WriteLine(text);
                    }
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Hata oluştu:  + {ex.Message}");

        }
        Console.ReadLine();
    }
}