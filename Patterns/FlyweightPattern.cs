using System.Drawing;
using Console = System.Console;

namespace Patterns;

class FlyweightFactory
{
    Dictionary<string, Flyweight> flyweights = new()
    {
        { "X", new ConcreteFlyweight() },
        { "Y", new ConcreteFlyweight() },
        { "Z", new ConcreteFlyweight() }
    };
    public Flyweight GetFlyweight(string key)
    {
        if(!flyweights.ContainsKey(key))
        {
            Flyweight flyweight = new ConcreteFlyweight();
            flyweights.Add(key, flyweight);
            return flyweight;
        }
        return flyweights[key];
    }
}
abstract class Flyweight
{
    public abstract void Operation(int extrinsicState);
}
class ConcreteFlyweight : Flyweight
{
    int intrinsicState;
    public override void Operation(int extrinsicState){}
}
class UnsharedConcreteFlyweight : Flyweight
{
    int allStates;
    public override void Operation(int extrinsicState)
        => allStates = extrinsicState;
}

