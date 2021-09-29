using System.Collections.Generic;
namespace Mods
{
    public class Store
    {
        public Store() {}

    

        public Store(string Name, string City)
        {
            this.Name = Name;
            this.City = City;
        }

        public int Id 
        {get; set;}
        
        public string Name
        {get; set;}

        public string City
        {get; set;}

        public List<Beer> beers
        {get; set;}

        public override string ToString()
        {
            return $"store name: {this.Name} city: {this.City}";
        }
    }
}