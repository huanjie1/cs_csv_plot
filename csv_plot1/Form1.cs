using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace csv_plot1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 0s
            //string text1 = File.ReadAllText(@"C:\Users\y689\Desktop\201601SOANoise\noisemeasure\Filethru.DAT");
            //label1.Text += text1;

            //char[] splitchar = {};
            //string[] linessplit = text1.Split(splitchar);

            //label4.Text = linessplit[0];

            //string text1 = File.ReadAllText(@"C:\Users\y689\Desktop\201601SOANoise\noisemeasure\Filethru.DAT");
            //char[] splitchar = { };
            //string[] linessplit = text1.Split(splitchar);
            //int numoflines = linessplit.Length;
            ////for (int ii = 0; ii < numoflines; ii++)
            ////{
            ////    label2.Text += linessplit[ii] + '\n';
            ////}

            char[] splitchar = { ','};
            string a1 = "10012500000, 1.789340e-001,";
            //string a1 = "9999502898.550724\t -134.8192138671875\t -200";
            //string a1 = "9999502898.550724,-134.8192138671875, -200,";
            string[] a1spl=a1.Split(splitchar);
            int numofa1spl = a1spl.Length;
            for (int ii = 0; ii < numofa1spl; ii++)
            {
                label1.Text += a1spl[ii] +'a'+ '\n';
            }
            label1.Text +=  'b';
            Regex rg = new Regex(";");
            MatchCollection mc = rg.Matches(a1);
            label1.Text += mc.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 5s
            //string fileAdd = @"C:\Users\y689\Desktop\201601SOANoise\noisemeasure\Trace_0027.csv";
            //string fileAdd = System.IO.Directory.GetParent(System.Environment.CurrentDirectory) + @"\57G.prn";
            string fileAdd = @"..\..\Trace_0171.csv";
            string alltext = File.ReadAllText(fileAdd);
            Regex rg1 = new Regex(",");
            Regex rg2 = new Regex(";");
            Regex rg3 = new Regex("\t");

            MatchCollection mc1 = rg1.Matches(alltext);
            MatchCollection mc2 = rg2.Matches(alltext);
            MatchCollection mc3 = rg3.Matches(alltext);
            int numofcomma = mc1.Count;
            int numofsemi = mc2.Count;
            int numotab = mc3.Count;

            int numofmainsym = numofcomma;
            char[] splitchar = { ',' };

            label4.Text = ",号" + numofcomma.ToString() + '\n';
            label4.Text += ";号" + numofsemi.ToString() + '\n';
            label4.Text += "Tab" + numotab.ToString() + '\n';

            if (numofmainsym < numofsemi)
            {
                numofmainsym = numofsemi;
                splitchar[0] = ';';
            }                
            if (numofmainsym < numotab)
            {
                numofmainsym = numotab;
                splitchar[0] = '\t';
            }

            int numofcolumn=0;
            string[] linesArray = File.ReadAllLines(fileAdd);
            int numoflines = linesArray.Length;
            string expdataforsplit = linesArray[numoflines - 5];
            string[] expspl = expdataforsplit.Split(splitchar);
            for(int i1=0; i1< expspl.Length; i1++)
            {
                if ("" != expspl[i1])
                    numofcolumn++;
            }

            label4.Text += "行数" + numoflines.ToString() + '\n';
            label4.Text += "分隔符" + splitchar[0].ToString() + '\n';
            label4.Text += "试验分割行" + expdataforsplit + '\n';
            label4.Text += "列数" + numofcolumn.ToString() + '\n';

            double[] col1 = new double[numoflines];
            double[] col2 = new double[numoflines];
            List<double> coli3 = new List<double>();
            List<double> coli4 = new List<double>();
            List<double> coli5 = new List<double>();
            List<double> coli6 = new List<double>();
            List<double> coli7 = new List<double>();
            bool headregion = true;

            double temp1 = 0;
            bool nextline = true;
            bool validdataregion = true;
            int headlinenum = 0;
            int validdatanum = 0;
            for (int i2=0; i2< numoflines; i2++)
            {
                string[] dataspl = linesArray[i2].Split(splitchar);
                if (headregion)
                {
                    if (dataspl.Length < numofcolumn)
                        continue;
                    for (int j2 = 0; j2 < numofcolumn; j2++)
                    {
                        try { temp1 = Convert.ToDouble(dataspl[j2]); }
                        catch
                        {
                            nextline = true;
                            break;
                        }
                        nextline = false;
                    }
                    if (nextline)
                        continue;
                    headregion = false;
                    headlinenum = i2;
                    i2--;
                }
                else
                {
                    for (int j2 = 0; j2 < numofcolumn; j2++)
                    {
                        try
                        {
                            temp1 = Convert.ToDouble(dataspl[j2]);
                            validdataregion = true;
                        }
                        catch
                        {
                            validdataregion = false;
                            break;                            
                        }

                        switch(j2)
                        {
                            case 0:
                                col1[i2 - headlinenum]= Convert.ToDouble(dataspl[j2]);
                                break;
                            case 1:
                                col2[i2 - headlinenum] = Convert.ToDouble(dataspl[j2]);
                                break;
                            case 2:
                                coli3.Add(Convert.ToDouble(dataspl[j2]));
                                break;
                            case 3:
                                coli4.Add(Convert.ToDouble(dataspl[j2]));
                                break;
                            case 4:
                                coli5.Add(Convert.ToDouble(dataspl[j2]));
                                break;
                            case 5:
                                coli6.Add(Convert.ToDouble(dataspl[j2]));
                                break;
                            case 6:
                                coli7.Add(Convert.ToDouble(dataspl[j2]));
                                break;
                            default:
                                break;
                        }
                    }

                    if (!validdataregion)
                        break;
                    validdatanum++;

                }
            
            }

            label4.Text += "标题行数" + headlinenum.ToString() + '\n';
            label4.Text += "数据行数" + validdatanum.ToString() + '\n';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //// 5s
            //StreamReader sr = new StreamReader(@"C:\Users\y689\Desktop\201601SOANoise\noisemeasure\Filethru.DAT");
            //string str;
            //while ((str = sr.ReadLine()) != null)
            //{
            //    label3.Text += str + '\n';
            //}
            double exp11 = 0;
            try
            {
                exp11 = Convert.ToDouble("1abs");
            }
            catch
            {
                MessageBox.Show("df");
            }
            label4.Text += exp11.ToString() + '\n';
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
