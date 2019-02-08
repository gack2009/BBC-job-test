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

        private int w;
        private int h;
        private int[,] Grid2D;
        private int resolution;//resolution for drawing method it is calculated based on the picture box size
        private bool isStep;

        //moveRow and moveCol are used to check surounding neighbours base on index e.g. index 0 checks cell (-1, -1) which is the top left cell
        private int[] moveRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private int[] moveCol = { -1, 0, 1, -1, 1, -1, 0, 1 };

        //background worker added to enable buttons while game is running
        private BackgroundWorker _worker = null;

        public Form1()
        {
            InitializeComponent();
            //size of the grid you can play around with this but keep it as a rectangle keep it below 20
            w = 18;
            h = 18;
            Grid2D = new int[w, h];
            resolution = picGameOfLife.Height / h;
            //moveRow and moveCol are used to check surounding neighbours base on index e.g. index 0 checks cell (-1, -1) which is the top left cell
            moveRow = new int[]{ -1, -1, -1, 0, 0, 1, 1, 1 };
            moveCol = new int[]{ -1, 0, 1, -1, 1, -1, 0, 1 };
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Grid2D = new int[w, h];

            //nextGrid2D = new int[w, h];
            resolution = picGameOfLife.Height / h;
            isStep = false;

            FillGrid();

            //backgroundworker to enable the buttons
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                do
                {
                    if (_worker.CancellationPending)
                        break;

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
                }
            }
            //a test case, if you want to try it disable the current FillGrid() implementation
            //Grid2D = new int[,]{
            //    {0,0,0,0,0 },
            //    {0,0,0,0,0 },
            //    {0,1,1,1,0 },
            //    {0,0,0,0,0 },
            //    {0,0,0,0,0 } };
            Draw();
        }

        public void StartGameOfLife()
        {
            
            int[,] nextGrid2D = new int[w, h];
            for (int row = 0; row < Grid2D.GetLength(0); row++)
            {
                for (int col = 0; col < Grid2D.GetLength(1); col++)
                {
                    //skips the edges assuming you do not want a wrapping game of life 
                    if (row != 0 && col != 0 && row != Grid2D.GetLength(0) - 1 && col != Grid2D.GetLength(1) - 1)
                    {

                        int currentState = Grid2D[row, col];
                        int neighbours = CountNeighbours(row, col);

                        if (currentState == 1)
                        {
                            // scenario 3
                            /* If a cell is alive we check if it has two or three living neighbours */
                            if (neighbours >= 2 && neighbours <= 3)
                            {
                                nextGrid2D[row, col] = 1;
                            }
                            else/* if not it will die scenario 1 and 2 */
                            {
                                nextGrid2D[row, col] = 0;
                            }
                        }
                        else
                        {
                            //scenario 4
                            /* If a dead cell has three neighbours it will become alive */
                            if (neighbours == 3)
                            {
                                nextGrid2D[row, col] = 1;
                            }
                            else /* else it will stay dead */
                            {
                                //include scenario 0
                                nextGrid2D[row, col] = 0;
                            }
                        }                       
                    }
                }
            }

            Grid2D = nextGrid2D;
            Draw();
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
                    g.FillRectangle(b, columnIndex * resolution, rowIndex * resolution, resolution - 1, resolution - 1);
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
            if (Grid2D == null)
            {
                MessageBox.Show("Please start the game first");
            }
            else
            {
                if (_worker != null && _worker.IsBusy)
                {
                    _worker.CancelAsync();
                    _worker.Dispose();
                }
            }
        }

        /// <summary>
        /// this button pause the game and allows you to view frame by frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStep_Click(object sender, EventArgs e)
        {
            if (Grid2D == null)
            {
                MessageBox.Show("Please press on start first");
            }
            else
            {
                isStep = true;
                if (_worker != null && _worker.IsBusy)
                {
                    _worker.CancelAsync();
                    _worker.Dispose();
                }
                StartGameOfLife();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (_worker!=null && _worker.IsBusy)
            {
                _worker.CancelAsync();
                _worker.Dispose();
            }
            if (isStep == false)
            {
                FillGrid();
            }
            isStep = true;
            StartGameOfLife();
        }
    }
}
