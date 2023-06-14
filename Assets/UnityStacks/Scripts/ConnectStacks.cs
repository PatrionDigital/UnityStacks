using System.Runtime.InteropServices;
using UnityEngine;

public class ConnectStacks
{
#if UNITY_WEBGL || UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void connectToSTXWallet();
    
    public static void ConnectToStacksWallet()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        connectToSTXWallet();
#endif
#if UNITY_EDITOR
        // Set Dummy Address in Editor
        StacksManager.Instance.GetStxAddressCallback("STX WALLET ADDRESS");
#endif
    }
#endif
}