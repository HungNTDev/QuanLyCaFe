using DAL_QLCaFe;
using DTO_QLCaFe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BUS_QLCaFe
{
    public class BUS_Staff
    {
        private static BUS_Staff instance;

        public static BUS_Staff Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Staff();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private BUS_Staff() { }

        public bool AcceptLogin(DTO_Staff staff)
        {
            return DAL_Staff.Instance.AcceptLogin(staff);
        }

        public string Encryption(string MatKhau)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] Encrypt;
            UTF8Encoding Encode = new UTF8Encoding();
            Encrypt = MD5.ComputeHash(Encode.GetBytes(MatKhau));
            StringBuilder Encryptdata = new StringBuilder();
            for (int i = 0; i < Encrypt.Length; i++)
            {
                Encryptdata.Append(Encrypt[i].ToString());
            }
            return Encryptdata.ToString();
        }

        public string Verification()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, false));
            builder.Append(RandomNumber(0,9));
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(0, 9));
            builder.Append(RandomString(1, false));
            return builder.ToString();
        }

        public string VerificationPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(1, true));
            builder.Append(RandomNumber(0, 9));
            builder.Append(RandomNumber(0, 9));
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(0, 9));
            builder.Append(RandomString(1, true));
            return builder.ToString();
        }
        public string RandomString(int Size, bool LowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
                Thread.Sleep(10);
            }
            if (LowerCase)
            {
                return builder.ToString().ToLower();
            }
            return builder.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public DataTable Check(DTO_Staff staff)
        {
            return DAL_Staff.Instance.Check(staff);
        }

        public bool ForgotPassword(DTO_Staff staff) 
        {
            return DAL_Staff.Instance.ForgotPassword(staff);
        }

        public bool UpdatePassword(DTO_Staff staff)
        {
            return DAL_Staff.Instance.UpdatePassword(staff);
        }

        public DataTable ListProfileStaff()
        {
            return DAL_Staff.Instance.ListProfileStaff();
        }
        public DataTable ListProfileStaff_DGV()
        {
            return DAL_Staff.Instance.ListProfileStaff_DGV();
        }
    }
}
