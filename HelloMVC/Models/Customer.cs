using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloMVC.Models
{
    public class Customer
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อ")]
        [StringLength(10, ErrorMessage = "ห้ามเกิน 10 ตัว")]
        [DisplayName("Enter your name")]
        public string Name { get; set; }


        public string Telephone { get; set; }
    }
}