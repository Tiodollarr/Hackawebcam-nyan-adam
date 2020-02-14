using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Data.SqlClient;


namespace Projekt_Hackawebcam_nya
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private FilterInfoCollection CaptureDevices;
        private VideoCaptureDevice videoSource;
        private void Form1_Load(object sender, EventArgs e)
        {

            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevices)
            {

                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            videoSource = new VideoCaptureDevice();

            string connetionString;
            SqlConnection con;
            connetionString = @"Data Source-CND8263Q7N; Initial Catalog-Webcam 18it;User ID-Adam;Password-itg123.";
            con = new SqlConnection(connetionString);
            con.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            string sql, output = "";

            sql = "SELECT Text FROM Texts";
            command = new SqlCommand(sql, con);
            dataReader = command.ExecuteReader();

            dataReader.Read();
            button1.Text = "Klicka här för att tjäna pengar";
            con.Close();


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            videoSource = new VideoCaptureDevice(CaptureDevices[comboBox1.SelectedIndex].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            videoSource.Start();

        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();



        }

     
    }
}
