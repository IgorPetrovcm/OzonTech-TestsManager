namespace OzonTestsManager.Exception;


public class ExceptionDefaultParams : OzonException
{
    public ExceptionDefaultParams(string body) : base("Ozon Library Exception", body)
    {

    }

    public override string ReturnException()
    {
        return base.ReturnException();
    }
}