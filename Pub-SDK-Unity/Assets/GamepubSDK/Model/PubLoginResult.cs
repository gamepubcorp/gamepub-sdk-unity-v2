using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    [Serializable]
    public class PubLoginResult
    {
        [SerializeField]
        private int responseCode = 0;
        [SerializeField]
        private PubLoginInfo loginInfo = null;        

        public int ResponseCode { get { return responseCode; } }        
        public PubLoginInfo LoginInfo { get { return loginInfo; } }
        
    }
}