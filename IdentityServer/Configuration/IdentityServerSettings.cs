using System.Configuration;

namespace IdentityServer.Configuration
{
    public class IdentityServerSettings : ConfigurationSection
    {
        // ReSharper disable once InconsistentNaming

        public static IdentityServerSettings Settings { get; } =
            ConfigurationManager.GetSection("IdentityServerSettings") as IdentityServerSettings;

        [ConfigurationProperty("Clients")]
        public IdentityServerClientsConfigCollection Clients
        {
            get { return base["Clients"] as IdentityServerClientsConfigCollection; }
        }

        [ConfigurationProperty("Scopes")]
        public IdentityServerScopesConfigCollection Scopes
        {
            get { return base["Scopes"] as IdentityServerScopesConfigCollection; }
        }
    }
}