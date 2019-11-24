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
    public partial class DifficultyForm : Form
    {
        DataTransfer transferDel;
        /// <summary>
        /// Form that lets us select difficulty
        /// </summary>
        /// <param name="del">Delegate through which we send our difficulty to PexesoUI form</param>
        public DifficultyForm(DataTransfer del)
        {
            InitializeComponent();
            transferDel = del;
        }
        private void DifficultyForm_Load(object sender, EventArgs e)
        {
            difficultyComboBox.SelectedItem = null;
            difficultyComboBox.SelectedText = "--select--";
        }
        /// <summary>
        /// After click chooses difficulty selected from difficultyComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("String send to delegate " + difficultyComboBox.Text);
            if (difficultyComboBox.Text.Contains("--select--"))
            {
                MessageBox.Show("Warning:You must choose difficulty in order to proceed.");
            }
            else
            {
                transferDel.Invoke(difficultyComboBox.Text);
                Close();
            }
            
        }
    }
}
