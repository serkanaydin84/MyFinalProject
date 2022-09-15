using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Core.Entities.Security.Hashing
{
    public class HashingHelper
    {
        //bu method verilen bir password değerinin Hash ve Salt değerini oluşturuyor
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                //Encoding.UTF8.GetBytes(password) bu kod ile bir stringin byte karşılığını alıyoruz
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //bu method sonradan sisteme girmek isteyen kişinin password'un bizim veri kaynağımızdaki
        //Hash'le ilgili Salta göre eşleşip eşlemediğini verdiğimiz yerdir
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
