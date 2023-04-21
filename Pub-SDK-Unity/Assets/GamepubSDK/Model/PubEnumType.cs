
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

    public enum PubResponseCode
    {        
        SUCCESS = 1000,
        FAILED,        
    }           
}