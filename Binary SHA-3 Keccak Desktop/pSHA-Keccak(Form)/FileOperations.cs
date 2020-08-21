using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace pSHA_Keccak_Form_
{
    class FileOperations
    {
        // Имя папки для загрузки и сохранения файлов
        const string NameDir = "Data";

        /// <summary>
        /// Общий метод открытия файла для записи текста
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public static string OpenFile(string Filter)
        {
            string OutPut = "";
            Stream myStream = null;
            
            OpenFileDialog openFileD = new OpenFileDialog();

            string pathString = Path.Combine(Application.StartupPath, NameDir);
            Directory.CreateDirectory(pathString);
            openFileD.InitialDirectory = pathString;

            //openFileD.Filter = "Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileD.Filter = Filter;
            openFileD.RestoreDirectory = true;

            if (openFileD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileD.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            StreamReader StreamR = File.OpenText(openFileD.FileName);
                            //String[] str = StreamR.ReadToEnd().Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                            OutPut = StreamR.ReadToEnd();
                            /*rTextBox.Text = str[0];
                            if (rTextBox.Visible && str.Length > 1) rTextBox.Text = str[1];*/
                            StreamR.Dispose(); // StreamR.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно прочитать файл с диска. " + ex.Message,
                                    "Ошибка чтения с диска!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return OutPut;
        }

        /// <summary>
        /// Cохранение информации в файл
        /// </summary>
        /// <param name="Input">Строка или строковое поле, которое нужно сохранить</param>
        /// <param name="Filter">Фильтр для окна "Сохранить"</param>
        /// <param name="FileName">Имя генерируемого файла для сохранения</param>
        /// <param name="Ext">Расширение файла</param>
        public static void SaveFile(string Input, string Filter, string FileName, string Ext)
        {
            SaveFileDialog saveFDialog = new SaveFileDialog();
            string ReadResFilePlace = null; // путь к файлу с ответом
            //saveFDialog.Filter = "Текстовый файл (*.txt)|*.txt|All files (*.*)|*.*";
            saveFDialog.Filter = Filter;
            //saveFDialog.FileName = "result_" + DateTime.Now.ToLocalTime().ToString().Replace(':', '-').Replace('.', '-').Replace(' ', '_');
            saveFDialog.FileName = FileName;
            //saveFDialog.DefaultExt = "txt";
            saveFDialog.DefaultExt = Ext;
            string pathString = Path.Combine(Application.StartupPath, NameDir);
            Directory.CreateDirectory(pathString);
            saveFDialog.InitialDirectory = pathString;

            if (saveFDialog.ShowDialog() == DialogResult.OK)
            {
                ReadResFilePlace = saveFDialog.FileName; //считываем путь к файлу с результатом
                try
                {
                    if (ReadResFilePlace != String.Empty)
                    {
                        using (StreamWriter sw = new StreamWriter(ReadResFilePlace))
                        {
                            sw.Write(Input);
                            sw.Dispose();
                            // отмечаем состояние
                            /*tlslbStatus.Text = "Информация о формате " + tabControl_Format.SelectedTab.Text + " записана в файл " + System.IO.Path.GetFileName(saveFDialog.FileName);
                            statusStrip.Refresh();*/
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно записать файл на диск. " + ex.Message,
                                    "Ошибка чтения с диска!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }



    }
}
