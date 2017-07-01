using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
using System;
using System.Reflection;
using System.IO;

public static class ShareClassExporterUtility
{
    public static string GetWorpClassText(string className,string text)
    {   
        string classWorp = "";
        classWorp += ReadClassHead(className, text);
        classWorp += "{\n";
        var type = Assembly.LoadFrom(Application.dataPath.Replace("Assets", "Library/ScriptAssemblies") + "/" + "Assembly-CSharp.dll").GetType(className);
        classWorp += GetMembersText(type);
        classWorp += "}\n";
        return classWorp;
    }
    public static string ReadClassHead(string className,string texts)
    {
        string[] lines = texts.Split('\n');
        var str = "";
        for (int i = 0; i < lines.Length; i++)
        {
            if (!lines[i].StartsWith("using") && lines[i].Contains("class"))
            {
                var newline = lines[i].Replace(className, className + "Worp").Replace("{","");
                str += newline + "\n";
                break;
            }
            else
            {
                str += lines[i] + "\n";
            }
        }
        return str;
    }
    public static string GetMembersText(Type type)
    {
        var members = type.GetMembers((BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                              BindingFlags.DeclaredOnly));
        string str = "";
        foreach (var item in members)
        {
            if (item.MemberType == MemberTypes.Field)
            {
                var field = item as FieldInfo;
                var state = field.IsPublic ? "public" : "private";
                str += "\t" + string.Format("{0} {1} {2};", state, ShareClassExporterUtility.GetTypeName(field.FieldType), field.Name) + "\n";
            }
        }
        return str;
    }
    public static string GetTypeName(Type type)
    {
        if (type == typeof(System.Single))
        {
            if (type == typeof(float))
            {
                return "float";
            }
            else if (type == typeof(int))
            {
                return "int";
            }
            else if (type == typeof(double))
            {
                return "double";
            }
            else
            {
                return type.Name;
            }
        }
        else if (type == typeof(System.String))
        {
            if (type == typeof(string))
            {
                return "string";
            }
            else
            {
                return type.Name;
            }
        }
        else
        {
            return type.Name;
        }
    }
}
