using UnityEngine;

namespace GamepubSDK.Examples
{
    public class Login : MonoBehaviour
    {        

        void OnGUI()
        {            
            GUIHelper.DrawArea(new Rect(0, 0, Screen.width, Screen.height),
                               () =>
                               {
                                   GUIHelper.DrawCenteredText("Gamepub Login");
                                   GUILayout.Space(5);                                   

                                   GUILayout.BeginVertical();
                                   
                                   if (GUIHelper.DrawButton(GUIHelper.ResizeButton(new Rect(50, 150, 220, 50)),
                                                  "Google Login"))
                                   {
                                       Debug.Log("Google");
                                   }

                                   if (GUIHelper.DrawButton(GUIHelper.ResizeButton(new Rect(50, 200, 220, 50)),
                                                  "Facebook Login"))
                                   {
                                       Debug.Log("Facebook");
                                   }

                                   if(GUIHelper.DrawButton(GUIHelper.ResizeButton(new Rect(50, 250, 220, 50)),
                                                           "Apple Login"))
                                   {
                                       Debug.Log("Apple");
                                   }

                                   GUILayout.EndVertical();
                               });            
            

            //if (GUI.Button(UtilResize.ResizeButton(new Rect(50, 300, 220, 50)), "Guest Login"))
            //{

            //}
        }
    }
}
