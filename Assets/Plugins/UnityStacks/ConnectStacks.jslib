var ConnectStacks = {
	// Get Stacks Address from Hiro Wallet
	connectToSTXWallet: function() {
		window.StacksProvider.request("getAddresses").then(function(res){
			// Stacks Wallet is in third slot
			window.StxAccount = res.result.addresses[2];

			unityGame.SendMessage("StacksManager", "GetStxAddressCallback", window.StxAccount.Address);
		});
	}
}
mergeInto(LibraryManager.library, ConnectStacks);