using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conecta4
{
    public partial class frmTablero : Form
    {
        private const int filas = 6;
        private const int columnas = 7;
        private PictureBox[,] grid;
        private int[,] tablero;
        private Button[] botones = new Button[7];
        private int jugador;
        private int victoriasRed = 0;
        private int victoriasYellow = 0;

        public frmTablero()
        {
            grid = new PictureBox[filas, columnas];
            tablero = new int[filas, columnas];
            jugador = 1;

            InitializeComponent();
            asignarBotones();
        }

        private void asignarBotones()
        {
            botones[0]=button0;
            botones[1]=button1;
            botones[2]=button2;
            botones[3]=button3;
            botones[4]=button4;
            botones[5]=button5;
            botones[6]=button6;
        }

        private void buttonClick(object sender){
            if (btnCeder.Visible)
            {
                btnCeder.Visible = false;
            }
            
            Button button = (Button)sender;
            int col = Convert.ToInt32(button.Name.Substring(6));

            int fila = tirar(col);

            if (fila == -1)
            {
                return;
            }

            tablero[fila, col] = jugador;

            // Actualiza la interfaz gráfica (cambia el color del PictureBox correspondiente)
            colorear(fila, col);

            if (checkWin(fila, col))
            {
                MessageBox.Show("¡Jugador " + jugador + " ha ganado!");
                reiniciarPartida();
                return;
            }

            // Cambia al siguiente jugador
            jugador = (jugador == 1) ? 2 : 1;
            if (jugador==1)
            {
                ptbTurno.BackColor=Color.Red;
            }
            else
            {
                ptbTurno.BackColor=Color.Yellow;
            }

            //VERIFICAR SI EL TABLERO ESTA LLENO
            int casillasLibres = 0;
            foreach (PictureBox item in panel1.Controls)
            {
                if (item.BackColor==Color.White)
                {
                    casillasLibres++;
                }
            }
            if (casillasLibres==0)
            {
                MessageBox.Show("Empate!!!");
                reiniciarPartida();
            }

            if (rdbJugvsIA.Checked && jugador==2)
            {
                IAMove();
            }
        }

        private int tirar(int col)
        {
            for (int fil = filas - 1; fil >= 0; fil--)
            {
                if (tablero[fil, col] == 0)
                {
                    return fil;
                }
            }
            return -1; // Columna llena
        }

        private void reiniciarPartida()
        {
            // Reiniciar el juego
            tablero = new int[filas, columnas];

            // Reiniciar los colores de los PictureBox
            foreach (Control control in panel1.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.BackColor = Color.White;
                }
            }

            // Reiniciar jugador
            jugador = 1;
            ptbTurno.BackColor = Color.Red;

            if (rdbJugvsIA.Checked)
            {
                btnCeder.Visible=true;
            }
        }

        private void colorear(int row, int col)
        {
            // Actualizar el color del PictureBox correspondiente
            PictureBox pictureBox = getPictureBox(row, col);
            pictureBox.BackColor = (jugador == 1) ? Color.Red : Color.Yellow;
        }

        private PictureBox getPictureBox(int row, int col)
        {
            // Obtener el PictureBox correspondiente según su nombre
            return (PictureBox)Controls.Find("pb" + row + col, true)[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            buttonClick(sender);
        }

        private void wins()
        {
            if (jugador==1)
            {
                victoriasRed++;
                lblRed.Text = victoriasRed.ToString();
            }
            else
            {
                victoriasYellow++;
                lblYellow.Text = victoriasYellow.ToString();
            }
        }

        private bool checkWin(int fila, int col)
        {
            // Verificar combinaciones ganadoras en filas
            for (int c = 0; c <= columnas - 4; c++)
            {
                if (tablero[fila, c] == jugador &&
                    tablero[fila, c + 1] == jugador &&
                    tablero[fila, c + 2] == jugador &&
                    tablero[fila, c + 3] == jugador)
                {
                    wins();
                    return true;
                }
            }

            // Verificar combinaciones ganadoras en columnas
            for (int f = 0; f <= filas - 4; f++)
            {
                if (tablero[f, col] == jugador &&
                    tablero[f + 1, col] == jugador &&
                    tablero[f + 2, col] == jugador &&
                    tablero[f + 3, col] == jugador)
                {
                    wins();
                    return true;
                }
            }

            // Verificar combinaciones ganadoras en diagonales hacia arriba
            for (int f = 3; f < filas; f++)
            {
                for (int c = 0; c <= columnas - 4; c++)
                {
                    if (tablero[f, c] == jugador &&
                        tablero[f - 1, c + 1] == jugador &&
                        tablero[f - 2, c + 2] == jugador &&
                        tablero[f - 3, c + 3] == jugador)
                    {
                        wins();
                        return true;
                    }
                }
            }

            // Verificar combinaciones ganadoras en diagonales hacia abajo
            for (int f = 0; f <= filas - 4; f++)
            {
                for (int c = 0; c <= columnas - 4; c++)
                {
                    if (tablero[f, c] == jugador &&
                        tablero[f + 1, c + 1] == jugador &&
                        tablero[f + 2, c + 2] == jugador &&
                        tablero[f + 3, c + 3] == jugador)
                    {
                        wins();
                        return true;
                    }
                }
            }

            return false;
        }

        private void rdbJugvsJug_CheckedChanged(object sender, EventArgs e)
        {
            reiniciarPartida();
            reinciarContadores();    
            btnCeder.Visible= false;
        }

        private void rdbJugvsIA_CheckedChanged(object sender, EventArgs e)
        {
            reiniciarPartida();
            reinciarContadores();
            btnCeder.Visible= true;
        }

        private void reinciarContadores()
        {
            victoriasRed=0;
            victoriasYellow=0;
            lblRed.Text=victoriasRed.ToString();
            lblYellow.Text=victoriasYellow.ToString();
        }

        private void btnCeder_Click(object sender, EventArgs e)
        {
            jugador=2;
            btnCeder.Visible= false;
            IAMove();
        }

        private void IAMove()
        {
            int movimiento = mejorMovimiento(tablero, 7); //Profundidad máxima de búsqueda

            //Encuentra el botón correspondiente a la columna seleccionada por la IA
            for (int col = 0; col < columnas; col++)
            {
                if (!columnaLlena(col))
                {
                    Button button = botones[col];
                    if (col == movimiento)
                    {
                        buttonClick(button); //Realiza el mejor movimiento encontrado por la IA
                        return;
                    }
                }
            }
        }


        private int mejorMovimiento(int[,] tablero, int profundidad)
        {
            int mejorMov = -1;
            int mejorPuntaje = int.MinValue;
            int alpha = int.MinValue;
            int beta = int.MaxValue;

            for (int col = 0; col < columnas; col++)
            {
                if (!columnaLlena(col))
                {
                    int fila = obtenerFila(tablero, col, 2); // Coloca la ficha de la IA en esta columna
                    int puntaje = Minimax(tablero, profundidad - 1, false, alpha, beta);

                    // Deshace el movimiento realizado para evaluar el siguiente
                    tablero[fila, col] = 0;

                    if (puntaje > mejorPuntaje)
                    {
                        mejorPuntaje = puntaje;
                        mejorMov = col;
                    }

                    alpha = Math.Max(alpha, mejorPuntaje);

                    if (beta <= alpha)
                        break;
                }
            }
            return mejorMov;
        }

        private int Minimax(int[,] tablero, int profundidad, bool isMaximizing, int alpha, int beta)
        {
            int jugador = isMaximizing ? 1 : 2;

            // Evaluar el tablero actual
            int tableroScore = evaluarTablero(tablero);
            if (tableroScore != 0)
            {
                return tableroScore;
            }

            int mejorPuntaje = isMaximizing ? int.MinValue : int.MaxValue;

            for (int col = 0; col < columnas; col++)
            {
                if (!columnaLlena(col))
                {
                    int fila = obtenerFila(tablero, col, jugador);
                    int puntaje = Minimax(tablero, profundidad - 1, !isMaximizing, alpha, beta);
                    tablero[fila, col] = 0; // Deshacer el movimiento

                    if (isMaximizing)
                    {
                        mejorPuntaje = Math.Max(mejorPuntaje, puntaje);
                        alpha = Math.Max(alpha, mejorPuntaje);
                    }
                    else
                    {
                        mejorPuntaje = Math.Min(mejorPuntaje, puntaje);
                        beta = Math.Min(beta, mejorPuntaje);
                    }

                    if (beta <= alpha)
                    {
                        break; //Poda alfa-beta
                    }
                }
            }

            return mejorPuntaje;
        }

        private bool columnaLlena(int col)
        {
            return tablero[0, col] != 0;
        }

        private int obtenerFila(int[,] tablero, int col, int jugador)
        {
            for (int fila = filas - 1; fila >= 0; fila--)
            {
                if (tablero[fila, col] == 0)
                {
                    tablero[fila, col] = jugador;
                    return fila;
                }
            }
            return -1; //Columna llena
        }

        private int evaluarTablero(int[,] tablero)
        {
            int puntos = 0;

            // Evaluar filas y columnas
            for (int fila = 0; fila < filas; fila++)
            {
                for (int col = 0; col < columnas; col++)
                {
                    if (col <= columnas - 4)
                    {
                        // Evaluar horizontalmente
                        puntos += evaluarJugada(tablero[fila, col], tablero[fila, col + 1], tablero[fila, col + 2], tablero[fila, col + 3]);
                    }
                    if (fila <= filas - 4)
                    {
                        // Evaluar verticalmente
                        puntos += evaluarJugada(tablero[fila, col], tablero[fila + 1, col], tablero[fila + 2, col], tablero[fila + 3, col]);
                    }
                }
            }

            // Evaluar diagonales
            for (int fila = 0; fila <= filas - 4; fila++)
            {
                for (int col = 0; col <= columnas - 4; col++)
                {
                    // Diagonal principal
                    puntos += evaluarJugada(tablero[fila, col], tablero[fila + 1, col + 1], tablero[fila + 2, col + 2], tablero[fila + 3, col + 3]);

                    // Diagonal secundaria
                    puntos += evaluarJugada(tablero[fila + 3, col], tablero[fila + 2, col + 1], tablero[fila + 1, col + 2], tablero[fila, col + 3]);
                }
            }

            return puntos;
        }

        private int evaluarJugada(int a, int b, int c, int d)
        {
            int[] lineas = { a, b, c, d };
            int jugador1 = lineas.Count(x => x == 2); // Contar fichas del jugador 1 (IA)
            int jugador2 = lineas.Count(x => x == 1); // Contar fichas del jugador 2 (oponente)

            if (jugador1 == 4)
            {
                return 4; // Cuatro fichas del jugador 1 en línea, jugador 1 gana
            }
            else if (jugador2 == 4)
            {
                return -4; // Cuatro fichas del jugador 2 en línea, jugador 2 gana
            }
            else if (jugador1 == 3 && jugador2 == 0)
            {
                return 3; // Tres fichas del jugador 1 en línea, ventaja para el jugador 1
            }
            else if (jugador1 == 2 && jugador2 == 0)
            {
                return 2; // Dos fichas del jugador 1 en línea, ventaja para el jugador 1
            }
            else if (jugador1 == 1 && jugador2 == 0)
            {
                return 1; // Una ficha del jugador 1 en línea, ventaja para el jugador 1
            }
            else if (jugador2 == 3 && jugador1 == 0)
            {
                return -3; // Tres fichas del jugador 2 en línea, IA bloquea al oponente
            }
            else if (jugador2 == 2 && jugador1 == 0)
            {
                return -2; // Dos fichas del jugador 2 en línea, IA bloquea al oponente
            }
            else if (jugador2 == 1 && jugador1 == 0)
            {
                return -1; // Una ficha del jugador 2 en línea, IA bloquea al oponente
            }
            else
            {
                return 0; // Sin ventaja clara
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            reinciarContadores();
            reiniciarPartida();
        }
    }
}
