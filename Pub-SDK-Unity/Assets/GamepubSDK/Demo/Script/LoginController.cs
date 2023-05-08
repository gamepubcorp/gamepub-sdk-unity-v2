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

					switch (value.Code)
					{
						case (int)PubSdkErrorCode.SUCCESS:
							GamePubSDK.Ins.InitBilling(result => {
								result.Match(
									value => {
										Debug.Log(JsonUtility.ToJson(value));
									},
									error => {
										Debug.Log("InitBilling: " + error.ErrCode.ToString() + " " + error.Message);
									});
							});
							break;

						case (int)PubSdkErrorCode.SERVER_MAINTENANCE:
							Debug.Log(value.Inspect.Message);
							break;
					}
				},
				error => {
					Debug.Log("Setup: " + error.ErrCode.ToString() + " " + error.Message);
				});
		});
    }
    public void Login(string loginType)
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
						switch (value.Code)
						{
							case (int)PubSdkErrorCode.SUCCESS:
								Debug.Log("Login: " + JsonUtility.ToJson(value));
								break;

							case (int)PubSdkErrorCode.BANNED_USER:
								Debug.Log("This user has been banned.");
								Debug.Log("ban message: "+value.RegMessage);
								Debug.Log("startDate: "+value.StartDate);
								Debug.Log("endDate: "+value.EndDate);
								break;

							case (int)PubSdkErrorCode.SERVER_MAINTENANCE:
								Debug.Log("The game server is in maintenance.");
								Debug.Log("inspect message: " + value.RegMessage);
								Debug.Log("startDate: " + value.StartDate);
								Debug.Log("endDate: " + value.EndDate);
								break;
						}
					},
					error => {
						Debug.Log("Login: " + error.ErrCode.ToString() + " " + error.Message);
					});
			});
	}
	public void AutoLogin()
	{
		GamePubSDK.Ins.AutoLogin(result => {
			result.Match(
				value => {
					Debug.Log("AutoLogin: " + JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log("AutoLogin: " + error.ErrCode.ToString() + " " + error.Message);
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
						Debug.Log("LinkAccount: " + JsonUtility.ToJson(value));
					},
					error => {
						Debug.Log("LinkAccount: " + error.ErrCode.ToString() + " " + error.Message);
					});
			});
	}

	public void Logout()
	{
		GamePubSDK.Ins.Logout(result => {
			result.Match(
				value => {
					Debug.Log("Logout: " + value.Code.ToString() + " " + value.Message);
				},
				error => {
					Debug.Log("Logout: " + error.ErrCode.ToString() + " " + error.Message);
				});
		});
	}

	public void Withdraw()
	{
		GamePubSDK.Ins.Withdraw(result => {
			result.Match(
				value => {
					Debug.Log("Withdraw: " + value.Code.ToString() + " " + value.Message);
				},
				error => {
					Debug.Log("Withdraw: " + error.ErrCode.ToString() + " " + error.Message);
				});
		});
	}

	public void Purchase(int productNum)
	{
		string productId = "pubsdk_"+productNum+"000";
		string channelId = "unityTestChannelId";
		string characterId = "unityTestCharacterId";

		GamePubSDK.Ins.Purchase(productId, channelId, characterId, result => {
			result.Match(
				value => {
					Debug.Log("Purchase: " + JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log("Purchase: " + error.ErrCode.ToString() + " " + error.Message);
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
					Debug.Log("RetryPurchase: " + JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log("RetryPurchase: " + error.ErrCode.ToString() + " " + error.Message);
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
					Debug.Log("OpenVoided: " + JsonUtility.ToJson(value));
				},
				error => {
					Debug.Log("OpenVoided: " + error.ErrCode.ToString() + " " + error.Message);
				});
		});
	}

	public void OpenTerms()
	{
		GamePubSDK.Ins.OpenTerms(result => {
			result.Match(
				value => {
					Debug.Log("Terms: " + value.Code.ToString() + " " + value.Message);
				},
				error => {
					Debug.Log("Terms: " + error.ErrCode.ToString() + " " + error.Message);
				});
		});
	}

	public void OpenImageBanner()
	{
		GamePubSDK.Ins.OpenImageBanner();
	}

	public void OpenHelp()
	{
		GamePubSDK.Ins.OpenHelp();
	}

	public void SetPushToken()
	{
		FirebaseMessaging.GetTokenAsync().ContinueWithOnMainThread(task => {
			string fcmToken = task.Result;
			Debug.Log("fcmToken: " + fcmToken);

			GamePubSDK.Ins.SetPushToken(fcmToken);
		});
	}

	public void SetPushConfig()
	{
		bool agreedPush = true;
		bool agreedNightPush = false;

		GamePubSDK.Ins.SetPushConfig(agreedPush, agreedNightPush);
	}
}
