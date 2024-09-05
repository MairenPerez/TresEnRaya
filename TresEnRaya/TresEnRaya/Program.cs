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

        static int[,] tablero = new int[3, 3];
        static char[] ficha = { ' ', 'O', 'X' };
        static string[] jugadores = new string[2];
        static int turno;
        static string ganador = "";
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
                while (ganador == "" || ComprobarEmpate())
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
                {
                    break;
                }
            } while (true);

        }

        static void MostrarTablero()
        {
            //Inicio del tablero
            Console.WriteLine();
            Console.WriteLine("-------------");

            //recorremos por fila y columnas
            for (int fila = 0; fila < 3; fila++)
            {
                Console.Write("|");
                for (int columna = 0; columna < 3; columna++)
                    //Cuando se encuentre con el contenido vacio hacemos que ponga el contenido de 'ficha[0]' que es un espacio
                    Console.Write(" {0} |", ficha[tablero[fila, columna]]);
                Console.WriteLine();
                Console.WriteLine("-------------");
            }
        }

        static void CambiarTurno()
        {
            //Hacemos la comprovacion de que jugador tiene el turno para pasarla al otro
            if (turno == 0)
                turno = 1;
            else
                turno = 0;

            //Indicamos que jugador continua
            Console.WriteLine("Turno de " + jugadores[turno]);
        }

        static void IniciarPartida()
        {
            //Mostramos el estado del tablero y guardamos los nombres de los jugadores
            MostrarTablero();
            GuardarJugadores();

            //Comunicamos la asignacion de las fichas
            Console.WriteLine("Asignacion de fichas:\n\t" + jugadores[0] + ": 'O' \n\t" + jugadores[1] + ": 'X'");

            //Creamos una variable 'Random' para empezar la partida de manera aleatoria e indicamos quien empieza
            Random rnd = new Random();
            turno = rnd.Next(2);
            Console.WriteLine("Empieza " + jugadores[turno]);

            //Vamos a iniciar la colocacion de fichas
            ComprovarPosicionYColocarFicha();
        }

        static void GuardarJugadores()
        {
            for (int i = 0; i < jugadores.Length; i++)
            {
                Console.WriteLine("Nombre del jugador " + (i + 1) + ": ");
                jugadores[i] = Console.ReadLine();
            }
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
            } while (true);
        }

        /// <summary>
        /// Método para comprobar si hay ganador
        /// </summary>
        static void HayGandor()
        {
            //si en alguna fila són todas las casillas iguales
            for (int fila = 0; fila < 3; fila++)
            {
                if (tablero[fila, 0] == tablero[fila, 1] &&
                    tablero[fila, 0] == tablero[fila, 2] &&
                    tablero[fila, 0] != 0)
                {
                    if (tablero[fila, 0] == 'O')
                        ganador = jugadores[0];
                    else
                        ganador = jugadores[1];
                }
            }

            //si en alguna columna són todas las casillas iguales
            for (int columna = 0; columna < 3; columna++)
            {
                if (tablero[0, columna] == tablero[1, columna] &&
                    tablero[0, columna] == tablero[2, columna] &&
                    tablero[0, columna] != 0)
                    if (tablero[0, columna] == 'O')
                        ganador = jugadores[0];
                    else
                        ganador = jugadores[1];
            }

            //si en alguna diagonal las casillas són iguales
            if (tablero[0, 0] == tablero[1, 1] &&
                tablero[0, 0] == tablero[2, 2] &&
                tablero[0, 0] != 0)
                if (tablero[0, 0] == ficha['O'])
                    ganador = jugadores[0];
                else
                    ganador = jugadores[1];
            if (tablero[0, 2] == tablero[1, 1] &&
                tablero[0, 2] == tablero[2, 0] &&
                tablero[0, 2] != 0)
                if (tablero[0, 2] == 'O')
                    ganador = jugadores[0];
                else
                    ganador = jugadores[1];
        }

        static bool ComprobarEmpate()
        {
            for (int fila = 0; fila < 3; fila++) // Recorremos las filas
                for (int columna = 0; columna < 3; columna++) // Recorremos las columnas
                    if (tablero[fila, columna] == 0) // Si hay una casilla vacía significa que hay hueco
                        return false;

            return true; // Significa que no hay huecos, por lo tanto hay empate
        }
    }
}