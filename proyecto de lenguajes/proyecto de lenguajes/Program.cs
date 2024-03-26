using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;


namespace Proyecto_Lenguajes
{
    class Automata
    {
        public string inicial;
        public int noDeEstados;
        public string inicio;
        public string transicion;
        public string final;

    }
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
            Console.Clear();

            switch (opcion)
            {
                case 1://determinista

                    LeerCarpeta(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas");
                    break;
                case 2://no determinista
                   
                    break;
                case 3://salir



                    Console.WriteLine("Adios");
                    Console.ReadKey();
                    break;

            }
        }

        static void LeerCarpeta(string carpeta)
        {
            int contadorTemp = 1;
            int opcion2 = 0;
            string[] archivos = Directory.GetFiles(carpeta);

            Console.WriteLine("Archivos en la carpeta:");

            foreach (string archivo in archivos)
            {
                Console.WriteLine( contadorTemp + ". " + Path.GetFileName(archivo));
                contadorTemp++;
            }

            Console.WriteLine("Seleccione el archivo a abrir y si desea enviar una ruta de forma manual, seleccione el no.6");
            opcion2 = int.Parse(Console.ReadLine());
            Console.Clear();

            switch(opcion2)
                {
                case 1:
                    LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\(0,1) 0 multiplos de 3 y 1 multiplos de 4 (1).txt");

                    break;

                case 2:
                    LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\automata fin 11.txt");
                    break;

                case 3:
                    LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\autómata no.2 – (abc  (ab))  (cc).txt");
                    break;

                case 4:
                    LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\automata3.txt");
                    break;

                case 5:
                    LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\automata4.txt");
                    break;

                case 6:
                    string rutaArchivo = null;
                    Console.WriteLine("ha escogido la opcion de ingreso manual, pegue la ruta de su archivo .txt");
                    rutaArchivo = Console.ReadLine();

                    if (!string.IsNullOrEmpty(rutaArchivo))
                    {
                        // Verificar si el archivo existe en la ruta especificada
                        if (File.Exists(rutaArchivo))
                        {
                            // Llamar a la función para leer el archivo
                            LeerArchivo(rutaArchivo);
                        }
                        else
                        {
                            Console.WriteLine("El archivo no existe en la ruta especificada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("La ruta ingresada es nula o vacía. No se puede continuar.");
                    }

                    break;
            }
            
           
        }

        static void LeerArchivo(string rutaArchivo)
        {

            try
            {
                
                string contenido = File.ReadAllText(rutaArchivo);
                Console.WriteLine("Contenido del archivo:");
                Console.WriteLine(contenido);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error al intentar leer el archivo:");
                Console.WriteLine(ex.Message);
            }

        }
    }
}