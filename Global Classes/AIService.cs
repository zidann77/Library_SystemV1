using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.AI.Services
{
    public class AIService
    {
        // جلب المفتاح من ملف App.config
        private readonly string _apiKey = ConfigurationManager.AppSettings["GeminiKey"];
        private readonly string _apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent";
        private static readonly HttpClient _httpClient = new HttpClient();

        public AIService()
        {
            // سطر حيوي لضمان عمل الاتصال المشفر مع سيرفرات جوجل
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }
        public async Task<string> GetResponseAsync(string userPrompt)
        {
            try
            {
                if (string.IsNullOrEmpty(_apiKey))
                    return "Error: API Key is missing from configuration.";

                // 1. Build the JSON Payload matching your cURL structure
                var payload = new
                {
                    contents = new[]
                    {
                new { parts = new[] { new { text = userPrompt + "Please provide your answer as a single, continuous paragraph. Do not use bullet points, numbering, or bold text." } } }
            }
                };

                string jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // 2. Clear headers and add the API Key (as seen in your cURL -H command)
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-goog-api-key", _apiKey);

                // 3. Send the POST request
                var response = await _httpClient.PostAsync(_apiUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"API Error ({response.StatusCode}): {responseString}";
                }

                // 4. Parse the response JSON
                JObject result = JObject.Parse(responseString);

                // Navigate the JSON tree: candidates -> content -> parts -> text
                var aiText = result["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

                return aiText ?? "Response received, but no text was generated.";
            }
            catch (Exception ex)
            {
                return "Connection Error: " + ex.Message;
            }
        }

    }
}