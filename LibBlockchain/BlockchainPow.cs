using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LibBlockchain
{
    public class BlockchainPow
    {
        private RNGCryptoServiceProvider rng;

        public BlockchainPow()
        {
            rng = new RNGCryptoServiceProvider();
        }

        public byte[] PoW(ulong difficulty, BlockchainEntry bce)
        {
            bool finish = false;

            while (!finish)
            {
                byte[] bytesToBeHashed = bce.HashPowEntry();
                byte[] hash = null;

                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(bytesToBeHashed);
                }

                ulong dif = BitConverter.ToUInt64(hash, 0);

                if (dif < difficulty)
                    return hash;
            }

            return null;
        }

        public byte[] PoW(int difficulty, BlockchainEntry bce)
        {
            bool finish = false;

            while (!finish)
            {
                byte[] bytesToBeHashed = bce.HashPowEntry();
                byte[] hash = null;

                using (SHA512 shaM = new SHA512Managed())
                {
                    hash = shaM.ComputeHash(bytesToBeHashed);
                }

                int clz = CountLeadingZeros(hash);

                if (clz == difficulty)
                    return hash;
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
