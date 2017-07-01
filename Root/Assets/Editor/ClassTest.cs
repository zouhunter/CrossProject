using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;
using System.IO;
using System.Reflection;
using System;

public class ClassTest {

	[Test]
	public void PrintClass1Infomations() {
        var assetPath = "Assets/Class1.cs";

        Debug.Log("1.guid:" + AssetDatabase.AssetPathToGUID(assetPath));

        MonoScript mono = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
        Debug.Log("2.Text:" + mono.text);

        Debug.Log("3.head\n"+ShareClassExporterUtility.ReadClassHead("Class1",mono.text));

        Type type = typeof(Class1);
        var members = type.GetMembers((BindingFlags.Instance |BindingFlags.Public |BindingFlags.NonPublic |
                                                BindingFlags.DeclaredOnly));
        foreach (var item in members)
        {
            if (item.MemberType == MemberTypes.Field)
            {
                var field = item as FieldInfo;
                var state = field.IsPublic ? "public" : "private";
                Debug.Log(string.Format("{0} {1} {2};",state, ShareClassExporterUtility.GetTypeName(field.FieldType),field.Name));
            }
        }
    }
    [Test]
    public void GenerateNewClassText()
    {
        var assetPath = "Assets/Class1.cs";
        var metaPath = assetPath + ".meta";
        var exportPath = "Export/Class1Worp.cs";
        var exportmetaPath = "Export/Class1Worp.cs.meta";
        MonoScript mono = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
        string class1Worp = ShareClassExporterUtility.GetWorpClassText("Class1", mono.text);
        File.Delete(Path.GetFullPath(exportmetaPath));
        File.WriteAllText(Path.GetFullPath(exportPath), class1Worp);
        FileUtil.CopyFileOrDirectory(Path.GetFullPath(metaPath), Path.GetFullPath(exportmetaPath));
    }
}
