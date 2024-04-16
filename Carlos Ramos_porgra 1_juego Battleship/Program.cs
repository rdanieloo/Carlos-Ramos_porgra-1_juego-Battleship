
//Carlos Ramos BattleShip

namespace JuegoBattleShipCarlosRamos
{
    class Program
    {
        static int tamañoTablero = 10;
        static char[,] tableroJugador = new char[tamañoTablero, tamañoTablero];
        static char[,] tableroMaquina = new char[tamañoTablero, tamañoTablero];

        static void Main(string[] args)
        {
            InicializarTableros();

            Console.WriteLine("====================================");
            Console.WriteLine("|     ¡Bienvenido a BattleShip!    |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|    ¡Prepárate para la batalla!   |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|          by Carlos Ramos         |");
            Console.WriteLine("====================================");

            Console.WriteLine("\n¿Qué modo de juego prefieres?");
            Console.WriteLine("1. Jugar contra la Máquina");
            Console.WriteLine("2. Jugar entre dos Jugadores");

            int modoJuego = PedirEnteroEntre("Selecciona una opción: ", 1, 2);

            string nombreJugador, nombreMaquina;
            nombreJugador = PedirNombre("Jugador, ingresa tu nombre: ");

            if (modoJuego == 1)
            {
                nombreMaquina = "Máquina";
                ColocarBarcos(tableroJugador, nombreJugador);
                ColocarBarcosAleatorios(tableroMaquina, nombreMaquina);
                JugarContraMaquina(tableroJugador, tableroMaquina, nombreJugador, nombreMaquina);
            }
            else
            {
                string nombreJugador2 = PedirNombre("Jugador 2, ingresa tu nombre: ");
                Console.WriteLine($"\n{nombreJugador}, coloca tus barcos:");
                ColocarBarcos(tableroJugador, nombreJugador);
                Console.WriteLine($"\n{nombreJugador2}, coloca tus barcos:");
                ColocarBarcos(tableroMaquina, nombreJugador2);
                JugarEntreDosJugadores(tableroJugador, tableroMaquina, nombreJugador, nombreJugador2);
            }
        }

        static void InicializarTableros()
        {
            for (int i = 0; i < tamañoTablero; i++)
            {
                for (int j = 0; j < tamañoTablero; j++)
                {
                    tableroJugador[i, j] = '-';
                    tableroMaquina[i, j] = '-';
                }
            }
        }

