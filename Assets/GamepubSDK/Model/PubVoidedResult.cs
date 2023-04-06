using GamePub.PubSDK;
using System;
using UnityEngine;

[Serializable]
public class PubVoidedResult
{
	[SerializeField] private PubPurchaseResult[] purchaseResults;

	public PubPurchaseResult[] PurchaseResults { get => purchaseResults; }
}
