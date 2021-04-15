namespace MaClassePA.Models.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountsModel
    {
        public Dictionary<string, string> Passwords { get; set; }
        public Dictionary<string, string> Permissions { get; set; }
    }
}
