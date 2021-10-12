using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void renameIt_Click(object sender, EventArgs e)
        {
            //с помощью диалога выбираем папку с файлами
            DialogResult result = folderBrowserDialog1.ShowDialog();

            Encoding win1252  = Encoding.GetEncoding(1252); 
            Encoding win1251 = Encoding.GetEncoding(1251);

            // ýëåêòðèôèêàöèÿ

            if (result == DialogResult.OK)
            {
                //Получить все файлы в папке
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                int ind = 0;
                for (int i = 0; i < files.Length; i++)
                {
                    //поиск индекса последнего слеша
                    ind = files[i].LastIndexOf('\\');
                    //переименование
                    string FileName = Path.GetFileNameWithoutExtension(files[i]);

                    byte[] win1252Bytes  = win1252.GetBytes(FileName);

                    // ну попробуем - взлетит или всплывет
                    byte[] win1251Bytes = Encoding.Convert(win1252, win1251, win1252Bytes);

                    char[] win1251Chars = new char[win1251.GetCharCount(win1251Bytes, 0, win1251Bytes.Length)];
                    win1251.GetChars(win1251Bytes, 0, win1251Bytes.Length, win1251Chars, 0);
                    string win1251String = new string(win1251Chars);

                    File.Move(files[i], files[i].Remove(ind + 1) + win1251String + ".txt");
                }
                MessageBox.Show("Готово!");
            }
        }
    }
}
