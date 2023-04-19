using System.Threading;
using System;
using Unipluss.Sign.ExternalContract.Entities;
using System.Runtime.ConstrainedExecution;

static class Program
{


    static int BufferSize = 10;
    static object[] arr = new object[BufferSize];
    static bool count = false;

    static void Main()
    {
        bool run = true;

        while (run == true)
        {
            Thread t = new Thread(Producer);
            t.Name = "Producer";
            t.Priority = ThreadPriority.Normal;
            t.Start();

            Thread t2 = new Thread(Consumer);
            t2.Name = "Consumer";
            t2.Priority = ThreadPriority.Normal;
            t2.Start();


            t.Join();
            t2.Join();
        }

    }

    internal class Drink
    {
        public string drink = "beer";
    }


    static void Producer()
    {
        Drink obj = new Drink();

        counter();

        if (count == true)
        {
            lock (arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == null)
                    {
                        arr[i] = (obj.drink);
                        Console.WriteLine("Insert {0}", arr[i]);
                        Thread.Sleep(100);
                    }
                }
                count = false;
                Console.WriteLine("Producer waits.");
            }

        }

    }

    static void counter()
    {
        int counter = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == null)
            {
                counter++;
            }
        }

        if (counter >= BufferSize - 1)
        {
            count = true;
        }
    }

    static void Consumer()
    {
        lock (arr)
        {

            for (int i = 0; i < BufferSize - 1; i++)
            {
                if (arr[i] != null)
                {
                    Console.WriteLine("removed {0}", arr[i]);
                    arr[i] = null;
                    Thread.Sleep(100);
                }
            }

            Console.WriteLine("Consumer waits.");
        }

    }

















}