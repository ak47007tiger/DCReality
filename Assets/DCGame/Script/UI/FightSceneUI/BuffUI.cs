using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DC.GameLogic;
using DC.ActorSystem;
using DC.SkillSystem;

namespace DC.UI
{
    public class BuffItemUI : BaseItemUI<BuffItemUI>
    {

        public Button iconButton;
        public Image iconImage;

        public Image cdImage;


        void Awake()
        {
            iconButton = transform.Find("cm_icon").GetComponent<Button>();
            iconImage = transform.Find("cm_icon").GetComponent<Image>();

            cdImage = transform.Find("cm_cd").GetComponent<Image>();


        }

    }

    public class BuffUI : BaseUI
    {
        GameActor mGameActor;

        

        private void Start()
        {
            GameActor gameActor = ActorSys.Instance.GetMainActor();
            mGameActor = gameActor;
            BuffCmpt buffCmpt = mGameActor.GetBuffCmpt();
            buffCmpt.AddOnBuffAddListener(OnAddBuff);
            buffCmpt.AddOnRemoveAddListener(OnRemoveBuff);
        }

        void OnAddBuff(Buff buff)
        {
            UpdateUi();
        }

        void OnRemoveBuff(Buff buff)
        {
            UpdateUi();
        }

        void UpdateUi()
        {

        }
    }
}