        static void ColocarBarcos(char[,] tablero, string nombreJugador)
        {
            string[] nombresBarcos = { "Barco: El Grandote", "Barco: Escobar", "Barco: Zack", "Barco: Cody", "Barco El Pequeñin" };
            int[] tamañosBarcos = { 5, 4, 3, 3, 2 };

            Console.WriteLine($"\n{nombreJugador}, coloca tus barcos en el tablero:");

            for (int i = 0; i < nombresBarcos.Length; i++)
            {
                Console.WriteLine($"\nColocando {nombresBarcos[i]} ({tamañosBarcos[i]} casillas)");

                bool colocado = false;
                while (!colocado)
                {
                    try
                    {
                        Console.WriteLine($"\nIngrese la fila y columna inicial del {nombresBarcos[i]} y su orientación (por ejemplo, A3H para horizontal o A3V para vertical):");
                        string entrada = Console.ReadLine().ToUpper();
                        int fila = entrada[0] - 'A';
                        int columna;
                        char orientacion = entrada[entrada.Length - 1];

                        if (orientacion != 'H' && orientacion != 'V')
                        {
                            Console.WriteLine("Orientación inválida. Utilice 'H' para horizontal o 'V' para vertical. Inténtalo de nuevo.");
                            continue;
                        }

                        if (int.TryParse(entrada.Substring(1, entrada.Length - 2), out columna) && fila >= 0 && fila < tamañoTablero && columna > 0 && columna <= tamañoTablero)
                        {
                            columna--;
                            if (orientacion == 'H')
                            {
                                if (columna + tamañosBarcos[i] <= tamañoTablero)
                                {
                                    bool posicionValida = true;
                                    for (int j = columna; j < columna + tamañosBarcos[i]; j++)
                                    {
                                        if (tablero[fila, j] != '-')
                                        {
                                            posicionValida = false;
                                            break;
                                        }
                                    }
                                    if (posicionValida)
                                    {
                                        for (int j = columna; j < columna + tamañosBarcos[i]; j++)
                                        {
                                            tablero[fila, j] = 'O';
                                        }
                                        colocado = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Posición inválida. Inténtalo de nuevo.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Posición inválida. El barco se sale del tablero. Inténtalo de nuevo.");
                                }
                            }
                            else if (orientacion == 'V')
                            {
                                if (fila + tamañosBarcos[i] <= tamañoTablero)
                                {
                                    bool posicionValida = true;
                                    for (int j = fila; j < fila + tamañosBarcos[i]; j++)
                                    {
                                        if (tablero[j, columna] != '-')
                                        {
                                            posicionValida = false;
                                            break;
                                        }
                                    }
                                    if (posicionValida)
                                    {
                                        for (int j = fila; j < fila + tamañosBarcos[i]; j++)
                                        {
                                            tablero[j, columna] = 'O';
                                        }
                                        colocado = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Posición inválida. Inténtalo de nuevo.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Posición inválida. El barco se sale del tablero. Inténtalo de nuevo.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Posición inválida. Inténtalo de nuevo.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Acabas de realizar una acción inválida, por favor inténtalo de nuevo :)");
                    }
                }
            }
        }

        static void ColocarBarcosAleatorios(char[,] tablero, string nombreMaquina)
        {
            Random rnd = new Random();
            string[] nombresBarcos = { "Portaviones", "Acorazado", "Destructor", "Submarino", "Patrullero" };
            int[] tamañosBarcos = { 5, 4, 3, 3, 2 };

            Console.WriteLine($"\nColocando barcos para {nombreMaquina}:");

            for (int i = 0; i < nombresBarcos.Length; i++)
            {
                bool colocado = false;
                while (!colocado)
                {
                    int fila = rnd.Next(0, tamañoTablero);
                    int columna = rnd.Next(0, tamañoTablero);
                    char orientacion = (rnd.Next(2) == 0) ? 'H' : 'V';

                    if (orientacion == 'H')
                    {
                        if (columna + tamañosBarcos[i] <= tamañoTablero)
                        {
                            bool posicionValida = true;
                            for (int j = columna; j < columna + tamañosBarcos[i]; j++)
                            {
                                if (tablero[fila, j] != '-')
                                {
                                    posicionValida = false;
                                    break;
                                }
                            }
                            if (posicionValida)
                            {
                                for (int j = columna; j < columna + tamañosBarcos[i]; j++)
                                {
                                    tablero[fila, j] = 'O';
                                }
                                colocado = true;
                            }
                        }
                    }
                    else if (orientacion == 'V')
                    {
                        if (fila + tamañosBarcos[i] <= tamañoTablero)
                        {
                            bool posicionValida = true;
                            for (int j = fila; j < fila + tamañosBarcos[i]; j++)
                            {
                                if (tablero[j, columna] != '-')
                                {
                                    posicionValida = false;
                                    break;
                                }
                            }
                            if (posicionValida)
                            {
                                for (int j = fila; j < fila + tamañosBarcos[i]; j++)
                                {
                                    tablero[j, columna] = 'O';
                                }
                                colocado = true;
                            }
                        }
                    }
                }
            }
        }

        static void JugarContraMaquina(char[,] tableroJugador, char[,] tableroMaquina, string nombreJugador, string nombreMaquina)
        {
            bool finJuego = false;
            bool turnoJugador = true;

            while (!finJuego)
            {
                if (turnoJugador)
                {
                    Console.WriteLine($"\nTurno de {nombreJugador}:");
                    TurnoJugador(tableroJugador, tableroMaquina, nombreJugador);
                }
                else
                {
                    Console.WriteLine("\nTurno de la Máquina:");
                    TurnoMaquina(tableroMaquina, tableroJugador);
                }

                MostrarTableros(tableroJugador, tableroMaquina, nombreJugador, nombreMaquina);

                turnoJugador = !turnoJugador;
                finJuego = VerificarFinJuego(tableroJugador) || VerificarFinJuego(tableroMaquina);
            }

            Console.WriteLine("\n¡Fin del juego!");
        }

        static void JugarEntreDosJugadores(char[,] tableroJugador1, char[,] tableroJugador2, string nombreJugador1, string nombreJugador2)
        {
            bool finJuego = false;
            bool turnoJugador1 = true;

            while (!finJuego)
            {
                if (turnoJugador1)
                {
                    Console.WriteLine($"\nTurno de {nombreJugador1}:");
                    TurnoJugador(tableroJugador1, tableroJugador2, nombreJugador1);
                }
                else
                {
                    Console.WriteLine($"\nTurno de {nombreJugador2}:");
                    TurnoJugador(tableroJugador2, tableroJugador1, nombreJugador2);
                }

                MostrarTableros(tableroJugador1, tableroJugador2, nombreJugador1, nombreJugador2);

                turnoJugador1 = !turnoJugador1;
                finJuego = VerificarFinJuego(tableroJugador1) || VerificarFinJuego(tableroJugador2);
            }

            Console.WriteLine("\n¡Fin del juego!");
        }

        static void TurnoJugador(char[,] tableroActual, char[,] tableroOponente, string nombreJugador)
        {
            try
            {
                Console.WriteLine($"\n{nombreJugador}, ingresa la fila y columna para disparar al tablero oponente (por ejemplo, A3):");
                string entrada = Console.ReadLine().ToUpper();
                int fila = entrada[0] - 'A';
                int columna;
                if (int.TryParse(entrada.Substring(1), out columna) && fila >= 0 && fila < tamañoTablero && columna > 0 && columna <= tamañoTablero)
                {
                    columna--;
                    if (tableroOponente[fila, columna] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("¡Has golpeado un barco!");
                        Console.ResetColor();
                        tableroOponente[fila, columna] = 'X';
                        ComprobarBarcoDestruido(tableroOponente, fila, columna);
                    }
                    else if (tableroOponente[fila, columna] == '-' || tableroOponente[fila, columna] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Has disparado al agua.");
                        Console.ResetColor();
                        tableroOponente[fila, columna] = '*';
                    }
                    else
                    {
                        Console.WriteLine("Ya has disparado a esta posición. Inténtalo de nuevo.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Disparo fuera de los límites del tablero. ¡El jugador pierde su turno!");
                    return;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Acabas de realizar una acción inválida, por favor inténtalo de nuevo");

                return;
            }
        }

        static void TurnoMaquina(char[,] tableroMaquina, char[,] tableroJugador)
        {
            Random rnd = new Random();
            int fila, columna;
            do
            {
                fila = rnd.Next(0, tamañoTablero);
                columna = rnd.Next(0, tamañoTablero);
            } while (tableroJugador[fila, columna] == '*' || tableroJugador[fila, columna] == 'X');

            if (tableroJugador[fila, columna] == 'O')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"La Máquina ha golpeado tu barco en la posición {Convert.ToChar(fila + 'A')}{columna + 1}.");
                Console.ResetColor();
                tableroJugador[fila, columna] = 'X';
                ComprobarBarcoDestruido(tableroJugador, fila, columna);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"La Máquina ha disparado al agua en la posición {Convert.ToChar(fila + 'A')}{columna + 1}.");
                Console.ResetColor();
                tableroJugador[fila, columna] = '*';
            }
        }

        static void ComprobarBarcoDestruido(char[,] tablero, int fila, int columna)
        {
            bool horizontalDestruido = true;
            for (int i = 0; i < tamañoTablero; i++)
            {
                if (tablero[fila, i] == 'O')
                {
                    horizontalDestruido = false;
                    break;
                }
            }

            bool verticalDestruido = true;
            for (int i = 0; i < tamañoTablero; i++)
            {
                if (tablero[i, columna] == 'O')
                {
                    verticalDestruido = false;
                    break;
                }
            }

            if (horizontalDestruido || verticalDestruido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("¡Barco destruido!");
                Console.ResetColor();
            }
        }

        static bool VerificarFinJuego(char[,] tablero)
        {
            for (int i = 0; i < tamañoTablero; i++)
            {
                for (int j = 0; j < tamañoTablero; j++)
                {
                    if (tablero[i, j] == 'O')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void MostrarTableros(char[,] tableroJugador1, char[,] tableroJugador2, string nombreJugador1, string nombreJugador2)
        {
            Console.WriteLine($"\nTablero de {nombreJugador1}");
            Console.Write("   ");
            for (int i = 0; i < tamañoTablero; i++)
            {
                Console.Write($"{i + 1,2} ");
            }
            Console.WriteLine();

            for (int i = 0; i < tamañoTablero; i++)
            {
                Console.Write($"{Convert.ToChar(i + 'A'),-2} ");
                for (int j = 0; j < tamañoTablero; j++)
                {
                    if (tableroJugador1[i, j] == 'O')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (tableroJugador1[i, j] == '-')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (tableroJugador1[i, j] == '*' || tableroJugador1[i, j] == 'X')
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write($"{tableroJugador1[i, j],2} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nTablero de {nombreJugador2}");
            Console.Write("   ");
            for (int i = 0; i < tamañoTablero; i++)
            {
                Console.Write($"{i + 1,2} ");
            }
            Console.WriteLine();

            for (int i = 0; i < tamañoTablero; i++)
            {
                Console.Write($"{Convert.ToChar(i + 'A'),-2} ");
                for (int j = 0; j < tamañoTablero; j++)
                {
                    if (tableroJugador2[i, j] == 'O')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (tableroJugador2[i, j] == '-')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (tableroJugador2[i, j] == '*' || tableroJugador2[i, j] == 'X')
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write($"{tableroJugador2[i, j],2} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static int PedirEnteroEntre(string mensaje, int min, int max)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                {
                    return valor;
                }
                Console.WriteLine($"Por favor, ingresa un número entre {min} y {max}.");
            }
        }

        static string PedirNombre(string mensaje)
        {
            string nombre;
            do
            {
                Console.Write(mensaje);
                nombre = Console.ReadLine().Trim();
            } while (string.IsNullOrEmpty(nombre));
            return nombre;
        }
    }
}
