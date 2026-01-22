using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static readonly string apiKey = "";
       static async Task Main(string[] args)
    {
        
        Console.WriteLine("Lütfen metni giriniz:");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Duygu analiz ediliyor...");
            Console.WriteLine();
            string sentiment = await AnalyzeSentiment(input);
            Console.WriteLine($"Metin Duygusu: {sentiment}");
        }
        else
        {
            Console.WriteLine("Geçersiz giriş.");
        }

        static async Task<string> AnalyzeSentiment(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    message = new[]
                    {
                        new { role = "system", content = $"Lütfen aşağıdaki metnin duygusunu analiz et ve sadece 'Pozitif', 'Negatif' veya 'Nötr' olarak cevap ver" },
                        new { role = "user", content = $"Analyze the sentiments of this text: \"{text}\"and return only Positive,Negative or Neutral" }
                    }
                };
                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/engines/text-sentiment-001/completions", content);

                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic responseObject = JsonConvert.DeserializeObject(responseString);
                    return responseObject.choices[0].message.content.ToString();

                }
                else
                {
                    Console.WriteLine("Bir hata oluştu" + response);
                    return "Hata";
                }
            }
        }

    }

}