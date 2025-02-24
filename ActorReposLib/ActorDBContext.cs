using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ActorReposLib
{
    public class ActorDBContext 
    {
        public static readonly string ConnectionStringLocal = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ActorDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    }
}
