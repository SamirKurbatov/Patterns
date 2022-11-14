namespace Patterns;



public abstract class CompositeComponent
{
    protected static int numberOfTabs = -1;
    protected string name;
    public CompositeComponent(string name)
        => this.name = name;
    public abstract void Add(CompositeComponent compositeComponent);
    public abstract void Remove(CompositeComponent compositeComponent);
    public abstract void Print();
}

public class CompositeDirectory : CompositeComponent
{
    private List<CompositeComponent> _components = new();
    public CompositeDirectory(string name) : base(name) { }

    public override void Add(CompositeComponent compositeComponent)
        => _components.Add(compositeComponent);
    public override void Remove(CompositeComponent compositeComponent)
        => _components.Remove(compositeComponent);

    public override void Print()
    {
        numberOfTabs++;
        Console.WriteLine(new string('\t',numberOfTabs) + "Узел: " + name);
        foreach (CompositeComponent component in _components)
            component.Print();
        numberOfTabs--;
    }
}
public class CompositeFile : CompositeComponent
{
    public CompositeFile(string name) : base(name) { }

    public override void Add(CompositeComponent compositeComponent) { }
    public override void Remove(CompositeComponent compositeComponent) { }
    public override void Print()
        => Console.WriteLine(new string('\t', numberOfTabs + 1) + name);
}