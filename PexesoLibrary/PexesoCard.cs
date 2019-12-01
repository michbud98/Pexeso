using System;
using System.Drawing;
using System.Windows.Forms;


namespace Pexeso
{
    public class PexesoCard
    {   
        public string Name { get; set; }
        /// <summary>
        /// Used to identify the same cards. This int must be higher then or equal to 0
        /// </summary>
        public int VeryfInt { get; set; }
        /// <summary>
        /// Picture that will represent PexesoCard in UI
        /// </summary>
        public Image Picture { get; set; }

        public PexesoCard(string name, int verificationInt, Image picture)
        {
            this.Name = name;
            this.VeryfInt = verificationInt;
            this.Picture = picture;
        }
        /// <summary>
        /// Debug ToString
        /// </summary>
        /// <returns>String with Verification int and PictureBox name</returns>
        public override String ToString()
        {
            return $"Pexeso card name: {Name}, Pexeso card verificationInt =  {VeryfInt}";
        }
    }
}
