using GamePub.PubSDK;
using System;
using UnityEngine;

[Serializable]
public class PubRetryPurchaseResult
{
	[SerializeField] private PubPurchaseResult[] purchaseResults;

	public PubPurchaseResult[] PurchaseResults { get => purchaseResults; }
}
