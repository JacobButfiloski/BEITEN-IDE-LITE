using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace BEITEN
{
    public partial class ExportForm : Form
    {
        PrivateFontCollection pfc = new PrivateFontCollection();
        Command command = new Command();
        public ExportForm(string[] text)
        {
            InitializeComponent();

            var sr = new StreamReader(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"/resources/settings.txt");
            //Set a fontsize variable for later from the settings file
            var fontSize = float.Parse(sr.ReadLine());
            //Set a Color variable for text color
            Color textColor = Color.FromName(sr.ReadLine());
            //Assign previous color variable to textbox font
            textBox1.ForeColor = textColor;

            textBox1.BackColor = Color.FromArgb(01, 00, 128);
            this.BackColor = Color.FromArgb(01, 00, 128);
            sr.Dispose();
            //Add embedded file to private font family
            pfc.AddFontFile(Path.GetDirectoryName(Application.ExecutablePath) + @"\Nouveau_IBM_Stretch.ttf");
            label1.Font = new Font(pfc.Families[0], 12, FontStyle.Regular);

            foreach(string s in text)
            {
                Console.WriteLine(s);
            }
            ///Convert
            try
            {
                //File.WriteAllLines(refURL, textBox1.Lines);
                List<string> commands = new List<string> { };
                List<string> args = new List<string> { };
                List<string> args2 = new List<string> { };
                List<string> args3 = new List<string> { };
                for (int i = 0; i < text.Length; i++)
                {
                    string[] refe = text[i].Split(' ');
                    commands.Add(refe[0].ToUpper());
                    if (refe.Length > 1) { args.Add(refe[1]); } else { args.Add(""); }
                    if (refe.Length > 2) { args2.Add(refe[2]); } else { args.Add(""); }
                    if (refe.Length > 3) { args3.Add(refe[3]); } else { args.Add(""); }
                }

                command.command = commands.ToArray();
                command.arg = args.ToArray();
                command.arg2 = args.ToArray();
                command.arg3 = args.ToArray();
                string json = new JavaScriptSerializer().Serialize(command);
                string[] jsonArr = json.Split('"');
                string news = "";
                json = json.Replace('"', '\'');
                textBox1.Text = "\"" + json + "\";";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {

        }
    }
    public class Command
    {

        public string[] command;
        public string[] arg;
        public string[] arg2;
        public string[] arg3;
    }
}
