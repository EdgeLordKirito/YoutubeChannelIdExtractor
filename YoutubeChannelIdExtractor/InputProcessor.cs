namespace YoutubeChannelIdExtractor;
class InputProcessor
{
    public string GetId(string url)
    {
        string id = ExtractChannelIdFromUrl(url);
        if (!id.Equals(""))
        {
            return id;
        }
        //else case
        string page = SourceGetter.GetHTMLSource(url);

        id = IdExtractor.ExtractChannelIdFromHtml(page);
        if (id.Equals("Channel ID not found"))
        {
            throw new Exception("Channel Id could not be found.");
        }
        else
        {
            return id;
        }
    }

    public IEnumerable<string> ProcessFile(string path)
    {
        FileInfo fileInfo = new FileInfo(path);
        if (!fileInfo.Exists)
        {
            return Enumerable.Empty<string>();
        }
        else
        {
            string[] lines = File.ReadAllLines(fileInfo.FullName);
            List<string> result = new List<string>();
            foreach (string line in lines)
            {
                result.Add(GetId(line));
            }
            return result;
        }
    }

    private static string ExtractChannelIdFromUrl(string url)
    {
        // Split the URL by "/"
        string[] parts = url.Split('/');

        // Find the part containing "channel" or "c" and return the following part
        for (int i = 0; i < parts.Length - 1; i++)
        {
            if (parts[i].Equals("channel", StringComparison.OrdinalIgnoreCase) || parts[i].Equals("c", StringComparison.OrdinalIgnoreCase))
            {
                return parts[i + 1];
            }
        }

        // If "channel" or "c" is not found, return an empty string
        return "";
    }
}
