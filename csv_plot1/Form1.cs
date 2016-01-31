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
using System.Drawing.Drawing2D;

namespace csv_plot1
{
    public partial class Form1 : Form
    {
        private Bitmap sBitmap = null;
        List<double> coli1 = new List<double>();
        List<double> coli2 = new List<double>();
        List<double> coli3 = new List<double>();
        List<double> coli4 = new List<double>();
        List<double> coli5 = new List<double>();
        List<double> coli6 = new List<double>();
        List<double> coli7 = new List<double>();
        int mousepositionx = 0;
        int mousepositiony = 0;

        public Form1()
        {
            InitializeComponent();
            this.sBitmap = (Bitmap)this.pictureBox1.Image;
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
            //string fileAdd = @"C:\Users\y689\Desktop\201601SOANoise\noisemeasure\Trace_0028.csv";
            //string fileAdd = System.IO.Directory.GetParent(System.Environment.CurrentDirectory) + @"\57G.prn";
            string fileAdd = @"..\..\A1550.9.csv";
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

            //double[] col1 = new double[numoflines];
            //double[] col2 = new double[numoflines];
            
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
                                //col1[i2 - headlinenum]= Convert.ToDouble(dataspl[j2]);
                                coli1.Add(Convert.ToDouble(dataspl[j2]));
                                break;
                            case 1:
                                //col2[i2 - headlinenum] = Convert.ToDouble(dataspl[j2]);
                                coli2.Add(Convert.ToDouble(dataspl[j2]));
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

            pictureBox1.Invalidate();

            //if (DrawData(coli1, coli2))
            //    figureprams.figexist = true;

            //coli1.Clear();
            //coli2.Clear();
            //coli3.Clear();
            //coli4.Clear();
            //coli5.Clear();
            //coli6.Clear();
            //coli7.Clear();


            //figureprams.RightMargin = figureprams.RightMargin + 5;
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
                //MessageBox.Show("df");
            }
            label4.Text += exp11.ToString() + '\n';
            Drawsn();
            figureprams.DownMargin = figureprams.DownMargin + 5;
        }

        private void Drawsn()
        {
            try
            {
                int num2;
                PointF tf;
                string str;
                Pen pen = new Pen(Color.Black, 1f);
                Graphics graphics = Graphics.FromImage(this.sBitmap);
                Font font = new Font("宋体", 9f);
                SolidBrush brush = new SolidBrush(Color.Black);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                for (num2 = 0; num2 <= 12; num2++)
                {
                    int num3 = (((((this.pictureBox1.Width - figureprams.LeftMargin) - figureprams.RightMargin) * num2) * 30) / Convert.ToInt32(360)) + figureprams.LeftMargin;
                    graphics.DrawLine(pen, num3, figureprams.TopMargin, num3, this.pictureBox1.Height - figureprams.DownMargin);
                    tf = new PointF((float)(num3 - 10), (float)((this.pictureBox1.Height - figureprams.DownMargin) + 5));
                    str = Convert.ToString((int)((num2 * 30) - 180));
                    graphics.DrawString(str, font, brush, tf);
                }
                for (num2 = 0; num2 <= 10; num2++)
                {
                    int num4 = (((((this.pictureBox1.Height - figureprams.TopMargin) - figureprams.DownMargin) * num2) * 10) / 100) + figureprams.TopMargin;
                    graphics.DrawLine(pen, figureprams.LeftMargin, num4, this.pictureBox1.Width - figureprams.RightMargin, num4);
                    tf = new PointF((float)(figureprams.LeftMargin - 0x1c), (float)(num4 - 7));
                    str = Convert.ToString(-(num2 * 10));
                    graphics.DrawString(str, font, brush, tf);
                }
                pen.Color = Color.Blue;
                pen.DashStyle = DashStyle.Dash;
                pen.DashPattern = new float[] { 6f, 6f };
                graphics.DrawLine(pen, figureprams.LeftMargin, ((((this.pictureBox1.Height - figureprams.TopMargin) - figureprams.DownMargin) * 3) / 100) + figureprams.TopMargin, this.pictureBox1.Width - figureprams.RightMargin, ((((this.pictureBox1.Height - figureprams.TopMargin) - figureprams.DownMargin) * 3) / 100) + figureprams.TopMargin);
                pen.Dispose();
                graphics.Dispose();

                //以下三者的效果是一样的，若都没有，则新图形不会立即显示，有机会刷新时会和之后改变的一并增量绘制
                //this.pictureBox1.Image = this.sBitmap;
                //pictureBox1.Invalidate();
                //pictureBox1.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Draws + " + exception.ToString());
            }
        }

