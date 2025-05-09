namespace BlockchainNet.Model;

public class Blockchain
{
    public List<Block> Chain { get; set; }

    public Blockchain()
    {
        Chain = new List<Block> { CreateGenesisBlock() };
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, new List<Transaction>(), "0");
    }

    public Block GetLatestBlock() => Chain.Last();

    public void AddBlock(Block newBlock)
    {
        newBlock.PreviousHash = GetLatestBlock().Hash;
        newBlock.Hash = newBlock.CalculateHash();
        Chain.Add(newBlock);
    }

    public bool VerifyChain()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            var currentBlock = Chain[i];
            var previousBlock = Chain[i - 1];
            
            if (currentBlock.Hash != currentBlock.CalculateHash())
            {
                return false;
            }

            if (currentBlock.PreviousHash != previousBlock.Hash)
            {
                return false;
            }
        }
        return true;
    }
};