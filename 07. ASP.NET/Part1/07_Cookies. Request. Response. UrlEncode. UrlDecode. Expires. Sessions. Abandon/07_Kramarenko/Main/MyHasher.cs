using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Main
{
	public static class MyHasher
	{
		public static string GetHash(string s)
		{
			string password = "h64k3n5ugb4y5bhuy";
			byte[] salt = Encoding.ASCII.GetBytes("hk43547jmom23");

			Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
			byte[] bk = key.GetBytes(16);

			HMACSHA512 hash = new HMACSHA512(bk);
			byte[] mes = Encoding.Unicode.GetBytes(s);
			hash.ComputeHash(mes);

			return Convert.ToBase64String(hash.Hash);
		}
	}
}