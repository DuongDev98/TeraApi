using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT
{
    class Program
    {
        static void Main(string[] args)
        {
            TeraAppConnect connect = new TeraAppConnect("71b246ca-f7e0-4b81-864f-40b708c19a3e", "102363ed-f0be-4425-8f33-3f488eda53b5");
            connect.GetOrderList();
            Console.ReadLine();
        }
    }
}