//============================================================
// Project: ProjectWatcher
// Author: Zoranner@ZORANNER
// Datetime: 2018-07-18 22:51:26
//============================================================

#if WATCH_DOG
using System.IO;
using System.Threading;
#else
using System.Linq;
#endif
using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zoranner.ProjectWatcher
{
    [InitializeOnLoad]
    [UsedImplicitly]
    public class ProjectBuilder
    {
#if WATCH_DOG
        private static readonly string _DataPath = Application.dataPath;
#else
    private const string SYMBOLS_DEFINE = "WATCH_DOG";
#endif

        static ProjectBuilder()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorApplication.update += BuilderWorks;
            }
        }

        private static void BuilderWorks()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
#if WATCH_DOG
                // Fix ProjectSetting
                PlayerSettings.fullScreenMode = FullScreenMode.FullScreenWindow;
                PlayerSettings.defaultIsNativeResolution = true;
                PlayerSettings.runInBackground = true;
                PlayerSettings.captureSingleScreen = true;
                PlayerSettings.usePlayerLog = true;
                PlayerSettings.resizableWindow = true;
                PlayerSettings.visibleInBackground = true;
                PlayerSettings.allowFullscreenSwitch = true;
                PlayerSettings.forceSingleInstance = true;
                PlayerSettings.SetAspectRatio(AspectRatio.Aspect16by10, true);
                PlayerSettings.SetAspectRatio(AspectRatio.Aspect16by9, true);
                PlayerSettings.SetAspectRatio(AspectRatio.Aspect4by3, true);
                PlayerSettings.SetAspectRatio(AspectRatio.Aspect5by4, true);
                PlayerSettings.SetAspectRatio(AspectRatio.AspectOthers, true);
                PlayerSettings.SplashScreen.show = false;
                PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
                PlayerSettings.useAnimatedAutorotation = true;
                PlayerSettings.allowedAutorotateToPortrait = false;
                PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
                PlayerSettings.allowedAutorotateToLandscapeLeft = true;
                PlayerSettings.allowedAutorotateToLandscapeRight = true;
                PlayerSettings.iOS.requiresFullScreen = true;
                PlayerSettings.statusBarHidden = true;
                PlayerSettings.iOS.statusBarStyle = iOSStatusBarStyle.Default;
                PlayerSettings.Android.disableDepthAndStencilBuffers = false;
                PlayerSettings.iOS.showActivityIndicatorOnLoading = iOSShowActivityIndicatorOnLoading.DontShow;
                PlayerSettings.use32BitDisplayBuffer = true;
                PlayerSettings.Android.showActivityIndicatorOnLoading = AndroidShowActivityIndicatorOnLoading.DontShow;
                new Thread(() =>
                {
                    foreach (var subType in Enum.GetNames(typeof(MainDirectory)))
                    {
                        var subDire = string.Format(@"{0}\{1}", _DataPath, subType);
                        if (!Directory.Exists(subDire))
                        {
                            Directory.CreateDirectory(subDire);
                        }
                    }

                    foreach (var subType in Enum.GetNames(typeof(ArtsDirectory)))
                    {
                        var subDire = string.Format(@"{0}\Arts\{1}", _DataPath, subType);
                        if (!Directory.Exists(subDire))
                        {
                            Directory.CreateDirectory(subDire);
                        }
                    }

                    foreach (var subType in Enum.GetNames(typeof(ResourcesDirectory)))
                    {
                        var subDire = string.Format(@"{0}\Resources\{1}", _DataPath, subType);
                        if (!Directory.Exists(subDire))
                        {
                            Directory.CreateDirectory(subDire);
                        }
                    }

                    foreach (var subType in Enum.GetNames(typeof(ScriptsDirectory)))
                    {
                        var subDire = string.Format(@"{0}\Scripts\{1}", _DataPath, subType);
                        if (!Directory.Exists(subDire))
                        {
                            Directory.CreateDirectory(subDire);
                        }
                    }

                    foreach (var subType in Enum.GetNames(typeof(MasterDirectory)))
                    {
                        var subDire = string.Format(@"{0}\Scripts\Master\{1}", _DataPath, subType);
                        if (!Directory.Exists(subDire))
                        {
                            Directory.CreateDirectory(subDire);
                        }
                    }

                    foreach (var subType in Enum.GetNames(typeof(StreamingAssetsDirectory)))
                    {
                        var subDire = string.Format(@"{0}\StreamingAssets\{1}", _DataPath, subType);
                        if (!Directory.Exists(subDire))
                        {
                            Directory.CreateDirectory(subDire);
                        }
                    }
                }).Start();
#elif WATCH_DOG_DEAD
#else
            var targets = Enum.GetValues(typeof(BuildTargetGroup))
                .Cast<BuildTargetGroup>()
                .Where(x => x != BuildTargetGroup.Unknown)
                .Where(x => !IsObsolete(x));

            foreach (var target in targets)
            {
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target).Trim();

                var list = defines.Split(';', ' ')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

                if (list.Contains(SYMBOLS_DEFINE))
                    continue;

                list.Add(SYMBOLS_DEFINE);
                defines = list.Aggregate((a, b) => a + ";" + b);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
            }
#endif
                AssetDatabase.Refresh();
            }

            EditorApplication.update -= BuilderWorks;
        }

#if WATCH_DOG
#elif WATCH_DOG_DEAD
#else
        private static bool IsObsolete(BuildTargetGroup group)
        {
            var attrs = typeof(BuildTargetGroup)
                .GetField(group.ToString())
                .GetCustomAttributes(typeof(ObsoleteAttribute), false);

            return attrs.Length > 0;
        }
#endif
    }
}