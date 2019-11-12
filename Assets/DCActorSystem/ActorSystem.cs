using System;
using System.Collections;
using System.Collections.Generic;
using DC.Collections.Generic;
using UnityEngine;
using DC.SkillSystem;
using DC.ValueSys;

/*
 和SkillSystem有耦合关系
 */

namespace DC.ActorSystem
{
    public interface IActorSystem
    {
        IActor GetActor(int id);

        IActor GetMainActor();

        void SetMainActor(IActor actor);
    }

    public class ActorSys : Singleton<ActorSys>, IActorSystem
    {
        private Dictionary<int, IActor> mIdToActor = new Dictionary<int, IActor>();

        private IActor mMainActor;

        public IActor GetActor(int id)
        {
            //dic[key] if not has key, it will throw exception
            return mIdToActor.GetValEx(id);
        }

        public IActor GetMainActor()
        {
            return mMainActor;
        }

        public void SetMainActor(IActor actor)
        {
            mMainActor = actor;
        }
    }

    public enum RoleType
    {
        Hero,
        Soldier,
        Building,
    }

    public enum ActorSide
    {
        neutral,
        blue,
        red,
    }

    public enum SideRelation
    {
        neutral,
        friend,
        enemy,
    }

    public enum EffectType
    {
        neutral,
        friend,
        enemy,
    }

    public enum ActorPos
    {
        root,

        head,

        body,
        body_front,
        body_back,
        body_left,
        body_right,
        body_top,
        body_bottom,

        hand_left,
        hand_right,

        foot_left,
        foot_right,
    }
    
}