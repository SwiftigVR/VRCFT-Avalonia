using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace VRCFT.Extension;

public static class ImageHelper
{
    public static Bitmap Load(string uri)
        => Load(new Uri(uri));

    public static Bitmap Load(Uri uri)
        => new Bitmap(AssetLoader.Open(uri));

    public static async Task<Bitmap?> LoadFromWeb(string uri)
        => await LoadFromWeb(new Uri(uri));

    public static async Task<Bitmap?> LoadFromWeb(Uri url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsByteArrayAsync();
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
            return null;
        }
    }
}