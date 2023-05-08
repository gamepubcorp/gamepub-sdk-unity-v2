using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    [Serializable]
    public class PubLoginResult
    {
        [SerializeField] private int code = 0;
        [SerializeField] private string message = "";
		[SerializeField] private string accountId = "";
		[SerializeField] private string accessToken = "";
		[SerializeField] private string regMessage = "";
		[SerializeField] private string startDate = "";
		[SerializeField] private string endDate = "";
		[SerializeField] private string clickLink = "";

		public int Code { get => code; }
		public string Message { get => message; }
		public string AccountId { get => accountId; }
		public string AccessToken { get => accessToken; }
		public string RegMessage { get => regMessage; }
		public string StartDate { get => startDate;}
		public string EndDate { get => endDate;}
		public string ClickLink { get => clickLink; }
	}
}