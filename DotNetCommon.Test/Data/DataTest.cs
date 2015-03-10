using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using DotNetCommon.Data.Extension;

namespace DotNetCommon.Test.Data
{
     [TestFixture]
    public class DataTest
    {
         [Test]
         public void DataTableToList()
         {
             DataTable table = new DataTable();

            
             table.Columns.Add(new DataColumn("Id", typeof(int)));
             table.Columns.Add(new DataColumn("Name", typeof(string)));
             table.Columns.Add(new DataColumn("BirthDay", typeof(DateTime)));
             table.Columns.Add(new DataColumn("Memo", typeof(string)));
             table.Columns.Add(new DataColumn("Salary", typeof(decimal)));

             table.Columns.Add(new DataColumn("Audit", typeof(bool)));

            

             //  public bool Audit { get; set; }
       

             DataRow row = table.NewRow();
             row["id"] = 1;
             row["Name"] = "Apple";
             row["BirthDay"] = DateTime.Now;
             row["Memo"] = "test apple";
             row["Salary"] = 10.20;
             row["Audit"] = 1;
           

            

             table.Rows.Add(row);

            var list =  table.ToList<User>();

            Console.WriteLine(list.Count);
            Console.WriteLine(list.First().Name);

          //  Assert.AreEqual(list.Count, 1);
           // Console.Out.WriteLine(list.First().Name);
           
             
         }
    }
}
