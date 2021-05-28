namespace MaClassePA.Models.Configuration
{
    using System.Collections.Generic;

    public class AccountsModel
    {
        public Dictionary<string, string> Passwords { get; set; }
        public Dictionary<string, string> Permissions { get; set; }
    }
}
