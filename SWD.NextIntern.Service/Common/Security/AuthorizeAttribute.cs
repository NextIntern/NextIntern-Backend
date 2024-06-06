namespace SWD.NextIntern.Service.Common.Security
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeAttribute : Attribute
    {
        public AuthorizeAttribute()
        {
            Roles = null!;
            Policy = null!;
        }

        public string Roles { get; set; }

        public string Policy { get; set; }
    }
}
