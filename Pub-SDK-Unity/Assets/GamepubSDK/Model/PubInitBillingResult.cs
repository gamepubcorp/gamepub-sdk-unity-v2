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
}