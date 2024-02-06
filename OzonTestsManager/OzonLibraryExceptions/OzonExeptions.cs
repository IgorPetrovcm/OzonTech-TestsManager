namespace OzonTestsManager.Exception;



public class OzonException
{
    private readonly string? _exceptionTitle;

    private readonly string? _exceptionBody;

    public OzonException(string title, string body)
    {
        _exceptionTitle = title;

        _exceptionBody = body;
    }

    public override string ToString()
    {
        return _exceptionTitle + "\n" + _exceptionBody; 
    }
}