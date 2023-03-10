using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    public class GamePubSDKSettings : ScriptableObject
    {
        public const string settingsAssetName = "GamePubSDKSettings";
        public const string settingsPath = "GamePubSDK/Resources";
        public const string settingsAssetExtension = ".asset";
        public const string unitySdkVersion = "1.1.10";
        public const string androidSdkVersion = "1.1.30";
        public const string iosSdkVersion = "1.1.9";

        private static GamePubSDKSettings instance;

        public static void SetInstance(GamePubSDKSettings settings)
        {
            instance = settings;
        }

        public static GamePubSDKSettings Instance
        {
            get
            {
                if (ReferenceEquals(instance, null))
                {
                    instance = Resources.Load(settingsAssetName) as GamePubSDKSettings;
                    if (ReferenceEquals(instance, null))
                    {
                        instance = CreateInstance<GamePubSDKSettings>();
                    }
                }
                return instance;
            }
        }
        
        [SerializeField]
        private string projectId = "";
        [SerializeField]
        private bool devBuild = true;

        public static string ProjectId
        {
            get { return Instance.projectId; }
            set { Instance.projectId = value; }
        }

        public static bool DevBuild
        {
            get { return Instance.devBuild; }
            set { Instance.devBuild = value; }
        }
    }
}