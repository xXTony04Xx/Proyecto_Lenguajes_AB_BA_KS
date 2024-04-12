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
        public int numeroEstados;
        public string EstadoInicial;
        public List<string> EstadoFinal = new List<string>();
        public List<(string EstadoActual, char Simbolo, string EstadoFuturo)> tabla = new List<(string, char, string)>();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;

            Console.WriteLine("1. Determinista");
            Console.WriteLine("2. No determinista");
            Console.WriteLine("3. salir");
            Console.WriteLine();
            opcion = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (opcion)
            {
                case 1://determinista

                    Automata automata = new Automata();
                    Console.WriteLine("Ingrese la ruta del archivo:");
                    string filePath = Console.ReadLine();
                    HashSet<string> estadosunicos = new HashSet<string>();
                    if (File.Exists(filePath))
                    {
                        using (var reader = new StreamReader(filePath))
                        {
                            // Leer el # de estados (n)
                            int numberOfStates = int.Parse(reader.ReadLine());
                            automata.numeroEstados = numberOfStates;

                            // Leer el estado inicial (1..n)
                            string initialState = reader.ReadLine();
                            automata.EstadoInicial = initialState;

                            // Leer el conjunto de estados finales separados por comas
                            string[] finalStates = reader.ReadLine().Split(',');
                            foreach (var estado in finalStates)
                            {
                                automata.EstadoFinal.Add(estado);
                            }

                            //Leer transiciones
                            string[] lines = File.ReadAllLines(filePath);
                            int contador = 0;
                            foreach (string line in lines)
                            {
                                string[] parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (parts.Length == 3)
                                {
                                    string estadoActual = parts[0].Trim();
                                    char simbolo = parts[1].Trim()[0];
                                    string estadoFuturo = parts[2].Trim();
                                    automata.tabla.Add((estadoActual, simbolo, estadoFuturo));
                                    estadosunicos.Add(estadoActual);
                                    estadosunicos.Add(estadoFuturo);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Archivo no encontrado.");
                    }
                    Console.Clear();
                    Console.WriteLine("Ingrese la cadena a evaluar:");
                    string cadena = Console.ReadLine();
                    int posicioncadena = 0;
                    bool continuar = true;
                    bool pasoInicio = false, EstadoCadena = false;
                    if (estadosunicos.Count == automata.numeroEstados)
                    {
                        string estadosiguiente = "";
                        while (continuar)
                        {
                            foreach (var validacion in automata.tabla)
                            {
                                if (posicioncadena <= (cadena.Length - 1))
                                {
                                    if (validacion.Simbolo == cadena[posicioncadena]) //Validar si la primera entrada existe en el automata
                                    {
                                        if (validacion.EstadoActual == automata.EstadoInicial && pasoInicio != true) //Validar si el estado actual es el inicial
                                        {
                                            pasoInicio = true;
                                            EstadoCadena = true;
                                            estadosiguiente = validacion.EstadoFuturo;
                                            posicioncadena++;
                                        }
                                        if (validacion.EstadoActual == estadosiguiente)
                                        {
                                            if (validacion.Simbolo == cadena[posicioncadena])
                                            {
                                                EstadoCadena = true;
                                                estadosiguiente = validacion.EstadoFuturo;
                                                posicioncadena++;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (EstadoCadena == true)
                                    {
                                        foreach (var estadoFinal in automata.EstadoFinal)
                                        {
                                            if (estadoFinal == estadosiguiente)
                                            {
                                                if (EstadoCadena == true)
                                                {
                                                    Console.WriteLine("La cadena es valida");
                                                    Console.ReadLine();
                                                    continuar = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("La cadena no es valida");
                                                    Console.ReadLine();
                                                    continuar = false;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    break;
                case 2://no determinista
                   
                    break;
                case 3://salir



                    Console.WriteLine("Adios");
                    Console.ReadKey();
                    break;

            }
        }
        #region Tony
        //static void LeerCarpeta(string carpeta)
        //{
        //    int contadorTemp = 1;
        //    int opcion2 = 0;
        //    string[] archivos = Directory.GetFiles(carpeta);

        //    Console.WriteLine("Archivos en la carpeta:");

        //    foreach (string archivo in archivos)
        //    {
        //        Console.WriteLine( contadorTemp + ". " + Path.GetFileName(archivo));
        //        contadorTemp++;
        //    }

        //    Console.WriteLine("Seleccione el archivo a abrir y si desea enviar una ruta de forma manual, seleccione el no.6");
        //    opcion2 = int.Parse(Console.ReadLine());
        //    Console.Clear();

        //    switch(opcion2)
        //        {
        //        case 1:
        //            LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\(0,1) 0 multiplos de 3 y 1 multiplos de 4 (1).txt");

        //            break;

        //        case 2:
        //            LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\automata fin 11.txt");
        //            break;

        //        case 3:
        //            LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\autómata no.2 – (abc  (ab))  (cc).txt");
        //            break;

        //        case 4:
        //            LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\automata3.txt");
        //            break;

        //        case 5:
        //            LeerArchivo(@"C:\Users\antho\OneDrive\Escritorio\Automatas deterministas\automata4.txt");
        //            break;

        //        case 6:
        //            string rutaArchivo = null;
        //            Console.WriteLine("ha escogido la opcion de ingreso manual, pegue la ruta de su archivo .txt");
        //            rutaArchivo = Console.ReadLine();

        //            if (!string.IsNullOrEmpty(rutaArchivo))
        //            {
        //                // Verificar si el archivo existe en la ruta especificada
        //                if (File.Exists(rutaArchivo))
        //                {
        //                    // Llamar a la función para leer el archivo
        //                    LeerArchivo(rutaArchivo);
        //                }
        //                else
        //                {
        //                    Console.WriteLine("El archivo no existe en la ruta especificada.");
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("La ruta ingresada es nula o vacía. No se puede continuar.");
        //            }

        //            break;
        //    }


        //}

        //static void LeerArchivo(string rutaArchivo)
        //{

        //    try
        //    {

        //        string contenido = File.ReadAllText(rutaArchivo);
        //        Console.WriteLine("Contenido del archivo:");
        //        Console.WriteLine(contenido);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Se produjo un error al intentar leer el archivo:");
        //        Console.WriteLine(ex.Message);
        //    }

        //}
        #endregion
    }
}