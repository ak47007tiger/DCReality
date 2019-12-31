using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DC
{
    [CreateAssetMenu(fileName = "ViewFinderSetting", menuName = "DC/ViewFinder/CreateSetting", order = 1)]
    public class ViewFinderSetting : ScriptableObject
    {
        public string mScriptSavePath;

        public static ViewFinderSetting GetSetting()
        {
            return AssetDatabase.LoadAssetAtPath<ViewFinderSetting>(
                "Assets/Editor Default Resources/ViewFinderSetting.asset");
        }
    }

}

