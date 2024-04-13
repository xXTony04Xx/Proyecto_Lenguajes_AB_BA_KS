using System;
using System.Collections.Generic;
using System.IO;

namespace Proyecto_Lenguajes
{

	class Automata
    {
        public int NumeroEstados { get; set; }
        public string EstadoInicial { get; set; }
        public List<string> EstadoFinal { get; set; } = new List<string>();
        public List<(string EstadoActual, char Simbolo, string EstadoFuturo)> Tabla { get; set; } = new List<(string, char, string)>();
    }

    internal class Program
    {
        static Automata automata = new Automata();
        
        static void Main(string[] args)
        {
            int opcion = 0;

			bool seguir2 = true;
			string dato2 = "";

            while (seguir2)
            {
                Console.WriteLine("1. Determinista");
                Console.WriteLine("2. No determinista");
                Console.WriteLine("3. Salir");
				Console.Write("Escoja una opción: ");
                opcion = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        if (LeerArchivo())
                        {
                            Valuar();
                        }
						
                        break;
                    case 2:
						if (LeerArchivo())
						{
							ValuarND();
						}
						break;
                    case 3:
                        Console.WriteLine("Adios :)");
                        Console.ReadKey();
						seguir2 = false;
						break;
                }
                if (seguir2 == true)
                {
                    Console.Clear();
                    Console.WriteLine("¿Desea probar otro automata? (si/no)");
                    dato2 = Console.ReadLine();
                    if (dato2.ToLower() == "no")
                    {
                        seguir2 = false;
						Console.WriteLine("Adios :)");
						Console.ReadKey();
					}
                }
			}
        }

        static bool LeerArchivo()
        {
            Console.WriteLine("Ingrese la ruta del archivo:");
            string filePath = Console.ReadLine();
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    int numberOfStates = int.Parse(reader.ReadLine());
                    automata.NumeroEstados = numberOfStates;

                    string initialState = reader.ReadLine();
                    automata.EstadoInicial = initialState;

                    string[] finalStates = reader.ReadLine().Split(',');
                    automata.EstadoFinal.AddRange(finalStates);

                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            string estadoActual = parts[0].Trim();
                            char simbolo = parts[1].Trim()[0];
                            string estadoFuturo = parts[2].Trim();
                            automata.Tabla.Add((estadoActual, simbolo, estadoFuturo));
                        }
                    }
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Archivo no encontrado.");
				Console.ReadKey();
				Console.Clear();
				return false;
			}
        }

        static void Valuar()
        {
            bool seguir = true;
            string dato = "";

            while (seguir)
            {
                Console.WriteLine("¿Desea evaluar una cadena? (si/no)");
                dato = Console.ReadLine();
                if (dato.ToLower() == "no")
                {
                    seguir = false;
                }
                else
                {
                    Console.Clear();
                    Console.Write("Ingrese la cadena a evaluar:");
                    string cadena = Console.ReadLine();
					Console.WriteLine("Procedimiento: ");
					int posicionCadena = 0;
                    string estadoActual = automata.EstadoInicial;

                    while (posicionCadena < cadena.Length)
                    {
                        bool transicionEncontrada = false;
                        foreach (var transicion in automata.Tabla)
                        {
                            if (transicion.EstadoActual == estadoActual && transicion.Simbolo == cadena[posicionCadena])
                            {
								Console.WriteLine(estadoActual + ", " + cadena[posicionCadena] + ", " + transicion.EstadoFuturo);
								estadoActual = transicion.EstadoFuturo;
                                posicionCadena++;
                                transicionEncontrada = true;
                                break;
                            }
                        }
                        if (!transicionEncontrada)
                        {
							Console.WriteLine("--------------------");
							Console.WriteLine("La cadena no es válida");
                            Console.ReadLine();
                            break;
                        }
                    }

                    if (posicionCadena == cadena.Length && automata.EstadoFinal.Contains(estadoActual))
                    {
						Console.WriteLine("--------------------");
						Console.WriteLine("La cadena es válida");
                        Console.ReadLine();
                    }
                    else if (posicionCadena == cadena.Length)
                    {
						Console.WriteLine("--------------------");
						Console.WriteLine("La cadena no es válida");
                        Console.ReadLine();
                    }
                }
            }
        }


		//No determinista

		static void ValuarND()
		{
			bool seguir = true;
			string dato;

			while (seguir)
			{
				Console.WriteLine("¿Desea evaluar una cadena? (si/no)");
				dato = Console.ReadLine();
				if (dato.ToLower() == "no")
				{
					seguir = false;
				}
				else
				{
					Console.Clear();
					Console.Write("Ingrese la cadena a evaluar:");
					string cadena = Console.ReadLine();
					Console.WriteLine("Procedimiento: ");

					List<string> validaciones = new List<string>();
					List<int> nocadena = new List<int>();
					validaciones.Add(automata.EstadoInicial);
                    nocadena.Add(0);
                    int indicevalidaciones = 0;
                    bool aceptada = false;

                    while(indicevalidaciones < validaciones.Count)
                    {
						Console.WriteLine("Rama " + indicevalidaciones);

                        string estadoActual = validaciones[indicevalidaciones];
                        string estadoActualP = validaciones[indicevalidaciones];
						int posicionCadena = nocadena[indicevalidaciones];
						while (posicionCadena < cadena.Length)
                        {
							bool agregar = false;
							foreach (var transicion in automata.Tabla)
                            {
                                if (transicion.EstadoActual == estadoActual && transicion.Simbolo == cadena[posicionCadena])
                                {
									if (agregar == false)
                                    {
										Console.WriteLine(estadoActual + ", " + cadena[posicionCadena] + ", " + transicion.EstadoFuturo);
										estadoActualP = transicion.EstadoFuturo;
                                        agregar = true;
                                    }
                                    else
                                    {
										Console.WriteLine(estadoActual + ", " + cadena[posicionCadena] + ", " + transicion.EstadoFuturo + " (NUEVA)");
										validaciones.Add(transicion.EstadoFuturo);
                                        nocadena.Add(posicionCadena + 1);
									}
                                }
                            }
                            estadoActual = estadoActualP;
							posicionCadena++;
						}
						Console.WriteLine("------------------------------");

						if (automata.EstadoFinal.Contains(estadoActual))
                        {
                            Console.WriteLine("Estado Final.");
                            aceptada = true;
                        }
                        else
                        {
							Console.WriteLine("Estado No Final.");
						}

                        Console.WriteLine();

						Console.ReadKey();
						indicevalidaciones++;
					}

					Console.WriteLine("------------------------------");

					if (aceptada)
                    {
                        Console.WriteLine("Cadena Aceptada");
                    }
                    else
                    {
						Console.WriteLine("Cadena No Aceptada");
					}

					Console.WriteLine("------------------------------");
				}
			}
		}
	}
}
