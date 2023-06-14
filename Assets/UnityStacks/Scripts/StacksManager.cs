using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;

public class StacksManager : MonoBehaviour
{
    // Parameters
    [Header("Wallets")]
    public string walletStacks;             // Used as "principal" in API queries.

    public enum NetworkSetting
    {
        mainnet,
        testnet,
        devnet
    }
    [Header("Network")]
    // Fix these to the new Hiro API endpoints
    public NetworkSetting networkSetting = NetworkSetting.mainnet;
    private string networkAddressMain = "https://api.mainnet.hiro.so";
    private string networkAddressTest = "https://api.testnet.hiro.so";
    private string networkAddressDev = "http://localhost:3999/";
    [SerializeField]
    private string networkAddressUse;       // The Network Address to use as API endpoint

    [Header("NFT Query Params")]
    public string fetchContract = "";       // Address of NFT contract to filter for.
    public int fetchNum = 50;               // Max number of tokens to fetch. Default 50
    public int fetchIndex = 0;              // Offset. Index of first tokens to fetch. Default 0
    public bool fetchMetadata = false;      // Whether or not to fetch txn metadata. Default False

    [Header("Stacks Data Objects")]
    // JSON Data Objects
    [SerializeReference]
    JSONAccountSTXBalance m_AccountSTXBalance;
    [SerializeReference]
    JSONNFTHoldings m_NFTHoldings;
    [SerializeReference]
    JSONNFTMetadata m_NFTMetadata;

    private StacksAPICaller stacksAPICaller;

    // Stacks API Query Function Callbacks
    public delegate void GetStacksAccountCallback(string address);
    static GetStacksAccountCallback stacksAccountCallback;

    public delegate void GetStxBalanceCallback<TResultType>(TResultType result);
    static GetStxBalanceCallback<JSONAccountSTXBalance> stxBalanceCallback;

    public delegate void GetNFTHoldingsCallback<TResultType>(TResultType result);
    static GetNFTHoldingsCallback<JSONNFTHoldings> nftHoldingsCallback;

    public delegate void GetNFTMetadataCallback<TResultType>(TResultType result);
    static GetNFTMetadataCallback<JSONNFTMetadata> nftMetadataCallback;

    public static StacksManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static StacksManager instance;

    private void Awake()
    {
        if (instance != this)
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (!stacksAPICaller)
        {
            stacksAPICaller = gameObject.AddComponent<StacksAPICaller>();
        }

        // Subscribe Internal Delegates

        // Ensure the GameObject is always named "StacksManager"
        // Otherwise ConnectOrdinals plugin will not work
        if (gameObject.name != "StacksManager")
            gameObject.name = "StacksManager";
    }

    private void OnDisable()
    {
        // De-subscribe Internal Delegates
    }

    public void GetStacksAccount()
    {
        try
        {
            ConnectStacks.ConnectToStacksWallet();
        }
        catch (Exception e)
        {
            Debug.LogError("StacksManager: Failed to get Stacks Account: " + e.Message);
        }
    }

    public void GetStxAddressCallback(string InWalletAddress)
    {
        walletStacks = InWalletAddress;

        Debug.Log(string.Format("Stacks Wallet Address (BTC) - {0}", PlayerPrefs.GetString("StacksWallet")));

        // Callback when complete;
        stacksAccountCallback?.Invoke(InWalletAddress);
    }

    // Bridge method to handle the asynchronous operation from UI
    public void CallGetStxBalance()
    {
        StartCoroutine(GetStxBalanceCoroutine());
    }
    private IEnumerator GetStxBalanceCoroutine()
    {
        yield return GetStxBalance();
    }
    async Task GetStxBalance()
    {
        if (walletStacks != null || walletStacks != "")
        {
            m_AccountSTXBalance = await stacksAPICaller.GetAccountStxBalance<JSONAccountSTXBalance>(walletStacks);
            if (m_AccountSTXBalance == null)
            {
                Debug.LogWarning("Stacks API: GetStxBalance Failed");
            }
            else
            {
                Debug.Log(string.Format("Stacks API: GetStxBalance -> {0} STX", m_AccountSTXBalance.balance));

                stxBalanceCallback?.Invoke(m_AccountSTXBalance);
            }
        }
        else
        {
            Debug.LogWarning("Stacks Manager - Get STX: No Wallet Set");
        }
    }

    // Bridge method to handle the asynchronous operation from UI
    public void CallGetNFTHoldings()
    {
        StartCoroutine(GetNFTHoldingsCoroutine());
    }
    private IEnumerator GetNFTHoldingsCoroutine()
    {
        yield return GetNFTHoldings();
    }
    async Task GetNFTHoldings()
    {
        // If _metadata is true, need to use a different data object since the JSON format is different
        // Also, limit can not be more than 200. Very likely  the API server will reject  anything over 200, but safer to guard that at the UI
        if (walletStacks != null || walletStacks != "")
        {
            string[] fetchContracts = { fetchContract };

            m_NFTHoldings = await stacksAPICaller.GetNFTHoldings<JSONNFTHoldings>(walletStacks, fetchContracts, fetchNum, fetchIndex, fetchMetadata);
            if (m_NFTHoldings == null)
            {
                Debug.LogWarning("Stacks API: GetNFTHoldings Failed");
            }
            else
            {
                Debug.Log(string.Format("Stacks API: GetNFTHoldings -> {0} NFTs", m_NFTHoldings.total));

                nftHoldingsCallback?.Invoke(m_NFTHoldings);
            }
        }
        else
        {
            Debug.LogWarning("Stacks Manager - Get NFTs: No Wallet Set");
        }
    }

    // Bridge method to handle the asynchronous operation from UI
    public void CallGetNFTMetadata(int _tokenId)
    {
        StartCoroutine(GetNFTMetadataCoroutine(_tokenId));
    }
    private IEnumerator GetNFTMetadataCoroutine(int _tokenId)
    {
        yield return GetNFTMetadata(_tokenId);
    }
    async Task GetNFTMetadata(int _tokenId)
    {
        if (walletStacks != null || walletStacks != "")
        {
            m_NFTMetadata = await stacksAPICaller.GetNFTMetadata<JSONNFTMetadata>(walletStacks, _tokenId);
            if (m_NFTMetadata == null)
            {
                Debug.LogWarning("Stacks API: GetNFTMetadata Failed");
            }
            else
            {
                Debug.Log(string.Format("Stacks API: GetNFTMetadata -> {0} - {1}", m_NFTMetadata.metadata.name, m_NFTMetadata.metadata.properties.collection));

                nftMetadataCallback?.Invoke(m_NFTMetadata);
            }
        }
        else
        {
            Debug.LogWarning("Stacks Manager - Get Metadata: No Wallet Set");
        }
    }
}
