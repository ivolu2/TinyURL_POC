using System;
using TinyURL_POC.Contracts;

namespace TinyURL_POC
{
	public class DefaultTokenGenerator : ITokenGenerator
	{
		private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		private readonly Random random = new Random();

		public string GenerateToken()
		{
			return new string(Enumerable.Repeat(Characters, 6).Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}

