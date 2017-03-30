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
                               ProductDistrib = distr_Prod.Element("productDiscription").Value,
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
                    var group_prod = products.GroupJoin(distributor, pr => pr.DistrID_Prod,
                                                          dis => dis.DistrID,
                                                          (prod, distr) => new
                                                          {
                                                              ProductName = prod.ProductName,
                                                              ProductDistrib = prod.ProductDistrib,
                                                              IDProd = prod.ProductID,
                                                              Price = prod.Price,
                                                              DistrName = distr.Select(d => d.DistrName)
                                                          });
                    foreach (var p in group_prod)
                    {
                        Console.WriteLine("\nID Product: {0}\nShop: {1}\nDiscription: {2}\nPrice: {3}", 
                                           p.IDProd, p.ProductName, p.ProductDistrib, p.Price);
                        foreach (var s in p.DistrName)
                        {
                            Console.WriteLine("Shop: {0}", s);
                        }
                    }
                    Repead();
                    break;
                case 2:
                    var group_shop = distributor.GroupJoin(products, dis => dis.DistrID,
                                                          pr => pr.DistrID_Prod,
                                                          (dist, prod) => new
                                                          {
                                                              DistrName = dist.DistrName,
                                                              ID = dist.DistrID,
                                                              ProductName = prod.Select(p => p.ProductName)
                                                          });
                    foreach (var s in group_shop)
                    {
                        Console.WriteLine("\nShop: {0}, ID: {1}", s.DistrName, s.ID);
                        foreach (var prod in s.ProductName)
                        Console.WriteLine("Product: {0}", prod);
                    }
                    Repead();
                    break;
                case 3:
                    var fullInfo = from p in products
                                   join s in distributor on p.DistrID_Prod equals s.DistrID
                                   where (int)p.Price != 0 && p.DistrID_Prod != "" && s.DistrID != ""
                                   orderby p.Price
                                   select new
                                   {
                                       DistrID_Prod = p.DistrID_Prod,
                                       ProductName = p.ProductName,
                                       ProductDistrib = p.ProductDistrib,
                                       Price = p.Price,
                                       DistrName = s.DistrName
                                   };
                    foreach (var dist in fullInfo)
                    {
                        Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1}\nProduct Discription: {2} \nPrace: {3}\nShop: {4}", 
                                            dist.DistrID_Prod, dist.ProductName, dist.ProductDistrib, dist.Price, dist.DistrName);
                    }
                    Repead();
                    break;
                case 4:
                    var group_del = (from i in products
                              join s in distributor on i.DistrID_Prod equals s.DistrID
                              select new Delete{ ProductID_Del = i.ProductID,
                                  ProductName_Del = i.ProductName,
                                  ProductDistrib_Del = i.ProductDistrib,
                                  DistrID_Prod_Del = i.DistrID_Prod,
                                  Price_Del = i.Price,
                                  DistrName_Del = s.DistrName}).Distinct();
                    var del = from i in group_del
                              where i.Price_Del != 0
                              orderby i.Price_Del
                              select i;
                    foreach (var dist in del)
                    {
                        Console.WriteLine("\nProduct ID: {0} \nProduct Name: {1} \nProduct Discription: {2} \nPrace: {3}",
                                            dist.ProductID_Del, dist.ProductName_Del, dist.ProductDistrib_Del, dist.Price_Del);
                    }
                    Repead();
                    break;
                case 5:
                    var sort_price = from p in products
                                     join s in distributor on p.DistrID_Prod equals s.DistrID
                                      where ((int)p.Price < 10 && (int)p.Price != 0)
                                      orderby p.Price
                                      select new {
                                         DistrID_Prod = p.DistrID_Prod,
                                         ProductName = p.ProductName,
                                         ProductDistrib = p.ProductDistrib,
                                         Price = p.Price,
                                         DistrName = s.DistrName};
                    foreach (var p in sort_price)
                    {
                        Console.WriteLine("\nID Product: {0}\nShop: {1}\nDiscription: {2}\nPrice: {3}\nShop: {4}", 
                                            p.DistrID_Prod, p.ProductName, p.ProductDistrib, p.Price, p.DistrName);
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

    class Delete
    {
        public string ProductID_Del { get; set; }
        public string ProductName_Del { get; set; }
        public string ProductDistrib_Del { get; set; }
        public string DistrID_Prod_Del { get; set; }
        public int Price_Del { get; set; }
        public string DistrName_Del { get; set; }

        public override bool Equals(object obj)
        {
            return this.Price_Del == ((Delete)obj).Price_Del;
        }

        public override int GetHashCode()
        {
            return this.Price_Del.GetHashCode();
        }
    }
}
