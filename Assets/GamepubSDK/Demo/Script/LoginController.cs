using UnityEngine;
using UnityEngine.UI;
using GamePub.PubSDK;

public class LoginController : MonoBehaviour
{
	public Text purchaseBtnText;
	int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetupSDKandInitBilling();

		//get Ad-ID
		string adId = "";
		AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass client = new AndroidJavaClass("com.google.android.gms.ads.identifier.AdvertisingIdClient");
		AndroidJavaObject adInfo = client.CallStatic<AndroidJavaObject>("getAdvertisingIdInfo", currentActivity);

		adId = adInfo.Call<string>("getId").ToString();
		Debug.Log("Ad-ID: "+adId);
	}
    
    public void SetupSDKandInitBilling()
    {
        GamePubSDK.Ins.SetupSDK(result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));

					GamePubSDK.Ins.InitBilling(result => {
						result.Match(
							value => {
								Debug.Log(JsonUtility.ToJson(value));
							},
							error => {
								Debug.Log(JsonUtility.ToJson(error));
							});
					});
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
    }
    public void SocialLogin(string loginType)
    {
		PubLoginType pubLoginType = 0;
		switch(loginType)
		{
			case "google":
				pubLoginType = PubLoginType.GOOGLE;
				break;
			case "facebook":
				pubLoginType = PubLoginType.FACEBOOK;
				break;
			case "apple":
				pubLoginType = PubLoginType.APPLE;
				break;
			case "guest":
				pubLoginType = PubLoginType.GUEST;
				break;
			default:
				Debug.Log("Wrong login type input");
				break;
		}

		GamePubSDK.Ins.Login(
			pubLoginType,
			PubAccountServiceType.ACCOUNT_LOGIN,
			result => {
				result.Match(
					value => {
						Debug.Log(JsonUtility.ToJson(value));
					},
					error => {
						Debug.Log(JsonUtility.ToJson(error));
					});
			});
	}
	public void AutoLogin()
	{
		GamePubSDK.Ins.AutoLogin(result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void LinkAccount(string loginType)
	{
		PubLoginType pubLoginType = 0;
		switch (loginType)
		{
			case "google":
				pubLoginType = PubLoginType.GOOGLE;
				break;
			case "facebook":
				pubLoginType = PubLoginType.FACEBOOK;
				break;
			case "apple":
				pubLoginType = PubLoginType.APPLE;
				break;
			default:
				Debug.Log("Wrong login type input");
				break;
		}

		GamePubSDK.Ins.Login(
			pubLoginType,
			PubAccountServiceType.ACCOUNT_LINK,
			result => {
				result.Match(
					value => {
						Debug.Log(JsonUtility.ToJson(value));
					},
					error => {
						Debug.Log(JsonUtility.ToJson(error));
					});
			});
	}

	public void SetPushToken()
	{
		string pushToken = "samplePushTokenUnity1jk234bg235jy23jk";
		GamePubSDK.Ins.SetPushToken(pushToken, result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void SetPushConfig()
	{
		PubPushConfig pushConfig = new PubPushConfig();
		pushConfig.AgreedPush = false;
		pushConfig.AgreedNightPush = true;
		pushConfig.AgreedAdPush = true;

		GamePubSDK.Ins.SetPushConfig(pushConfig, result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void GoogleLogout()
	{
		GamePubSDK.Ins.Logout(result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void Withdraw()
	{
		GamePubSDK.Ins.Withdraw(result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void Purchase()
	{
		string productId = "pubsdk_"+count+"000";
		//string productId = "com.gamepub.test"+count+"000";

		string channelId = "unityTestChannelId";
		string characterId = "unityTestCharacterId";

		GamePubSDK.Ins.Purchase(productId, channelId, characterId, result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
					if (error.Message == "테스트용 결제 장애")
					{
						count++;
						if (productId.Contains("pubsdk_") && count == 4)
						{
							count = 5;
						}
						if (productId.Contains("pubsdk_") && count == 12)
						{
							count = 1;
						}
						if (productId.Contains("com.gamepub.test") && count == 11)
						{
							count = 1;
						}
						purchaseBtnText.text = "결제 상품 "+count;
					}
				});
		});
	}

	public void RetryPurchase()
	{
		string channelId = "unityTestChannelId";
		string characterId = "unityTestCharacterId";

		GamePubSDK.Ins.RetryPurchase(channelId, characterId, result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
		count = 1;
		purchaseBtnText.text = "결제 상품 "+count;
	}

	public void RestoreRefund()
	{
		GamePubSDK.Ins.RestoreRefund(result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void OpenTerms()
	{
		GamePubSDK.Ins.OpenTerms(result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void OpenImageBanner()
	{
		GamePubSDK.Ins.OpenImageBanner(result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}
}
