#r "System.Xml.Linq"
using System.Net;
using System.Xml.Linq;

private static async Task<XDocument> ConstructModifiedFeed(string rssFeed){
    var feedResponse = await new HttpClient().GetAsync(rssFeed);
    var content = await feedResponse.Content.ReadAsStringAsync();
    
    return XDocument.Parse(content);
}

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
	var feed = req.GetQueryNameValuePairs().FirstOrDefault(q => q.Key.ToLower() == "feed").Value;
    var rssFeed = $"https://www.reddit.com/r/{feed}/.rss";
    var doc =  await ConstructModifiedFeed(rssFeed);
    return req.CreateResponse(HttpStatusCode.OK, doc);
}