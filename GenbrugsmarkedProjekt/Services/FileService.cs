using System.Net.Http.Json;

namespace GenbrugsmarkedProjekt.Services;

public class FileService
{
    private readonly HttpClient http;

    public FileService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<(bool success, string info)> SendFile(string filename, Stream s)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(s), "file", filename);

        var response = await http.PostAsync("files/upload", content);   // FJERNET /
        string key = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode
            ? (true, key)
            : (false, response.ReasonPhrase ?? "Ukendt fejl");
    }

    public async Task<List<string>> GetAllKeys()
    {
        var keys = await http.GetFromJsonAsync<List<string>>("files/getall");
        return keys ?? new List<string>();
    }

    public string ConvertToUrl(string key) => $"files/{key}";          // FJERNET /

    public async Task<(bool success, string info)> DeleteFile(string filename)
    {
        var httpResp = await http.DeleteAsync($"files/{filename}");     // evt. også her
        return httpResp.IsSuccessStatusCode
            ? (true, "File deleted")
            : (false, httpResp.ReasonPhrase ?? "Fejl");
    }
}