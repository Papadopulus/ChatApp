using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppDesktop.A52
{
    public class A52
    {
        public A52() { }

        static BitArray R1 = new BitArray(19);
        static BitArray R2 = new BitArray(22);
        static BitArray R3 = new BitArray(23);
        static BitArray R4 = new BitArray(17);



        private static bool OutBit()
        {
            bool endBits = R1[18] ^ R2[21] ^ R3[22];

            bool xorBits =
                endBits
                ^ Majority(R1[12], R1[14] ^ true, R1[15])
                ^ Majority(R2[9], R2[13], R2[16] ^ true)
                ^ Majority(R3[13] ^ true, R3[16], R3[18])
            ;
            return xorBits;

        }

        private static bool Majority(bool v1, bool v2, bool v3)
        {
            int sum = (v1 ? 1 : 0) + (v2 ? 1 : 0) + (v3 ? 1 : 0);
            return sum >= 2;
        }

        private static void ShiftR(BitArray R, Action firstBit)
        {
            for (int i = R.Length - 1; i > 0; i--)
            {
                R[i] = R[i - 1];
            }
            firstBit();

        }

        private static void Clock(bool clockAll)
        {
            bool clockingBit = Majority(R4[3], R4[7], R4[10]);
            if (clockingBit == R4[10] || clockAll == true)
            {
                ShiftR(R1, () => { R1[0] = R1[13] ^ R1[16] ^ R1[17] ^ R1[18]; });
            }
            if (clockingBit == R4[3] || clockAll == true)
            {
                ShiftR(R2, () => { R2[0] = R2[20] ^ R2[21]; });
            }
            if (clockingBit == R4[7] || clockAll == true)
            {
                ShiftR(R3, () => { R3[0] = R3[7] ^ R3[20] ^ R3[21] ^ R3[22]; });
            }
            ShiftR(R4, () => { R4[0] = R4[11] ^ R4[16]; });
        }
        public BitArray Run(string inputString, string keyString)
        {
            R1 = new BitArray(19);
            R2 = new BitArray(22);
            R3 = new BitArray(23);
            R4 = new BitArray(17);
            //string keyString = "1111111111111111111111111111111111111111111111111111111111111111";

            //bool[] keyArray = keyString.Select(c => c == '1').ToArray();
            //BitArray key = new(keyArray);
            byte[] bytes = Encoding.UTF8.GetBytes(keyString);
            BitArray key = new BitArray(bytes);

            string fString = "1010110111111011011001";
            bool[] fArray = fString.Select(c => c == '1').ToArray();
            BitArray f = new BitArray(fArray);

            for (int i = 0; i < key.Length; i++)
            {
                Clock(true);
                R1[0] = R1[0] ^ key[i];
                R2[0] = R2[0] ^ key[i];
                R3[0] = R3[0] ^ key[i];
                R4[0] = R4[0] ^ key[i];
            }
            for (int i = 0; i < f.Length; i++)
            {
                Clock(true);
                R1[0] = R1[0] ^ f[i];
                R2[0] = R2[0] ^ f[i];
                R3[0] = R3[0] ^ f[i];
                R4[0] = R4[0] ^ f[i];

            }
            R1[15] = true; R2[16] = true; R3[18] = true; R4[10] = true;

            //string inputString = "aleksa";

            BitArray inputBits = new BitArray(8 * inputString.Length);
            for (int i = 0; i < inputString.Length; i++)
            {
                string binaryRepresentation = Convert.ToString(inputString[i], 2).PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    inputBits[i * 8 + j] = binaryRepresentation[j] == '1';
                }
            }
            Console.WriteLine("Before encryption: " + ToBitString(inputBits));

            for (int i = 0; i < inputBits.Length; i++)
            {
                inputBits[i] ^= OutBit();
                Clock(false);

            }
            return inputBits;
        }
        public string NizUChar(string niz)
        {
            int duzina = niz.Length;
            char[] charArray = new char[duzina / 8];

            for (int i = 0; i < duzina; i += 8)
            {
                string osamBitova = niz.Substring(i, 8);
                int decimalniBroj = Convert.ToInt32(osamBitova, 2);
                char karakter = (char)decimalniBroj;
                charArray[i / 8] = karakter;
            }

            return new string(charArray);
        }
        public string ToBitString(BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
