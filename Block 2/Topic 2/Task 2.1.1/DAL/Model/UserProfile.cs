using System;
using System.Collections.Generic;
using System.Text;

namespace Task_2._1._1.DAL.Model {
    class UserProfile {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public PersonalCard PersonalCard { get; set; }
    }
}
