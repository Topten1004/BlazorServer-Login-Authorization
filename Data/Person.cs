using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorBaseApp.Data
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
