
namespace GamePub.PubSDK
{
    public enum PubLoginType
    {        
        GOOGLE,
        FACEBOOK,
        GUEST,
        APPLE,
    }

    public enum PubAccountServiceType
    {
        NONE,
        ACCOUNT_LOGIN,        
        ACCOUNT_LINK,
    }    
    
    public enum PubLanguageCode
    {
        KOREAN,     //한글
        ENGLISH,    //영어
        JAPANESE,   //일본
        ZH_CN,      //중국(간체)
        ZH_TW,      //대만(번체)
        THAI,       //태국
        VIETNAMESE, //베트남
        SPANISH,    //스페인
        PORTUGAL,   //포르투갈
        FRENCH,     //프랑스
        GERMAN,     //독일
        RUSSIAN,    //러시아
    }

    public enum PubResponseCode
    {        
        SUCCESS = 5000,
        FAILED,        
    }           
}