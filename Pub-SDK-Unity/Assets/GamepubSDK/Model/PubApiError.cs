using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    [Serializable]
    public class PubApiError
    {        
        [SerializeField]
        private int errCode;
        [SerializeField]
        private string message;               

        public int ErrCode { get { return errCode; } }

        public string Message { get { return message; } }

        public PubApiError(int errCode, string message)
        {            
            this.errCode = errCode;
            this.message = message;
        }
    }
}