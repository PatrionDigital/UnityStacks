[System.Serializable]
public class JSONNFTHoldings
{
    public int limit;
    public int offset;
    public int total;
    public NonFungibleTokenHoldingWithTxId[] results;
}

[System.Serializable]
public class NonFungibleTokenHoldingWithTxId
{
    public string asset_identifier;
    public Nft_Transfer_Value value;
    public int block_height;
    public string tx_id;
}