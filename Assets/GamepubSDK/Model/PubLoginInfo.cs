using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    [Serializable]
    public class PubLoginInfo
    {
        [SerializeField]
        private string accountId = "";
        [SerializeField]
        private string regMessage = "";
        [SerializeField]
        private string startDate = "";
        [SerializeField]
        private string endDate = "";

        public string AccountId { get { return accountId; } }
        public string RegMessage { get { return regMessage; } }
        public string StartDate { get { return startDate; } }
        public string EndDate { get { return endDate; } }        
    }
}