using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using Newtonsoft.Json;
using Patterns;
using Subsystem = Patterns.Subsystem;

// Facade
{
    var firstSubsystem = new Subsystem();
    var secondSubsystem = new Subsystem2();
    var facade = Facade.Initialize(firstSubsystem, secondSubsystem);
    Client.ClientCode(facade);
}
// Composite
{
    CompositeComponent fileSystem = new CompositeDirectory("Файловая система");

    CompositeComponent diskD = new CompositeDirectory("Диск D");

    CompositeComponent secretFolder = new CompositeDirectory("Секретная папка");
    CompositeComponent passwordsTxt = new CompositeFile("passwords.txt");
    secretFolder.Add(passwordsTxt);
    diskD.Add(secretFolder);

    CompositeComponent photosFolder = new CompositeDirectory("Папка с фотками");
    CompositeComponent iPhotoPng = new CompositeFile("i.png");
    CompositeComponent youPhotoPng = new CompositeFile("you.png");
    photosFolder.Add(iPhotoPng);
    photosFolder.Add(youPhotoPng);
    diskD.Add(photosFolder);

    fileSystem.Add(diskD);
    fileSystem.Print();
}

// Decorator
{
    var fileDataSource = new FileDataSource();
    var message = CreateMessage();
    fileDataSource.WriteData(message);
    var readMessages = ReadMessages(fileDataSource);
    var encryptionDecorator = new EncryptionDecorator(fileDataSource);
    encryptionDecorator.WriteData(message);
    encryptionDecorator.ReadData();
    var comprasionDecorator = new CompressionDecorator(fileDataSource);
    comprasionDecorator.WriteData(message);
    comprasionDecorator.ReadData();
    PrintMessages(readMessages);

    static Message CreateMessage()
    {
        return new MessageCreator<Message>().Create();
    }

    static Message[] ReadMessages(FileDataSource source)
    {
        var files = source.ReadData();
        return files;
    }

    static void PrintMessages(Message[] messages)
    {
        foreach (var message in messages)
        {
            Console.WriteLine(
                $"Сообщение: {message.Title}\nДата отправки: {message.SendTime}\nОтправитель: {message.Sender.Name}");
        }
    }
}

// Adapter
{
    var driver = new Driver();
    var car = new Car();

    driver.Travel(car);
    // Ошибка! 
    // var camel = new Camel(); 
    // driver.Travel(camel);

    var camel = new Camel();
    ITransport camelTransport = new CamelToTransportAdapter(camel);
    driver.Travel(camelTransport);
}

// FlyWeight
{
    int extrinsicstate = 22;

    FlyweightFactory flyweightFactory = new FlyweightFactory();

    Flyweight fx = flyweightFactory.GetFlyweight("X");
    fx.Operation(--extrinsicstate);

    Flyweight flyweight = flyweightFactory.GetFlyweight("Y");
    flyweight.Operation(--extrinsicstate);

    Flyweight fd = flyweightFactory.GetFlyweight("D");
    fd.Operation(--extrinsicstate);

    UnsharedConcreteFlyweight unsharedConcreteFlyweight = new UnsharedConcreteFlyweight();

    unsharedConcreteFlyweight.Operation(--extrinsicstate);
}

// Proxy
{
    var client = new ProxyClient();

    Console.WriteLine("Client: Executing the client code with a real subject: ");
    var realSubject = new RealSubject();
    client.ClientCode(realSubject);
    
    Console.WriteLine();

    Console.WriteLine($"Client: Executing the same client code with a proxy: ");
    var proxy = new Proxy(realSubject);
    client.ClientCode(proxy);
}