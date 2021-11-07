using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
namespace Microchain
{
    public class Microchain
    {

        List<Block> Chain = new List<Block>();
        private String fileName = null;

        private const String FirstHash = "TITFBOMBC";
        public Block[] GetBlocks()
        {
            return Chain.ToArray();
        }
        private string HashString(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }


        public String getHash(Block block)
        {
            String serializedBlock = JsonSerializer.Serialize(block);
            String hash = HashString(serializedBlock);
            return hash;
        }

        public Block createBlock(Object content)
        {
            int newId =  (Chain.Count >= 1?  Chain[Chain.Count - 1].id + 1  : 0);
            String passHash =  (Chain.Count >= 1? getHash(Chain[Chain.Count - 1]) : HashString(FirstHash)); //This Is The Fitst Block Of My BlockChain
            Block block = new Block(newId, passHash, content);
            Chain.Add(block);
            return block;
        }

        public void LoadFromFile(String FileName){
            if (!File.Exists(FileName)) throw new FileNotFoundException();
            String data = File.ReadAllText(FileName);
            Chain = JsonSerializer.Deserialize<List<Block>>(data);
            if (!check()) {
                Chain = new List<Block>();
                throw new Exception("Violation of integrity");
            }
            this.fileName = FileName;   
        }
        public void SaveToFile(String FileName = null){
            if (!check()) throw new Exception("Violation of integrity");
            String serializedBlock = JsonSerializer.Serialize(Chain);
            File.WriteAllText(FileName,serializedBlock);
        }
        public Boolean check()
        {
            Boolean result = true;
            String prevHash = HashString(FirstHash);
            foreach (Block block in Chain)
            {
                if (block.hash != prevHash)
                {
                    result = false;
                    break;
                }
                prevHash = getHash(block);
            }
            return result;
        }
    }
}