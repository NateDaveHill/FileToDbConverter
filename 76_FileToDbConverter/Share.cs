using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _76_FileToDbConverter
{
    internal class Share
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double? First { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Final { get; set; }
        public double? Volume { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Updated { get; set; }
    }
}