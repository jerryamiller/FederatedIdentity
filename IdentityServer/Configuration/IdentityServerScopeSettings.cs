using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IdentityServer3.Core.Models;

namespace IdentityServer.Configuration
{
    public class IdentityServerScopeSettings : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("enabled", IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public ScopeType Type
        {
            get { return (ScopeType)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("claims", IsRequired = true)]
        public string Claims
        {
            get { return this["claims"] as string; }
            set { this["claims"] = value; }
        }
    }

    [ConfigurationCollection(typeof(IdentityServerScopeSettings), AddItemName = "Scope", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class IdentityServerScopesConfigCollection : ConfigurationElementCollection
    {
        public IEnumerable<Scope> Get()
        {
            foreach (var key in BaseGetAllKeys())
            {
                var scopeSettings = (IdentityServerScopeSettings)BaseGet(key);
                yield return new Scope()
                {
                    Name = scopeSettings.Name,
                    Enabled = scopeSettings.Enabled,
                    Type = scopeSettings.Type,
                    Claims = scopeSettings.Claims.Split(';').Select(c=>new ScopeClaim(c)).ToList()
                };
            }
            foreach (var scope in StandardScopes.All)
            {
                yield return scope;
            }
        }
        public IdentityServerScopeSettings this[int index]
        {
            get { return (IdentityServerScopeSettings)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(IdentityServerScopeSettings serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new IdentityServerScopeSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IdentityServerScopeSettings)element).Name;
        }

        public void Remove(IdentityServerScopeSettings serviceConfig)
        {
            BaseRemove(serviceConfig.Name);
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