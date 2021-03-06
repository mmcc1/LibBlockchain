using System;
using System.Security.Cryptography;

namespace LibBlockchain
{
    public class BlockchainPow
    {
        private RNGCryptoServiceProvider rng;

        public BlockchainPow()
        {
            rng = new RNGCryptoServiceProvider();
        }

        public Tuple<byte[], Guid> PoW(ulong difficulty, BlockchainEntry bce)
        {
            bool finish = false;
            

            while (!finish)
            {
                Guid nonce = Guid.NewGuid();
                byte[] bytesToBeHashed = bce.HashPowEntry(nonce);
                byte[] hash = null;

                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(bytesToBeHashed);
                }

                ulong dif = BitConverter.ToUInt64(hash, 0);

                if (dif < difficulty)
                    return Tuple.Create(hash, nonce);
            }

            return null;
        }

        public Tuple<byte[], Guid> PoW(int difficulty, BlockchainEntry bce)
        {
            bool finish = false;
            

            while (!finish)
            {
                Guid nonce = Guid.NewGuid();
                byte[] bytesToBeHashed = bce.HashPowEntry(nonce);
                byte[] hash = null;

                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(bytesToBeHashed);
                }

                int clz = CountLeadingZeros(hash);

                if (clz >= difficulty)
                    return Tuple.Create(hash, nonce);
            }

            return null;
        }

        private int CountLeadingZeros(byte[] hash)
        {
            for (int i = 0; i < hash.Length; i++)
                if (hash[i] != 0)
                    return i;

            return -1;
        }
    }
}
