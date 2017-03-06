using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IdentityServer3.Core.Models;

namespace IdentityServer.Configuration
{
    public class IdentityServerClientSettings : ConfigurationElement
    {
        [ConfigurationProperty("clientId", IsKey = true, IsRequired = true)]
        public string ClientId
        {
            get { return this["clientId"] as string; }
            set { this["clientId"] = value; }
        }

        [ConfigurationProperty("enabled", IsRequired = true)]
        public bool Enabled
        {
            get { return (bool) this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("clientName", IsRequired = true)]
        public string ClientName
        {
            get { return this["clientName"] as string; }
            set { this["clientName"] = value; }
        }

        [ConfigurationProperty("flow", IsRequired = true)]
        public Flows Flow
        {
            get { return (Flows) this["flow"]; }
            set { this["flow"] = value; }
        }

        [ConfigurationProperty("redirectUris", IsRequired = true)]
        public string RedirectUris
        {
            get { return this["redirectUris"] as string; }
            set { this["redirectUris"] = value; }
        }

        [ConfigurationProperty("allowAccessToAllScopes", IsRequired = true)]
        public bool AllowAccessToAllScopes
        {
            get { return (bool) this["allowAccessToAllScopes"]; }
            set { this["allowAccessToAllScopes"] = value; }
        }
    }

    [ConfigurationCollection(typeof(IdentityServerClientSettings), AddItemName = "Client", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class IdentityServerClientsConfigCollection : ConfigurationElementCollection
    {
        public IEnumerable<Client> Get()
        {
            foreach (var key in BaseGetAllKeys())
            {
                var clientSettings = (IdentityServerClientSettings) BaseGet(key);
                yield return new Client()
                {
                    ClientId = clientSettings.ClientId,
                    Enabled=clientSettings.Enabled,
                    ClientName = clientSettings.ClientName,
                    Flow=clientSettings.Flow,
                    RedirectUris = clientSettings.RedirectUris.Split(';').ToList(),
                    AllowAccessToAllScopes = clientSettings.AllowAccessToAllScopes
                };
            }
        }
        public IdentityServerClientSettings this[int index]
        {
            get { return (IdentityServerClientSettings)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(IdentityServerClientSettings serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new IdentityServerClientSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IdentityServerClientSettings)element).ClientId;
        }

        public void Remove(IdentityServerClientSettings serviceConfig)
        {
            BaseRemove(serviceConfig.ClientId);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(String name)
        {
            BaseRemove(name);
        }

    }
}