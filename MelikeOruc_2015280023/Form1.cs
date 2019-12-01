using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MelikeOruc_2015280023
{
    public partial class Form1 : Form
    {
        //tüm roma rakamları sadece kontrol için kullanacağım
        public List<string> data = new List<string>();
        public Form1()
        {
            InitializeComponent();
            fillDataForControl();
        }
        private void fillDataForControl()
        { 
            //kontrol amaçlı bütün roma rakamlarını tuttum
            for (int i = 1; i <= 3999; i++)
            {   
                data.Add(convertIntToRoman(i));
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n;
            // textboxa girilen değerin sayı olup olmama kontrol ettim
            bool isNumeric = Int32.TryParse(textBox2.Text, out n);
            //sayıyı roma rakamına çevirme
            if (isNumeric)
            {
                string sonuc = convertIntToRoman(n);
                if (sonuc != null)
                    label6.Text = sonuc;
                else
                    MessageBox.Show("Sayı aralık dışındadır");

            }
            else
            {
                MessageBox.Show("girdi bir sayı değil");

            }
            textBox2.Text = "";
        }

        /// <summary>
        /// verilen sayıyı basamaklarına ayırıp tanımladığım dizilerden roma rakamı karşılıklarına çevirdim
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string convertIntToRoman(int num)
        {
            string[] thArr = new string[] { "M", "MM", "MMM" };
            string[] huArr = new string[] { "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            string[] tenArr = new string[] { "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] oneArr = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
            StringBuilder sb = new StringBuilder();

            int thousand = 0;
            int hundred = 0;
            int ten = 0;
            int one = 0;
            if (num <= 0 || num > 3999) // sayı 1 ile 3999 arasından değil ise
            {
                return null;
            }
            else
            {
                if (num >= 1000)
                {
                    thousand = num / 1000;
                    hundred = (num - thousand * 1000) / 100;
                    ten = (num - thousand * 1000 - hundred * 100) / 10;
                    one = (num - thousand * 1000 - hundred * 100 - ten * 10);
                }
                else if (num >= 100)
                {
                    hundred = num / 100;
                    ten = (num - hundred * 100) / 10;
                    one = (num - hundred * 100 - ten * 10);
                }
                else if (num >= 10)
                {
                    ten = num / 10;
                    one = num - ten * 10;
                }
                else
                {
                    one = num;
                }

                

                if (thousand > 0)
                {
                    sb.Append(thArr[thousand - 1]);
                }
                if (hundred > 0)
                {
                    sb.Append(huArr[hundred - 1]);
                }
                if (ten > 0)
                {
                    sb.Append(tenArr[ten - 1]);
                }
                if (one > 0)
                {
                    sb.Append(oneArr[one - 1]);
                }
                return sb.ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //dışarıdan aldığım stringi bir roma rakamı mı diye oluşturduğum datadan kontrol ediyorum
            if (!data.Contains(textBox1.Text.ToUpper()))
            {
                MessageBox.Show("Gecersiz bir roma rakamı ifadesidir");

            }
            else
            {
                label2.Text = convertRomanToInt(textBox1.Text.ToUpper()).ToString();
            }
            textBox1.Text = "";

        }
        /// <summary>
        /// aldığım geçerli roma rakamının her harfinin konumuna göre roma rakamlarının toplama çıkarma
        /// kurallarını kullanarak sayıya çeviriyorum.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int convertRomanToInt(string s)
        {
            int result = 0;

            char prev = '?';

            for (int i = s.Length - 1; i >= 0; i--)
            {
                switch (s[i])
                {
                    case 'I':
                        if (prev == 'V' || prev == 'X')
                        {
                            result = result - 1;
                        }
                        else
                        {
                            result = result + 1;
                        }
                        prev = 'I';
                        break;
                    case 'V':
                        result = result + 5;
                        prev = 'V';
                        break;
                    case 'X':
                        if (prev == 'L' || prev == 'C')
                        {
                            result = result - 10;
                        }
                        else
                        {
                            result = result + 10;
                        }
                        prev = 'X';
                        break;
                    case 'L':
                        result = result + 50;
                        prev = 'L';
                        break;
                    case 'C':
                        if (prev == 'D' || prev == 'M')
                        {
                            result = result - 100;
                        }
                        else
                        {
                            result = result + 100;
                        }
                        prev = 'C';
                        break;
                    case 'D':
                        result = result + 500;
                        prev = 'D';
                        break;
                    case 'M':
                        result = result + 1000;
                        prev = 'M';
                        break;
                    default:
                        break;

                }
            }
            return result;
        }


    }
}
