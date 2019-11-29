using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
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
        /// <summary>
        /// Stores PexesoCards object in array
        /// </summary>
        private PexesoBoard _board;
        public DataTransfer transferDelegate;
        /// <summary>
        /// Stores game difficulty
        /// </summary>
        private string _gameDifficulty;
        /// <summary>
        /// Dictionary that stores 2 selected cards
        /// Key - string with value equal to 0 (First card selected) or 1 (Second card selected)
        /// Value - Array that stores information about PictureBox selected (0) and PexesoCard selected (1)
        /// </summary>
        private Dictionary<int, Object[]> _selectedCards = new Dictionary<int, Object[]>(2);
        
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
                    board.AddToPexesoBoard(row, column, ResourcesLibrary.Resource1._1);
                    
                }
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
            PexesoCard selectedPexesoCard = _board.GetPexesoCard(GetPictureBoxRow(clickedPictureBox.Name), GetPictureBoxColumn(clickedPictureBox.Name));
            SaveSelectedCard(clickedPictureBox, selectedPexesoCard);
            
        }
        /// <summary>
        /// Saves Clicked PictureBox and selected PexesoCard into _selectedCards Dictionary
        /// </summary>
        /// <param name="clickedPictureBox">PictureBox that player clicked on</param>
        /// <param name="selectedPexesoCard">PexesoCard that was selected through clicked PictureBox</param>
        private void SaveSelectedCard(PictureBox clickedPictureBox, PexesoCard selectedPexesoCard)
        {
            if (_selectedCards[0] == null)
            {
                SaveSelectedCardInternal(clickedPictureBox, selectedPexesoCard, 0);
            }
            else if(_selectedCards[0] != null && _selectedCards[1] == null)
            {
                SaveSelectedCardInternal(clickedPictureBox, selectedPexesoCard, 1);
            }
            else if (_selectedCards[0] != null && _selectedCards[1] != null)
            {
                _selectedCards[0] = null;
                _selectedCards[1] = null;
                SaveSelectedCardInternal(clickedPictureBox, selectedPexesoCard, 0);
            }
            PrintSelectedCard();
        }
        /// <summary>
        /// Internal method of SaveSelectedCard
        /// </summary>
        /// <param name="clickedPictureBox">PictureBox that player clicked on</param>
        /// <param name="selectedPexesoCard">PexesoCard that was selected through clicked PictureBox</param>
        private void SaveSelectedCardInternal(PictureBox clickedPictureBox, PexesoCard selectedPexesoCard, int orderInt)
        {
            if(orderInt > 1 && orderInt < 0)
            {
                throw new ArgumentException("Order int for selected card is 0 or 1.", "orderInt");
            }
            else
            {
                Object[] PexesoCardArray = new Object[2];
                PexesoCardArray[0] = clickedPictureBox;
                PexesoCardArray[1] = selectedPexesoCard;
                _selectedCards[orderInt] = PexesoCardArray;
            }
        }
        /// <summary>
        /// Prints out currently selected PexesoCards
        /// </summary>
        private void PrintSelectedCard()
        {
            PictureBox checkPictureBox = null;
            if (_selectedCards[0] != null && _selectedCards[1] == null)
            {
                Console.WriteLine("-----First selected card-----");
                checkPictureBox = _selectedCards[0][0] as PictureBox;
                Console.WriteLine($"Clicked PictureBox name:{checkPictureBox.Name}");
                Console.WriteLine($"[{_selectedCards[0][1].ToString()}]");
                Console.WriteLine("-----Second selected card-----");
                Console.WriteLine("-----Not yet selected-----");
                Console.WriteLine();
            }
            else if (_selectedCards[0] != null && _selectedCards[1] != null)
            {
                Console.WriteLine("-----First selected card-----");
                checkPictureBox = _selectedCards[0][0] as PictureBox;
                Console.WriteLine($"Clicked PictureBox name:{checkPictureBox.Name}");
                Console.WriteLine($"[{_selectedCards[0][1].ToString()}]");
                Console.WriteLine("-----Second selected card-----");
                checkPictureBox = _selectedCards[1][0] as PictureBox;
                Console.WriteLine($"Clicked PictureBox name:{checkPictureBox.Name}");
                Console.WriteLine($"[{_selectedCards[1][1].ToString()}]");
                Console.WriteLine();
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
        /// Sets difficulty
        /// </summary>
        /// <param name="difficultyRecieved">Difficulty recieved from DifficultyForm</param>
        public void SetDifficulty(string difficultyRecieved)
        {
            _gameDifficulty = difficultyRecieved;
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //Garbage collection from previous game
            if(_board != null)
            {
                _board.CleanPexesoBoard();
                _board = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            //Prepares space for selected cards
            _selectedCards.Add(0, null);
            _selectedCards.Add(1, null);

            //Shows a difficulty form to select difficulty
            DifficultyForm diffForm = new DifficultyForm(transferDelegate);
            diffForm.ShowDialog();
            diffForm.Dispose();
            try
            {
                switch (_gameDifficulty)
                {
                    case "Easy":
                        _board = new PexesoBoard(4, 4);
                        GeneratePexesoTable(4, 4, _board);
                        break;
                    case "Normal":
                        _board = new PexesoBoard(8, 8);
                        GeneratePexesoTable(8, 8, _board);
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

