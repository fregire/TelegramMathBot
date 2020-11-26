using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.Domain
{
    public class ClientId
    {
        public long Id { get; }

        public ClientId(long id)
        {
            Id = id;
        }
    }
}
