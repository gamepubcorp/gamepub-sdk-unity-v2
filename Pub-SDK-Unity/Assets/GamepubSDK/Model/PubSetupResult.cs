using System;
using UnityEngine;

[Serializable]
public class PubSetupResult
{
	[SerializeField] int code;
	[SerializeField] string message;
	[SerializeField] PubVersion version;
	[SerializeField] PubInspect inspect;

	public int Code { get => code; }
	public string Message { get => message; }
	public PubVersion Version { get => version; }
	public PubInspect Inspect { get => inspect; }
}