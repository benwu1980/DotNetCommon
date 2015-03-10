using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Test
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Memo { get; set; }
        public decimal Salary { get; set; }
        public bool Audit { get; set; }
        public bool IsStudent { get; set; }
    }

    public class Handler
    {
        public Handler()
        {

        }

        public string Message { get; set; }

        public void DoWork()
        {
            Console.WriteLine(Message);
            Console.WriteLine(GetHashCode());
 
        }
    }
}
