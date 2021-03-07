using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace LibBlockchain
{
    public class MerkleTree
    {
        public string BuildMerkleRoot(List<string> merkelLeaves)
        {
            if (merkelLeaves == null || !merkelLeaves.Any())

                return string.Empty;

            if (merkelLeaves.Count() == 1)
            {
                return merkelLeaves.First();
            }

            if (merkelLeaves.Count() % 2 > 0)
            {
                merkelLeaves.Add(merkelLeaves.Last());
            }

            var merkleBranches = new List<string>();

            for (int i = 0; i < merkelLeaves.Count(); i += 2)
            {
                var leafPair = string.Concat(merkelLeaves[i], merkelLeaves[i + 1]);
                merkleBranches.Add(HashSHA512(HashSHA512(leafPair)));
            }
            return BuildMerkleRoot(merkleBranches);
        }

        private string HashSHA512(string data)
        {
            using (var sha512 = SHA512Managed.Create())
            {
                return ByteArrayToHex(sha512.ComputeHash(HexToByteArray(data)));
            }
        }

        private string ByteArrayToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        private byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
