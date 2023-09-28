﻿namespace TraversalCoreProje.Areas.Member.Models
{
    public class UserEditViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
