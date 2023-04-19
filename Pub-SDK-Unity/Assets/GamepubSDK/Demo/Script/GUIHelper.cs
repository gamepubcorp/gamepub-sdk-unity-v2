using System;
using UnityEngine;

namespace GamepubSDK.Examples
{
    public enum FontType
    {
        Title,
        Message,
        Button,
    }

    public static class GUIHelper
    {
        private static GUIStyle centerAlignedLabel;
        private static GUIStyle rightAlignedLabel;
        private static GUIStyle keyLabel;
        private static GUIStyle btnStyle;

        //public static Rect ClientArea;

        private static float landscapeWidth = 1280.0f;
        private static float landscapeHeight = 720.0f;
        private static float portraitWidth = 720.0f;
        private static float portraitHeight = 1280.0f;
        private static float factorX = 1.0f;
        private static float factorY = 1.0f;
        private static float fontSizeFactor = 1.0f;

        private static void Setup()
        {
            // These has to be called from OnGUI
            if (centerAlignedLabel == null)
            {
                SetScreenSizeFactor();                
                centerAlignedLabel = new GUIStyle(GUI.skin.label);
                centerAlignedLabel.alignment = TextAnchor.MiddleCenter;
                centerAlignedLabel.fontSize = Mathf.RoundToInt(fontSizeFactor * 100);
                //centerAlignedLabel.fontSize = UtilResize.ResizeFont(UtilResize.FontType.Title);

                rightAlignedLabel = new GUIStyle(GUI.skin.label);
                rightAlignedLabel.alignment = TextAnchor.MiddleRight;
                rightAlignedLabel.fontSize = Mathf.RoundToInt(fontSizeFactor * 30);
                //rightAlignedLabel.fontSize = UtilResize.ResizeFont(UtilResize.FontType.Message);

                keyLabel = new GUIStyle(GUI.skin.label);
                keyLabel.alignment = TextAnchor.MiddleCenter;
                keyLabel.fontSize = Mathf.RoundToInt(fontSizeFactor * 25);
                //keyLabel.fontSize = UtilResize.ResizeFont(UtilResize.FontType.Message);

                btnStyle = new GUIStyle(GUI.skin.button);
                btnStyle.fontSize = Mathf.RoundToInt(fontSizeFactor * 30);
                //btnStyle.fontSize = UtilResize.ResizeFont(UtilResize.FontType.Message);
            }
        }

        public static void DrawArea(Rect area, Action action)
        {
            Setup();

            // Draw background
            GUI.Box(area, string.Empty);
            GUILayout.BeginArea(area);            

            if (action != null)
                action();

            GUILayout.EndArea();
        }

        public static void DrawCenteredText(string msg)
        {
            Setup();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(msg, centerAlignedLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static void DrawRow(string key, string value)
        {
            Setup();

            GUILayout.BeginHorizontal();
            GUILayout.Label(key, keyLabel);
            GUILayout.FlexibleSpace();
            GUILayout.Label(value, rightAlignedLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static void DrawRow(string text)
        {
            Setup();
            
            GUILayout.Label(text, keyLabel);
        }

        public static bool DrawButton(Rect position, string text)
        {
            return GUI.Button(position, text, btnStyle);
        }

        public static bool DrawButton(string text, float width, float height)
        {
            Setup();
            return GUILayout.Button(text, btnStyle, GUILayout.Width(width-20),
                                                    GUILayout.Height(height*factorY));
        }

        //public static Rect ResizeGUI(Rect rect)
        //{
        //    float FillScreenWidth = rect.width / 320;
        //    float rectWidth = FillScreenWidth * Screen.width;
        //    float FillScreenHeight = rect.height / 480;
        //    float rectHeight = FillScreenHeight * Screen.height;
        //    float rectX = (rect.x / 320) * Screen.width;
        //    float rectY = (rect.y / 480) * Screen.height;

        //    return new Rect(rectX, rectY, rectWidth, rectHeight);
        //}

        public static Rect ResizeButton(Rect rect)
        {
            float FillScreenHeight = rect.height / 480;
            float rectHeight = FillScreenHeight * Screen.height;
            float rectY = (rect.y / 480) * Screen.height;

            return new Rect(0, rectY, Screen.width, rectHeight);
        }

        public static void SetScreenSizeFactor()
        {
            if (Screen.height > Screen.width) //Portrait
            {
                factorX = Screen.width / portraitWidth;
                factorY = Screen.height / portraitHeight;
                fontSizeFactor = factorY;
                
            }
            else
            { //Landscape
                factorX = Screen.width / landscapeWidth;
                factorY = Screen.height / landscapeHeight;
                fontSizeFactor = factorX;
                
            }
        }
    }




    public class GUIMessageList
    {
        System.Collections.Generic.List<string> messages = new System.Collections.Generic.List<string>();
        Vector2 scrollPos;

        public void Draw()
        {
            Draw(Screen.width, 0);
        }

        public void Draw(float minWidth, float minHeight)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, false, GUILayout.MinHeight(minHeight));
            for (int i = 0; i < messages.Count; ++i)
                GUILayout.Label(messages[i], GUILayout.MinWidth(minWidth));
            GUILayout.EndScrollView();
        }

        public void Add(string msg)
        {
            messages.Add(msg);
            scrollPos = new Vector2(scrollPos.x, float.MaxValue);
        }

        public void Clear()
        {
            messages.Clear();
        }
    }
}