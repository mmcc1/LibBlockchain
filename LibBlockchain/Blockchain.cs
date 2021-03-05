using System;
using System.Collections.Generic;
using System.Text;

namespace LibBlockchain
{
    public class Blockchain
    {
        public Guid Id { get; set; }
        public List<BlockchainEntry> Entries { get; set; }
        public DateTime Timestamp { get; set; }
        public string Hash { get; set; }
    }
}
