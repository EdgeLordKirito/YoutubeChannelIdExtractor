namespace YoutubeChannelIdExtractor;
class SourceGetter
{
    public static async Task<string> GetHTMLSourceAsync(string url)
    {
        HttpClient client = new HttpClient();
        string page = await client.GetStringAsync(url);
        return page;
    }

    public static string GetHTMLSource(string url)
    {
        Task<string> task = Task.Run(async () => await GetHTMLSourceAsync(url));
        task.Wait();
        return task.Result;
    }
}
