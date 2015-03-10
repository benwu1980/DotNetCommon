using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Web;

namespace DotNetCommon.Test.Web
{
    public class QueryBuilderTest
    {
        public void CreateUri()
        {
            QueryBuilder qb = new QueryBuilder("http://we.wlstock.com/welcome.html?a=2&b=3");
            qb.AddQuery("c", "7");
            qb.RemoveQuery("a");
            Console.WriteLine(qb.URL);
            Console.WriteLine(qb.PathAndQuery);
        }
    }
}
