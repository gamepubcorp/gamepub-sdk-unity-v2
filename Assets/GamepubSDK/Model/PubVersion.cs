using System;
using UnityEngine;

[Serializable]
public class PubVersion
{
	[SerializeField] string versionCode;
	[SerializeField] string versionName;
	[SerializeField] string store;
	[SerializeField] string link;

	public string VersionCode { get => versionCode; }
	public string VersionName { get => versionName; }
	public string Store { get => store; }
	public string Link { get => link; }
}
