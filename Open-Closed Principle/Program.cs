using System;
using System.Collections;
using System.Collections.Generic;

namespace Open_Closed_Principle
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    //if I need to implement a filtering by size AND color, I might modify existing methods. But it is a bad practice. 
    //I'll brake Open-Close Principle. "Classes should be OPENED for extension, but they should be CLOSED for modification." 
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var item in products)
            {
                if (item.Size == size)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var item in products)
            {
                if (item.Color == color)
                {
                    yield return item;
                }
            }
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;
        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == _size;
        }
    }
    
       public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;
        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == _color;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {

        private ISpecification<T> _first, _second;
        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public bool IsSatisfied(T t)
        {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product> 
    {
        //I shouldn't add something in this class. Instead I should create a new class and implement ISpecification interface 
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i))
                {
                    yield return i;

                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var fp = new ProductFilter();
            Console.WriteLine("Green colors (old): ");
            foreach (var p in fp.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green");
            }


            var bf = new BetterFilter();

            Console.WriteLine("Green products (new):");
            foreach (var i in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {i.Name} is green");
            }

            Console.WriteLine("Combined filter: ");
            foreach (var m in bf.Filter(products, 
                new AndSpecification<Product>(new SizeSpecification(Size.Large),new ColorSpecification(Color.Blue))))
            {
                Console.WriteLine($" - {m.Name} is large and blue");
            }
            Console.ReadLine();
        }
    }
}
