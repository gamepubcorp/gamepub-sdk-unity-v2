using System;
using UnityEngine;

[Serializable]
public class PubTermsResult
{
	[SerializeField] private string deviceId;
	[SerializeField] private bool agreed;
	[SerializeField] private PubPushConfig pushConfig;
}
