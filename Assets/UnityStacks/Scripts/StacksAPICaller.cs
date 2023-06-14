using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class StacksAPICaller : MonoBehaviour
{
    public string m_ApiEndpoint;

    // Accounts
    public async Task<TResultType> GetAccountInfo<TResultType>(string _principal)
    {
        string _json;
        var _url = m_ApiEndpoint + "v2/accounts/" + _principal;
        
        Debug.Log("API Call - GetAccountInfo: " + _url);
        
        using var _www = UnityWebRequest.Get(_url);
        _www.SetRequestHeader("Content-Type", "application/json");
        var _operation = _www.SendWebRequest();
        
        while (!_operation.isDone)
        {
            await Task.Yield();
        }

        if (_www.result == UnityWebRequest.Result.Success)
        {
            _json = _www.downloadHandler.text;
            var _result = JsonUtility.FromJson<TResultType>(_json);
            return _result;
        } else { return default; }
    }

    public async Task<TResultType> GetAccountStxBalance<TResultType>(string _principal)
    {
        string _json;
        var _url = m_ApiEndpoint + "extended/v1/address/" + _principal + "/stx";

        Debug.Log("API Call - GetAccountStxBalance");

        using var _www = UnityWebRequest.Get (_url);
        _www.SetRequestHeader("Content-Type", "application/json");
        var _operation = _www.SendWebRequest();

        while(!_operation.isDone)
        {
            await Task.Yield();
        }

        if (_www.result == UnityWebRequest.Result.Success)
        {
            _json = _www.downloadHandler.text;
            var _result = JsonUtility.FromJson<TResultType>(_json);
            return _result;
        } else { return default; }
    }

    public async Task<TResultType> GetAccountTransactions<TResultType>(string _principal)
    {
        string _json;
        var _url = m_ApiEndpoint + "extended/v1/address/" + _principal + "/transactions_with_transfers";

        Debug.Log("API Call - GetAccountTransactions");

        using var _www = UnityWebRequest.Get(_url);
        _www.SetRequestHeader("Content-Type", "application/json");
        var _operation = _www.SendWebRequest();

        while(!_operation.isDone)
        {
            await Task.Yield();
        }

        if (_www.result == UnityWebRequest.Result.Success)
        {
            _json = _www.downloadHandler.text;
            var _result = JsonUtility.FromJson<TResultType>(_json);
            return _result;
        } else { return default; }
    }

    // Non-Fungible Tokens
    public async Task<TResultType> GetNFTHoldings<TResultType>(string _principal, string[] _assetIds = null, int _limit = 50, int _offset = 0, bool _metadata = false)
    {
        string _asset_identifiers = "";
        foreach (var _Id in _assetIds)
        {
            if (_Id != "")
            {
                _asset_identifiers += "&asset_identifiers=" + _Id;
            } else
            {
                break;
            }
        }
        string _json;
        var _url = string.Format("{0}extended/v1/tokens/nft/holdings?principal={1}{2}&limit={3}&offset={4}&tx_metadata={5}", m_ApiEndpoint, _principal, _asset_identifiers, _limit, _offset, _metadata);
        //var _url = string.Format("{0}extended/v1/tokens/nft/holdings?principal={1}", m_ApiEndpoint, _principal);
        Debug.Log(_url);

        using var _www = UnityWebRequest.Get(_url);
        _www.SetRequestHeader("Content-Type", "application/json");
        var _operation = _www.SendWebRequest();

        while(!_operation.isDone)
        {
            await Task.Yield();
        }

        if (_www.result == UnityWebRequest.Result.Success)
        {
            _json = _www.downloadHandler.text;
            var _result = JsonUtility.FromJson<TResultType>(_json);
            return _result;
        } else { return default; }
    }

    // Non-Fungible Token Metadata
    public async Task<TResultType> GetNFTMetadata<TResultType>(string _principal, int _tokenId)
    {
        string _json;
        var _url = string.Format("{0}metadata/v1/nft/{1}/{2}", m_ApiEndpoint, _principal, _tokenId);
        Debug.Log(_url);
        using var _www = UnityWebRequest.Get(_url);
        _www.SetRequestHeader("Content-Type", "application/json");
        var _operation = _www.SendWebRequest();

        while (!_operation.isDone)
        {
            await Task.Yield();
        }

        if (_www.result == UnityWebRequest.Result.Success && _www.responseCode == 200)
        {
            _json = _www.downloadHandler.text;
            var _result = JsonUtility.FromJson<TResultType>(_json);
            return _result;
        } else { return default; } // PSD : I do want to better handle the different error codes, but I'll leave that for later
    }
}
