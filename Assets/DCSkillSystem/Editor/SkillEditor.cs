using UnityEditor;
using UnityEngine;

namespace DC.ss.e
{
    /*
     * 打开技能编辑器
     * 如果没有选定技能则显示默认界面
     * 如果选定了技能则加载选定的技能
     * 提供创建技能的菜单
     * 提供加载技能的菜单
     *
     * 技能编辑器采用json方式保存编辑的技能数据
     * 提供技能搜索功能
     */
    public class SkillEditor
    {
        public SkillDesc skillDesc;

        [MenuItem("DC/SkillEditor/Create")]
        public static void Create()
        {
//            var skillDesc = ScriptableObject.CreateInstance<SkillDesc>();
//            AssetDatabase.CreateAsset(skillDesc, "Assets/DCSkillSystem/Assets/temp.asset");
        }

        [MenuItem("DC/SkillEditor/Create")]
        public static void OpenSkillEditor()
        {

        }
    }
}