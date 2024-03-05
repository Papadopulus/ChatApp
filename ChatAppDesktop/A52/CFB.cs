using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppDesktop.A52
{
    public class CFB
    {
        private string _initializationVector;
        BitArray cipherText;
        public CFB() { }
        public CFB(int initializationVectorInt)
        {
            string s = Convert.ToString(initializationVectorInt, 2);
            s = s.PadLeft(8, '0');
            _initializationVector = s;
        }

        public string Encryption(string plainText, A52 a52, string key)
        {
            cipherText = new BitArray(plainText.Length * 8);
            BitArray plainTextBits = new BitArray(8 * plainText.Length);
            for (int i = 0; i < plainText.Length; i++)
            {
                string binaryRepresentation = Convert.ToString(plainText[i], 2).PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    plainTextBits[i * 8 + j] = binaryRepresentation[j] == '1';
                }
            }
            BitArray cipherToA52 = new BitArray(a52.Run(_initializationVector, key));
            for (int i = 0; i < 8; i++)
            {
                cipherText[i] = cipherToA52[i] ^ plainTextBits[i];

            }
            string ciperString = a52.ToBitString(cipherText);
            string cipherTextString = a52.NizUChar(ciperString);

            for (int i = 1; i < plainText.Length; i++)
            {
                cipherToA52 = new BitArray(a52.Run(cipherTextString, key));
                for (int j = i * 8; j < i * 8 + 8; j++)
                {
                    cipherText[j] = cipherToA52[j] ^ plainTextBits[j];
                }
                ciperString = a52.ToBitString(cipherText);
                cipherTextString = a52.NizUChar(ciperString);
            }
            return cipherTextString;
        }
        public string Decription(string cipherTextInput, A52 a52, string key)
        {
            BitArray plainText = new BitArray(cipherTextInput.Length * 8);
            BitArray cipherTextInputBits = new BitArray(8 * cipherTextInput.Length);
            for (int i = 0; i < cipherTextInput.Length; i++)
            {
                string binaryRepresentation = Convert.ToString(cipherTextInput[i], 2).PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    cipherTextInputBits[i * 8 + j] = binaryRepresentation[j] == '1';
                }
            }
            BitArray plainToA52 = new BitArray(a52.Run(_initializationVector, key));
            for (int i = 0; i < 8; i++)
            {
                plainText[i] = plainToA52[i] ^ cipherTextInputBits[i];

            }
            string plainString = a52.ToBitString(plainText);
            string plainTextString = a52.NizUChar(plainString);
            for (int i = 1; i < cipherTextInput.Length; i++)
            {
                plainToA52 = new BitArray(a52.Run(plainTextString, key));
                for (int j = i * 8; j < i * 8 + 8; j++)
                {
                    plainText[j] = plainToA52[j] ^ cipherTextInputBits[j];
                }
                plainString = a52.ToBitString(plainText);
                plainTextString = a52.NizUChar(plainString);
            }
            return plainTextString;
        }

    }
}
