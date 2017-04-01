using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LINQtoXMLTest
{
    class NewXMLDoc
    {
        public void New_xml()
        {
            XDocument xml_doc_prod = new XDocument(new XElement("products",
                new XElement("productId",
                    new XAttribute("ID_Prod", 4893),
                    new XElement("productName", "Ноутбук HP 250"),
                    new XElement("productDescription", "HP"),
                    new XElement("distributorId", 6583),
                    new XElement("price", 9)),
                new XElement("productId",
                    new XAttribute("ID_Prod", ""),
                    new XElement("productName", "Ноутбук HP 250 G5"),
                    new XElement("productDescription", "HP"),
                    new XElement("distributorId", 3563),
                    new XElement("price", 0)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 1964),
                    new XElement("productName", "Ноутбук Asus X756UA"),
                    new XElement("productDescription", "Asus"),
                    new XElement("distributorId", ""),
                    new XElement("price", 14)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 3966),
                    new XElement("productName", "Ноутбук Asus X540LJ"),
                    new XElement("productDescription", "Asus"),
                    new XElement("distributorId", 3563),
                    new XElement("price", 12)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 1037),
                    new XElement("productName", "Ноутбук Asus VivoBook X556UQ"),
                    new XElement("productDescription", "Asus"),
                    new XElement("distributorId", 1214),
                    new XElement("price", 20)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 1854),
                    new XElement("productName", "Ноутбук Lenovo IdeaPad 110-15"),
                    new XElement("productDescription", "Lenovo"),
                    new XElement("distributorId", 6583),
                    new XElement("price", 8)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 8653),
                    new XElement("productName", "Ноутбук Acer Aspire F5-573G-51Q7"),
                    new XElement("productDescription", "Acer"),
                    new XElement("distributorId", 3563),
                    new XElement("price", 0)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 6590),
                    new XElement("productName", "Ноутбук Lenovo IdeaPad 100-15 IBD"),
                    new XElement("productDescription", "Lenovo"),
                    new XElement("distributorId", 6583),
                    new XElement("price", 12)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 2263),
                    new XElement("productName", "Ноутбук Dell Inspiron 3552"),
                    new XElement("productDescription", "Dell"),
                    new XElement("distributorId", 1214),
                    new XElement("price", 8)),
                new XElement("productId",
                    new XAttribute("ID_Prod", 3984),
                    new XElement("productName", "Ноутбук Lenovo IdeaPad 310-15"),
                    new XElement("productDescription", "Lenovo"),
                    new XElement("distributorId", ""),
                    new XElement("price", 12))));

            XDocument xml_doc_distr = new XDocument(new XElement("distributors",
                new XElement("distributorId",
                    new XAttribute("ID_Distr", 1214),
                    new XElement("distributorName", "PCShop")),
                new XElement("distributorId",
                    new XAttribute("ID_Distr", 3563),
                    new XElement("distributorName", "ABC")),
                new XElement("distributorId",
                    new XAttribute("ID_Distr", ""),
                    new XElement("distributorName", "Comfi")),
                new XElement("distributorId",
                    new XAttribute("ID_Distr", 6583),
                    new XElement("distributorName", "ElMir")),
                new XElement("distributorId",
                    new XAttribute("ID_Distr", ""),
                    new XElement("distributorName", "Rozetka"))));


            xml_doc_prod.Save("Product.xml");
            xml_doc_distr.Save("Distributor.xml");

            Console.WriteLine(xml_doc_distr);
            Console.WriteLine("\n" + xml_doc_prod);
        }
    }
}
