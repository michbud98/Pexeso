using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    /// <summary>
    /// Delegate that transfers data from Difficulty form to PexesoUI form
    /// </summary>
    /// <param name="data">Data to be transfered</param>
    public delegate void DataTransfer(string data);

    public partial class PexesoForm : Form
    {
        private PexesoBoard board;
        public DataTransfer transferDelegate;
        private string gameDifficulty;
        /// <summary>
        /// Form that represents PexesoBoard on UI
        /// </summary>
        public PexesoForm()
        {
            InitializeComponent();
            transferDelegate += new DataTransfer(SetDifficulty);
        }
        /// <summary>
        /// Creates a PexesoBoard representation on UI
        /// </summary>
        /// <param name="rowCount">Number of rows</param>
        /// <param name="columnCount">Number of columns</param>
        /// <param name="board">PexesoBoard object to which we add PictureBoxes representing Pexeso cards</param>
        /// <exception cref="ArgumentException">RowCount or columnCount number is not the same as in PexesoBoard object</exception>
        private void GeneratePexesoTable(int rowCount, int columnCount, PexesoBoard board)
        {
            if (rowCount != board.getPexesoBoardRows())
            {
                throw new ArgumentException("UI board rowCount and PexesoBoard rowCount is not the same.","rowCount");
            }
            if (columnCount != board.getPexesoBoardColumns())
            {
                throw new ArgumentException("UI board columnCount and PexesoBoard coulmnCount is not the same.", "columnCount");
            }
            //Clear out the existing controls, we are generating a new table layout
            pexesoLayoutPanel.Controls.Clear();

            //Clear out the existing row and column styles
            pexesoLayoutPanel.ColumnStyles.Clear();
            pexesoLayoutPanel.RowStyles.Clear();

            //Now we will generate the table, setting up the row and column counts first
            pexesoLayoutPanel.ColumnCount = columnCount;
            pexesoLayoutPanel.RowCount = rowCount;

            for (int column = 0; column < columnCount; column++)
            {
                //First add a column
                pexesoLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                for (int row = 0; row < rowCount; row++)
                {
                    //Next, add a row.  Only do this once, when creating the first column
                    if (column == 0)
                    {
                        pexesoLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                    }

                    int width = pexesoLayoutPanel.GetColumnWidths()[column];
                    int height = pexesoLayoutPanel.GetRowHeights()[row];
                    //Create the control, in this case we will add a button
                    PictureBox picture = new PictureBox
                    {
                        Name = $"pictureBox[{row}][{column}]",
                        Image = ResourcesLibrary.Resource1.Smile,
                        Size = new Size(width, height),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Anchor = AnchorStyles.None
                    };
                    picture.MouseClick += Picture_Click;
                    pexesoLayoutPanel.Controls.Add(picture, column, row);
                    board.AddToPexesoBoard(row, column, picture);
                    
                }
            }
        }
        /// <summary>
        /// Finds row coordinate of pictureBox
        /// </summary>
        /// <param name="pictureBoxName">PictureBox name on which we clicked</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>PictureBox row coordinate</returns>
        private int GetPictureBoxRow(string pictureBoxName)
        {
            Regex regex = new Regex(@"(pictureBox)\[(\d+)\]\[(\d+)\]");
            Match match = regex.Match(pictureBoxName);
            if (match.Success && Int32.TryParse(match.Groups[2].Value, out int result))
            {
                return result;
            }else if (!match.Success)
            {
                throw new ArgumentException($"{pictureBoxName} doesnt match regex.", "pictureBoxName");
            }else
            {
                throw new ArgumentException($"Cant extract row position from {pictureBoxName}", "pictureBoxName");
            }
        }
        /// <summary>
        /// Finds column coordinate of pictureBox
        /// </summary>
        /// <param name="pictureBoxName">PictureBox name on which we clicked</param>
        /// <exception cref="ArgumentException">RowCount or columnCount number is not the same as in PexesoBoard object</exception>
        /// <returns>PictureBox column coordinate</returns>
        private int GetPictureBoxColumn(string pictureBoxName)
        {
            Regex regex = new Regex(@"(pictureBox)\[(\d+)\]\[(\d+)\]");
            Match match = regex.Match(pictureBoxName);
            if (match.Success && Int32.TryParse(match.Groups[3].Value, out int result))
            {
                return result;
            }
            else if (!match.Success)
            {
                throw new ArgumentException($"{pictureBoxName} doesnt match regex.", "pictureBoxName");
            }
            else
            {
                throw new ArgumentException($"Cant extract column position from {pictureBoxName}", "pictureBoxName");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">A Picture box object which we clicked</param>
        /// <param name="e">A click event</param>
        private void Picture_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            Console.WriteLine("Clicked PictureBox name: " + clickedPictureBox.Name);
            Console.WriteLine($"Row parameter: {GetPictureBoxRow(clickedPictureBox.Name)}" +
                $"\r\nColumn parameter {GetPictureBoxColumn(clickedPictureBox.Name)}");
        }

        /// <summary>
        /// Sets difficulty
        /// </summary>
        /// <param name="difficultyRecieved">Difficulty recieved from DifficultyForm</param>
        public void SetDifficulty(string difficultyRecieved)
        {
            gameDifficulty = difficultyRecieved;
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //Garbage collection
            board = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            DifficultyForm diffForm = new DifficultyForm(transferDelegate);
            diffForm.ShowDialog();
            diffForm.Dispose();
            try
            {
                switch (gameDifficulty)
                {
                    case "Easy":
                        board = new PexesoBoard(4, 4);
                        GeneratePexesoTable(4, 4, board);
                        break;
                    case "Normal":
                        board = new PexesoBoard(8, 8);
                        GeneratePexesoTable(8, 8, board);
                        break;
                    default:
                        MessageBox.Show("Warning: Difficulty was not selected.");
                        break;
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

