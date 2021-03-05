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
        public bool IsPOW { get; set; }
        public byte[] POWHash { get; set; }


        public byte[] HashEntry()
        {
            string toBeHashed = Id.ToString() + PrevId.ToString() + Data.ToString() + Timestamp.ToString();
            byte[] bytesToBeHashed = Encoding.Unicode.GetBytes(toBeHashed);

            using (SHA512 shaM = new SHA512Managed())
            {
                return shaM.ComputeHash(bytesToBeHashed);
            }
        }

        public byte[] HashPowEntry()
        {
            string toBeHashed = Id.ToString() + PrevId.ToString() + Data.ToString() + Timestamp.ToString() + Hash.ToString() + Guid.NewGuid();
            byte[] bytesToBeHashed = Encoding.Unicode.GetBytes(toBeHashed);

            using (SHA512 shaM = new SHA512Managed())
            {
                return shaM.ComputeHash(bytesToBeHashed);
            }
        }
    }
}
