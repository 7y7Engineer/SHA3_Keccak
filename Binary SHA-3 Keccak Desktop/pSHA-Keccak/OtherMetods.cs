using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Windows;

namespace pSHA_Keccak
{
    class OtherMetods
    {
        /// <summary>
        /// Перевод 16-строки в двоичную. Никаких доп. проверок нет, 
        /// т. к. входная стока всегда 16-ная (сформированный хеш) 
        /// </summary>
        /// <param name="input">Входная 16-ричная строка</param>
        /// <returns>Двоичная строка</returns>
        public static string HexToBin(string input)
        {
            string TempStr = "";
            string OutStr = "" ;
            char[] ch = input.ToUpper().ToCharArray();

            for (int i = 0; i < ch.Length; i++) 
            {
                TempStr = Convert.ToString((ch[i] > 57) ? ch[i] - 55 : ch[i] - 48, 2);
                for (int j = 0; j < TempStr.Length % 4; j++) TempStr = TempStr.Insert(0, "0");
                OutStr += TempStr;
            }
            return OutStr;
        }

        /// <summary>
        /// Перевод 2-ной строки в 16-ричную. 
        /// Внимание, здесь нет проверки на то, что входная строка действительно двоичная 
        /// </summary>
        /// <param name="input">Входная двоичная строка</param>
        /// <returns></returns>
        public static string BinToHex(string input)
        {
            string InStr = input;
            long aa = 0;
            string TempStr = "";
            string OutStr = "";
            string[] arr16 = new string[16] { "0000", "0001", "0010", "0011", "0100", 
                                              "0101", "0110", "0111", "1000", "1001", 
                                              "1010", "1011", "1100", "1101", "1110", "1111" };
            if (InStr.Length % 4 != 0)
                for (int k = 0; k < InStr.Length % 4; k++) InStr = InStr.Insert(0, "0");

            for (int i = 0; i < InStr.Length; i = i + 4)
           {
               TempStr = InStr.Substring(i, 4);
               for (UInt16 j = 0; j < arr16.Length; j++) if (TempStr == arr16[j]) { aa = j; break; }
               OutStr += Convert.ToString(aa, 16).ToUpper();
           }         
           return OutStr;
        }


    }
}
