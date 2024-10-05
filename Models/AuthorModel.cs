﻿using System.Text.Json.Serialization;

namespace WebApi8_Library.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [ JsonIgnore ]
        public ICollection<BookModel> Books { get; set; }
        public AuthorModel() { }
    }
}