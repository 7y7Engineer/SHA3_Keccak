using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace pSHA_Keccak_Form_
{
    public partial class MainForm : Form
    {
        #region GlobalVar

        Stopwatch stw = new Stopwatch();        // для отсчета времени
        String ChoosePath = String.Empty;       // для пути к файлу в файловом режиме
        public static bool NoHex = true;        // Проверка шестнадцатеричной строки

        #endregion



        public MainForm()
        {
            InitializeComponent();

            this.gB_File.Height = 147; // Устанавливаем гроупбокс для контролов файлового режима
            
        }
        
        // ----------------------------------------------------------------------------------------------------------

        // Выход из программы!!!
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Управление активностью строки "соль..." 
        /// </summary>       
        private void chB_HMAC_CheckedChanged(object sender, EventArgs e)
        {
            this.tB_HMAC.Enabled = (this.chB_HMAC.Checked) ? true : false;
            this.Lb_SaveSalt.Enabled = this.tB_HMAC.Enabled;
            this.Lb_AddSalt.Enabled = this.tB_HMAC.Enabled;
            this.Lb_DelSalt.Enabled = this.tB_HMAC.Enabled;
            this.tB_HMAC.BackColor = (this.chB_HMAC.Checked) ? Color.LavenderBlush : Color.WhiteSmoke;
        }
        // ----------------------------------------------------------------------------------------------------------
        
        
        /// <summary>
        /// Загрузка файла в окно для хеширования
        /// </summary>
        private void pBox_LoadText_Click(object sender, EventArgs e)
        {
            rTextBox.Text = FileOperations.OpenFile("Все файлы (*.*)|*.*");
        }

        /// <summary>
        /// Сохранение хешируемого текста в файл
        /// </summary>
        private void pBox_SaveText_Click(object sender, EventArgs e)
        {
            FileOperations.SaveFile(rTextBox.Text, 
                                    "Все файлы (*.*)|*.*",
                                    "text_" + DateTime.Now.ToLocalTime().ToString().Replace('/', '-').Replace(':', '-').Replace('.', '-').Replace(' ', '_'), 
                                    "txt");
        }

        

        private void pBox_ClearHMAC_Click(object sender, EventArgs e)
        {
            tB_HMAC.Text = String.Empty;
        }

        private void pBox_ClearText_Click(object sender, EventArgs e)
        {
            rTextBox.Text = String.Empty;
        }

        private void pBox_ClearHash_Click(object sender, EventArgs e)
        {
            tBox_Hash.Text = String.Empty;
        }


        #region Get_SHA3_Hash

        /// <summary>
        /// Выбор необходимого алгоритма в завистимости от типа строки
        /// </summary>
        /// <param name="Text">Входные хешированные данные</param>
        /// <param name="NameSHA">Выбор алгоритма</param>
        /// <param name="Is_Text_Input">Проверка, что выбрана текстовая или 16-ричная строка</param>
        /// <returns></returns>
        public static string Get_SHA3_Hash(string Text, string NameSHA, bool Is_Text_Input)
        {
            if (Is_Text_Input)  // если текстовая строка
                switch (NameSHA)
                {
                    case "SHA3-512":
                        return SHA3.SHA3_512(Text);
                    case "SHA3-384":
                        return SHA3.SHA3_384(Text);
                    case "SHA3-256":
                        return SHA3.SHA3_256(Text);
                    case "SHA3-224":
                        return SHA3.SHA3_224(Text);
                }
            else                // если 16-ричная строка
                switch (NameSHA)
                {
                    case "SHA3-512":
                        return SHA3.SHA3_512_HEX(Text);
                    case "SHA3-384":
                        return SHA3.SHA3_384_HEX(Text);
                    case "SHA3-256":
                        return SHA3.SHA3_256_HEX(Text);
                    case "SHA3-224":
                        return SHA3.SHA3_224_HEX(Text);
                }
            return "Error!";
        }
        // Перегруженная функция для файлового режима
        public static string Get_SHA3_Hash(FileStream fs, string NameSHA)
        {
            switch (NameSHA)
            {
                case "SHA3-512":
                    return SHA3.SHA3_512(fs);
                case "SHA3-384":
                    return SHA3.SHA3_384(fs);
                case "SHA3-256":
                    return SHA3.SHA3_256(fs);
                case "SHA3-224":
                    return SHA3.SHA3_224(fs);
            }            
            return "Error!";
        }

        #endregion

        #region Get_HMAC_SHA3_Hash

        public static string Get_HMAC_SHA3_Hash(string Text, string NameSHA, bool Is_Text_Input, string Key)
        {
                        
            if (Is_Text_Input)      // если текстовая строка
                switch (NameSHA)
                {
                    case "SHA3-512":
                        return SHA3.SHA3_HMAC_512(Text, Key);
                    case "SHA3-384":
                        return SHA3.SHA3_HMAC_384(Text, Key);
                    case "SHA3-256":
                        return SHA3.SHA3_HMAC_256(Text, Key);
                    case "SHA3-224":
                        return SHA3.SHA3_HMAC_224(Text, Key);
                }
            else                    // если 16-ричная строка
                switch (NameSHA)
                {
                    case "SHA3-512":
                        return SHA3.SHA3_HMAC_512_HEX(Text, Key);
                    case "SHA3-384":
                        return SHA3.SHA3_HMAC_384_HEX(Text, Key);
                    case "SHA3-256":
                        return SHA3.SHA3_HMAC_256_HEX(Text, Key);
                    case "SHA3-224":
                        return SHA3.SHA3_HMAC_224_HEX(Text, Key);
                }
            return "Error!";
        }

        // Перегруженная функция для файлового режима
        public static string Get_HMAC_SHA3_Hash(FileStream fs, string NameSHA, string Key)
        {
            switch (NameSHA)
            {
                case "SHA3-512":
                    return SHA3.SHA3_HMAC_512(fs, Key);
                case "SHA3-384":
                    return SHA3.SHA3_HMAC_384(fs, Key);
                case "SHA3-256":
                    return SHA3.SHA3_HMAC_256(fs, Key);
                case "SHA3-224":
                    return SHA3.SHA3_HMAC_224(fs, Key);
            }
            return "Error!";
        }

        #endregion

        // Проверка, введена ли парольная фраза?
        public static bool Кey_not_empty(string Key)
        {
            if (!String.IsNullOrEmpty(Key))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Парольная фраза не задана! \nВведите парольную фразу и повторите хеширование",
                                "Ошибка получения парольной фразы!",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Определение хеша и возвращение его в строку + расчет времени
        /// </summary>
        /// <returns></returns>
        private string Calc_Hash()
        {
            string HashKey = null;
            string HashStr = null;
            
            stw.Start();                // Запускаем отсчет времени
            tSGlobalLabel.ForeColor = Color.Black;
            tSGlobalLabel.Text = "Идет процесс...";
            statStripHash.Refresh();    // обновляем строку состояния

            if (gB_Text.Visible)        // если текстовый режим

                if (!chB_HMAC.Checked)  // если не используется парольная фраза
                    HashStr = Get_SHA3_Hash(rTextBox.Text, tStripLabel_NameHash.Text, rB_InputText.Checked);
                else                    // если применяется парольная фраза
                    if (!Кey_not_empty(tB_HMAC.Text)) HashStr = String.Empty;
                    else
                    {
                        HashKey = Get_SHA3_Hash(tB_HMAC.Text, tStripLabel_NameHash.Text, true); // хешируем парольную фразу всегда как текст 
                        HashStr = Get_HMAC_SHA3_Hash(rTextBox.Text, tStripLabel_NameHash.Text, rB_InputText.Checked, HashKey);
                    }

            else                        // если графический режим

                if (!chB_HMAC.Checked)  // если не используется парольная фраза
                    HashStr = Calc_File_Hash();
                else                    // если применяется парольная фраза
                {
                    if (!Кey_not_empty(tB_HMAC.Text)) HashStr = String.Empty;
                    else
                    {
                        HashKey = Get_SHA3_Hash(tB_HMAC.Text, tStripLabel_NameHash.Text, true); // хешируем парольную фразу всегда как текст 
                        HashStr = Calc_File_Hash_with_key(HashKey);
                    }
                }

            stw.Stop();  // Останавливаем отсчет времени
            tStripLabel_TimeHash.Text = String.Format("{0} сек.", stw.Elapsed.TotalSeconds.ToString());
            stw.Reset(); // Сбрасываем временные параметры 

            return HashStr;
        }

        // Установление метки в строке статуса при сравнении хеша
        private void Hash_Compare(string hash1, string hash2)
        {

            if (hash1.ToUpper() != String.Empty)
                if (hash1.ToUpper() == hash2.ToUpper()) 
                {
                    tSGlobalLabel.ForeColor = Color.Green;
                    tSGlobalLabel.Text = "ХЕШ-СУММЫ СОВПАДАЮТ!";
                }
                else
                {
                    tSGlobalLabel.ForeColor = Color.Red;
                    tSGlobalLabel.Text = "ХЕШ-СУММЫ НЕ СОВПАДАЮТ!";
                }
            else
            {
                tSGlobalLabel.ForeColor = Color.Black;
                tSGlobalLabel.Text = "Хеш-сумма не задана";
            }
        }

        //=======================================================================================
        
        /// <summary>
        /// Расчитываем хеш
        /// </summary>
        private void bt_CalcHash_Click(object sender, EventArgs e)
        {
             string HexStr = null;
             bt_CalcHash.Enabled = false; // пока вычисления, кнопка не активна
             
             HexStr = Calc_Hash().ToUpper();  // поднимаем регистр для вывода на экран 
            
            if(rBt_HexHash.Checked) tBox_Hash.Text = HexStr;
            else tBox_Hash.Text = pSHA_Keccak.OtherMetods.HexToBin(HexStr);

            tSGlobalLabel.ForeColor = Color.Black;
            if (NoHex) tSGlobalLabel.Text = "Готово";
            else tSGlobalLabel.Text = "Введена не 16-ричная строка. Возвращена хеш-сумма пустой строки";
            //statStripHash.Refresh();    // обновляем строку состояния

            bt_CalcHash.Enabled = true; // возвращаем активность кнопки
        }
        
        //=======================================================================================

        /// <summary>
        /// Проверяем хеш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_CompareHash_Click(object sender, EventArgs e)
        {
            bt_CompareHash.Enabled = false; // пока вычисления, кнопка не активна
            if (rBt_HexHash.Checked) Hash_Compare(tBox_Hash.Text, Calc_Hash());
            else Hash_Compare(pSHA_Keccak.OtherMetods.BinToHex(tBox_Hash.Text), Calc_Hash());
            bt_CompareHash.Enabled = true; // возвращаем активность кнопки
        }

        //=======================================================================================

        
        private void rBt_HexHash_CheckedChanged(object sender, EventArgs e)
        {
            if(rBt_HexHash.Checked) tBox_Hash.Text = pSHA_Keccak.OtherMetods.BinToHex(tBox_Hash.Text);
        }

        private void rBt_BinHash_CheckedChanged(object sender, EventArgs e)
        {
            if (rBt_BinHash.Checked) tBox_Hash.Text = pSHA_Keccak.OtherMetods.HexToBin(tBox_Hash.Text);
        }

        #region Menu_mStripHash

        /// <summary>
        /// Операции с меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new About();
            frm.ShowDialog();
        }

        private void файлДляХешированияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gB_Text.Visible) pBox_LoadText_Click(sender, e);  // для текстового режима
            else pB_FileMode_Load_Click(sender, e);               // для файлового режима
        }       

        private void sHA3512ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sHA3512ToolStripMenuItem.Checked = true;
            sHA3384ToolStripMenuItem.Checked = false;
            sHA3256ToolStripMenuItem.Checked = false;
            sHA3224ToolStripMenuItem.Checked = false;
            tStripLabel_NameHash.Text = "SHA3-512";
        }

        private void sHA3384ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sHA3384ToolStripMenuItem.Checked = true;
            sHA3512ToolStripMenuItem.Checked = false;
            sHA3256ToolStripMenuItem.Checked = false;
            sHA3224ToolStripMenuItem.Checked = false;
            tStripLabel_NameHash.Text = "SHA3-384";

        }

        private void sHA3256ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sHA3256ToolStripMenuItem.Checked = true;
            sHA3512ToolStripMenuItem.Checked = false;
            sHA3384ToolStripMenuItem.Checked = false;
            sHA3224ToolStripMenuItem.Checked = false;
            tStripLabel_NameHash.Text = "SHA3-256";

        }

        private void sHA3224ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sHA3224ToolStripMenuItem.Checked = true;
            sHA3512ToolStripMenuItem.Checked = false;
            sHA3384ToolStripMenuItem.Checked = false;
            sHA3256ToolStripMenuItem.Checked = false;
            tStripLabel_NameHash.Text = "SHA3-224";

        }

        // Переход в текстовый режим
        private void TextR_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            хешируемыйТекстToolStripMenuItem.Enabled = true;
            TextR_ToolStripMenuItem.Checked = true;
            FileR_ToolStripMenuItem.Checked = false;
            gB_File.Visible = false;
            gB_Text.Visible = true;

        }

        // Переход в файловый режим
        private void FileR_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            хешируемыйТекстToolStripMenuItem.Enabled = false;
            FileR_ToolStripMenuItem.Checked = true;
            TextR_ToolStripMenuItem.Checked = false;
            gB_File.Visible = true;
            gB_Text.Visible = false;
        }

        #endregion

        // Вставить хеш из буфера обмена
        private void pBox_BuffHash_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText() == true) this.tBox_Hash.Text = Clipboard.GetText();
        }

        // Копировать текст из текстового блока
        private void pBox_CopyText_Click(object sender, EventArgs e)
        {
            try
            {
                string SBuff = "<null>";
                if (this.rTextBox.Text != "") SBuff = this.rTextBox.Text.Normalize();
                Clipboard.SetText(SBuff, TextDataFormat.Text);
                tSGlobalLabel.ForeColor = Color.Black;
                tSGlobalLabel.Text = "Тескст скопирован в буфер обмена";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно скопировать в буфер.\n" + ex.Message,
                                "Ошибка работы с буфером обмена!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Загрузка хеш-суммы
        private void pBox_LoadHash_Click(object sender, EventArgs e)
        {
            string StempLoad = FileOperations.OpenFile("Файл SHA3 (*.sh3)|*.sh3").ToUpper();
            if (rBt_BinHash.Checked) tBox_Hash.Text = pSHA_Keccak.OtherMetods.HexToBin(StempLoad);
            else tBox_Hash.Text = StempLoad;
        }

        // Запись в файл хеш-суммы
        private void pBox_SaveHash_Click(object sender, EventArgs e)
        {
            string StempSave;
            if (rBt_BinHash.Checked) StempSave = pSHA_Keccak.OtherMetods.BinToHex(tBox_Hash.Text);
            else StempSave = tBox_Hash.Text;
            FileOperations.SaveFile(StempSave,
                                    "Файл SHA3 (*.sh3)|*.sh3",
                                    "sha3_" + DateTime.Now.ToLocalTime().ToString().Replace('/', '-').Replace(':', '-').Replace('.', '-').Replace(' ', '_'),
                                    "sh3");
        }

        // Изменился текст, нужно пересчитать хеш-сумму
        private void rTextBox_TextChanged(object sender, EventArgs e)
        {
            tSGlobalLabel.ForeColor = Color.Black;
            tSGlobalLabel.Text = "Изменился исходный текст. Пересчитайте хеш-сумму";
        }

        private void tB_HMAC_TextChanged(object sender, EventArgs e)
        {
            tSGlobalLabel.ForeColor = Color.Black;
            tSGlobalLabel.Text = "Изменилась парольная фраза. Пересчитайте хеш-сумму";
        }   

        //-------------------------------------------------------------------------------------

        /// <summary>
        /// Управление кнопками парольной фразы
        /// </summary>  
        // Удаление криптосоли 
        private void Lb_DelSalt_Click(object sender, EventArgs e)   
        {
            tB_HMAC.Text = String.Empty;
        }

        // Сохранение в файл криптосоли
        private void Lb_SaveSalt_Click(object sender, EventArgs e)
        {
            FileOperations.SaveFile(tB_HMAC.Text,
                                    "Ключ SHA3 (*.ks3)|*.ks3",
                                    "key3_" + DateTime.Now.ToLocalTime().ToString().Replace('/', '-').Replace(':', '-').Replace('.', '-').Replace(' ', '_'),
                                    "ks3");
        }

        // Загрузка из файла криптосоли
        private void Lb_AddSalt_Click(object sender, EventArgs e)
        {
            tB_HMAC.Text = FileOperations.OpenFile("Ключ SHA3 (*.ks3)|*.ks3");
        }

        //-------------------------------------------------------------------------------------
        
        /// <summary>
        /// Загрузка пути к файлу в файловом режиме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pB_FileMode_Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                ChoosePath = OPF.FileName;
                tB_PathFile.Text = ChoosePath;
                FileInfo fileInfo = new FileInfo(ChoosePath);
                double len_in_MiBs = fileInfo.Length/1024/1024;
                if (len_in_MiBs > 10)
                    MessageBox.Show("Файл имеет большой размер и может хешироваться долго.\n(Ожидаемое время: не менее "
                                    + (Math.Truncate(len_in_MiBs * 0.41)).ToString() + " сек.)",
                                    "Предупреждение!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ПРоверка существования файла по файловому пути ChoosePath 
        private bool File_exist_check()
        {
            if (File.Exists(ChoosePath)) 
                return true;
            else
            {
                MessageBox.Show("Файл не найден.", "Ошибка работы с файловой системой!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Подсчет хеша у файла по пути ChoosePath
        private string Calc_File_Hash()
        {
            try
            {
                using (FileStream fs = File.OpenRead(ChoosePath))
                {
                    return Get_SHA3_Hash(fs, tStripLabel_NameHash.Text);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Файл не найден.\n" + exc.Message, "Ошибка работы с файловой системой!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
            }              
        }

        // Подсчет хеша с парольной фразой у файла по пути ChoosePath
        private string Calc_File_Hash_with_key(string Key)
        {
            try
            {
                using (FileStream fs = File.OpenRead(ChoosePath))
                {
                    return Get_HMAC_SHA3_Hash(fs, tStripLabel_NameHash.Text, Key);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Файл не найден.\n" + exc.Message, "Ошибка работы с файловой системой!",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
            }              
        }

        private void tStripLabel_NameHash_Click(object sender, EventArgs e)
        {

            switch (tStripLabel_NameHash.Text)
            {
                case "SHA3-512":
                    sHA3384ToolStripMenuItem_Click(sender, e); break;
                case "SHA3-384":
                    sHA3256ToolStripMenuItem_Click(sender, e); break;
                case "SHA3-256":
                    sHA3224ToolStripMenuItem_Click(sender, e); break;
                default: /*"SHA3-224"*/
                    sHA3512ToolStripMenuItem_Click(sender, e); break;
            }


        }

        private void tStripLabel_NameHash_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void tStripLabel_NameHash_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
       
    }
}
