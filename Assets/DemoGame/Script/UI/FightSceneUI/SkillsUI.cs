using System.Collections.Generic;
using DC.ActorSystem;
using DC.GameLogic;
using DC.GameLogic.UI;
using DC.SkillSystem;

namespace DC.UI
{
    public class SkillsUI : BaseUI
    {
        public HeroEntity mHero;

        private SimpleListView<KeyToSkill, SkillItemUI> mSkillListView;
        private SkillListViewFunc listViewFuncs;

        void Awake()
        {
            var mainActor = ActorSys.Instance.GetMainActor();
            mHero = mainActor.GetTransform().GetComponent<HeroEntity>();
        }

        void Start()
        {
            var heroCfg = mHero.mHeroCfg;

            mSkillListView = new SimpleListView<KeyToSkill, SkillItemUI>();

            listViewFuncs = new SkillListViewFunc();
            listViewFuncs.MainUi = this;
            listViewFuncs.SetParent(CacheTransform)
                .SetData(heroCfg.mKeyToSkillPairList);

            mSkillListView.Init(listViewFuncs);
            mSkillListView.Create();
        }

        public void OnSkillIconClick(int index)
        {
            var keyCode = listViewFuncs.GetDataFunc()[index].Key;
            GameInput.Instance.AddDownKeyCode(keyCode);
        }

        class SkillListViewFunc : BaseListViewFuncs<KeyToSkill, SkillItemUI>
        {
            public SkillsUI MainUi;

            public override SkillItemUI CreateViewItemFunc(int index)
            {
                var viewItem = UIManager.Instance.CreateUiInstance<SkillItemUI>(mItemParentTf);
                var skillItemUiGen = viewItem.ViewGen;
                skillItemUiGen.iconButton.onClick.AddListener(() =>
                {
                    MainUi.OnSkillIconClick(index);
                });
                return viewItem;
            }

            public override void UpdateItemUiFunc(KeyToSkill itemData, SkillItemUI itemUi)
            {
                var key = itemData.Key;
                itemUi.mSendKeyCode = key;

                var value = itemData.Value[0];
                var skillCfg = SkillConfigMgr.Instance.GetSkillCfg(value);
                itemUi.UpdateUi(skillCfg);
            }
        }
    }

}
