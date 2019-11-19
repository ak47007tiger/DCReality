using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DC
{
    [CreateAssetMenu(fileName = "ViewFinderSetting", menuName = "DC/ViewFinder/CreateSetting", order = 1)]
    public class ViewFinderSetting : ScriptableObject
    {
        public string mScriptSavePath;
    }

}

