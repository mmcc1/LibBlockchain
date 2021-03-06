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
            //Create a test blockchain
            Blockchain blockchain = GenerateTestBlockChain();
            
            //Do Proof of Work
            BlockchainPow bp = new BlockchainPow();

            //Proof of work - ulong - lower ulong equals increased difficulty
            Tuple<byte[], Guid> tp = bp.PoW(ulong.MaxValue, blockchain.Entries[10]);
            blockchain.Entries[10].POWHash = tp.Item1;
            blockchain.Entries[10].Nonce = tp.Item2;

            Console.WriteLine(GetStringFromHash(blockchain.Entries[10].POWHash));

            //Proof of work - int - higher int equals increased difficulty
            Tuple<byte[], Guid> tp2 = bp.PoW(2, blockchain.Entries[10]);
            blockchain.Entries[10].POWHash = tp2.Item1;
            blockchain.Entries[10].Nonce = tp2.Item2;

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

            //Create a blockchain
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

            blockchain.Entries.AddRange(blockchains);

            //Generate hash for genesis block
            blockchain.Entries[0].HashGenesisEntry();


            List<string> hashes = new List<string>();
            hashes.Add(GetStringFromHash(blockchain.Entries[0].Hash));

            //Generate hashes of blocks 1-9
            for (int i = 1; i < blockchain.Entries.Count - 1; i++)
            {
                blockchain.Entries[i].HashEntry(blockchain.Entries[i - 1].Hash);
                hashes.Add(GetStringFromHash(blockchain.Entries[i].Hash));
            }

            //Create Merkle tree and store result
            MerkleTree mt = new MerkleTree();
            blockchain.Entries[10].Data = mt.BuildMerkleRoot(hashes);

            //Add hash for PoW block
            blockchain.Entries[10].HashEntry(blockchain.Entries[9].Hash);

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
