using UnityEngine;
using GamePub.PubSDK;

public class LoginController : MonoBehaviour
{
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

	public void Logout()
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

	public void Purchase(int productNum)
	{
		string productId = "";
		switch (productNum)
		{
			case 1:
				productId = "com.gamepub.test1000";
				break;
			case 2:
				productId = "com.gamepub.test2000";
				break;
			default:
				Debug.Log("wrong product number input");
				return;
		}
		GamePubSDK.Ins.InAppPurchase(productId, result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log(JsonUtility.ToJson(error));
				});
		});
	}

	public void RestorePurchase()
	{
		GamePubSDK.Ins.RestorePurchase(result => {
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
