using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Pexeso
{
    /// <summary>
    /// Delegate that transfers data from Difficulty form to PexesoUI form
    /// </summary>
    /// <param name="data">Data to be transfered</param>
    public delegate void DataTransfer(string data);
    public partial class PexesoUI : Form
    {
        /// <summary>
        /// Stores PexesoCards object in array
        /// </summary>
        private PexesoBoard _board;
        /// <summary>
        /// Delegate used to transfer selected difficulty from DifficultyForm
        /// </summary>
        public DataTransfer transferDelegate;
        /// <summary>
        /// Stores game difficulty
        /// </summary>
        private string _gameDifficulty;
        /// <summary>
        /// Stores currently select PictureBox
        /// </summary>
        private PictureBox _selectedPictureBox;
        /// <summary>
        /// Stores PictureBox that was selected previously
        /// </summary>
        private PictureBox _beforeSelectedPictureBox;
        /// <summary>
        /// Stores currently select PexesoCard
        /// </summary>
        private PexesoCard _selectedPexesoCard;
        /// <summary>
        /// Stores PexesoCard that was selected previously
        /// </summary>
        private PexesoCard _beforeSelectedPexesoCard;
        /// <summary>
        /// Form that represents PexesoBoard on UI
        /// </summary>
        public PexesoUI()
        {
            InitializeComponent();
            transferDelegate += new DataTransfer(SetDifficulty);
            CreateNewGame();
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
                throw new ArgumentException("UI board rowCount and PexesoBoard rowCount is not the same.", "rowCount");
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
                        Image = ResourcesLibrary.Resource1.question_mark,
                        Size = new Size(width, height),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Anchor = AnchorStyles.None
                    };
                    picture.MouseClick += Picture_Click;
                    pexesoLayoutPanel.Controls.Add(picture, column, row);
                    board.AddToPexesoBoard(row, column);
                }
            }
        }
        /// <summary>
        /// Selects a PictureBox from pexesoLayoutPanel
        /// </summary>
        /// <param name="sender">A Picture box object which we clicked</param>
        /// <param name="e">A click event</param>aa
        private void Picture_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            PexesoCard clickedPexesoCard = _board.GetPexesoCard(GetPictureBoxRow(clickedPictureBox.Name), GetPictureBoxColumn(clickedPictureBox.Name));
            clickedPictureBox.Image = clickedPexesoCard.Picture;
            clickedPictureBox.Refresh();
            if (_selectedPictureBox == null && _selectedPexesoCard == null)
            {
                _selectedPictureBox = clickedPictureBox;
                _selectedPexesoCard = clickedPexesoCard;
            }
            else if (_selectedPictureBox != null && _selectedPexesoCard != null)
            {
                _beforeSelectedPictureBox = _selectedPictureBox;
                _beforeSelectedPexesoCard = _selectedPexesoCard;
                _selectedPictureBox = clickedPictureBox;
                _selectedPexesoCard = clickedPexesoCard;
                if (CheckIfSamePair())
                {
                    CheckIfGameEnds();
                }
                _selectedPictureBox = null;
                _beforeSelectedPictureBox = null;
                _selectedPexesoCard = null;
                _beforeSelectedPexesoCard = null;
                
            }
        }
        /// <summary>
        /// Checks if two selected PexesoCards are from the same pair
        /// </summary>
        private bool CheckIfSamePair()
        {
            //Sleeps main thread so that user can see the card that he selected
            Thread.Sleep(700);
            pexesoLayoutPanel.Enabled = false;
            if (_board.CheckIfSame(_selectedPexesoCard, _beforeSelectedPexesoCard))
            {
                _board.PexesoBoardPairs -= 1;
                _selectedPictureBox.Image = ResourcesLibrary.Resource1.Smile;
                _beforeSelectedPictureBox.Image = ResourcesLibrary.Resource1.Smile;
                _selectedPictureBox.Enabled = false;
                _beforeSelectedPictureBox.Enabled = false;
                pexesoLayoutPanel.Enabled = true;
                return true;
            }
            else
            {
                _selectedPictureBox.Image = ResourcesLibrary.Resource1.question_mark;
                _beforeSelectedPictureBox.Image = ResourcesLibrary.Resource1.question_mark;
            }
            pexesoLayoutPanel.Enabled = true;
            return false;
        }
        /// <summary>
        /// Checks if player found all PexesoCars pair and if he did asks him if he wants to create a new game
        /// </summary>
        private void CheckIfGameEnds()
        {
            if (_board.IsGameWon() == true)
            {
                DialogResult dialogResult = MessageBox.Show("You found all PexesoCard pairs. The game will now exit if you dont create a new game." +
                    " Do you want to create a new game?", "Congratulations!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    CreateNewGame();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// Finds row coordinate of pictureBox
        /// </summary>
        /// <param name="pictureBoxName">PictureBox name on which we clicked</param>
        /// <exception cref="ArgumentException">PictureBox name doesnt match regex or position cant be extracted</exception>
        /// <returns>PictureBox row coordinate</returns>
        private int GetPictureBoxRow(string pictureBoxName)
        {
            Regex regex = new Regex(@"(pictureBox)\[(\d+)\]\[(\d+)\]");
            Match match = regex.Match(pictureBoxName);
            if (match.Success && Int32.TryParse(match.Groups[2].Value, out int result))
            {
                return result;
            }
            else if (!match.Success)
            {
                throw new ArgumentException($"{pictureBoxName} doesnt match regex.", "pictureBoxName");
            }
            else
            {
                throw new ArgumentException($"Cant extract position from {pictureBoxName}", "pictureBoxName");
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
        /// Creates a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CreateNewGame();
        }
        /// <summary>
        /// Creates a new game
        /// </summary>
        private void CreateNewGame()
        {
            //Garbage collection from previous game
            if (_board != null)
            {
                _board.CleanPexesoBoard();
                _board = null;
                _gameDifficulty = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
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
                    case "Hard":
                        _board = new PexesoBoard(8, 8);
                        GeneratePexesoTable(8, 8, _board);
                        break;
                    default:
                        MessageBox.Show("Warning: Difficulty was not selected.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

