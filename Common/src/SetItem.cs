using System;
using System.Runtime.Serialization;

namespace DNANET.Service
{
    [DataContract]
    [Serializable]
    public class SetItem
    {
        public SetItem()
        {
            this.Item = "";
            this.Data = null;
        }

        public SetItem(string item, object value)
        {
            this.Item = item;
            this.Data = value;
        }

        [DataMember(Name = "item")]
        public string Item { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public object Data { get; set; }
    }
}