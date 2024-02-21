using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

class Robot : User
{
  public Robot(string username = "Robot") : base(username)
  {

  }

  public override async Task<string> GetAnswer(string messageToReplyTo, string apiKey)
  {
    string apiUrl = "https://wsapi.simsimi.com/190410/talk";
    string answerPropertyName = "atext";

    using (HttpClient client = new HttpClient())
    {
      StringContent requestContent = new StringContent(JsonSerializer.Serialize(new
      {
        utext = messageToReplyTo,
        lang = "en"
      }));
      requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      requestContent.Headers.Add("x-api-key", apiKey);

      try
      {
        HttpResponseMessage response = await client.PostAsync(apiUrl, requestContent);

        if (!response.IsSuccessStatusCode)
        {
          throw new Exception($"Failed to get response. Status code: {response.StatusCode}");
        }

        string resultJson = await response.Content.ReadAsStringAsync();
        JsonDocument jsonDocument = JsonDocument.Parse(resultJson);
        JsonElement element = jsonDocument.RootElement.GetProperty(answerPropertyName);
        string robotAnswer = element.GetString();

        return robotAnswer;
      }
      catch (Exception ex)
      {
        // TODO
        return $"Error: {ex.Message}";
      }
    }
  }
}