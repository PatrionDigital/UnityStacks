[System.Serializable]
public class JSONAccountTransactions
{
    public int limit;
    public int offset;
    public int total;
    public AddressTransactionWithTransfers[] results;
}

[System.Serializable]
public class AddressTransactionWithTransfers
{
    public Tx tx;
    public string stx_sent;
    public string stx_received;
    public Stx_Transfers[] stx_transfers;
    public Ft_Transfers[] ft_transfers;
    public Nft_Transfers[] nft_transfers;
}

[System.Serializable]
public class Tx
{
    public string tx_id;
    public string tx_type;
    public int nonce;
    public string fee_rate;
    public string sender_address;
    public bool sponsored;
    public string post_condition_mode;
    public string tx_status;
    public string block_hash;
    public int block_height;
    public int burn_block_time;
    public string burn_block_time_iso;
    public bool canonical;
    public bool is_unanchored;
    public string microblock_hash;
    public int microblock_sequence;
    public bool microblock_canonical;
    public int tx_index;
    public Tx_Result tx_result;
    public Post_Conditions[] post_conditions;
    public Contract_Call contract_call;
    public string[] events;                         // Look into events data format.
    public int event_count;
}
[System.Serializable]
public class Tx_Result
{
    public string hex;
    public string repr;
}
[System.Serializable]
public class Post_Conditions
{
    public string type;
    public string condition_code;
    public string amount;
    public Post_Conditions_Principal principal;
}
[System.Serializable]
public class Post_Conditions_Principal
{
    public string type_id;
    public string address;
}
[System.Serializable]
public class Contract_Call
{
    public string contract_id;
    public string function_name;
    public string function_signature;
    public Contract_Call_Function_Args[] function_args;
}
[System.Serializable]
public class Contract_Call_Function_Args
{
    public string hex;
    public string repr;
    public string name;
    public string type;
}
[System.Serializable]
public class Stx_Transfers
{
    public string amount;
    public string sender;
    public string recipient;
}
[System.Serializable]
public class Ft_Transfers
{
    public string amount;
    public string asset_identifier;
    public string sender;
    public string recipient;
}
[System.Serializable]
public class Nft_Transfers
{
    public string asset_identifier;
    public Nft_Transfer_Value value;
    public string sender;
    public string recipient;
}
[System.Serializable]
public class Nft_Transfer_Value
{
    public string hex;
    public string repr;
}
