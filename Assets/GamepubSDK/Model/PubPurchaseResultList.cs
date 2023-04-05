using GamePub.PubSDK;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PubPurchaseResultList
{
	[SerializeField] private PubPurchaseResult[] pubPurchaseResultList;

	public PubPurchaseResult[] PurchaseResultList { get => pubPurchaseResultList; }
}
