using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace bank_accounts.Models{
    public class User{
        public int UserId{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        
        public string Email{get;set;}
        public string Password{get;set;}
        public List<Record> records { get; set; }

        public User(){
            records=new List<Record>();
        }
    }
}
