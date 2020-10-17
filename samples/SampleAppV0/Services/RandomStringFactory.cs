using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppV1.Services
{
    public sealed class RandomStringFactory : IFactory<string>
    {
        public string Get()
        {
            var random = new Random();
            return new List<int>(30)
                .Select(n =>
                {
                    var randomNumber = random.Next(1, 400);
                    var @char = (char)randomNumber;

                    return @char;
                })
                .Aggregate(new StringBuilder(string.Empty), (acc, charItem) =>
                {
                    return acc.Append(charItem);
                })
                .ToString();
        }
    }
}
