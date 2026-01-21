using Google.Cloud.Vision.V1;

class Program
{
       static void Main(string[] args)
    {
       Console.WriteLine("Resim yolunu giriniz");
        Console.WriteLine();
        string imagePath = Console.ReadLine();

        string credentialPath = "";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        try
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);
            Console.WriteLine("Resimde meti:");
            Console.WriteLine();
            foreach (var annotation in response)
            {
                if (!string.IsNullOrEmpty(annotation.Description))
                {
                    Console.WriteLine(annotation.Description);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu: { ex.Message}");
        }
    }

}