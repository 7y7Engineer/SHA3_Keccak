using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pSHA_Keccak
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           InitializeComponent();

           mWin.PreviewKeyDown += new KeyEventHandler(mWin_PreviewKeyDown);   // обработчик нажатия клавиши
           
            gBox_Hash.Header += "SHA-3 256";
        }

        /// <summary>
        /// Управление включением/выключением активности и очисткой 
        /// окна ввода "криптосоли" по флажку chBox_HMAC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chBox_HMAC_Checked(object sender, RoutedEventArgs e)
        {            
            textBox_HMAC.IsEnabled = true;
            buttClear_HMAC.IsEnabled = true;
        }

        private void chBox_HMAC_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox_HMAC.IsEnabled = false;
            buttClear_HMAC.IsEnabled = false;
        }
        
        private void buttClear_HMAC_Click(object sender, RoutedEventArgs e)
        {
            textBox_HMAC.Text = "";
        }
        
        /// <summary>
        /// Это переключение хеша из 16 с/с в 2 с/с и наоборот
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rB_HexHesh_Checked(object sender, RoutedEventArgs e)
        {
            tB_HashBox.Text = OtherMetods.HexToBin(tB_HashBox.Text);
        }

        private void rB_BinHesh_Checked(object sender, RoutedEventArgs e)
        {
            tB_HashBox.Text = OtherMetods.HexToBin(tB_HashBox.Text);
        }



        /// <summary>
        /// Очистка хеша
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClearHash_Click(object sender, RoutedEventArgs e)
        {
            tB_HashBox.Text = "";
        }

        private void btLoadText_Click(object sender, RoutedEventArgs e)
        {

        }

      


        /// <summary>
        /// Обработчик нажатия клавиш в программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mWin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    //btGoAll_Click(sender, e);
                    break;                
                case Key.Escape:
                    btClearHash_Click(sender, e);
                    break;
                default:
                    break;
            }          
        }



    }
}
