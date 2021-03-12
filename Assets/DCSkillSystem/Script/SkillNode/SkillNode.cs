using System;

namespace DC.ss
{
    public class SkillNode
    {
        public bool onlyTickOnce;

        public bool ticked;

        public virtual void Tick(float delta)
        {
            if (onlyTickOnce && ticked)
            {
                return;
            }

            ticked = true;

            OnTick(delta);
        }

        public virtual void OnTick(float delta)
        {

        }

        public virtual bool Exec(object userData)
        {
            throw new NotImplementedException();
        }

    }
}