using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Task_2._1._1.DAL.Model {
    class PersonalCard {
        public long Id { get; set; }
        public float? Discount { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
