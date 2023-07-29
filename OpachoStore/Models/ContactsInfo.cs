﻿using System.ComponentModel.DataAnnotations;

namespace OpachoStore.Models
{
    public class ContactsInfo
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
