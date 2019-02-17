using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SearchfightCore
{
    public class ConfigSearchSection : ConfigurationSection
    {

        [ConfigurationProperty("ConfigSearchs", IsRequired = true)]
        public ConfigSearchsCollection ConfigSearchs
        {
            get
            {
                return base["ConfigSearchs"] as ConfigSearchsCollection;
            }
        }

    }

    [ConfigurationCollection(typeof(ConfigSearch), AddItemName = "ConfigSearch")]
    public class ConfigSearchsCollection : ConfigurationElementCollection, IEnumerable<ConfigSearch>
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigSearch();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_ConfigSearch = element as ConfigSearch;
            if (l_ConfigSearch != null)
                return l_ConfigSearch.Key;
            else
                return null;
        }

        public ConfigSearch this[int index]
        {
            get
            {
                return BaseGet(index) as ConfigSearch;
            }
        }

        #region IEnumerable<ConfigSearch> Members

        IEnumerator<ConfigSearch> IEnumerable<ConfigSearch>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }

        #endregion
    }

    public class ConfigSearch : ConfigurationElement
    {

        [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return base["key"] as string;
            }
            set
            {
                base["key"] = value;
            }
        }
        [ConfigurationProperty("assembly",  IsRequired = true)]
        public string assembly
        {
            get
            {
                return base["assembly"] as string;
            }
            set
            {
                base["assembly"] = value;
            }
        }
        [ConfigurationProperty("assemblyClass",  IsRequired = true)]
        public string assemblyClass
        {
            get
            {
                return base["assemblyClass"] as string;
            }
            set
            {
                base["assemblyClass"] = value;
            }
        }

        [ConfigurationProperty("ConfigSearchParameters")]
        public ConfigSearchParametersCollection ConfigSearchParameters
        {
            get
            {
                return base["ConfigSearchParameters"] as ConfigSearchParametersCollection;
            }
        }

    }

    [ConfigurationCollection(typeof(ConfigSearchParameter), AddItemName = "ConfigSearchParameter")]
    public class ConfigSearchParametersCollection : ConfigurationElementCollection, IEnumerable<ConfigSearchParameter>
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigSearchParameter();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_ConfigSearch = element as ConfigSearchParameter;
            if (l_ConfigSearch != null)
                return l_ConfigSearch.Key;
            else
                return null;
        }

        public ConfigSearchParameter this[int index]
        {
            get
            {
                return BaseGet(index) as ConfigSearchParameter;
            }
        }

        #region IEnumerable<ConfigSearchParameter> Members

        IEnumerator<ConfigSearchParameter> IEnumerable<ConfigSearchParameter>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }

        #endregion
    }

    public class ConfigSearchParameter : ConfigurationElement
    {

        [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return base["key"] as string;
            }
            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return base["value"] as string;
            }
            set
            {
                base["value"] = value;
            }
        }

    }


}