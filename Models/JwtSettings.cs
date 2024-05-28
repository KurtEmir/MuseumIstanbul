using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MuseumIstanbul.Models
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
