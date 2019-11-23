using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class PexesoForm : Form
    {
        private PexesoBoard board;
        public PexesoForm()
        {
            InitializeComponent();
        }

        private void GeneratePexesoTable(int rowCount, int columnCount)
        {
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
                    //Next, add a row.  Only do this when once, when creating the first column
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
                        Image = Image.FromFile("Smile.jpg"),
                        Size = new Size(width, height),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Anchor = AnchorStyles.None
                    };
                    picture.MouseClick += Picture_Click;
                    pexesoLayoutPanel.Controls.Add(picture, column, row);
                }
            }
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            Console.WriteLine(clickedPictureBox.Name);
        }

        private void newGameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                board = new PexesoBoard(8, 8);
                GeneratePexesoTable(8, 8);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

