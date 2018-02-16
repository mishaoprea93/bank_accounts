using System;
namespace bank_accounts.Models{
    public class Record{
        public int RecordId{get;set;}
        public int? UserId{get;set;}
        public int balance{get;set;}
        public int record{get;set;}
        public DateTime created_at{get;set;}
        public DateTime updated_at{get;set;}

        public Record(int? uid){
            UserId=uid;
            balance=0;
            record=0;
            created_at=DateTime.Now;
            updated_at=DateTime.Now;
        }

        public Record(){
            
        }
    }
}