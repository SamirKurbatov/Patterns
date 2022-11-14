namespace Patterns;


public sealed class Facade
{
    private static Subsystem _subsystem;
    private static Subsystem2 _subsystem2;
    
    private static Facade _facadeInstance;
    public Facade(Subsystem subsystem, Subsystem2 subsystem2)
    {
        _subsystem = subsystem;
        _subsystem2 = subsystem2;
    }

    public static Facade Initialize(Subsystem subsystem, Subsystem2 subsystem2)
    {
        if (_facadeInstance == null)
        {
            _facadeInstance = new Facade(subsystem, subsystem2);
        }

        return _facadeInstance;
    }
    
    public string Operation()
    {
        string result = "Facade initializes subsystems!";
        result += _subsystem.GetFirstOperation();
        result += _subsystem2.GetFirstOperation();
        result += "Facad orders subsystems to perform the action:\n";
        result += _subsystem.GetSomeOperation();
        result += _subsystem2.GetSomeOperation();
        return result;
    }
}

public class Subsystem
{
    public string GetFirstOperation()
    {
        return "Subsystem1: ready";
    }

    public string GetSomeOperation()
    {
        return "Subsystem1: Go! Some";
    }
}

public class Subsystem2
{
    public string GetFirstOperation()
    {
        return "Subsystem2:Get ready";
    }

    public string GetSomeOperation()
    {
        return "Subsystem2: Fire!";
    }
}

public class Client
{
    public static void ClientCode(Facade facade)
    {
        Console.Write(facade.Operation());
    }
}




