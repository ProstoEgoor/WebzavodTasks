using System;
using System.Collections.Generic;
using System.Text;

namespace Task_2._1._1.DAL.Model {
    class Purchase {
        public long Id { get; set; }
        public long? CardId { get; set; }
        public ulong? PurchaseSum { get; set; }

        public PersonalCard PersonalCard { get; set; }
    }
}
