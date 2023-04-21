using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    [Serializable]
    public class PubInitBillingResult
    {
        [SerializeField]
        private PubInAppProduct[] productList = null;

        public PubInAppProduct[] ProductList { get { return productList; } }
    }

	[Serializable]
	public class PubInAppProduct
	{
		[SerializeField]
		private string productId = "";
		[SerializeField]
		private string currency = "";
		[SerializeField]
		private string price = "";
		[SerializeField]
		private string title = "";
		[SerializeField]
		private string desc = "";

		public string ProductID { get { return productId; } }
		public string Currency { get { return currency; } }
		public string Price { get { return price; } }
		public string Title { get { return title; } }
		public string Desc { get { return desc; } }
	}
}