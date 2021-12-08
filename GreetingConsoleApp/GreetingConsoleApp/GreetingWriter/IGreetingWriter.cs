namespace GreetingConsoleApp;

public interface IGreetingWriter                //this is how we declare an interface
{
    public void Write(string message);          //This is a contract, all types that implement this interface must at least implement the methods declared here

    public void Write(Greeting greeting);
}