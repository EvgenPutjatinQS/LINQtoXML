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
                               ProductID = ((string)distr_Prod.Attribute("ID_Prod")),
                               ProductName = ((string)distr_Prod.Element("productName")),
                               ProductDistrib = ((string)distr_Prod.Element("productDiscription")),
                               DistrID_Prod = ((string)distr_Prod.Element("distributorId")),
                               Price = ((int)distr_Prod.Element("price"))
                           };
            var distributor = from distr in xmlData.Element("distributors").Elements("distributorId")
                              select new Distributor
                              {
                                  DistrID = ((string)distr.Attribute("ID_Distr")),
                                  DistrName = ((string)distr.Element("distributorName"))
                              };
            IEnumerable<Product> sort_Product = products.OrderBy(i => i.ProductID);
            IEnumerable<Distributor> sort = distributor.OrderBy(i => i.DistrID);

            int currVal = Int32.Parse(Console.ReadLine());
            switch (currVal)
            { 
                case 1:
                    var group_prod = products.GroupJoin(distributor, pr => pr.DistrID_Prod, 
                                                          dis => dis.DistrID,
                                                          (prod, distr) => new
                                                          {ProductName = prod.ProductName,
                                                              ProductDistrib = prod.ProductDistrib,
                                                              IDProd = prod.ProductID,
                                                           Price = prod.Price,
                                                           DistrName = distr.Select(d => d.DistrName)
                                                          });
                    var sort_prod = from s in group_prod
                                    orderby s.IDProd
                                        select s;
                    foreach (var s in sort_prod)
                    {
                        Console.WriteLine("\nID: {0}\nShop: {1}\nDiscription: {2}\nPrice: {3}\nShop: {4}"
                                            , s.IDProd, s.ProductName, s.ProductDistrib, s.Price, s.DistrName); 
                    }
                    Repead();
                    break;
                case 2:
                    var group_shop = distributor.GroupJoin(products, dis => dis.DistrID, 
                                                          pr => pr.DistrID_Prod,
                                                          (dist, prod) => new
                                                          {DistrName = dist.DistrName,
                                                           ID = dist.DistrID,
                                                           ProductName = prod.Select(p => p.ProductName)});
                    var sort_shop = from s in group_shop
                                    orderby s.ID
                                        select s;
                    foreach (var s in sort_shop)
                    {
                        Console.WriteLine("\nShop: {0}, ID: {1}", s.DistrName, s.ID);
                        foreach (var prod in s.ProductName)
                        { Console.WriteLine("Product: {0}", prod); }
                    }
                    Repead();
                    break;
                case 3:
                    var fullInfo = from fi in products
                                   where ((int)fi.Price != 0 && (string)fi.ProductID != null)
                                   select fi;
                    foreach (var dist in fullInfo)
                    {
                        Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1}", dist.ProductID, dist.ProductName);
                        Console.WriteLine("Product Discription: {0} \nPrace: {1}", dist.ProductDistrib, dist.Price);
                        foreach (var dist_shop in sort)
                        {
                            if (dist_shop.DistrID == dist.DistrID_Prod)
                            Console.WriteLine("Shop: {0}", dist_shop.DistrName);
                        }
                     }
                    Repead();
                    break;
                case 4:
                    var _sp = from i in products
                             where (i.Price != 0)
                             orderby(i.Price)
                             select i;
                    foreach (var dist in _sp)
                    {
                        Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1} \nProduct Discription: {2} \nPrace: {3}",
                                            dist.ProductID, dist.ProductName, dist.ProductDistrib, dist.Price);
                    }
                    Repead();
                    break;
                case 5:
                    var sp = from i in products
                             where ((int)i.Price < 10 && (int)i.Price != 0)
                             orderby(i.Price)
                             select i;
                    foreach (var dist in sp)
                    {
                        Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1} \nProduct Discription: {2} \nPrace: {3}",
                                            dist.ProductID, dist.ProductName, dist.ProductDistrib, dist.Price);
                          foreach (var dist_shop in sort)
                          {
                             if (dist_shop.DistrID == dist.DistrID_Prod)
                             Console.WriteLine("Shop: {0}", dist_shop.DistrName);
                          }
                    }
                    Repead();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }

        void Repead()
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
        public string ProductDistrib { get; set; }
        public string DistrID_Prod { get; set; }
        public int Price { get; set; }
    }
}
