//============================================================
// Project: ProjectWatcher
// Author: Zoranner@ZORANNER
// Datetime: 2018-07-18 15:32:14
//============================================================

#if WATCH_DOG
using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Zoranner.ProjectWatcher
{
    [UsedImplicitly]
    public class ScriptEnhance : UnityEditor.AssetModificationProcessor
    {
        private const string SCRIPT_MARK = "//============================================================";

        private const string SCRIPT_ERROR =
            " {\r\n\r\n	// Use this for initialization\r\n	void Start () {\r\n		\r\n	}\r\n	\r\n	// Update is called once per frame\r\n	void Update () {\r\n		\r\n	}\r\n}";

        private static string _FileName;
        private static int _SubIndex;
        private static string _RootPath;
        private static string _FilePath;
        private static string _FileText;
        private static string _PrefixText;
        private static string _ScriptText;

        [UsedImplicitly]
        public static void OnWillCreateAsset(string metaFile)
        {
            _FileName = metaFile.Replace(".meta", "");
            _SubIndex = Application.dataPath.LastIndexOf("Assets", StringComparison.Ordinal);
            _RootPath = Application.dataPath.Substring(0, _SubIndex);
            _FilePath = string.Format("{0}{1}", _RootPath, _FileName);
            if (File.Exists(_FilePath))
            {
                if (Path.GetExtension(_FilePath) == ".cs")
                {
                    _FileText = File.ReadAllText(_FilePath);

                    if (_FileText.IndexOf(SCRIPT_MARK, StringComparison.Ordinal) != 0)
                    {
                        _PrefixText = string.Format(
                            "{0}\r\n// Project: {1}\r\n// Author: {2}@{3}\r\n// Datetime: {4:yyyy-MM-dd HH:mm:ss}\r\n// Description: TODO >> This is a script Description.\r\n{5}",
                            SCRIPT_MARK,
                            PlayerSettings.productName,
                            Environment.UserName,
                            Environment.MachineName, DateTime.Now,
                            SCRIPT_MARK);
                        _ScriptText = _FileText.Replace(SCRIPT_ERROR, "\r\n{\r\n\r\n}");
                        
                        File.WriteAllText(_FilePath, string.Format("{0}\r\n\r\n{1}", _PrefixText, _ScriptText),
                            Encoding.UTF8);

                        AssetDatabase.Refresh();
                    }
                }
            }
        }
    }
}
#endif