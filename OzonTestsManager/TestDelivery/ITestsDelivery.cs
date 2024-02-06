namespace OzonTestsManager;

using System.Net;


public interface ITestsDelivery
{
    Uri UriTask {get;}

    string UriStringTask {get;} 

    KeyValuePair<string,HttpContent> GetTestArchive();
    KeyValuePair<string,HttpContent> GetTestArchive(Uri uri);

    KeyValuePair<string,HttpContent> GetTestArchive(string uri);
}