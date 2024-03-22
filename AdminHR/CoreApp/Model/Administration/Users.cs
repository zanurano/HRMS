﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Model.Administration
{
    [Table("Users")]
    public class Users
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
