using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShreeDattaInventory.Models
{
    public class ReelMaster
    {
        [DisplayName("GSM")]
        public long GSM { get; set; }
        [DisplayName("Mill Serial Number")]
        public int MillSerialNumber { get; set; }
        [DisplayName("Mill Name")]
        public string MillName { get; set; }
        [DisplayName("Reel Number")]
        public int ReelNumber { get; set; }
        [DisplayName("Weight")]
        public long Weight { get; set; }
        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }
    }
}
