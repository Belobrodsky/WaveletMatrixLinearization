using MathNet.Numerics;
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

namespace WaveletMatrixLinearization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //double[] A = {  1,2,3,4,5,6,7,8,9,10};
            //double[] B = {  1,2,3,4,5,6,7,8,9,10};

            //MessageBox.Show(Fit.Line(A,B).Item2 + " " + Fit.Line(A, B).Item1);
            int cc = 0;

            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {

                StreamWriter sw = new StreamWriter("lin_a_b.csv");
                double val;
                

                foreach (var item in openFileDialog1.FileNames)
                {
                    List<double> Y = new List<double>();
                    List<double> X = new List<double>();
                    int cnt = 0;

                    string line = "123";
                    StreamReader sr = new StreamReader(item);

                    if (checkBox2.Checked)
                    {
                        sr.ReadLine();
                    }

                    while (line!=null)
                    {
                        line = sr.ReadLine();

                        if (line!=null)
                        {
                            List<string> vals = line.Split(' ').Select(n => n.Trim()).ToList();
                            vals.RemoveAll(n=>n=="");


                            List<double> vald;

                            if (checkBox1.Checked)
                            {
                                 vald = new List<double>(vals.Select(n => double.Parse(n)* double.Parse(n)));
                            }
                            else  vald = new List<double>(vals.Select(n => double.Parse(n)));


                            val = vald.Average();
                            


                           // foreach (var item2 in vals)
                           // {
                           //    MessageBox.Show("_" + item2 + "_");
                           //}

                            cnt++;


                            if (checkBox3.Checked)
                            {
                                X.Add(Math.Log10(cnt));
                            }
                            else X.Add(cnt);




                            if (checkBox4.Checked)
                            {

                                if (val > 0)
                                {
                                    Y.Add(Math.Log10(val));
                                }
                                else X.RemoveAt(X.Count - 1);
                                
                            } else Y.Add(val);

                                                  //  X.Add(cnt);
                            // Y.Add(val);

                            //sw.WriteLine("{0};{1}", cnt, val);




                        }


                    }
                    double a = MathNet.Numerics.Fit.Line(X.ToArray(),Y.ToArray()).Item2;
                    double b = MathNet.Numerics.Fit.Line(X.ToArray(), Y.ToArray()).Item1;


                    //sw.WriteLine("МНК=");
                    sw.WriteLine("{0};{1};{2}",item,a,b);

                    cc++;



                    sr.Close();
                }




                sw.Close();



                MessageBox.Show("Все готово! Обработано " + cc.ToString() + " файла(ов)!");

            }



        }
    }
}
