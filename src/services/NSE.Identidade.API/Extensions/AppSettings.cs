using System;
namespace NSE.Identidade.API.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissior { get; set; }
        public string ValidoEm { get; set; }
    }
}

