﻿namespace Joboard.Entities.Customer
{
    public class UserRole 
    {
        public int User_Id { get; set; }
        public User User { get; set; }

        public int Role_Id { get; set; }
        public Role Role { get; set; }
    }
}
