using System;
using System.Net.Http.Headers;
using System.Text.Json;

class BibleAPIHandler
{
  private string _apiKey;

  public BibleAPIHandler(string apiKey)
  {
    _apiKey = apiKey;
  }

  public async Task<string> GetVerseByReference(string reference)
  {
    string apiUrl = "https://api.esv.org/v3/passage/text/?" +
    $"q={reference}" +
    "&include-verse-numbers=false" +
    "&include-short-copyright=false" +
    "&include-passage-references=false" +
    "&include-headings=false" +
    "&indent-paragraphs=0" +
    "&indent-poetry-lines=1" +
    "&include-footnotes=false";

    string result = "Fail";

    using (HttpClient client = new HttpClient())
    {
      // Set the Authorization header
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _apiKey);

      try
      {
        HttpResponseMessage response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
          // get the verse from the whole response
          JsonElement passagesElement = await GetJsonPropertyAsync(response, "passages");

          if (passagesElement.ValueKind == JsonValueKind.Array)
          {
            JsonElement.ArrayEnumerator arrayEnumerator = passagesElement.EnumerateArray();
            // Check if the array is not empty
            if (arrayEnumerator.MoveNext())
            {
              JsonElement firstElement = arrayEnumerator.Current;
              // Check if the first element is a string
              if (firstElement.ValueKind == JsonValueKind.String)
              {
                string passages = firstElement.GetString();
                passages = passages.Replace("\n", "").Replace(",", ", ").Replace(":", ": ").Replace("  ", " ").Trim();
                result = passages;
              }
            }
            else
            {
              // Handle the case when the array is empty (result == "Fault" by default)
              Console.WriteLine($"The error in your reference: {reference}. There is no such scripture.");
            }
          }
        }
        else
        {
          Console.WriteLine($"Failed to get API data. Status code: {response.StatusCode}");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred during the API request: {ex.Message}");
      }
    }

    return result;
  }

  private async Task<JsonElement> GetJsonPropertyAsync(HttpResponseMessage response, string propertyName)
  {
    string jsonContent = await response.Content.ReadAsStringAsync();
    JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
    return jsonDocument.RootElement.GetProperty(propertyName);
  }
}

