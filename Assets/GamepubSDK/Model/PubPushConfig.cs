using System;
using UnityEngine;

[Serializable]
public class PubPushConfig
{
	[SerializeField] private bool agreedPush;
	[SerializeField] private bool agreedNightPush;
	[SerializeField] private bool agreedAdPush;

	public bool AgreedPush { get => agreedPush; set => agreedPush = value; }
	public bool AgreedNightPush { get => agreedNightPush; set => agreedNightPush = value; }
	public bool AgreedAdPush { get => agreedAdPush; set => agreedAdPush = value; }
}

