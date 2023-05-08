
namespace GamePub.PubSDK
{
    public enum PubLoginType
    {        
        GOOGLE,
        FACEBOOK,
		APPLE,
		GUEST,
    }

    public enum PubAccountServiceType
    {
        ACCOUNT_LOGIN,        
        ACCOUNT_LINK,
    }    
    
    public enum PubLanguageCode
    {
        KO,   //한글
        EN,   //영어
        JA,   //일본
        CN,   //중국(간체)
        TW,   //대만(번체)
        TH,   //태국
        VI,   //베트남
        ES,   //스페인
        PT,   //포르투갈
        FR,   //프랑스
        DE,   //독일
        RU,   //러시아
    }

    public enum PubSdkErrorCode
    {        
        SUCCESS = 1000,

		SDK_NOT_INITIALIZED = 1,
		NOT_LOGGED_IN = 2,
		BANNED_USER = 7,
		SERVER_MAINTENANCE = 8,
		NETWORK_ERROR = 14,
		SDK_INTERNAL_ERROR = 15,

		AUTH_USER_CANCELED = 3001,
		AUTH_UNSUPPORTED_PROVIDER = 3002,
		AUTH_UNSUPPORTED_SERVICE = 3003,
		AUTH_CLIENT_ID_NOT_EXIST = 3004,
		AUTH_EXISTING_SOCIAL_USER = 3005,
		AUTH_LINK_SAME_TYPE = 3006,
		AUTH_IDP_GOOGLE_ERROR = 40001,
		AUTH_IDP_FACEBOOK_ERROR = 40002,
		AUTH_IDP_APPLE_ERROR = 40003,

		IAP_NOT_INITIALIZED = 4001,
		IAP_UNSUPPORTED_MARKET = 4002,
		IAP_PRODUCT_LIST_NOT_EXIST = 4003,
		IAP_USER_CANCELED = 4004,
		IAP_PRODUCT_ID_NOT_EXIST = 4005,
		IAP_AGENT_GOOGLE_ERROR = 4006,
		IAP_AGENT_APPLE_ERROR = 4007,
		IAP_AGENT_ONE_ERROR = 4008,
		IAP_AGENT_GALAXY_ERROR = 4009,
		IAP_RETRY_LIST_EMPTY = 4010,
		IAP_VOIDED_LIST_EMPTY = 4011,
		IAP_SERVICE_DISCONNECTED = 90000,

		TERMS_NOT_EXIST_IN_CONSOLE = 5000,
		TERMS_DISAGREED = 5001,

		SERVER_INTERNAL_ERROR = 6000,
	}           
}