using MaquininhaTheos.Domain.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace MaquininhaTheos.Data.Repository
{
    public class ConnectionsRepository
    {
        private readonly Dictionary<string, Usuario> connections =
            new Dictionary<string, Usuario>();


        public void Add(string uniqueID, Usuario user)
        {
            if (!connections.ContainsKey(uniqueID))
                connections.Add(uniqueID, user);
        }

        public string GetUserId(long id)
        {
            return (from con in connections
                    where con.Value.Id == id
                    select con.Key).FirstOrDefault();
        }

        public List<Usuario> GetAllUser()
        {
            return (from con in connections
                    select con.Value
            ).ToList();
        }
    }
}
