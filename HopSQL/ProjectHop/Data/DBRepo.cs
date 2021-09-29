using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mods;
using Entity = Data.Entities;

namespace Data
{
    public class DBRepo : IRepo
    {
        private Entity.HopDBContext _context;

        public DBRepo(Entity.HopDBContext context)
        {
            _context = context;
        }

        public Mods.Customer AddCustomer(Mods.Customer custo)
        {
            Entity.Customer custotoadd = new Entity.Customer(){
                Name = custo.Name,
                Code = custo.Code,
                IsManager = custo.IsManager
            };

            _context.Add(custotoadd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return custo;
        }

        public List<Mods.Customer> GetAllCustomers()
        {
            return _context.Customers.Select(customer => new Mods.Customer() {
                Id = customer.Id,
                Name = customer.Name,
                Code = customer.Code,
                IsManager = (bool)customer.IsManager

            }).ToList();

        }

        public List<Mods.Store> GetAllStores()
        {
            return _context.Stores.Select(store => new Mods.Store() {
                Id = store.Id,
                Name = store.Name,
                City = store.City    
            }).ToList();
        }

        public List<Mods.Beer> GetAllBeers(Mods.Store store)
        {

            //var pe = (decimal)_context.Inventories.Where(invo => invo.BeersId == beer.Id).Select(p => p.Price);

            return _context.Beers.Where(beero => beero.StoresId == store.Id).Select(beer => new Mods.Beer() {
                Id = beer.Id,
                Name = beer.Name,
                Price = (decimal)_context.Inventories.Where(invo => invo.BeersId == beer.Id).Select(p => p.Price).ToArray()[0],
                Stock = (int)_context.Inventories.Where(invo => invo.BeersId == beer.Id).Select(s => s.Stock).ToArray()[0]

            }).ToList();
        }

        public List<Mods.Beer> GetAllBeers()
        {

            //var pe = (decimal)_context.Inventories.Where(invo => invo.BeersId == beer.Id).Select(p => p.Price);

            return _context.Beers.Select(beer => new Mods.Beer() {
                Id = beer.Id,
                Name = beer.Name,
                Price = (decimal)_context.Inventories.Where(invo => invo.BeersId == beer.Id).Select(p => p.Price).ToArray()[0],
                Stock = (int)_context.Inventories.Where(invo => invo.BeersId == beer.Id).Select(s => s.Stock).ToArray()[0]

            }).ToList();
        }

        public Mods.Order AddOrder(Mods.Order ordo, int custoindex)
        {
            Entity.LineItem linotoadd = new Entity.LineItem(){
                Quantity = ordo.Quantity,
                BeersId = ordo.SelectedBeer.Id

            };

            _context.Add(linotoadd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            Entity.Order ordotoadd = new Entity.Order(){
                CustomerId = custoindex + 1,
                LineItemId = linotoadd.Id,
                OrderDate = DateTime.Today
            };

            _context.Add(ordotoadd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();


             System.Console.WriteLine("updating inv");
            Entity.Inventory invotoupdate2 = new Entity.Inventory() {
                Id = _context.Inventories.Where(invo => invo.BeersId == linotoadd.BeersId).Select(ii => ii.Id).ToArray()[0],
                Price = ordo.SelectedBeer.Price,
                Stock = ordo.SelectedBeer.Stock - ordo.Quantity,
                BeersId = linotoadd.BeersId

            };

            _context.Inventories.Update(invotoupdate2);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            Console.WriteLine("Order Successful\n");
            return ordo;
        }

        public List<Order> GetAllOrders()
        {
            List<Mods.Beer> obeers = GetAllBeers();

            //Mods.Beer storebeer =
            return _context.Orders.Select(ordo => new Mods.Order() {
                Id = ordo.Id,
                Date = ordo.OrderDate,
                Quantity = (int)_context.LineItems.Where(lino => lino.Id == ordo.LineItemId).Select(q => q.Quantity).ToArray()[0],
                SelectedBeer = obeers[(int)_context.LineItems.Where(lino => lino.Id == ordo.LineItemId).Select(b => b.BeersId).ToArray()[0] - 1],
                CustomerIndex = ordo.CustomerId - 1,
                StoresId = (int)_context.LineItems.Where(sto => sto.Id == ordo.LineItemId).Select(i => i.Beers.StoresId).ToArray()[0]


            }).ToList();
        }

        public Mods.Beer UpdateInventory(int toadd, Mods.Beer ivbeer)
        {
            System.Console.WriteLine("updating inv");
            Entity.Inventory invotoupdate = new Entity.Inventory() {
                Id = _context.Inventories.Where(invo => invo.BeersId == ivbeer.Id).Select(ii => ii.Id).ToArray()[0],
                Price = ivbeer.Price,
                Stock = ivbeer.Stock + toadd,
                BeersId = ivbeer.Id

            };

            _context.Inventories.Update(invotoupdate);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return ivbeer;
        }
        
    }
}