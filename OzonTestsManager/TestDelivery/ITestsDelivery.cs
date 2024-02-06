namespace OzonTestsManager;

using System.Net;


public interface ITestsDelivery
{
    Uri UriTask {get;}

    string UriStringTask {get;} 

    HttpContent GetTestArchive();
    HttpContent GetTestArchive(Uri uri);

    HttpContent GetTestArchive(string uri);
}