using System;
using System.Threading;


namespace lab1
{
    class ThreadTest
    {
        static void Main(string[] args)
        {
            Thread thr = new Thread(WriteY);
            thr.Start();
            while(true)
                Console.Write("X");
        }

        static void WriteY()
        {
            while(true)
                Console.Write("Y");
        }
    }
}

