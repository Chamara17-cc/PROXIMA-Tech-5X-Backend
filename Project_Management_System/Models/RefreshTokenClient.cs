﻿using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Models
{
    public class RefreshTokenClient
    {
        [Key]
        public int TokenId { get; set; }
        public string Token { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
