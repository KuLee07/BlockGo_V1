using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockGo_ControlPanel
{
    public partial class Form2 : Form
    {
        public string ResultMessage = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            ResultMessage = "FirstPlayer";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSecond_Click(object sender, EventArgs e)
        {
            ResultMessage = "SecondPlayer";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
