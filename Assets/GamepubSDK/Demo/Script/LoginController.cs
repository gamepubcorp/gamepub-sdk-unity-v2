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
								Debug.Log("InitBilling: "+error.Code.ToString()+" "+error.Message);
							});
					});
				},
				error => {
					Debug.Log("Setup: " + error.Code.ToString() + " " + error.Message);
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
						Debug.Log("Login: " + error.Code.ToString() + " " + error.Message);
					});
			});
	}
	public void AutoLogin()
	{
		GamePubSDK.Ins.AutoLogin(result => {
			result.Match(
				value => {
					Debug.Log("AutoLogin: " + value.Code.ToString() + " " + value.Msg);
				},
				error => {
					Debug.Log("AutoLogin: " + error.Code.ToString() + " " + error.Message);
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
						Debug.Log("LinkAccount: " + error.Code.ToString() + " " + error.Message);
					});
			});
	}

	public void SetPushToken()
	{
		string pushToken = "samplePushTokenUnity1jk234bg235jy23jk";
		GamePubSDK.Ins.SetPushToken(pushToken, result => {
			result.Match(
				value => {
					Debug.Log("SetPushToken: " + value.Code.ToString() + " " + value.Msg);
				},
				error => {
					Debug.Log("SetPushToken: " + error.Code.ToString() + " " + error.Message);
				});
		});
	}

	public void SetPushConfig()
	{
		PubPushConfig pushConfig = new PubPushConfig();
		pushConfig.AgreedPush = true;
		pushConfig.AgreedNightPush = false;

		GamePubSDK.Ins.SetPushConfig(pushConfig, result => {
			result.Match(
				value => {
					Debug.Log("SetPushConfig: " + value.Code.ToString() + " " + value.Msg);
				},
				error => {
					Debug.Log("SetPushConfig: " + error.Code.ToString() + " " + error.Message);
				});
		});
	}

	public void Logout()
	{
		GamePubSDK.Ins.Logout(result => {
			result.Match(
				value => {
					Debug.Log("Logout: " + value.Code.ToString() + " " + value.Msg);
				},
				error => {
					Debug.Log("Logout: " + error.Code.ToString() + " " + error.Message);
				});
		});
	}

	public void Withdraw()
	{
		GamePubSDK.Ins.Withdraw(result => {
			result.Match(
				value => {
					Debug.Log("Withdraw: " + value.Code.ToString() + " " + value.Msg);
				},
				error => {
					Debug.Log("Withdraw: " + error.Code.ToString() + " " + error.Message);
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
					Debug.Log("Purchase: " + error.Code.ToString() + " " + error.Message);
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
					Debug.Log("RetryPurchase: " + error.Code.ToString() + " " + error.Message);
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
					Debug.Log("RestoreRefund: " + error.Code.ToString() + " " + error.Message);
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
					Debug.Log("Terms: " + error.Code.ToString() + " " + error.Message);
				});
		});
	}

	public void OpenImageBanner()
	{
		GamePubSDK.Ins.OpenImageBanner(result => {
			result.Match(
				value => {
					Debug.Log("Banner: " + value.Code.ToString() + " " + value.Msg);
				},
				error => {
					Debug.Log("Banner: " + error.Code.ToString() + " " + error.Message);
				});
		});
	}

	public void OpenCustomerCenter()
	{
		GamePubSDK.Ins.OpenCustomerCenter(result => {
			result.Match(
				value => {
					Debug.Log("Customer: " + value.Code.ToString() + " " + value.Msg);
				},
				error => {
					Debug.Log("Customer: " + error.Code.ToString() + " " + error.Message);
				});
		});
	}
}
