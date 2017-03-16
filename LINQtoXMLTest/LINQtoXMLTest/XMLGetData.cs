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
                    foreach (var dist in sort_Product)
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
                case 2:
                    foreach (var dist_shop in sort)
                    {
                        Console.WriteLine("\nShop: {0}", dist_shop.DistrName);
                        foreach (var dist in sort_Product)
                        {
                            if (dist_shop.DistrID == dist.DistrID_Prod)
                            {
                                Console.WriteLine("Product ID: {0} \nProduct Name: {1}", dist.ProductID, dist.ProductName);
                                Console.WriteLine("Product Discription: {0} \nPrace: {1}\n", dist.ProductDistrib, dist.Price);
                            }
                        }
                    }
                    Repead();
                    break;
                case 3:
                    foreach (var dist in sort_Product)
                    {
                        if (dist.Price != 0 && dist.ProductID != null)
                        {
                            Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1}", dist.ProductID, dist.ProductName);
                            Console.WriteLine("Product Discription: {0} \nPrace: {1}", dist.ProductDistrib, dist.Price);
                            foreach (var dist_shop in sort)
                            {
                                if (dist_shop.DistrID == dist.DistrID_Prod)
                                    Console.WriteLine("Shop: {0}", dist_shop.DistrName);
                            }
                        }
                     }
                    Repead();
                    break;
                case 4:
                    
                    foreach (var dist in sort_Product)
                    {
                        if (dist.Price != 0)
                        {
                            Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1}", dist.ProductID, dist.ProductName);
                            Console.WriteLine("Product Discription: {0} \nPrace: {1}", dist.ProductDistrib, dist.Price);
                            foreach (var dist_shop in sort)
                            {
                                if (dist_shop.DistrID == dist.DistrID_Prod)
                                    Console.WriteLine("Shop: {0}", dist_shop.DistrName);
                            }
                        }
                    }
                    Repead();
                    break;
                case 5:
                    var sortPrace = products.OrderBy(i => i.Price);

                    foreach (var dist in sortPrace)
                    {
                        if (dist.Price <= 10 && dist.Price != 0)
                        {
                            Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1}", dist.ProductID, dist.ProductName);
                            Console.WriteLine("Product Discription: {0} \nPrace: {1}", dist.ProductDistrib, dist.Price);
                            foreach (var dist_shop in sort)
                            {
                                if (dist_shop.DistrID == dist.DistrID_Prod)
                                    Console.WriteLine("Shop: {0}", dist_shop.DistrName);
                            }
                        }
                    }
                    Repead();
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
