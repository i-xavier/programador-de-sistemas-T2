using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testandoShowHide
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void Voltar(object sender, EventArgs e)
        {
            frmShowHide form = new frmShowHide();
            form.Show();
            this.Hide();
        }
    }
}
