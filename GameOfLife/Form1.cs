using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int w = 18;
        private int h = 18;
        private int[,] Grid2D;
        private int[,] nextGrid2D;
        private int resolution;//resolution for drawing method
        private bool isStep;

        //moveRow and moveCol are used to check surounding neighbours base on index e.g. index 0 checks cell (-1, -1) which is the top left cell
        private int[] moveRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private int[] moveCol = { -1, 0, 1, -1, 1, -1, 0, 1 };

        //background worker added to enable buttons while game is running
        private BackgroundWorker _worker = null;
        bool run;


        private void btnStart_Click(object sender, EventArgs e)
        {
            run = true;
            Grid2D = new int[w, h];

            nextGrid2D = new int[w, h];
            resolution = picGameOfLife.Height / h;
            isStep = false;
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                do
                {
                    if (_worker.CancellationPending)
                        break;
                    if (!isStep)
                    {
                        FillGrid();
                    }
                    StartGameOfLife();

                } while (true);
            });

            _worker.RunWorkerAsync();

        }
        

        public void FillGrid()
        {

            int rowCount = Grid2D.GetLength(0);
            int colCount = Grid2D.GetLength(1);

            //Action action = () => picGameOfLife.Size = new Size(550,300);
            //picGameOfLife.Invoke(action);
            //fill the cells with random value 0-1 live or dead
            Random randNum = new Random();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    Grid2D[i, j] = randNum.Next(0, 2);
                    nextGrid2D[i, j] = 0;
                }
            }
        }

        public void StartGameOfLife()
        {
            //infite loop to keep the game running
            while (run)
            {
                if (isStep)
                {
                    run = false;
                }
                for (int row = 0; row < Grid2D.GetLength(0); row++)
                {
                    for (int col = 0; col < Grid2D.GetLength(1); col++)
                    {
                        //skips the edges
                        if (row != 0 && col != 0 && row != Grid2D.GetLength(0) - 1 && col != Grid2D.GetLength(1) - 1)
                        {
                            int currentState = Grid2D[row, col];
                            int neighbours = CountNeighbours(row, col);

                            //Scenario 1
                            if (neighbours < 2 && currentState == 1)
                            {
                                nextGrid2D[row, col] = 0;
                            }
                            //Scenario 2
                            else if (neighbours > 3 && currentState ==1 )
                            {
                                nextGrid2D[row, col] = 0;
                            }
                            //Scenario 3
                            else if ((neighbours == 2 || neighbours == 3) && currentState == 1)
                            {
                                nextGrid2D[row, col] = 1;
                            }
                            //Scenario 4
                            else if (neighbours == 3 && currentState == 0)
                            {
                                nextGrid2D[row, col] = 1;
                            }
                            else
                            {
                                //this include Scenario 0
                                nextGrid2D[row, col] = Grid2D[row, col];
                            }
                        }
                    }
                }

                Draw();

                Grid2D = nextGrid2D;
            }
        }

        /// <summary>
        /// counts how many neighbours for a cell
        /// </summary>
        /// <param name="Grid2D"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int CountNeighbours(int row, int col)
        {
            int sum = 0;
            int Row;
            int Col;

            for (int i = 0; i < 8; i++)
            {
                Row = row + moveRow[i];
                Col = col + moveCol[i];
                sum += Grid2D[Row, Col];
            }

            return sum;
        }


        /// <summary>
        /// draws the result into picture box using graphics and brush classes
        /// </summary>
        private void Draw()
        {
            int rows = Grid2D.GetLength(0);
            int cols = Grid2D.GetLength(1);
            Graphics g = picGameOfLife.CreateGraphics();
            Brush alive = Brushes.White;
            Brush dead = Brushes.Black;

            for (int rowIndex = 0; rowIndex < rows; ++rowIndex)
            {

                for (int columnIndex = 0; columnIndex < cols; ++columnIndex)
                {
                    Brush b;
                    if (Grid2D[rowIndex, columnIndex] == 1)
                    {
                        b = alive;
                    }
                    else
                    {
                        b = dead;
                    }
                    g.FillRectangle(b, rowIndex * resolution, columnIndex * resolution, resolution-1, resolution-1);
                }
            }
        }
        
        /// <summary>
        /// stops the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if(nextGrid2D == null)
            {
                MessageBox.Show("Please start the game first");
            }
            else
            {
                run = false;
                _worker.CancelAsync();
            }            
        }
        /// <summary>
        /// this button pause the game and allows you to view frame by frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStep_Click(object sender, EventArgs e)
        {
            if(nextGrid2D == null)
            {
                MessageBox.Show("Please press on start first");
            }
            else
            {
                isStep = true;
                run = true;
                _worker.CancelAsync();
                StartGameOfLife();
            }            
        }
    }
}
