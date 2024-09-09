using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRaya
{
    internal class Program
    {

        static int[,] tablero;
        static char[] ficha = { ' ', 'O', 'X' };
        static string[] jugadores = new string[2];
        static int turno;
        static string ganador = "";
        static int dimensiones;

        static void Main(string[] args)
        {
            Jugar();
        }


        static void Jugar()
        {
            do
            {
                IniciarPartida();

                //Si el ganador tiene contenido o hay empate el juego se acaba.
                while (ganador == "" && !ComprobarEspacios())
                {
                    CambiarTurno();
                    ComprovarPosicionYColocarFicha();
                    HayGandor();
                    MostrarTablero();
                }

                //Comprovamos si hay ganador para mencionarlo y si no decimos que hay empate.
                if (ganador != "")
                    Console.WriteLine("El ganador es: " + ganador);
                else
                    Console.WriteLine("Hay empate.");

                Console.WriteLine(@"
Quereis volver a jugar?
    1. Si
    2. No");
                if (Console.ReadLine() == "2")
                    break;

                //Ponemos los valores iniciales
                Array.Clear(tablero, 0, tablero.Length);
                ganador = "";

            }
            while (true);

        }

        static void MostrarTablero()
        {
            //Inicio del tablero
            Console.WriteLine();

            //Linea separadora de filas del tablero
            string lineaSeparadora = "";
            for (int i = 0; i < dimensiones; i++)
            {
                lineaSeparadora += "----";
                if (i == dimensiones - 1)
                    lineaSeparadora += "-";
            }

            //recorremos por fila y columnas
            for (int fila = 0; fila < dimensiones; fila++)
            {
                if (fila == 0)
                    Console.WriteLine(lineaSeparadora);
                Console.Write("|");
                for (int columna = 0; columna < dimensiones; columna++)
                    //Cuando se encuentre con el contenido vacio hacemos que ponga el contenido de 'ficha[0]' que es un espacio
                    Console.Write(" {0} |", ficha[tablero[fila, columna]]);
                Console.WriteLine();
                Console.WriteLine(lineaSeparadora);

            }
        }

        static void CambiarTurno()
        {
            //Hacemos la comprovacion de que jugador tiene el turno para pasarla al otro
            turno = turno == 0 ? 1 : 0;

            //Indicamos que jugador continua
            Console.WriteLine("Turno de " + jugadores[turno]);
        }

        static void IniciarPartida()
        {
            //Mostramos el estado del tablero y guardamos los nombres de los jugadores
            if (jugadores[0] == null)
                GuardarParametros();

            //Comunicamos la asignacion de las fichas
            Console.WriteLine("Asignacion de fichas:\n\t" + jugadores[0] + ": 'O' \n\t" + jugadores[1] + ": 'X'");

            //Creamos una variable 'Random' para empezar la partida de manera aleatoria e indicamos quien empieza
            Random rnd = new Random();
            turno = rnd.Next(2);
            Console.WriteLine("Empieza " + jugadores[turno]);

            //Vamos a iniciar la colocacion de fichas
            ComprovarPosicionYColocarFicha();
        }

        static void GuardarParametros()
        {
            for (int i = 0; i < jugadores.Length; i++)
            {
                Console.WriteLine("Nombre del jugador " + (i + 1) + ": ");
                jugadores[i] = Console.ReadLine();
            }

            Console.WriteLine("De que dimensiones quieres la partida? 'ejemplo: 3enRaya = 3'");
            dimensiones = Int32.Parse(Console.ReadLine());
            tablero = new int[dimensiones, dimensiones];
        }

        static void ComprovarPosicionYColocarFicha()
        {
            do
            {
                //Mostramos el tablero
                MostrarTablero();

                //Pedimos el numero de fila y le restamos uno para que corresponda con el array
                Console.WriteLine("En que fila quieres colocar tu ficha?");
                int fila = Int32.Parse(Console.ReadLine()) - 1;

                //Pedimos el numero de fila y le restamos uno para que corresponda con el array
                Console.WriteLine("En que colummna quieres colocar tu ficha?");
                int columna = Int32.Parse(Console.ReadLine()) - 1;

                //Comprovamos que este lugar en el array este vacio
                if (tablero[fila, columna] == 0)
                {
                    tablero[fila, columna] = turno + 1;
                    break;
                }

                //Si no esta vacio te sale un mensaje de informacion y vuelve a preguntar
                else
                    Console.WriteLine("Posicion ocupada vuelve a probar.");
            }
            while (true);
        }

        /// <summary>
        /// Método para comprobar si hay ganador
        /// </summary>
        static void HayGandor()
        {
            int fichasDiagonalJ1;
            int fichasDiagonalJ2;

            //si en alguna fila són todas las casillas iguales
            for (int fila = 0; fila < dimensiones; fila++)
            {
                fichasDiagonalJ1 = 0;
                fichasDiagonalJ2 = 0;
                for (int columna = 0; columna < dimensiones; columna++)
                {
                    if (ficha[tablero[fila, columna]] == 'O')
                        fichasDiagonalJ1++;
                    else if (ficha[tablero[fila, columna]] == 'X')
                        fichasDiagonalJ2++;
                }
                if (fichasDiagonalJ1 == dimensiones)
                {
                    ganador = jugadores[0];
                    return;
                }
                else if (fichasDiagonalJ2 == dimensiones)
                {
                    ganador = jugadores[1];
                    return;
                }
                else
                    ganador = "";
            }


            //si en alguna columna són todas las casillas iguales
            for (int columna = 0; columna < dimensiones; columna++)
            {
                fichasDiagonalJ1 = 0;
                fichasDiagonalJ2 = 0;
                for (int fila = 0; fila < dimensiones; fila++)
                {
                    if (ficha[tablero[fila, columna]] == 'O')
                        fichasDiagonalJ1++;
                    else if (ficha[tablero[fila, columna]] == 'X')
                        fichasDiagonalJ2++;
                }
                if (fichasDiagonalJ1 == dimensiones)
                {
                    ganador = jugadores[0];
                    return;
                }
                else if (fichasDiagonalJ2 == dimensiones)
                {
                    ganador = jugadores[1];
                    return;
                }
                else
                    ganador = "";
            }

            fichasDiagonalJ1 = 0;
            fichasDiagonalJ2 = 0;
            //Si la diagonal es de 'O' devulve true y si es 'X' devulve false
            for (int i = 0; i < dimensiones; i++)
            {
                if (ficha[tablero[i, i]] == 'O')
                    fichasDiagonalJ1++;
                else if (ficha[tablero[i, i]] == 'X')
                    fichasDiagonalJ2++;
            }

            if (fichasDiagonalJ1 == dimensiones)
            {
                ganador = jugadores[0];
                return;
            }
            else if (fichasDiagonalJ2 == dimensiones)
            {
                ganador = jugadores[1];
                return;
            }
            else
            {
                fichasDiagonalJ1 = 0;
                fichasDiagonalJ2 = 0;
                int j = dimensiones - 1;
                for (int i = 0; i < dimensiones; i++)
                {
                    if (ficha[tablero[j, i]] == 'O')
                        fichasDiagonalJ1++;
                    else if (ficha[tablero[j, i]] == 'X')
                        fichasDiagonalJ2++;
                    j--;
                }
            }

            if (fichasDiagonalJ1 == dimensiones)
            {
                ganador = jugadores[0];
                return;
            }
            else if (fichasDiagonalJ2 == dimensiones)
            {
                ganador = jugadores[1];
                return;
            }
            else
                ganador = "";
        }

        static bool ComprobarEspacios()
        {
            for (int fila = 0; fila < dimensiones; fila++) // Recorremos las filas
                for (int columna = 0; columna < dimensiones; columna++) // Recorremos las columnas
                    if (tablero[fila, columna] == 0)
                        return false;
            return true;
        }
    }
}