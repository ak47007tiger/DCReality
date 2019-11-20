using System.Collections.Generic;
using DC.DCResourceSystem;
using DC.GameLogic;
using DC.GameLogic.UI;
using UnityEngine.SceneManagement;

namespace DC.UI
{
    public class SelectHeroUI : BaseUI, IListViewFuncs<HeroCfg, SelectHeroItemUI>
    {
        private SelectHeroUIGen mView;

        List<HeroCfg> allHeroCfgs;

        private SimpleListView<HeroCfg, SelectHeroItemUI> mHeroListView;

        private HeroCfg mCur;
        private HeroCfg mMy;
        private HeroCfg mAi;

        public override void Init(params object[] param)
        {
            base.Init(param);

            allHeroCfgs = HeroConfigMgr.Instance.GetAllHeroCfgs();

            mView = GetComponent<SelectHeroUIGen>();
            mView.HeroItemGameObject.SetActive(false);

            mHeroListView = new SimpleListView<HeroCfg, SelectHeroItemUI>();
            mHeroListView.Init(this);
            mHeroListView.Create();

            mView.confirmButton.onClick.AddListener(OnConfirmBtnClick);
        }

        private void OnConfirmBtnClick()
        {
            if (mMy == null)
            {
                mMy = mCur;
            }

            if (null == mAi)
            {
                mAi = mCur;
            }

            if (null != mAi)
            {
                //to fight scene
                SceneManager.LoadScene("FightScene");
                FightScene.Instance.CreateActor(mMy, PlayerDataMgr.Instance.GetMainActorId());

                FightScene.Instance.CreateActor(mAi, PlayerDataMgr.Instance.GenerateActorId());

                Close();
            }
        }

        public List<HeroCfg> GetDataFunc()
        {
            return allHeroCfgs;
        }

        public SelectHeroItemUI CreateViewItemFunc(int index)
        {
            var instantiate = Instantiate(mView.HeroItemGameObject, mView.heroGridRectTransform);
            instantiate.SetActive(true);

            var itemUi = instantiate.AddComponent<SelectHeroItemUI>();
            var viewGen = itemUi.ViewGen;
            viewGen.headerButton.onClick.AddListener(() =>
            {
                OnItemClick(itemUi, index);
            });
            return itemUi;
        }

        public void UpdateItemUiFunc(HeroCfg data, SelectHeroItemUI itemUi)
        {
            itemUi.ViewGen.nameText.text = data.mName;
            ResourceSys.Instance.SetImage(itemUi.ViewGen.headerImage, data.mUiIcon);
        }

        private void OnItemClick(SelectHeroItemUI itemUi, int index)
        {
            mCur = GetDataFunc()[index];
        }
        
    }

    public class SelectHeroItemUI : BaseItemUI<HeroItemGen>
    {

    }

}
