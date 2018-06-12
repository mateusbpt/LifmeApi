using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lifme.Security
{
    public class CryptographyService
    {

        public static string PasswordCryptograpy(string email, string password)
        {
            string passwordFormat = $"lifme_{email}__{password}_lifme";
            return ConvertToMD5(passwordFormat);
        }

        public static bool VerifyPassword(string email, string password, string userPassword)
        {
            return PasswordCryptograpy(email, password).Equals(userPassword);
        }

        private static string ConvertToMD5(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}