        private void Drawsn2()
        {
            try
            {
                int num2;
                PointF tf;
                string str;
                Pen pen = new Pen(Color.Black, 1f);
                Graphics graphics = Graphics.FromImage(this.sBitmap);
                Font font = new Font("Arial", 15f);
                SolidBrush brush = new SolidBrush(Color.Black);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                for (num2 = 0; num2 <= 5; num2++)
                {
                    int num3 = (((((this.pictureBox1.Width - figureprams.LeftMargin) - figureprams.RightMargin) * num2) * 30) / Convert.ToInt32(360)) + figureprams.LeftMargin;
                    graphics.DrawLine(pen, num3, figureprams.TopMargin, num3, this.pictureBox1.Height - figureprams.DownMargin);
                    tf = new PointF((float)(num3 - 10), (float)((this.pictureBox1.Height - figureprams.DownMargin) + 5));
                    str = Convert.ToString((int)((num2 * 30) - 180));
                    graphics.DrawString(str, font, brush, tf);
                }
                for (num2 = 0; num2 <= 10; num2++)
                {
                    int num4 = (((((this.pictureBox1.Height - figureprams.TopMargin) - figureprams.DownMargin) * num2) * 10) / 100) + figureprams.TopMargin;
                    graphics.DrawLine(pen, figureprams.LeftMargin, num4, this.pictureBox1.Width - figureprams.RightMargin, num4);
                    tf = new PointF((float)(figureprams.LeftMargin - 0x1c), (float)(num4 - 7));
                    str = Convert.ToString(-(num2 * 10));
                    graphics.DrawString(str, font, brush, tf);
                }
                pen.Color = Color.Blue;
                pen.DashStyle = DashStyle.Dash;
                pen.DashPattern = new float[] { 6f, 6f };
                graphics.DrawLine(pen, figureprams.LeftMargin, ((((this.pictureBox1.Height - figureprams.TopMargin) - figureprams.DownMargin) * 3) / 100) + figureprams.TopMargin, this.pictureBox1.Width - figureprams.RightMargin, ((((this.pictureBox1.Height - figureprams.TopMargin) - figureprams.DownMargin) * 3) / 100) + figureprams.TopMargin);
                pen.Dispose();
                graphics.Dispose();

                //以下三者的效果是一样的，若都没有，则新图形不会立即显示
                //this.pictureBox1.Image = this.sBitmap;
                pictureBox1.Invalidate();
                //pictureBox1.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Draws + " + exception.ToString());
            }
        }

