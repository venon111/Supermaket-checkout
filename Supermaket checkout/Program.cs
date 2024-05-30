using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int numCustomers = 20;
        int numSelfCheckouts = 4;
        int numCashiers = 5;

        // Scenario 1: One cashier processes multiple self-checkout lanes
        Console.WriteLine("Scenario 1: One cashier processes multiple self-checkout lanes");
        List<Thread> selfCheckoutThreads = new List<Thread>();
        for (int i = 0; i < numSelfCheckouts; i++)
        {
            int checkoutLane = i;
            Thread thread = new Thread(() => ProcessSelfCheckoutLane(numCustomers, checkoutLane));
            selfCheckoutThreads.Add(thread);
            thread.Start();
        }

        foreach (var thread in selfCheckoutThreads)
        {
            thread.Join();
        }

        // Scenario 2: Each cashier processes one checkout lane
        Console.WriteLine("\nScenario 2: Each cashier processes one checkout lane");
        List<Thread> cashierThreads = new List<Thread>();
        for (int i = 0; i < numCashiers; i++)
        {
            int cashier = i;
            Thread thread = new Thread(() => ProcessCashierLane(numCustomers / numCashiers, cashier));
            cashierThreads.Add(thread);
            thread.Start();
        }

        foreach (var thread in cashierThreads)
        {
            thread.Join();
        }
    }

    static void ProcessSelfCheckoutLane(int numCustomers, int checkoutLane)
    {
        for (int i = 0; i < numCustomers / 4; i++)
        {
            Console.WriteLine($"Self-checkout lane {checkoutLane + 1}: Processing customer {i + 1}");
            Thread.Sleep(100); // Simulate time taken to process a customer
        }
        Console.WriteLine($"Self-checkout lane {checkoutLane + 1}: Finished processing customers\n");
    }

    static void ProcessCashierLane(int numCustomers, int cashier)
    {
        for (int i = 0; i < numCustomers; i++)
        {
            Console.WriteLine($"Cashier {cashier + 1}: Processing customer {i + 1}");
            Thread.Sleep(100); // Simulate time taken to process a customer
        }
        Console.WriteLine($"Cashier {cashier + 10}: Finished processing customers\n");
    }
}

