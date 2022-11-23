using System;

public interface IError
{
    public Exception GetException();

    public string GetMessage();
}
