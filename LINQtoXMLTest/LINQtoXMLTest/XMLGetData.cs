using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LINQtoXMLTest
{
    class XMLGetData 
    {

        XDocument xmlData;
        XDocument xmlDataProduct;

        public XMLGetData()
        {
            xmlData = XDocument.Load("Distributor.xml");
            xmlDataProduct = XDocument.Load("Product.xml");
        }

        public void LoadData()
        {
            var products = from distr_Prod in xmlDataProduct.Element("products").Elements("productId")
                           select new Product
                           {
                               ProductID = distr_Prod.Attribute("ID_Prod").Value,
                               ProductName = distr_Prod.Element("productName").Value,
                               ProductDescrip = distr_Prod.Element("productDescription").Value,
                               DistrID_Prod = distr_Prod.Element("distributorId").Value,
                               Price = (int)distr_Prod.Element("price")
                           };
            var distributor = from distr in xmlData.Element("distributors").Elements("distributorId")
                              select new Distributor
                              {
                                  DistrID = distr.Attribute("ID_Distr").Value,
                                  DistrName = distr.Element("distributorName").Value
                              };

            int currVal = Int32.Parse(Console.ReadLine());
            switch (currVal)
            { 
                case 1:
                    Console.WriteLine("Все товары с информацией о соответствующем магазите (если есть), \nотсортированные по producrId");
                    var group_prod = (from p in products
                                     join s in distributor on p.DistrID_Prod equals s.DistrID
                                     select new {
                                                    ProductID = p.ProductID,
                                                    ProductName = p.ProductName,
                                                    ProductDistrib = p.ProductDescrip,
                                                    Price = p.Price,
                                                    DistrName = s.DistrName
                                                })
                                     .OrderBy(i => i.ProductID);
                    foreach (var p in group_prod)
                    {
                        Console.WriteLine("\nProduct ID: {0}\nProduct Name: {1}\nProduct Description: {2}\nPrice: {3}\nShop: {4}",
                                           p.ProductID, p.ProductName, p.ProductDistrib, p.Price, p.DistrName);
                    }
                    Repeat();
                    break;
                case 2:
                    Console.WriteLine("Все магазины с информацией о товарах в них (если есть),\nкаждая пара магазин/товар - отдельный результат; \nсортировка по distributorId");
                    var group_shop = (from s in distributor
                                     join p in products on s.DistrID equals p.DistrID_Prod
                                     select new{
                                                    DistrID = s.DistrID,
                                                    DistrName = s.DistrName,
                                                    ProductID = p.ProductID,
                                                    ProductName = p.ProductName,
                                                    ProductDistrib = p.ProductDescrip,
                                                    Price = p.Price    
                                                })
                                     .OrderBy(i => i.DistrID);
                    foreach (var s in group_shop)
                    {
                        Console.WriteLine("\nShop: {0}, ID: {1}\nProduct ID: {2}\nProduct Name: {3}\nProduct Description: {4}\nPrice: {5}",
                                        s.DistrName, s.DistrID, s.ProductID, s.ProductName, s.ProductDistrib, s.Price);
                    }
                    Repeat();
                    break;
                case 3:
                    Console.WriteLine("Только те товары/магазины, для которых данные есть и в одной и другой таблице");
                    var fullInfo = (from p in products
                                   join s in distributor on p.DistrID_Prod equals s.DistrID
                                   select new {
                                                ProductID = p.ProductID,
                                                DistrID_Prod = p.DistrID_Prod,
                                                ProductName = p.ProductName,
                                                ProductDistrib = p.ProductDescrip,
                                                Price = p.Price,
                                                DistrName = s.DistrName
                                              })
                                    .Where(i => i.Price != 0 && i.DistrID_Prod != "" && i.ProductID != "")
                                    .OrderBy(i => i.ProductID);
                    foreach (var dist in fullInfo)
                    {
                        Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1}\nProduct Description: {2} \nPrace: {3}\nShop: {4}", 
                                            dist.ProductID, dist.ProductName, dist.ProductDistrib, dist.Price, dist.DistrName);
                    }
                    Repeat();
                    break;
                case 4:
                    Console.WriteLine("Отобразить только продукты с уникальными значениями цен");
                    var group_Uniq = (from i in products select i)
                                      .Distinct()
                                      .Where(i => i.Price != 0)
                                      .OrderBy(i => i.Price);
                    foreach (var dist in group_Uniq)
                    {
                        Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1} \nProduct Description: {2} \nPrace: {3}",
                                            dist.ProductID, dist.ProductName, dist.ProductDescrip, dist.Price);
                    }
                    Repeat();
                    break;
                case 5:
                    Console.WriteLine("Отобразить продукты и соответствующие магазины, \nгде цена меньше 10, сортировка по возрастанию цены");
                    var sort_price = (from p in products
                                      join s in distributor on p.DistrID_Prod equals s.DistrID
                                      select new {
                                                    DistrID_Prod = p.DistrID_Prod,
                                                    ProductName = p.ProductName,
                                                    ProductDistrib = p.ProductDescrip,
                                                    Price = p.Price,
                                                    DistrName = s.DistrName
                                                 })
                                     .Where(i => i.Price <10 && i.Price != 0)
                                     .OrderBy(i => i.Price);
                    foreach (var p in sort_price)
                    {
                        Console.WriteLine("\nProduct ID: {0}\nProduct Name: {1}\nProduct Description: {2}\nPrice: {3}\nShop: {4}", 
                                            p.DistrID_Prod, p.ProductName, p.ProductDistrib, p.Price, p.DistrName);
                    }
                    Repeat();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }

        void Repeat()
        {
            Console.Write("\nПовторить выбор вывода данных  ");
            LoadData();
        }
    }

    class Distributor
    {
        public string DistrID { get; set; }
        public string DistrName { get; set; }
    }

    class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescrip { get; set; }
        public string DistrID_Prod { get; set; }
        public int Price { get; set; }

        public override bool Equals(object obj)
        {
            return this.Price == ((Product)obj).Price;
        }

        public override int GetHashCode()
        {
            return this.Price.GetHashCode();
        }
    }

}
