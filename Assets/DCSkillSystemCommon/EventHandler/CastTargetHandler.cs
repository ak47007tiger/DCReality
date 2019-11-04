using System.Collections.Generic;
using DC.ActorSystem;

namespace DC.SkillSystem
{
    public enum CastTargetType
    {
        single,
        multi
    }

    public class CastTargetHandler : BaseEvtHandler
    {
        public override void OnEvt(params object[] objs)
        {
            Skill skill = (Skill) objs[0];
            CastTargetType type = (CastTargetType) objs[1];

            if (type == CastTargetType.single)
            {
                IActor target = (IActor) objs[2];
                
            }
            else
            {
                List<IActor> targets = (List<IActor>) objs[2];
                
            }

        }

    }

}