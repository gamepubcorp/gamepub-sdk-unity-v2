using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    [Serializable]
    public class PubLoginResult
    {
        [SerializeField] private int responseCode = 0;
		[SerializeField] private string accountId = "";
		[SerializeField] private string accessToken = "";
		[SerializeField] private string regMessage = "";
		[SerializeField] private string startDate = "";
		[SerializeField] private string endDate = "";

		public int ResponseCode { get { return responseCode; } }
		public string AccountId { get { return accountId; } }
		public string AccessToken { get { return accessToken; } }
		public string RegMessage { get { return regMessage; } }
		public string StartDate { get { return startDate; } }
		public string EndDate { get { return endDate; } }
    }
}