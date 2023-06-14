# UNITY Stacks

## Overview

UNITY Stacks is a Unity Template project for creating WebGL applications that can be token-gated by Stacks NFTs

Unity Stacks can access the Hiro wallet browser plugin to retrieve the user's wallet.

It can then retrieve a list of Stacks NFTs stored in that wallet from the Hiro public Stacks API.

### Contents

- [Installation](#installation)
- [Demo Scene and Basic Setup](#demo-scene-and-basic-setup)
- [Public Functions](#public-functions)
- [Callbacks](#callbacks)
- [Requirements](#requirements)
- [Changelist](#changelist)
- [Contact](#contact)
- [License](#license)
- [Credits](#credits)

## Installation

Currently Unity Stacks can be cloned from GitHub to setup a new project.

We will provide a Unity `.asset` package in the future.

## Demo Scene and Basic Setup

A demo scene is located at `Assets/Scenes/Test_Stacks.unity`

To use Unity Stacks in another scene, simply add `Assets/UnityStacks/Prefabs/StacksManager.prefab` to your scene hierarchy.

**WARNING**
_Changing the prefab's name will cause the linkage to the JavaScript plugin to break._

_The component will rename itself if the name has been changed, but best practice is to leave it named `StacksManager`_

### First Build

- Ensure your project is set to build for WebGL in `File -> Build Settings`
- Click the `Player Settings...` button (or `Edit -> Project Settings...`) and select the `Player` tab on the left
  - Under `Resolution and Presentation` in the `WebGL Template` subsection, ensure `Better2020` is selected.
- In `Build Settings` click `Build And Run` and select the output folder (create one called `Builds` if needed)
- Use your browser's Developer Tools `F12` to view the console to see output.



## Public Functions

Unity Ordinals exposes the following functions:

`GetStacksAccount()`

- Opens the wallet (Hiro) browser plugin and prompts the user to connect to the webpage.
- Callback to `GetStxAddressCallback(string adddress)` from the ConnectStacks JavaScript plugin that handles the wallet request.

`CallGetStxBalance()`

- Bridge method for use with Unity UI and NGUI buttons and other UI functions.
- Uses `APICaller` to retrieve the balance of STX tokens in the given user's wallet.
- Uses the wallet set in `walletOrdinals`

`CallGetNFTHoldings()`

- Bridge method for use with Unity UI and NGUI buttons and other UI functions.
- Uses `APICaller` to retrieve the first 20 NFTs in the given user's wallet.
- Uses the wallet set in `walletOrdinals`

`CallGetNFTMetadata(int _tokenId)`

- Bridge method for use with Unity UI and NGUI buttons and other UI functions.
- Uses `APICaller` to retrieve the metadata of the NFT passed as '_tokenId'.
- Uses the wallet set in `walletOrdinals`

## Callbacks

Use these to get receive notifications in your Components when the connected functions return their results.

`GetStacksAccountCallback`

- Uses Unity's C# delegate framework.
- Called when the Hiro Wallet returns an address (or empty if the wallet is not initialized or logged in.)
- Passes the string `address` as parameter

`GetStxBalanceCallback`

- Uses Unity's C# delegate framework.
- Called when the Hiro API returns a JSON object of STX token balance (or empty if the wallet has no STX.)
- Passes the JSON object `JSONAccountSTXBalance` as parameter

`GetNFTHoldingsCallback`

- Uses Unity's C# delegate framework.
- Called when the Hiro API returns a JSON object of NFTs (or empty if the wallet has no NFTs.)
- Passes the JSON object `JSONNFTHoldings` as parameter

`GetNFTMetadataCallback`

- Uses Unity's C# delegate framework.
- Called when the Hiro API returns a JSON object of NFT metadata (or empty if the tokenID does not exist.)
- Passes the JSON object `JSONNFTMetadata` as parameter

### Example delegate use

```C#
// In your Component's Awake() method add the following
private void Awake()
{
  myComponentAccountCallback += OnStacksAccountCallback;
  myComponentHoldingsCallback == OnNFTHoldingsCallback;
}

// Define your callbacks
private void myComponentAccountCallback(string address)
{
  Debug.Log("My Component Callback: Stacks Account: " + address);
}
    
private void myComponentHoldingsCallback(JSONNFTHoldings result)
{
  Debug.Log("My Component Callback: NFT Holdings: " + result.total);
} 

// Cleanup: de-register the callbacks
private void OnDisable()
{
  myComponentAccountCallback -= OnStacksAccountCallback;
  myComponentHoldingsCallback -= OnNFTHoldingsCallback;
}
```

## Requirements

Currently Unity Stacks is only compatible with [Hiro Wallet](https://wallet.hiro.so/)

Unity Stacks has been tested with Unity 2021 LTS (2021.3.26f1)

## Changelist

- 0.1.1
  - Retrieve Stacks address from Hiro wallet
  - Retrive balance of STX tokens from given wallet
  - Retrieve list of NFTs (up to 20) from given wallet
  - Retrieve Metadata for a specified tokenId from given wallet

## Contact

For more information about Unity Stacks, contact the author

- [Patrion@MetaBoy.run](mailto:Patrion@MetaBoy.run) / [@PatrionDigital](https://twitter.com/@PatrionDigital)

## License

Unity Stacks is licensed under the LGPL 3.0

For more information, see the LICENSE file in the repository.

## Credits

- Unity Stacks uses the [better-unity-webgl-template](https://github.com/greggman/better-unity-webgl-template)
