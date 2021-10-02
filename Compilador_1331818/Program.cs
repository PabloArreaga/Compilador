using System;

namespace Compilador_1331818
{
	class Program
	{
		static void Main(string[] args)
		{
            Parser parser = new Parser();
            string opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("-->> Mi primer compilador <<-- \n");
                Console.WriteLine("Ingrese una cadena:");
                try
                {
                    var result = parser.Parse(Console.ReadLine());
                    if (double.IsNegativeInfinity(result) || double.IsInfinity(result))
                    {
                        Console.WriteLine("Error matemático");
                    }  
                    else
                    {
                        Console.WriteLine("\nResultado: " + result + "\n");
                    }
                        
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Si desea realizar otra operación ingrese >> 1 <<");

                opcion = Console.ReadLine();

            } while (opcion == "1");
        }
	}
}
