//============================================================
// Project: ProjectWatcher
// Author: Zoranner@ZORANNER
// Datetime: 2018-07-18 22:59:48
//============================================================

#if WATCH_DOG
using System;
using System.Collections;
using System.IO;
using System.Threading;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zoranner.ProjectWatcher
{
    [InitializeOnLoad]
    [UsedImplicitly]
    public class SmugglerKiller
    {
        private static readonly string _MainPath = Application.dataPath;
        private static readonly string _ArtsPath = string.Format("{0}/Arts", _MainPath);
        private static readonly string _ResourcesPath = string.Format("{0}/Resources", _MainPath);
        private static readonly string _ScriptsPath = string.Format("{0}/Scripts", _MainPath);
        private static readonly string _MasterPath = string.Format("{0}/Scripts/Master", _MainPath);
        private static readonly string _StreamingAssetsPath = string.Format("{0}/StreamingAssets", _MainPath);
        private static readonly DirectoryInfo _MainInfo = new DirectoryInfo(_MainPath);
        private static readonly DirectoryInfo _ArtsInfo = new DirectoryInfo(_ArtsPath);
        private static readonly DirectoryInfo _ResourcesInfo = new DirectoryInfo(_ResourcesPath);
        private static readonly DirectoryInfo _ScriptsInfo = new DirectoryInfo(_ScriptsPath);
        private static readonly DirectoryInfo _MasterInfo = new DirectoryInfo(_MasterPath);
        private static readonly DirectoryInfo _StreamingAssetsInfo = new DirectoryInfo(_StreamingAssetsPath);

        static SmugglerKiller()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorApplication.update += KillerWorks;
            }
        }

        private static void KillerWorks()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                //var mainPath = Application.dataPath;
                new Thread(() =>
                {
                    //var artsPath = string.Format("{0}/Arts", mainPath);
                    //var resourcesPath = string.Format("{0}/Resources", mainPath);
                    //var scriptsPath = string.Format("{0}/Scripts", mainPath);
                    //var masterPath = string.Format("{0}/Scripts/Master", mainPath);
                    //var streamingAssetsPath = string.Format("{0}/Scripts/Master", mainPath);
                    //var mainInfo = new DirectoryInfo(mainPath);
                    //var artsInfo = new DirectoryInfo(artsPath);
                    //var resourcesInfo = new DirectoryInfo(resourcesPath);
                    //var scriptsInfo = new DirectoryInfo(scriptsPath);
                    //var masterInfo = new DirectoryInfo(masterPath);
                    //var streamingAssetsInfo = new DirectoryInfo(streamingAssetsPath);

                    if (_MainInfo.Exists)
                    {
                        foreach (var childInfo in _MainInfo.GetDirectories())
                        {
                            if (!(Enum.GetNames(typeof(MainDirectory)) as IList).Contains(childInfo.Name))
                            {
                                //childInfo.Delete(true);
                                Debug.LogError(string.Format(
                                    "发现不规范资源目录[Assets/{0}]，为方便项目维护，建议规范化存放资源，或通过ProjectWatcher脚本配置目录结构。",
                                    childInfo.Name));
                            }
                        }
                    }

                    if (_ArtsInfo.Exists)
                    {
                        foreach (var childInfo in _ArtsInfo.GetDirectories())
                        {
                            if (!(Enum.GetNames(typeof(ArtsDirectory)) as IList).Contains(childInfo.Name))
                            {
                                //childInfo.Delete(true);
                                Debug.LogError(string.Format(
                                    "发现不规范资源目录[Assets/Arts/{0}]，为方便项目维护，建议规范化存放资源，或通过ProjectWatcher脚本配置目录结构。",
                                    childInfo.Name));
                            }
                        }
                    }

                    if (_ResourcesInfo.Exists)
                    {
                        foreach (var childInfo in _ResourcesInfo.GetDirectories())
                        {
                            if (!(Enum.GetNames(typeof(ResourcesDirectory)) as IList).Contains(childInfo.Name))
                            {
                                //childInfo.Delete(true);
                                Debug.LogError(string.Format(
                                    "发现不规范资源目录[Assets/Resources/{0}]，为方便项目维护，建议规范化存放资源，或通过ProjectWatcher脚本配置目录结构。",
                                    childInfo.Name));
                            }
                        }
                    }

                    if (_ScriptsInfo.Exists)
                    {
                        foreach (var childInfo in _ScriptsInfo.GetDirectories())
                        {
                            if (!(Enum.GetNames(typeof(ScriptsDirectory)) as IList).Contains(childInfo.Name))
                            {
                                //childInfo.Delete(true);
                                Debug.LogError(string.Format(
                                    "发现不规范资源目录[Assets/Scripts/{0}]，为方便项目维护，建议规范化存放资源，或通过ProjectWatcher脚本配置目录结构。",
                                    childInfo.Name));
                            }
                        }
                    }

                    if (_MasterInfo.Exists)
                    {
                        foreach (var childInfo in _MasterInfo.GetDirectories())
                        {
                            if (!(Enum.GetNames(typeof(MasterDirectory)) as IList).Contains(childInfo.Name))
                            {
                                //childInfo.Delete(true);
                                Debug.LogError(string.Format(
                                    "发现不规范资源目录[Assets/Scripts/Master/{0}]，为方便项目维护，建议规范化存放资源，或通过ProjectWatcher脚本配置目录结构。",
                                    childInfo.Name));
                            }
                        }
                    }

                    if (_StreamingAssetsInfo.Exists)
                    {
                        foreach (var childInfo in _StreamingAssetsInfo.GetDirectories())
                        {
                            if (!(Enum.GetNames(typeof(StreamingAssetsDirectory)) as IList).Contains(childInfo.Name))
                            {
                                //childInfo.Delete(true);
                                Debug.LogError(string.Format(
                                    "发现不规范资源目录[Assets/StreamingAssets/{0}]，为方便项目维护，建议规范化存放资源，或通过ProjectWatcher脚本配置目录结构。",
                                    childInfo.Name));
                            }
                        }
                    }
                }).Start();
                AssetDatabase.Refresh();
            }

            EditorApplication.update -= KillerWorks;
        }

        //public static void OnWillCreateAsset(string metaName)
        //{
        //    string direName = metaName.Replace(".meta", "");
        //    int subIndex = Application.dataPath.LastIndexOf("Assets");
        //    string rootPath = Application.dataPath.Substring(0, subIndex);
        //    string direPath = string.Format("{0}{1}", rootPath, direName);
        //    string metaPath = string.Format("{0}{1}", rootPath, metaName);
        //    if (!Directory.Exists(direPath))
        //    {
        //        return;
        //    }
        //    DirectoryInfo direInfo = new DirectoryInfo(direPath);
        //    switch (string.Format("{0}/{1}", direInfo.Parent.Name, direInfo.Parent.Parent.Name))
        //    {
        //        case "Develop/Asset":
        //            if ((Enum.GetNames(typeof(MainDirectory)) as IList).Contains(direInfo.Name))
        //            {
        //                return;
        //            }
        //            break;
        //        case "Asset/Arts":
        //            if ((Enum.GetNames(typeof(ArtsDirectory)) as IList).Contains(direInfo.Name))
        //            {
        //                return;
        //            }
        //            break;
        //        case "Asset/Resources":
        //            if ((Enum.GetNames(typeof(ResourcesDirectory)) as IList).Contains(direInfo.Name))
        //            {
        //                return;
        //            }
        //            break;
        //        case "Asset/Scripts":
        //            if ((Enum.GetNames(typeof(ScriptsDirectory)) as IList).Contains(direInfo.Name))
        //            {
        //                return;
        //            }
        //            break;
        //        case "Scripts/Master":
        //            if ((Enum.GetNames(typeof(MasterDirectory)) as IList).Contains(direInfo.Name))
        //            {
        //                return;
        //            }
        //            break;
        //    }
        //    direInfo.Delete(true);
        //    Debug.LogError(metaPath);
        //    if (File.Exists(metaPath))
        //    {
        //        Debug.LogError(metaPath);
        //        File.Delete(metaPath);
        //    }
        //    AssetDatabase.Refresh();
        //}
    }
}
#endif