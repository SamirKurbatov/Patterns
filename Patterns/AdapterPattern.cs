namespace Patterns;

public interface ITransport
{
    void Drive();
}

public interface IAnimal
{
    void Move();
}

class Driver
{
    public void Travel(ITransport transport)
    {
        transport.Drive();
    }
}

public class CamelToTransportAdapter : ITransport
{
    private Camel _camel;

    public CamelToTransportAdapter(Camel camel)
    {
        _camel = camel;
    }
    
    public void Drive()
    {
        _camel.Move();
    }
}

public class Car : ITransport
{
    public void Drive()
    {
        Console.WriteLine("Машина едет по дороге! ");
    }
}

public class Camel : IAnimal
{
    public void Move()
    {
        Console.WriteLine($"Вербрлюд идет по дороге! ");
    }
}