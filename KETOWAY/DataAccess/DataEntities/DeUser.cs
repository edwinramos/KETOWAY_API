using System;
using System.Collections.Generic;
using System.Text;

namespace KetoWay.DataAccess.DataEntities
{
    public class DeUser
    {
        public string UserCode { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string ImagePath { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender {
        H,
        M,
    }
}
