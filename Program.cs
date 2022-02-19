using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Net;
using System.Text;
namespace LruCache
{
    class Program
    {
       // static int[] a=new int[10];
        static void Main(string[] args)
        {
           
           var h= new HttpSer();
           int port=2679;
           h.Start(new IPEndPoint(IPAddress.Parse("127.0.0.1"),port),"127.0.0.1:"+port);
           Console.ReadKey();
        }
       
       
    }
}
