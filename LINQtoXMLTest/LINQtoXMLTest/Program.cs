using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQtoXMLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //NewXMLDoc n_xml_d = new NewXMLDoc();
            //n_xml_d.New_xml();

            XMLGetData xml_Data = new XMLGetData();
            xml_Data.LoadData();

            Console.ReadLine();
        }
    }
}
