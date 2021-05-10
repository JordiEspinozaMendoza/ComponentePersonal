using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace VisorFotos
{
    public partial class UserControl1 : UserControl
    {
        public delegate void ImageSelectedDelegate(object sender, ImageSelectedArgs e);
        public event ImageSelectedDelegate imageSelected;
        private String directory = "";
        private int dimention;
        private int separation;

        private int border = 7;

        private List<ImageWithName> images = new List<ImageWithName>();
        public string Directory
        {
            get { return directory; }
            set
            {
                directory = value;
                GetImages();
                UpdateControl();
            }
        }
        public int Dimention
        {
            get { return dimention; }
            set
            {
                dimention = value;
                UpdateControl();
            }
        }
        public int Separation
        {
            get { return separation; }
            set
            {
                separation = value;
                UpdateControl();
            }
        }
        public UserControl1()
        {
            InitializeComponent();
        }
        private void GetImages()
        {
            if (this.directory.Length != 0)
            {
                images.Clear();

                DirectoryInfo directoryInfo = new DirectoryInfo(Directory);

                foreach (FileInfo file in directoryInfo.GetFiles("*.jpg"))
                {
                    images.Add(new ImageWithName(Bitmap.FromFile(file.FullName), file.FullName));
                }
            }
        }
        private void UpdateControl()
        {
            panel1.SuspendLayout();

            foreach (Control control in panel1.Controls)
            {
                control.Dispose();
            }
            panel1.Controls.Clear();
            int col = border, fila = border;
            foreach (ImageWithName image in images)
            {
                PictureBox pic = new PictureBox();
                pic.Image = image.image;
                pic.Tag = image.FileName;
                pic.Size = new Size(dimention, dimention);
                pic.Location = new Point(col, fila);
                panel1.Controls.Add(pic);
                pic.Click += new EventHandler(this.pic_Click);
                col += dimention + separation;
                if ((col + separation + dimention + border) > this.Width)
                {
                    col = border;
                    fila += dimention + separation;
                }
            }
            panel1.ResumeLayout();
        }
        public void Refresh()
        {
            GetImages();
            UpdateControl();
        }
        public void pic_Click(object sender, System.EventArgs e)
        {
            PictureBox picSelected = (PictureBox)sender;
            ImageSelectedArgs args = new ImageSelectedArgs(picSelected.Image,(string)picSelected.Tag);
            imageSelected(this, args);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateControl();
            base.OnSizeChanged(e);
        }
    }
    internal class ImageWithName
    {
        public Image image { get; set; }
        public string FileName { get; set; }
        public ImageWithName(Image image, string FileName)
        {
            this.image = image;
            this.FileName = FileName;
        }
    }    
    public class ImageSelectedArgs:EventArgs
    {
        public Image image { get; set; }
        public string FileName { get; set; }
        public ImageSelectedArgs(Image image, string FileName)
        {
            this.image = image;
            this.FileName = FileName;
        }
    }
}