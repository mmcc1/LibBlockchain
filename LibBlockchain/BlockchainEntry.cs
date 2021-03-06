using System;
using System.Security.Cryptography;
using System.Text;

namespace LibBlockchain
{
    public class BlockchainEntry
    {
        public Guid Id { get; set; }
        public Guid? PrevId { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
        public byte[] Hash { get; set; }
        public byte[] PreviousHash { get; set; }
        public bool IsPOW { get; set; }
        public byte[] POWHash { get; set; }
        public Guid? Nonce { get; set; }


        public void HashGenesisEntry()
        {
            string toBeHashed = Id.ToString() + Data.ToString() + Timestamp.ToString();
            byte[] bytesToBeHashed = Encoding.Unicode.GetBytes(toBeHashed);

            using (SHA512 shaM = new SHA512Managed())
            {
                Hash = shaM.ComputeHash(bytesToBeHashed);
            }
        }

        public void HashEntry(byte[] previousHash)
        {
            string toBeHashed = Id.ToString() + PrevId.ToString() + Data.ToString() + Timestamp.ToString() + previousHash.ToString();
            byte[] bytesToBeHashed = Encoding.Unicode.GetBytes(toBeHashed);

            using (SHA512 shaM = new SHA512Managed())
            {
                Hash = shaM.ComputeHash(bytesToBeHashed);
            }
        }

        public byte[] HashPowEntry(Guid newGuid)
        {
            string toBeHashed = Id.ToString() + PrevId.ToString() + Data.ToString() + Timestamp.ToString() + Hash.ToString() + newGuid;
            byte[] bytesToBeHashed = Encoding.Unicode.GetBytes(toBeHashed);

            using (SHA512 shaM = new SHA512Managed())
            {
                return shaM.ComputeHash(bytesToBeHashed);
            }
        }
    }
}
