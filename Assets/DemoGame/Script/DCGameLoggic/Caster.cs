using System.Collections.Generic;
using DC.SkillSystem;
using DC.ActorSystem;
using DC.ValueSys;
using UnityEngine;

namespace DC.GameLogic
{
    public class Caster : GameElement, ICaster
    {
        public CastMsg ConsumeEnough(ISkill skill)
        {
            var values = GetActor().GetValueComponent();
            var mp = values.GetValue(GValueType.mp);
            var consumes = skill.GetSkillCfg().GetConsumes();

            return CastMsg.s_Suc;
        }

        public CastMsg CdEnough(ISkill skill)
        {
            return CastMsg.s_Suc;
        }

        public Transform GetCastTransform(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool Cast(SkillCfg skillCfg)
        {
            var castCfg = GetCastSystem().GetDefaultCastCfg(skillCfg);
            return Cast(skillCfg, castCfg);
        }

        public bool Cast(SkillCfg skillCfg, CastCfg castCfg)
        {
            LogDC.LogEx("cast", skillCfg.mId);

            var skill = GetSkillSystem().CreateSkill(skillCfg);
            skill.SetCaster(this);
            skill.SetCastCfg(castCfg);

            /*
             判断是否可以释放
             buff
                 沉默
                 晕眩
             能量
             cd
             */

            var buffAllowCast = BuffAllowCast(skill);
            if (buffAllowCast.Error)
            {
                return false;
            }

            var consumeEnough = ConsumeEnough(skill);
            if (consumeEnough.Error)
            {
                return false;
            }

            if (CdEnough(skill).Error)
            {
                return false;
            }

            switch (skillCfg.mTargetType)
            {
                case SkillTargetType.Actor:
                {
                    foreach (var target in castCfg.mTargets)
                    {
                        if (!skill.AllowCastTo(target))
                        {
                            return false;
                        }
                    }
                }
                    break;
            }

            skill.Apply();
            return true;
        }

        public void SetSkillActive(SkillCfg skillCfg, bool active)
        {
            throw new System.NotImplementedException();
        }

        public List<CastCfg> GetActiveCastCfgs()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCastConfig(ICastInput input)
        {
            throw new System.NotImplementedException();
        }

        public List<ISkill> GetActiveSkills()
        {
            throw new System.NotImplementedException();
        }

        public ISkill GetActiveSkill()
        {
            throw new System.NotImplementedException();
        }

        public IActor GetActor()
        {
            throw new System.NotImplementedException();
        }

        public CastMsg BuffAllowCast(ISkill skill)
        {
            var ownerBuffs = GetActor().GetOwnerBuffs();
            var rejectBuff = ownerBuffs.Find((buff => !buff.AllowCast(skill)));
            if (null == rejectBuff)
            {
                return CastMsg.s_Suc;
            }

            return new CastMsg(CastMsgType.buff_reject, rejectBuff);
        }
    }
}