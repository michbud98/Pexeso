using System;
using System.Windows.Forms;

namespace Pexeso
{
    class PexesoCard
    {   
        /// <summary>
        /// Used to identify the same cards. This int must be higher then or equal to 0
        /// </summary>
        public int VerificationInt { get; set; }
        /// <summary>
        /// Identifies which picturebox represents the PexesoCard
        /// </summary>
        public PictureBox Picture { get; set; }

        public PexesoCard(int verificationInt, PictureBox picture)
        {
            this.VerificationInt = verificationInt;
            this.Picture = picture;
        }
        /// <summary>
        /// Debug ToString
        /// </summary>
        /// <returns>String with Verification int and PictureBox name</returns>
        public override String ToString()
        {
            return "Pexeso card verificationInt = " + VerificationInt + ", pictureBox = " + Picture.Name;
        }
    }
}
