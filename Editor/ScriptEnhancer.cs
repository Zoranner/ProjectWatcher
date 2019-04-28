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

        [UsedImplicitly]
        public static void OnWillCreateAsset(string metaFile)
        {
            var fileName = metaFile.Replace(".meta", "");
            var subIndex = Application.dataPath.LastIndexOf("Assets", StringComparison.Ordinal);
            var rootPath = Application.dataPath.Substring(0, subIndex);
            var filePath = string.Format("{0}{1}", rootPath, fileName);
            if (!File.Exists(filePath))
            {
                return;
            }

            if (Path.GetExtension(filePath) != ".cs")
            {
                return;
            }

            var fileText = File.ReadAllText(filePath);
            if (fileText.IndexOf(SCRIPT_MARK, StringComparison.Ordinal) == 0)
            {
                return;
            }

            var prefix = string.Format(
                "{0}\r\n// Project: {1}\r\n// Author: {2}@{3}\r\n// Datetime: {4:yyyy-MM-dd HH:mm:ss}\r\n// Description: TODO >> This is a script Description.\r\n{5}",
                SCRIPT_MARK,
                PlayerSettings.productName,
                Environment.UserName,
                Environment.MachineName, DateTime.Now,
                SCRIPT_MARK);
            var script = string.Format(
                "using UnityEngine;\r\n\r\nnamespace {0}.{1}\r\n{{\r\n\tpublic class {2} : MonoBehaviour\r\n\t{{\r\n\r\n\t}}\r\n}}",
                PlayerSettings.companyName,
                PlayerSettings.productName,
                Path.GetFileNameWithoutExtension(filePath));
            File.WriteAllText(filePath, string.Format("{0}\r\n\r\n{1}", prefix, script), Encoding.UTF8);
            AssetDatabase.Refresh();
        }
    }
}
#endif