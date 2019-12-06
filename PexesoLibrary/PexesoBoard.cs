using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pexeso
{
    public class PexesoBoard
    {
        /// <summary>
        /// 2D field which saves all PexesoCars on board
        /// </summary>
        private PexesoCard[,] _pexesoCardsArray;
        /// <summary>
        /// Random number generator used to create PexesoCards verification ints
        /// </summary>
        private Random _rnd = new Random();
        /// <summary>
        /// Stores verification int and number of times it can be used in PexesoCards on PexesoBoard 
        /// </summary>
        private Dictionary<int, int> _verificationNumDictionary = new Dictionary<int, int>();
        /// <summary>
        /// Stores images based on verification numbers
        /// </summary>
        private Dictionary<int, Image> _picturesDictionary = new Dictionary<int, Image>();
        /// <summary>
        /// Saves how much pairs of PexesoCards is on PexesoBoard
        /// </summary>a
        public int PexesoBoardPairs { get; set; }

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
                PexesoBoardPairs = _pexesoCardsArray.Length / 2;
                FillVerificationDictionary(PexesoBoardPairs);
                FillImagesDictionary();
            }
            else
            {
                throw new ArgumentException("Error: Cant create PexesoBoard with odd number of elements. Create PexesoBoard with even number of elements");
            }
        }

        /// <summary>
        /// Adds new PexesoCard to coordinates on Pexesoboard
        /// </summary>
        /// <param name="row">Rows coordinate in field</param>
        /// <param name="column">Column coordinate in field</param>
        /// <param name="picture">Picture represeinting PexesoCard</param>
        public void AddToPexesoBoard(int row, int column)
        {
            int verifInt = FindVerificationInt(PexesoBoardPairs);
            _pexesoCardsArray[row, column] = new PexesoCard($"PexesoCard[{row}][{column}]", verifInt, FindImage(verifInt));
            Console.WriteLine("Added [" + _pexesoCardsArray[row, column].ToString() + "]");
        }

        private Image FindImage(int verificationInt)
        {
            return _picturesDictionary[verificationInt];
        }
        /// <summary>
        /// Finds verificationInt that wasnt used 2 times 
        /// </summary>
        /// <param name="numOfPexesoPairs"></param>
        /// <returns>Verification int used in PexesoCards</returns>
        private int FindVerificationInt(int numOfPexesoPairs)
        {
            bool verificationIntFound = false;
            int verNumCandidate = -1;
            while (verificationIntFound == false)
            {
                verNumCandidate = _rnd.Next(numOfPexesoPairs);
                if(_verificationNumDictionary[verNumCandidate] > 0)
                {
                    _verificationNumDictionary[verNumCandidate] -= 1;
                    verificationIntFound = true;
                }
            }
            return verNumCandidate;
        }
        /// <summary>
        /// Fills _verificationNumDictionary with verification numbers and how much times they can be used (2 times for each)
        /// </summary>
        /// <param name="numOfPexesoPairs">Number of Pexeso card pairs on Pexeso board</param>
        private void FillVerificationDictionary(int numOfPexesoPairs)
        {
            for(int i = 0; i < numOfPexesoPairs; i++)
            {
                _verificationNumDictionary.Add(i, 2);
                Console.WriteLine($"Key: {i} Value: {_verificationNumDictionary[i]}");
            }
        }
        /// <summary>
        /// Fills images dictionary with images from Resources dictionary
        /// </summary>
        /// <param name="numOfPexesoPairs"></param>
        private void FillImagesDictionary()
        {
            _picturesDictionary.Add(0, ResourcesLibrary.Resource1._1);
            _picturesDictionary.Add(1, ResourcesLibrary.Resource1._2);
            _picturesDictionary.Add(2, ResourcesLibrary.Resource1._3);
            _picturesDictionary.Add(3, ResourcesLibrary.Resource1._4);
            _picturesDictionary.Add(4, ResourcesLibrary.Resource1._5);
            _picturesDictionary.Add(5, ResourcesLibrary.Resource1._6);
            _picturesDictionary.Add(6, ResourcesLibrary.Resource1._7);
            _picturesDictionary.Add(7, ResourcesLibrary.Resource1._8);
            _picturesDictionary.Add(8, ResourcesLibrary.Resource1._9);
            _picturesDictionary.Add(9, ResourcesLibrary.Resource1._10);
            _picturesDictionary.Add(10, ResourcesLibrary.Resource1._11);
            _picturesDictionary.Add(11, ResourcesLibrary.Resource1._12);
            _picturesDictionary.Add(12, ResourcesLibrary.Resource1._13);
            _picturesDictionary.Add(13, ResourcesLibrary.Resource1._14);
            _picturesDictionary.Add(14, ResourcesLibrary.Resource1._15);
            _picturesDictionary.Add(15, ResourcesLibrary.Resource1._16);
            _picturesDictionary.Add(16, ResourcesLibrary.Resource1._17);
            _picturesDictionary.Add(17, ResourcesLibrary.Resource1._18);
            _picturesDictionary.Add(18, ResourcesLibrary.Resource1._19);
            _picturesDictionary.Add(19, ResourcesLibrary.Resource1._20);
            _picturesDictionary.Add(20, ResourcesLibrary.Resource1._21);
            _picturesDictionary.Add(21, ResourcesLibrary.Resource1._22);
            _picturesDictionary.Add(22, ResourcesLibrary.Resource1._23);
            _picturesDictionary.Add(23, ResourcesLibrary.Resource1._24);
            _picturesDictionary.Add(24, ResourcesLibrary.Resource1._25);
            _picturesDictionary.Add(25, ResourcesLibrary.Resource1._26);
            _picturesDictionary.Add(26, ResourcesLibrary.Resource1._27);
            _picturesDictionary.Add(27, ResourcesLibrary.Resource1._28);
            _picturesDictionary.Add(28, ResourcesLibrary.Resource1._29);
            _picturesDictionary.Add(29, ResourcesLibrary.Resource1._30);
            _picturesDictionary.Add(30, ResourcesLibrary.Resource1._31);
            _picturesDictionary.Add(31, ResourcesLibrary.Resource1._32);

        }
        /// <summary>
        /// Checks if player won the game
        /// </summary>
        /// <returns>Bool true or false</returns>
        public bool IsGameWon()
        {
            if(PexesoBoardPairs == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if PexesoCards belong to same pair
        /// </summary>
        /// <param name="card1">First PexesoCard</param>
        /// <param name="card2">Second PexesoCard</param>
        /// <returns>True - belong to same pair, False - doesnt belong to same pair</returns>
        public bool CheckIfSame(PexesoCard card1, PexesoCard card2)
        {
            if(card1.VeryfInt == card2.VeryfInt)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public PexesoCard[,] GetPexesoCardsArray()
        {
            return _pexesoCardsArray;
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
