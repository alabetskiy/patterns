using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(new CocaColaFactory());
            client.Run();
        }
    }

    public class Client
    {
        private AbstactBottle bottle;
        private AbstractWater water;
        public Client(MyAbstractFactory factory)
        {
            bottle = factory.CreateBottle();
            water = factory.CreateWater();
        }

        public void Run()
        {
            Console.WriteLine(bottle.Interact(water));

            Console.WriteLine(bottle.NameOfBottle);
            Console.WriteLine(water.NameOfWater);

            Console.WriteLine(water.ShowMyWater());
            Console.ReadLine();

        }
    }
    public abstract class MyAbstractFactory
    {
        public abstract AbstactBottle CreateBottle();
        public abstract AbstractWater CreateWater();

    }

    public class CocaColaFactory : MyAbstractFactory
    {
        public override AbstactBottle CreateBottle()
        {

            return new CocaColaBottle();
        }

        public override AbstractWater CreateWater()
        {

            return new CocaColaWater();
        }
    }

    public class PepsiFactory : MyAbstractFactory
    {
        public override AbstactBottle CreateBottle()
        {
            return new PepsiBottle();
        }

        public override AbstractWater CreateWater()
        {
            return new PepsiWater();
        }
    }

    public class CocaColaWater : AbstractWater
    {
        public override string NameOfWater { get => "This is from CocaCola Water propterty "; set => throw new NotImplementedException(); }

        public override string ShowMyWater()
        {
            string response = "Result of interactring bewteen CoCaCola Water and CoCaCola Bottle";
            return response;
        }
    }

    public class PepsiWater : AbstractWater
    {
        public override string NameOfWater { get; set; }

        public override string ShowMyWater()
        {
            return "";
        }
    }

    public class CocaColaBottle : AbstactBottle
    {
        public override string NameOfBottle { get => "This is from CocaCola Bottle property"; set => throw new NotImplementedException(); }

        public override string Interact(AbstractWater abstrWater)
        {
           return abstrWater.ShowMyWater() + "(Overriden by CocaColaBottle Class. Interact())";

        }
    }

    public class PepsiBottle : AbstactBottle
    {
        public override string NameOfBottle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string Interact(AbstractWater abstrWater)
        {
            return "";
        }
    }

    public abstract class AbstractWater
    {
        public abstract string NameOfWater { get; set; }

        public abstract string ShowMyWater();
    }


    public abstract class AbstactBottle
    {
        public abstract string NameOfBottle { get; set; }
        public abstract string Interact(AbstractWater abstrWater);

    }
}
