using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisorFotos;

namespace Prueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            userControl11.imageSelected += new VisorFotos.UserControl1.ImageSelectedDelegate(visor_ImageSelected);
        }
        private void visor_ImageSelected(object sender,VisorFotos.ImageSelectedArgs e)
        {
            MessageBox.Show("Haz pinchado en " + e.FileName);
        }
    }
}
