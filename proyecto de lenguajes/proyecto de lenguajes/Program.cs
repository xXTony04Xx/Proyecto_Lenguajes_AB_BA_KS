using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Proyecto_Lenguajes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;
            //tabla hash :p
            Dictionary<string, int> tablaHash = new Dictionary<string, int>();

            Console.WriteLine("1. Determinista");
            Console.WriteLine("2. No determinista");
            Console.WriteLine("3. salir");
            Console.WriteLine();
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1://determinista

                    LeerTxt();
                    break;
                case 2://no determinista
                    LeerTxt();
                    break;
                case 3://salir



                    Console.WriteLine("Hola");
                    break;

            }
        }

        static void LeerTxt()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\automata fin 11.txt");
            System.Console.WriteLine("Contenido del archivo = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);
            }
            Console.WriteLine("Presione cualquier tecla para salir.");
            System.Console.ReadKey();
        }
    }
}