using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WebcamCamera
{
    public partial class Camera : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice VideoDevices;
        public Camera()
        {
            InitializeComponent();
            getallcameralist();
        }

        void getallcameralist()
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Devices in CaptureDevice)
            {
                comboCamera.Items.Add(Devices.Name);
            }
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            try
            {
                VideoDevices = new VideoCaptureDevice(CaptureDevice[comboCamera.SelectedIndex].MonikerString);
                VideoDevices.NewFrame += new NewFrameEventHandler(NewVideoFrame);
                VideoDevices.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NewVideoFrame(Object sender, NewFrameEventArgs eventArgs)
        {

            pbLeft.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void btCaputre_Click(object sender, EventArgs e)
        {
            pbRight.Image = (Bitmap)pbLeft.Image.Clone();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Image As";
            sfd.Filter = "Image files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
            System.Drawing.Imaging.ImageFormat imageformate = System.Drawing.Imaging.ImageFormat.Png;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        imageformate = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        imageformate = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;

                }
                pbRight.Image.Save(sfd.FileName, imageformate);
            }
        }
        private byte[] _cameraPhoto = null;

        public byte[] CameraPhoto
        {
            get
            {
                return _cameraPhoto;
            }
            set
            {
                _cameraPhoto = value;

            }
        }
        public static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (VideoDevices.IsRunning == true)
            {
                VideoDevices.Stop();
            }

            CameraPhoto = ImageToByte2(pbRight.Image);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
