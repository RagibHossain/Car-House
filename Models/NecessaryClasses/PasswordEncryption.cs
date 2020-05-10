using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_House.Models.NecessaryClasses
{
    public static class PasswordEncryption
    {
          public static void CreatePasswordHash(string password,out byte[] hash,out byte[] salt)
          {
             
            using( var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //ComputeHash function takes byte array as parameter .
            }
              
          }

        public static bool VerifyPasswordHash(string password,byte[] hash,byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               for(int i=0;i< computedHash.Length;i++)
                {

                    if (computedHash[i] != hash[i]) return false;
                }
            }
            return true;
        }
         
    }
}
