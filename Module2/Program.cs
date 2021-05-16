using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;

namespace Module2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new MainWindow());
        }
    }

    class MainWindow : Form
    {
        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label3;

        private string googleMaps = "https://maps.googleapis.com/maps/api/elevation/json";
        private string key = "AIzaSyDlR2SjaR1_ZuGwSf1NPs0n8es73goqLuQ";

        private IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };


        public MainWindow()
        {
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label2 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();

            label1.AutoSize = true;
            label1.Location = new Point(31, 24);
            label1.Name = "label1";
            label1.Size = new Size(45, 13);
            label1.TabIndex = 0;
            label1.Text = "Широта";

            button1.Location = new Point(70, 96);
            button1.Name = "button1";
            button1.Size = new Size(127, 23);
            button1.TabIndex = 1;
            button1.Text = "Дізнатись висоту";
            button1.UseVisualStyleBackColor = true;
            button1.Click += GetElevationAsync;

            textBox1.Location = new Point(82, 21);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 20);
            textBox1.TabIndex = 2;

            textBox2.Location = new Point(82, 55);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 20);
            textBox2.TabIndex = 3;

            label2.AutoSize = true;
            label2.Location = new Point(26, 55);
            label2.Name = "label2";
            label2.Size = new Size(50, 13);
            label2.TabIndex = 4;
            label2.Text = "Довгота";

            textBox3.Location = new Point(82, 141);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 20);
            textBox3.TabIndex = 5;
            textBox3.Enabled = false;

            label3.AutoSize = true;
            label3.Location = new Point(33, 144);
            label3.Name = "label3";
            label3.Size = new Size(43, 13);
            label3.TabIndex = 6;
            label3.Text = "Висота";

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(248, 197);

            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label1);

            Name = "Form1";
            Text = "Form1";
        }

        private async void GetElevationAsync(object sender, EventArgs e)
        {
            double lat = double.Parse(textBox1.Text, formatter);
            double lng = double.Parse(textBox2.Text, formatter);

            string url = googleMaps + $"?locations={lat.ToString(CultureInfo.InvariantCulture)},{lng.ToString(CultureInfo.InvariantCulture)}&key=" + key;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Results resultRequest;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string message = reader.ReadToEnd();
                    resultRequest = JsonSerializer.Deserialize<Results>(message);
                }
            }

            response.Close();

            textBox3.Text = String.Format("{0:0.00}", resultRequest.results[0].elevation);
        }
    }

    public class Results
    {
        public IList<Result> results { get; set; }
    }

    public class Result
    {
        public double elevation { get; set; }
        public Location location { get; set; }
        public double resolution { get; set; }
        public string status { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
