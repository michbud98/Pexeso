using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    class PexesoBoard
    {
        /// <summary>
        /// 2D field which saves all PexesoCars on board
        /// </summary>
        private PexesoCard[,] pexesoCardsArray;
        
        /// <summary>
        /// Constructor of PexesoBoard class
        /// </summary>
        /// <param name="x">number of rows</param>
        /// <param name="y">number of columns</param>
        public PexesoBoard(int x, int y)
        {
            if (x * y % 2 == 0)
            {
                this.pexesoCardsArray = new PexesoCard[x, y];
            }
            else
            {
                throw new ArgumentException($"Error: Cant create PexesoBoard with odd number of elements. Create PexesoBoard with even number of elements");
            }
        }

        /// <summary>
        /// Adds new PexesoCard to coordinates on Pexesoboard
        /// </summary>
        /// <param name="x">Rows coordinate in field</param>
        /// <param name="y">Column coordinate in field</param>
        /// <param name="picture">PictureBox representing a PexesoCard in UI</param>
        public void AddToPexesoBoard(int x, int y, PictureBox picture)
        {
            Random rnd = new Random();
            pexesoCardsArray[x, y] = new PexesoCard(rnd.Next(8), picture);
            Console.WriteLine("Added [" + pexesoCardsArray[x, y].ToString() + "]");
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
