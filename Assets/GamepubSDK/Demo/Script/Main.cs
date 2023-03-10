using UnityEngine;
using GamePub.PubSDK;

namespace GamepubSDK.Examples
{
    public class Main : MonoBehaviour
    {
        Vector2 scrollPos;

        void OnGUI()
        {
            GUIHelper.DrawArea(new Rect(0, 0, Screen.width, Screen.height / 4),
                              () =>
                              {
                                  //scrollPos = GUILayout.BeginScrollView(scrollPos);
                                  GUILayout.FlexibleSpace();
                                  GUIHelper.DrawRow("gamepub sytem team");
                                  GUIHelper.DrawRow("129381923771231");
                                  GUIHelper.DrawRow("gamepub.system@gmail.com");
                                  GUILayout.FlexibleSpace();
                                  //GUILayout.EndScrollView();
                              });

            GUIHelper.DrawArea(new Rect(0, Screen.height / 4, Screen.width, Screen.height * 3 / 4),
                              () =>
                              {
                                  scrollPos = GUILayout.BeginScrollView(scrollPos);

                                  GUILayout.BeginHorizontal();
                                  if (GUIHelper.DrawButton("SetupSDK", Screen.width / 2, 150))
                                  {
                                      Debug.Log("SetupSDK");

                                      GamePubSDK.Ins.SetupSDK(result =>
                                      {
                                          result.Match(
                                              value =>
                                              {
                                                  Debug.Log(JsonUtility.ToJson(value));
                                              },
                                              error =>
                                              {
                                                  Debug.Log(JsonUtility.ToJson(error));
                                              });
                                      });
                                  }
                                  GUILayout.FlexibleSpace();
                                  if (GUIHelper.DrawButton("Google Login", Screen.width / 2, 150))
                                  {
                                      Debug.Log("Google Login");

                                      GamePubSDK.Ins.Login(
                                          PubLoginType.GOOGLE,
                                          PubAccountServiceType.ACCOUNT_LOGIN,
                                          result =>
                                          {
                                              result.Match(
                                                  value =>
                                                  {
                                                      Debug.Log(JsonUtility.ToJson(value));
                                                  },
                                                  error =>
                                                  {
                                                      Debug.Log(JsonUtility.ToJson(error));
                                                  });
                                          });
                                  }
                                  GUILayout.EndHorizontal();

                                  GUILayout.BeginHorizontal();
                                  if (GUIHelper.DrawButton("Facebook Login", Screen.width / 2, 150))
                                  {
                                      Debug.Log("Facebook Login");
                                  }
                                  GUILayout.FlexibleSpace();
                                  if (GUIHelper.DrawButton("Apple Login", Screen.width / 2, 150))
                                  {
                                      Debug.Log("Apple Login");
                                  }
                                  GUILayout.EndHorizontal();

                                  GUILayout.BeginHorizontal();
                                  if (GUIHelper.DrawButton("InAppPurchase", Screen.width / 2, 150))
                                  {
                                      Debug.Log("InAppPurchase");

                                      GamePubSDK.Ins.InAppPurchase("gamepub_1000", result =>
                                      {
                                          result.Match(
                                              value =>
                                              {
                                                  Debug.Log(JsonUtility.ToJson(value));
                                              },
                                              error =>
                                              {
                                                  Debug.Log(JsonUtility.ToJson(error));
                                              });
                                      });
                                  }
                                  GUILayout.FlexibleSpace();
                                  if (GUIHelper.DrawButton("TEST 1", Screen.width / 2, 150))
                                  {
                                      Debug.Log("TEST 1");
                                  }
                                  GUILayout.EndHorizontal();

                                  GUILayout.BeginHorizontal();
                                  if (GUIHelper.DrawButton("TEST 2", Screen.width / 2, 150))
                                  {
                                      Debug.Log("TEST 2");
                                  }
                                  GUILayout.FlexibleSpace();
                                  if (GUIHelper.DrawButton("TEST 3", Screen.width / 2, 150))
                                  {
                                      Debug.Log("TEST 3");
                                  }
                                  GUILayout.EndHorizontal();                                  

                                  GUILayout.EndScrollView();

                              });
        }
    }
}