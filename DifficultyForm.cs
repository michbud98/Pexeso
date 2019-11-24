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
        public DifficultyForm()
        {
            InitializeComponent();
        }

        private void DifficultyForm_Load(object sender, EventArgs e)
        {
            difficultyBox.SelectedItem = null;
            difficultyBox.SelectedText = "--select--";
        }
    }
}
