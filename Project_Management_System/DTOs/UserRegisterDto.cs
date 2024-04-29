﻿namespace Project_Management_System.DTOs
{
    public class UserRegisterDto
    {
        public required string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string NIC { get; set; }

        //public string ProfilePictureLink { get; set; }
        public string UserCategoryType { get; set; }
        public string JobRoleType { get; set; }

    }
}
