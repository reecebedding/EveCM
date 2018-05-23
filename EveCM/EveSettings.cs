using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM
{
    public class EveSettings
    {
        public string Agent { get; set; }
        public SSOConnection SSO { get; set; }
        public class SSOConnection
        {
            public string LoginHost { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string ResponseType { get; set; }
            public string CallBackPath { get; set; }
        }
    }
}
