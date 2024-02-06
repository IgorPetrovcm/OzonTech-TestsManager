namespace OzonTestsManager;

using System.Net.Http;
using System.Net.Http.Headers;
using HtmlAgilityPack;

public class OzonTestsDelivery : ITestsDelivery
{
    private readonly Uri? _uriTask;

    public Uri? UriTask {get {return _uriTask;}}

    public string? UriStringTask {get {return _uriTask?.AbsolutePath;}}


    public OzonTestsDelivery() {}

    public OzonTestsDelivery(Uri uriTask)
    {
        _uriTask = uriTask;
    }

    public OzonTestsDelivery(string strUriTask)
    {
        _uriTask = new Uri(strUriTask);
    }

    public HttpContent GetTestArchive()
    {
        if (_uriTask is null)
            return null;

        string  downloadUri = DeliveryParser.GetHrefToArchiveFile(_uriTask);

        HttpResponseMessage response = DeliveryClient.Send(HttpMethod.Get,downloadUri);

        return response.Content;
    }
    public HttpContent GetTestArchive(Uri uri)
    {
        if (uri is null)
            return null;

        string downloadUri = DeliveryParser.GetHrefToArchiveFile(uri);

        HttpResponseMessage response = DeliveryClient.Send(HttpMethod.Get, downloadUri);

        return response.Content;
    }
    public HttpContent GetTestArchive(string uri)
    {
        if (uri is null)
            return null;

        string downloadUri = DeliveryParser.GetHrefToArchiveFile(uri);

        HttpResponseMessage response = DeliveryClient.Send(HttpMethod.Get, downloadUri);

        return response.Content;
    }

    private static class DeliveryClient
    {
        public static HttpClient? httpClient;

        public static HttpRequestMessage? request;

        public static HttpResponseMessage? response;
        
        public static async Task<HttpResponseMessage> SendAsync(HttpMethod method, Uri uri)
        {
            httpClient = new HttpClient();

            request = new HttpRequestMessage(method, uri);

            return await httpClient.SendAsync(request);
        }

        public static HttpResponseMessage Send(HttpMethod method, Uri uri)
        {
            httpClient = new HttpClient();

            request = new HttpRequestMessage(method, uri);

            return httpClient.SendAsync(request).Result;
        }
        public static HttpResponseMessage Send(HttpMethod method, string uri)
        {
            httpClient = new HttpClient();

            request = new HttpRequestMessage(method, uri);

            return httpClient.SendAsync(request).Result;
        }
    }

    private static class DeliveryParser
    {
        private static HtmlDocument? document;

        private static HtmlWeb? webFromDocument;

        public static readonly string xPath = @"//*[@id=""__nuxt""]/div/main/div/section/div[2]/div[1]/div[2]/div/div[2]/div/a";


        public static string GetHrefToArchiveFile(Uri uri)
        {
            webFromDocument = new HtmlWeb();
            document = webFromDocument.Load(uri);

            HtmlNode href = document.DocumentNode.SelectSingleNode(xPath);

            return href.Attributes["href"].Value;
        }
        public static string GetHrefToArchiveFile(string uri)
        {
            webFromDocument = new HtmlWeb();
            document = webFromDocument.Load(uri);

            HtmlNode href = document.DocumentNode.SelectSingleNode(xPath);

            return href.Attributes["href"].Value;
        }
    }
}