        private bool DrawData(List<double> colx, List<double> coly)
        {
            if(colx.Count!=coly.Count)
            {
                return false;
            }
            try
            {
                int num2;
                float num3;
                float num4;
                figureprams.xrange = (colx.Max() - colx.Min()) * figureprams.xexpandf;
                figureprams.yrange = (coly.Max() - coly.Min()) * figureprams.yexpandf;
                figureprams.xlinespace = figureprams.xrange / figureprams.numofxgrid;
                figureprams.ylinespace = figureprams.yrange / figureprams.numofygrid;
                figureprams.effpicxrange = this.pictureBox1.Width - figureprams.LeftMargin - figureprams.RightMargin;
                figureprams.effpicyrange = this.pictureBox1.Height - figureprams.TopMargin - figureprams.DownMargin;
                figureprams.picxmin = colx.Min() - (colx.Max() - colx.Min()) * (figureprams.xexpandf - 1) / 2;
                figureprams.picymax = coly.Max() + (coly.Max() - coly.Min()) * (figureprams.yexpandf - 1) / 2;

                PointF tf;
                string str;
                Pen penouter = new Pen(Color.Black, 2f);
                Pen peninter = new Pen(Color.Black, 1f);
                Pen pendata = new Pen(Color.Blue, 2f);
                peninter.DashStyle = DashStyle.Dash;
                peninter.DashPattern = new float[] { 6f, 6f };
                Graphics graphics = Graphics.FromImage(this.sBitmap);
                graphics.Clear(Color.White);
                Font font = new Font("Arial", 7.5f);
                SolidBrush brush = new SolidBrush(Color.Black);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                for (num2 = 0; num2 <= figureprams.numofxgrid; num2++)
                {
                    num3 = (float)(figureprams.effpicxrange / figureprams.xrange * num2 * figureprams.xlinespace + figureprams.LeftMargin);
                    if(0==num2 || figureprams.numofxgrid == num2)
                        graphics.DrawLine(penouter, num3, figureprams.TopMargin, num3, this.pictureBox1.Height - figureprams.DownMargin);
                    else
                        graphics.DrawLine(peninter, num3, figureprams.TopMargin, num3, this.pictureBox1.Height - figureprams.DownMargin);                    
                    tf = new PointF((float)(num3 - 35), (float)((this.pictureBox1.Height - figureprams.DownMargin) + 5));
                    str = (num2 * figureprams.xlinespace + figureprams.picxmin).ToString("E4");
                    graphics.DrawString(str, font, brush, tf);
                }
                for (num2 = 0; num2 <= figureprams.numofygrid; num2++)
                {
                    num4 = (float)((figureprams.effpicyrange / figureprams.yrange) * num2 * figureprams.ylinespace + figureprams.TopMargin);
                    if (0 == num2 || figureprams.numofygrid == num2)
                        graphics.DrawLine(penouter, figureprams.LeftMargin, num4, this.pictureBox1.Width - figureprams.RightMargin, num4);
                    else
                        graphics.DrawLine(peninter, figureprams.LeftMargin, num4, this.pictureBox1.Width - figureprams.RightMargin, num4);                   
                    tf = new PointF(0f, (float)(num4 - 7));
                    str = (-num2 * figureprams.ylinespace + figureprams.picymax).ToString("E4");
                    //str = Convert.ToString(-(num2 * 10));
                    graphics.DrawString(str, font, brush, tf);
                }

                List<PointF> dataCor = new List<PointF>();
                for (num2 = 0; num2 < colx.Count; num2++)
                {
                    dataCor.Add(new PointF(
                        (float)((colx[num2] - figureprams.picxmin) / figureprams.xrange * figureprams.effpicxrange + figureprams.LeftMargin),
                        (float)((figureprams.picymax - coly[num2]) / figureprams.yrange * figureprams.effpicyrange + figureprams.TopMargin)
                        ));
                }
                graphics.DrawLines(pendata, dataCor.ToArray());

                //float x1, x2, y1, y2;
                //x1 = (float)((colx[0] - figureprams.picxmin) / figureprams.xrange * figureprams.effpicxrange + figureprams.LeftMargin);
                //y1 = (float)((figureprams.picymax - coly[0]) / figureprams.yrange * figureprams.effpicyrange + figureprams.TopMargin);
                //for (num2 = 1; num2 < colx.Count; num2++)
                //{
                //    x2 = (float)((colx[num2] - figureprams.picxmin) / figureprams.xrange * figureprams.effpicxrange + figureprams.LeftMargin);
                //    y2 = (float)((figureprams.picymax - coly[num2]) / figureprams.yrange * figureprams.effpicyrange + figureprams.TopMargin);
                //    graphics.DrawLine(pendata, x1, y1, x2, y2);
                //    //System.Threading.Thread.Sleep(10);
                //    //pictureBox1.Invalidate();  //这种方式下Invalidate()不好用
                //    //this.pictureBox1.Image = this.sBitmap; //这种方式下Image = 不好用
                //    pictureBox1.Refresh(); //这种方式下仅Refresh()好用
                //    y1 = y2;
                //    x1 = x2;
                //}

                penouter.Dispose();
                peninter.Dispose();
                pendata.Dispose();
                graphics.Dispose();

                this.pictureBox1.Image = this.sBitmap;
                //sBitmapbak = sBitmap;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DrawData(List<double> colx, List<double> coly, PaintEventArgs e)
        {
            if (colx.Count != coly.Count)
            {
                return false;
            }
            try
            {
                int num2;
                float num3;
                float num4;
                figureprams.xrange = (colx.Max() - colx.Min()) * figureprams.xexpandf;
                figureprams.yrange = (coly.Max() - coly.Min()) * figureprams.yexpandf;
                figureprams.xlinespace = figureprams.xrange / figureprams.numofxgrid;
                figureprams.ylinespace = figureprams.yrange / figureprams.numofygrid;
                figureprams.effpicxrange = this.pictureBox1.Width - figureprams.LeftMargin - figureprams.RightMargin;
                figureprams.effpicyrange = this.pictureBox1.Height - figureprams.TopMargin - figureprams.DownMargin;
                figureprams.picxmin = colx.Min() - (colx.Max() - colx.Min()) * (figureprams.xexpandf - 1) / 2;
                figureprams.picymax = coly.Max() + (coly.Max() - coly.Min()) * (figureprams.yexpandf - 1) / 2;

                PointF tf;
                string str;
                Pen penouter = new Pen(Color.Black, 2f);
                Pen peninter = new Pen(Color.Black, 1f);

                Pen pendata = new Pen(Color.Blue, 2f);
                peninter.DashStyle = DashStyle.Dash;
                peninter.DashPattern = new float[] { 6f, 6f };

                Pen peninter2 = new Pen(Color.Gray, 1f);
                peninter2.DashStyle = DashStyle.Dash;
                peninter2.DashPattern = new float[] { 6f, 6f };

                Graphics graphics = e.Graphics;
                graphics.Clear(Color.White);
                Font font = new Font("Arial", 7.5f);
                SolidBrush brush = new SolidBrush(Color.Black);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                for (num2 = 0; num2 <= figureprams.numofxgrid; num2++)
                {
                    num3 = (float)(figureprams.effpicxrange / figureprams.xrange * num2 * figureprams.xlinespace + figureprams.LeftMargin);
                    if (0 == num2 || figureprams.numofxgrid == num2)
                        graphics.DrawLine(penouter, num3, figureprams.TopMargin, num3, this.pictureBox1.Height - figureprams.DownMargin);
                    else
                        graphics.DrawLine(peninter, num3, figureprams.TopMargin, num3, this.pictureBox1.Height - figureprams.DownMargin);
                    tf = new PointF((float)(num3 - 35), (float)((this.pictureBox1.Height - figureprams.DownMargin) + 5));
                    str = (num2 * figureprams.xlinespace + figureprams.picxmin).ToString("E4");
                    graphics.DrawString(str, font, brush, tf);
                }
                for (num2 = 0; num2 <= figureprams.numofygrid; num2++)
                {
                    num4 = (float)((figureprams.effpicyrange / figureprams.yrange) * num2 * figureprams.ylinespace + figureprams.TopMargin);
                    if (0 == num2 || figureprams.numofygrid == num2)
                        graphics.DrawLine(penouter, figureprams.LeftMargin, num4, this.pictureBox1.Width - figureprams.RightMargin, num4);
                    else
                        graphics.DrawLine(peninter, figureprams.LeftMargin, num4, this.pictureBox1.Width - figureprams.RightMargin, num4);
                    tf = new PointF(0f, (float)(num4 - 7));
                    str = (-num2 * figureprams.ylinespace + figureprams.picymax).ToString("E4");
                    //str = Convert.ToString(-(num2 * 10));
                    graphics.DrawString(str, font, brush, tf);
                }

                List<PointF> dataCor = new List<PointF>();
                for (num2 = 0; num2 < colx.Count; num2++)
                {
                    dataCor.Add(new PointF(
                        (float)((colx[num2] - figureprams.picxmin) / figureprams.xrange * figureprams.effpicxrange + figureprams.LeftMargin),
                        (float)((figureprams.picymax - coly[num2]) / figureprams.yrange * figureprams.effpicyrange + figureprams.TopMargin)
                        ));
                }
                graphics.DrawLines(pendata, dataCor.ToArray());

                //float x1, x2, y1, y2;
                //x1 = (float)((colx[0] - figureprams.picxmin) / figureprams.xrange * figureprams.effpicxrange + figureprams.LeftMargin);
                //y1 = (float)((figureprams.picymax - coly[0]) / figureprams.yrange * figureprams.effpicyrange + figureprams.TopMargin);
                //for (num2 = 1; num2 < colx.Count; num2++)
                //{
                //    x2 = (float)((colx[num2] - figureprams.picxmin) / figureprams.xrange * figureprams.effpicxrange + figureprams.LeftMargin);
                //    y2 = (float)((figureprams.picymax - coly[num2]) / figureprams.yrange * figureprams.effpicyrange + figureprams.TopMargin);
                //    graphics.DrawLine(pendata, x1, y1, x2, y2);
                //    //System.Threading.Thread.Sleep(10);
                //    //pictureBox1.Invalidate();  //这种方式下Invalidate()不好用
                //    //this.pictureBox1.Image = this.sBitmap; //这种方式下Image = 不好用
                //    pictureBox1.Refresh(); //这种方式下仅Refresh()好用
                //    y1 = y2;
                //    x1 = x2;
                //}

                graphics.DrawLine(peninter2, mousepositionx, figureprams.TopMargin, mousepositionx, this.pictureBox1.Height - figureprams.DownMargin);
                graphics.DrawLine(peninter2, figureprams.LeftMargin, mousepositiony, this.pictureBox1.Width - figureprams.RightMargin, mousepositiony);

                //penouter.Dispose();
                //peninter.Dispose();
                //peninter2.Dispose();
                //pendata.Dispose();
                ////graphics.Dispose();//集中绘图后，Dispose()会导致异常使无法绘图

                //this.pictureBox1.Image = this.sBitmap;
                //sBitmapbak = sBitmap;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(figureprams.figexist)
            {
                if(
                    e.Y > figureprams.TopMargin && 
                    e.Y < this.pictureBox1.Height - figureprams.DownMargin && 
                    e.X < this.pictureBox1.Width - figureprams.RightMargin && 
                    e.X > figureprams.LeftMargin
                    )
                {
                    mousepositionx = e.X;
                    mousepositiony = e.Y;
                    pictureBox1.Invalidate();









                    ////DrawData(coli1, coli2);
                    //Pen peninter2 = new Pen(Color.Gray, 1f);
                    //peninter2.DashStyle = DashStyle.Dash;
                    //peninter2.DashPattern = new float[] { 6f, 6f };
                    //Graphics graphics = Graphics.FromImage(this.sBitmap);

                    //////Region region = new Region(new Rectangle(figureprams.LeftMargin, figureprams.LeftMargin, figureprams.effpicxrange, figureprams.effpicyrange));
                    ////Graphics graphics = Graphics.FromImage(new Bitmap(pictureBox1.Width,pictureBox1.Height));
                    //////graphics.SetClip(region, CombineMode.Union);





                    //graphics.DrawLine(peninter2, e.X, figureprams.TopMargin, e.X, this.pictureBox1.Height - figureprams.DownMargin);
                    //graphics.DrawLine(peninter2, figureprams.LeftMargin, e.Y, this.pictureBox1.Width - figureprams.RightMargin, e.Y);

                    ////peninter.Dispose();                   
                    ////graphics.Dispose();

                    ////this.pictureBox1.Image = this.sBitmap;
                    ////pictureBox1.Refresh();

                    label5.Text =
                        "X: " +
                        ((e.X - figureprams.LeftMargin) / (float)figureprams.effpicxrange * figureprams.xrange + figureprams.picxmin).ToString("E5") +
                        "\nY: " +
                        (-(e.Y - figureprams.TopMargin) / (float)figureprams.effpicyrange * figureprams.yrange + figureprams.picymax).ToString("E5");
                }
                
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (DrawData(coli1, coli2, e))
                figureprams.figexist = true;
        }
    }
}
