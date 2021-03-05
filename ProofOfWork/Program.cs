using System;
using System.Text;
using LibBlockchain;
using System.Collections.Generic;

namespace ProofOfWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = GenerateTestBlockChain();
            
            BlockchainPow bp = new BlockchainPow();
            
            blockchain.Entries[10].POWHash = bp.PoW(21531652117561719, blockchain.Entries[10]);
            Console.WriteLine(GetStringFromHash(blockchain.Entries[10].POWHash));

            blockchain.Entries[10].POWHash = bp.PoW(2, blockchain.Entries[10]);
            Console.WriteLine(GetStringFromHash(blockchain.Entries[10].POWHash));
            Console.ReadLine();
        }

        private static Blockchain GenerateTestBlockChain()
        {
            Blockchain blockchain = new Blockchain();
            blockchain.Entries = new List<BlockchainEntry>();

            Guid[] guids =
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            BlockchainEntry[] blockchains =
            {
                new BlockchainEntry() { Id = guids[0], Data = "genesis", IsPOW = false, PrevId = null, Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[1], Data = "First block", IsPOW = false, PrevId = guids[0], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[2], Data = "Second block", IsPOW = false, PrevId = guids[1], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[3], Data = "Third block", IsPOW = false, PrevId = guids[2], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[4], Data = "Fourth block", IsPOW = false, PrevId = guids[3], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[5], Data = "Fifth block", IsPOW = false, PrevId = guids[4], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[6], Data = "Sixth block", IsPOW = false, PrevId = guids[5], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[7], Data = "Seventh block", IsPOW = false, PrevId = guids[6], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[8], Data = "Eighth block", IsPOW = false, PrevId = guids[7], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[9], Data = "Ninth block", IsPOW = false, PrevId = guids[8], Timestamp = DateTime.UtcNow },
                new BlockchainEntry() { Id = guids[10], Data = "ProofOfWork" , IsPOW = true, PrevId = guids[9], Timestamp = DateTime.UtcNow }
            };

            blockchain.Entries[10].Data = GetStringFromHash(blockchain.Entries[0].Hash) +
                    GetStringFromHash(blockchain.Entries[1].Hash) +
                    GetStringFromHash(blockchain.Entries[2].Hash) +
                    GetStringFromHash(blockchain.Entries[3].Hash) +
                    GetStringFromHash(blockchain.Entries[4].Hash) +
                    GetStringFromHash(blockchain.Entries[5].Hash) +
                    GetStringFromHash(blockchain.Entries[6].Hash) +
                    GetStringFromHash(blockchain.Entries[7].Hash) +
                    GetStringFromHash(blockchain.Entries[8].Hash) +
                    GetStringFromHash(blockchain.Entries[9].Hash);

            for (int i = 0; i < blockchains.Length; i++)
            {
                blockchains[i].Hash = blockchains[i].HashEntry();
            }

            blockchain.Entries.AddRange(blockchains);

            return blockchain;
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            foreach (byte t in hash)
                result.Append(t.ToString("X2"));
            return result.ToString();
        }
    }
}
