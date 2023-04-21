using UnityEngine;
using GamePub.PubSDK;
using Firebase;
using Firebase.Messaging;
using Firebase.Extensions;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class LoginController : MonoBehaviour
{
	string CHANNEL_ID = "pubSdkChannel";
	int apiLevel;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
		InitializeAndroidLocalPush();
		InitializeFCM();
#elif UNITY_IOS

#endif 
		SetupSDKandInitBilling();
    }

	public void InitializeAndroidLocalPush()
	{
		string androidInfo = SystemInfo.operatingSystem;
		Debug.Log("androidInfo: " + androidInfo);
		apiLevel = int.Parse(androidInfo.Substring(androidInfo.IndexOf("-") + 1, 2));
		Debug.Log("apiLevel: " + apiLevel);

		if (apiLevel >= 33 &&
			!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
		{
			Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
		}

		if (apiLevel >= 26)
		{
			var channel = new AndroidNotificationChannel()
			{
				Id = CHANNEL_ID,
				Name = "pubSdk",
				Importance = Importance.High,
				Description = "for test",
			};
			AndroidNotificationCenter.RegisterNotificationChannel(channel);
		}
	}

	public void InitializeFCM()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == DependencyStatus.Available)
			{
				Debug.Log("Google Play version OK");

				FirebaseMessaging.TokenReceived += OnTokenReceived;
				FirebaseMessaging.MessageReceived += OnMessageReceived;
			}
			else
			{
				Debug.LogError(string.Format(
					"Could not resolve all Firebase dependencies: {0}", 
					dependencyStatus
				));
			}
		});
	}

	public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
	{
		Debug.Log("OnTokenReceived: " + token.Token);
	}
	public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
	{
		string type = "";
		string title = "";
		string body = "";

		// for notification message
		if (e.Message.Notification != null) 
		{
			type = "notification";
			title = e.Message.Notification.Title;
			body = e.Message.Notification.Body;
		}
		// for data message
		else if (e.Message.Data.Count > 0)
		{
			type = "data";
			title = e.Message.Data["title"];
			body = e.Message.Data["body"];
		}
		Debug.Log("message type: " + type + ", title: " + title + ", body: " + body);

		// send local notification
		var notification = new AndroidNotification();
		notification.SmallIcon = "icon_0";
		notification.Title = title;
		notification.Text = body;
		notification.FireTime = System.DateTime.Now;

		if (apiLevel >= 26)
		{
			AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
		}
		else
		{
			Debug.LogError("Notifications couldn't be displayed, because the Android SDK level of your device is lower than 26.");
		}
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
		var task = FirebaseMessaging.GetTokenAsync().ContinueWithOnMainThread(task => {
			string fcmToken = task.Result;
			Debug.Log("fcmToken: "+fcmToken);

			GamePubSDK.Ins.SetPushToken(fcmToken, result => {
				result.Match(
					value => {
						Debug.Log("SetPushToken: " + value.Code.ToString() + " " + value.Msg);
					},
					error => {
						Debug.Log("SetPushToken: " + error.Code.ToString() + " " + error.Message);
					});
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

	public void Purchase(int productNum)
	{
		string productId = "pubsdk_"+productNum+"000";
		//string productId = "com.gamepub.test"+productNum+"000";

		string channelId = "unityTestChannelId";
		string characterId = "unityTestCharacterId";

		GamePubSDK.Ins.Purchase(productId, channelId, characterId, result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log("Purchase: " + error.Code.ToString() + " " + error.Message);
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
	}

	public void OpenVoided()
	{
		string channelId = "unityTestChannelId";
		string characterId = "unityTestCharacterId";

		GamePubSDK.Ins.OpenVoided(channelId, characterId, result => {
			result.Match(
				value => {
					Debug.Log(JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log("OpenVoided: " + error.Code.ToString() + " " + error.Message);
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