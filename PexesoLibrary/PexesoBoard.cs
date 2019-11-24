using System;
using System.Windows.Forms;

namespace Pexeso
{
    public class PexesoBoard
    {
        /// <summary>
        /// 2D field which saves all PexesoCars on board
        /// </summary>
        private PexesoCard[,] pexesoCardsArray;

        /// <summary>
        /// Constructor of PexesoBoard class
        /// </summary>
        /// <param name="rowCount">number of rows</param>
        /// <param name="columnCount">number of columns</param>
        /// <exception cref="ArgumentException">User tried to create PexesoBoard with odd number of elements.</exception>
        public PexesoBoard(int rowCount, int columnCount)
        {
            if (rowCount * columnCount % 2 == 0)
            {
                this.pexesoCardsArray = new PexesoCard[rowCount, columnCount];
            }
            else
            {
                throw new ArgumentException($"Error: Cant create PexesoBoard with odd number of elements. Create PexesoBoard with even number of elements");
            }
        }

        /// <summary>
        /// Adds new PexesoCard to coordinates on Pexesoboard
        /// </summary>
        /// <param name="row">Rows coordinate in field</param>
        /// <param name="column">Column coordinate in field</param>
        /// <param name="picture">PictureBox representing a PexesoCard in UI</param>
        public void AddToPexesoBoard(int row, int column, PictureBox picture)
        {
            Random rnd = new Random();
            pexesoCardsArray[row, column] = new PexesoCard(rnd.Next(8), picture);
            Console.WriteLine("Added [" + pexesoCardsArray[row, column].ToString() + "]");
        }

        public int getPexesoBoardRows()
        {
            return pexesoCardsArray.GetLength(0);
        }

        public int getPexesoBoardColumns()
        {
            return pexesoCardsArray.GetLength(1);
        }
    }
}
