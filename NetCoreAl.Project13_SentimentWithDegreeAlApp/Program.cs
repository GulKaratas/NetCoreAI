using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "";

    public async static Task Main(string[] args)
    {
        Console.WriteLine("Lütfen metni giriniz:");
        string input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Duygu ve derece analiz ediliyor...");
            Console.WriteLine();
            string sentiment = await AdvancedSentimentalAnalysis(input);
            Console.WriteLine($"Metin Duygusu: {sentiment}");
        }
        else
        {
            Console.WriteLine("Geçersiz giriş.");
        }

        static async Task<string> AdvancedSentimentalAnalysis(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-4",
                    messages = new[]
                    {
                        new { role = "system", content = $"Lütfen aşağıdaki metnin duygusunu ve derecesini analiz et ve sadece 'Pozitif', 'Negatif' veya 'Nötr' olarak cevap ver. Ayrıca, duygunun derecesini 1 ile 10 arasında bir sayı ile belirt." },
                        new { role = "user", content = $"Analyze the sentiments of this text: \"{text}\" and return only Positive, Negative or Neutral along with a degree from 1 to 10." }
                    }
                };
                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if(response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseString);
                    string sentiment = result.choices[0].message.content.ToString();
                    return sentiment;
                }
                else
                {
                    Console.WriteLine("API isteği başarısız oldu: " + responseString);
                    return "Bilinmiyor";
                }
            }
        }
    }
}