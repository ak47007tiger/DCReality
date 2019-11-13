using DC.ActorSystem;
using DC.GameLogic;
using DC.SkillSystem;

namespace DC.UI
{
    public class SkillsUI : BaseMonoBehaviour
    {
        public SkillItemUI[] mSkillItemUis;

        public HeroEntity mHero;

        void Awake()
        {
            var mainActor = ActorSys.Instance.GetMainActor();
            mHero = mainActor.GetTransform().GetComponent<HeroEntity>();
            var heroCfg = mHero.mHeroCfg;

            var keyToSkill = heroCfg.mKeyToSkillPairList;
            for (var i = 0; i < keyToSkill.Count; i++)
            {
                var key = keyToSkill[i].Key;
                var value = keyToSkill[i].Value[0];
                var skillCfg = SkillSys.Instance.GetSkillCfg(value);

                var viewItem = mSkillItemUis[i];
                viewItem.mSendKeyCode = key;
                viewItem.UpdateUi(skillCfg);
            }
        }
    }
}