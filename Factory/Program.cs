using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConcreteVehicleFactory();

            IFactory bike = factory.GetVehicle("bike");
            bike.Drive(50);

            IFactory scooter = factory.GetVehicle("scooter");
            scooter.Drive(25);

            Console.ReadLine();
        }
    }



    public class ConcreteVehicleFactory : VehicleFactory
    {
        public override IFactory GetVehicle(string vehicle)
        {
            switch (vehicle)
            {
                case "scooter":
                return new Scooter();

                case "bike":
                    return new Bike();
                default:
                    throw new ApplicationException($"Vehicle {vehicle} cannot be created");
            }
        }
    }

    public abstract class VehicleFactory
    {
        public abstract IFactory GetVehicle(string vehicle);
    }

    public interface IFactory
    {
        void Drive(int miles);
    }

    public class Scooter : IFactory
    {
        public void Drive(int miles)
        {
            Console.WriteLine($"Scooter speed limit is: {miles} ");
        }
    }

    public class Bike : IFactory
    {
        public void Drive(int miles)
        {
            Console.WriteLine($"Bike speed limit is: {miles} ");
        }
    }
}
