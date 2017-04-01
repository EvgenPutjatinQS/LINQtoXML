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
            Console.WriteLine("Выберите вариант вывода данных:");
            Console.WriteLine("1 - Все товары с информацией о магазине");
            Console.WriteLine("2 - Все магазины с информацией о товаре");
            Console.WriteLine("3 - Только товары/магазины для которых есть все данные");
            Console.WriteLine("4 - Отобразить только продукты с уникальными значениями цен");
            Console.WriteLine("5 - Отобразить товар и соответствуюшие магазины, где цена меньше 10");
            Console.WriteLine("0 - Выход\n");

            //NewXMLDoc n_xml_d = new NewXMLDoc();
            //n_xml_d.New_xml();

            XMLGetData xml_Data = new XMLGetData();
            xml_Data.LoadData();

            Console.ReadLine();
        }
    }
}
