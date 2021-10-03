using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Servcicio_Post_DarkSky
{
    public partial class Form1 : Form
    {
        /* 
        https://docs.microsoft.com/en-us/troubleshoot/iis/make-get-request
        https://www.youtube.com/watch?v=ryz3Q_xsmPI
        */
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string sURL;
            WebRequest wrGETURL;
            JObject json;
            Stream objStream;

            try
            {
                sURL = "https://api.darksky.net/forecast/b4497c8a6b18bf75776b7fd997f2b90a/-34.6119666,-58.4030375?lang=es&units=ca";
                wrGETURL = WebRequest.Create(sURL);
                objStream = wrGETURL.GetResponse().GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);

                string sLine = "";
                string sLinePrev = "";
                int i = 0;

                while (sLine != null)
                {
                    i++;
                    sLinePrev = sLine;
                    sLine = objReader.ReadLine();
                    //if (sLine != null)
                    //    MessageBox.Show(sLine);                    

                }
                //MessageBox.Show(sLinePrev);

                json = JObject.Parse(sLinePrev);

                pictureBox1.Image = Image.FromFile("imagenes/" + json["daily"]["data"][0]["icon"] + ".png");
                pictureBox2.Image = Image.FromFile("imagenes/" + json["daily"]["data"][1]["icon"] + ".png");
                pictureBox3.Image = Image.FromFile("imagenes/" + json["daily"]["data"][2]["icon"] + ".png");
                pictureBox4.Image = Image.FromFile("imagenes/" + json["daily"]["data"][3]["icon"] + ".png");
                pictureBox5.Image = Image.FromFile("imagenes/" + json["daily"]["data"][4]["icon"] + ".png");
                pictureBox6.Image = Image.FromFile("imagenes/" + json["daily"]["data"][5]["icon"] + ".png");
                pictureBox7.Image = Image.FromFile("imagenes/" + json["daily"]["data"][6]["icon"] + ".png");

                label1.Text = json["daily"]["data"][0]["summary"].ToString();
                label2.Text = json["daily"]["data"][1]["summary"].ToString();
                label3.Text = json["daily"]["data"][2]["summary"].ToString();
                label4.Text = json["daily"]["data"][3]["summary"].ToString();
                label5.Text = json["daily"]["data"][4]["summary"].ToString();
                label6.Text = json["daily"]["data"][5]["summary"].ToString();
                label7.Text = json["daily"]["data"][6]["summary"].ToString();

                T1.Text = json["currently"]["temperature"]+"°C".ToString();
                label9.Text = json["currently"]["apparentTemperature"]+"°C".ToString();
                label11.Text = json["currently"]["precipProbability"]+"%".ToString();
                label12.Text = json["timezone"].ToString();

                T2.Text = json["daily"]["data"][1]["temperatureHigh"]+"°C".ToString();
                T4.Text = json["daily"]["data"][2]["temperatureHigh"]+"°C".ToString();
                T6.Text = json["daily"]["data"][3]["temperatureHigh"]+"°C".ToString();
                T8.Text = json["daily"]["data"][4]["temperatureHigh"]+"°C".ToString();
                T10.Text = json["daily"]["data"][5]["temperatureHigh"]+"°C".ToString();
                T12.Text = json["daily"]["data"][6]["temperatureHigh"]+"°C".ToString();

                T3.Text = json["daily"]["data"][1]["temperatureLow"]+"°C".ToString();
                T5.Text = json["daily"]["data"][2]["temperatureLow"]+"°C".ToString();
                T7.Text = json["daily"]["data"][3]["temperatureLow"]+"°C".ToString();
                T9.Text = json["daily"]["data"][4]["temperatureLow"]+"°C".ToString();
                T11.Text = json["daily"]["data"][5]["temperatureLow"]+"°C".ToString();
                T13.Text = json["daily"]["data"][6]["temperatureLow"]+"°C".ToString();

                double time = Convert.ToDouble(json["currently"]["time"]);
                DateTime dia = Tiempo(time);
                string day = dia.ToLongDateString();
                string[] d = day.Split(',');
                D1.Text = d[0];

                time = Convert.ToDouble(json["daily"]["data"][1]["time"]);
                dia = Tiempo(time);
                day = dia.ToLongDateString();
                d = day.Split(',');
                D2.Text = d[0];

                time = Convert.ToDouble(json["daily"]["data"][2]["time"]);
                dia = Tiempo(time);
                day = dia.ToLongDateString();
                d = day.Split(',');
                D3.Text = d[0];

                time = Convert.ToDouble(json["daily"]["data"][3]["time"]);
                dia = Tiempo(time);
                day = dia.ToLongDateString();
                d = day.Split(',');
                D4.Text = d[0];

                time = Convert.ToDouble(json["daily"]["data"][4]["time"]);
                dia = Tiempo(time);
                day = dia.ToLongDateString();
                d = day.Split(',');
                D5.Text = d[0];

                time = Convert.ToDouble(json["daily"]["data"][5]["time"]);
                dia = Tiempo(time);
                day = dia.ToLongDateString();
                d = day.Split(',');
                D6.Text = d[0];

                time = Convert.ToDouble(json["daily"]["data"][6]["time"]);
                dia = Tiempo(time);
                day = dia.ToLongDateString();
                d = day.Split(',');
                D7.Text = d[0];
            }

            catch
            {
                MessageBox.Show("se requiere conexiona internet intentelo mas tarde", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                Close();
            }
        }

        public static DateTime Tiempo(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
    }
}
