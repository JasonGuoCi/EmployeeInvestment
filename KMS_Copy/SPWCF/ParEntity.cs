using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SPWCF
{
    [DataContract]
    public class ParEntity
    {
        [DataMember]
        public string listName { get; set; }
        [DataMember]
        public string itemValue { get; set; }
    }
}
