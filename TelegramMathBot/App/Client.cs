using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Client
    {
        public ClientId ClientId { get; }
        public Client(long id)
        {
            ClientId = new ClientId(id);
        }
    }
}
