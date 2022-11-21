using System;

[System.Serializable]
public class ErrorData : IError
{
    private Exception _exception;
    private string _message;

    public ErrorData(Exception exception)
    {
        _exception = exception;
        _message = _exception.Message;
    }
    
    public ErrorData(string message)
    {
        _exception = null;
        _message = message;
    }

    public Exception GetException()
    {
        return _exception;
    }

    public string GetMessage()
    {
        return _message;
    }
}
