using Newtonsoft.Json;

namespace Patterns;

interface IDataSource
{
    void WriteData(Message message);
    Message[] ReadData();
}

interface ICreate<T>
{
    T Create();
}


class FileDataSource : IDataSource
{
    private readonly string _directoryPath = "D://Top/";
    private readonly string _fileName = $"{Guid.NewGuid()}.json";

    private List<Message> _messages = new();

    public void WriteData(Message message)
    {
        if (Directory.Exists(_directoryPath) == false)
        {
            Directory.CreateDirectory(_directoryPath);
        }

        var fullPath = Path.Combine(_directoryPath, _fileName);

        _messages.Add(message);
        var jsonSettings = new JsonSerializerSettings
        {
            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
        };

        var json = JsonConvert.SerializeObject(_messages, jsonSettings);
        File.WriteAllText(fullPath, json);
    }

    public Message[] ReadData()
    {
        if (File.Exists(_fileName))
        {
            _messages = JsonConvert.DeserializeObject<List<Message>>(File.ReadAllText(_fileName)) ??
                        throw new NullReferenceException("Сообщения не могут быть пустым значением! ");
        }

        return _messages.ToArray();
    }
}

abstract class DataSourceDecorator : IDataSource
{
    protected IDataSource _dataSource;

    public DataSourceDecorator(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public virtual void WriteData(Message message)
    {
        _dataSource.WriteData(message);
    }

    public virtual Message[] ReadData()
    {
        return _dataSource.ReadData();
    }
}

class EncryptionDecorator : DataSourceDecorator
{
    public EncryptionDecorator(IDataSource dataSource) : base(dataSource)
    {
    }

    public override void WriteData(Message message)
    {
        Console.WriteLine($"Шифрование сообщения... Работает - {this.GetType().Name}");
    }
}

class CompressionDecorator : DataSourceDecorator
{
    public CompressionDecorator(IDataSource dataSource) : base(dataSource)
    {
    }

    public override void WriteData(Message message)
    {
        Console.WriteLine($"Сжатие данных... Работает - {this.GetType().Name}");
    }
}

public class MessageCreator<T> : ICreate<T> where T : Message
{
    public T Create()
    {
        var sender = new SenderCreator<Sender>().Create();
        Console.Write("Введите сообщение: ");
        var title = Console.ReadLine();
        return (T)new Message(sender, title);
    }
}

public class SenderCreator<T> : ICreate<T> where T : Sender
{
    public T Create()
    {
        Console.Write("Введите ваше имя: ");
        var name = Console.ReadLine();
        return (T)new Sender(name);
    }
}


public class Message
{
    public Sender Sender { get; private set; }

    public Message(Sender sender, string title)
    {
        Sender = sender;
        Title = title;
    }

    public string Title { get; }

    public DateTimeOffset SendTime { get; set; } = DateTimeOffset.Now;
}

public class Sender
{
    public Sender(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    
}


class Sensei
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    private Sensei(string name, string lastName)
    {
        Name = name;
        LastName = lastName;
    }
}