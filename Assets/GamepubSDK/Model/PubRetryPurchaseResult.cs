using GamePub.PubSDK;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PubRetryPurchaseResult
{
	[SerializeField] private PubPurchaseResult[] retryPurchaseList;

	public PubPurchaseResult[] RetryPurchaseList { get => retryPurchaseList; }
}
