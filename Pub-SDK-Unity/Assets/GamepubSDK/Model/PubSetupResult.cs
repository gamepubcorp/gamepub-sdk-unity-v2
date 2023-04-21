using System;
using UnityEngine;


namespace GamePub.PubSDK
{
	[Serializable]
	public class PubSetupResult
	{
		[SerializeField] int code;
		[SerializeField] string message;
		[SerializeField] PubVersion version = null;
		[SerializeField] PubInspect inspect = null;

		public int Code { get => code; }
		public string Message { get => message; }
		public PubVersion Version { get => version; }
		public PubInspect Inspect { get => inspect; }
	}

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

	[Serializable]
	public class PubInspect
	{
		[SerializeField] string startDate;
		[SerializeField] string endDate;
		[SerializeField] string language;
		[SerializeField] string message;
		[SerializeField] string whiteIP = "";

		public string StartDate { get => startDate; }
		public string EndDate { get => endDate; }
		public string Language { get => language; }
		public string Message { get => message; }
		public string WhiteIP { get => whiteIP; }
	}

}
