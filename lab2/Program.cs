using System;

namespace lab2
{
    class Program {

        static void Main(string[] args) {
            int amount = GetPositiveInt("Enter amount of data: ");

            
            Console.WriteLine("Hello World!");
        }

        static int GetPositiveInt(string prompt) {
            while (true) {
                try {
                    Console.WriteLine(promt);
                    string data = Console.ReadLine();

                    int result = Int32.Parse(data);
                    if (result < 1)
                        throw new ArgumentException("Number must be greater than 0");

                    return result;
                }
                catch(FormatException) {
                    Console.WriteLine("Bad format for {0}", data);
                    return -1;
                }
                catch(OverflowException) {
                    Console.WriteLine("Too big number {0}", data);
                    return -1;
                }
                catch(Exception e) {
                    Console.WriteLine("Error: {0}", e.Message);
                    return -1;
                }
            }
        }
    }
}
