using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pexeso
{
    public class PexesoBoard
    {
        /// <summary>
        /// 2D field which saves all PexesoCars on board
        /// </summary>
        private PexesoCard[,] _pexesoCardsArray;

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
                this._pexesoCardsArray = new PexesoCard[rowCount, columnCount];
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
        /// <param name="picture">Picture represeinting PexesoCard</param>
        public void AddToPexesoBoard(int row, int column, Image picture)
        {
            Random rnd = new Random();
            _pexesoCardsArray[row, column] = new PexesoCard($"PexesoCard[{row}][{column}]", rnd.Next(8), picture);
            Console.WriteLine("Added [" + _pexesoCardsArray[row, column].ToString() + "]");
        }
        /// <summary>
        /// Gets number of rows of pexesoCardsArray
        /// </summary>
        /// <returns>Number of rows of pexesoCardsArray</returns>
        public int getPexesoBoardRows()
        {
            return _pexesoCardsArray.GetLength(0);
        }
        /// <summary>
        /// Gets number of columns of pexesoCardsArray
        /// </summary>
        /// <returns>Number of columns of pexesoCardsArray</returns>
        public int getPexesoBoardColumns()
        {
            return _pexesoCardsArray.GetLength(1);
        }
        /// <summary>
        /// Gets PexesoCard object from pexesoCardsArray on position from parameters
        /// </summary>
        /// <param name="row">Row parameter</param>
        /// <param name="column">Column parameter</param>
        /// <returns>PexesoCard object</returns>
        public PexesoCard GetPexesoCard(int row, int column)
        {
            return _pexesoCardsArray[row, column];
        }
        /// <summary>
        /// Clears all PexesoCards from array
        /// </summary>
        public void CleanPexesoBoard()
        {
            for(int i = 0; i < _pexesoCardsArray.GetLength(0); i++)
            {
                for (int j = 0; j < _pexesoCardsArray.GetLength(1); j++)
                {
                    _pexesoCardsArray[i, j] = null;
                }
            }
        }
    }
}
