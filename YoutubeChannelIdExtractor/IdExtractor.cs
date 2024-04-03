using System.Text.RegularExpressions;

namespace YoutubeChannelIdExtractor;
class IdExtractor
{
    public static string ExtractChannelIdFromHtml(string htmlSource)
    {
        // Regular expression pattern to find channel IDs
        Regex regex = new Regex(@"https://www.youtube.com/channel/([a-zA-Z0-9_-]+)", RegexOptions.Compiled);

        // Match the pattern in the HTML source
        Match match = regex.Match(htmlSource);

        // Check if a match is found
        if (match.Success)
        {
            // Extract the channel ID from the match
            return match.Groups[1].Value;
        }
        else
        {
            return "Channel ID not found";
        }
    }
}
