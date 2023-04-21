using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    [Serializable]
    public class PubPurchaseResult
    {
		[SerializeField]
		private float price = 0;
        [SerializeField]
        private string gamepubProductId = "";
        [SerializeField]
        private string currency = "";
        [SerializeField]
        private string gamepubTid = "";
        [SerializeField]
        private string purchaseToken = "";
        [SerializeField]
        private string marketProductId = "";        
        [SerializeField]
        private string originalJSONData = "";
        
        public float Price { get { return price; } }
        public string GamepubProductId { get { return gamepubProductId; } }
        public string Currency { get { return currency; } }
        public string GamepubTid { get { return gamepubTid; } }
        public string PurchaseToken { get { return purchaseToken; } }
        public string MarketProductId { get { return marketProductId; } }        
        public string OriginalJSONData { get { return originalJSONData; } }
    }
}
