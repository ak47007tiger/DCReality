using System;
using System.Collections.Generic;
using UnityEngine;
using DC.ActorSystem;
using DC.DCPhysics;

namespace DC.SkillSystem
{
    public enum CastMsgType
    {
        ok,
        hp_low,
        mp_low,
        cd,
        buff_reject,
    }

    public class CastMsg
    {
        public static readonly CastMsg s_Suc = new CastMsg();

        public CastMsgType mMsgType = CastMsgType.ok;

        public object mAttach;

        public CastMsg()
        {
        }

        public CastMsg(CastMsgType type)
        {
            mMsgType = type;
        }

        public CastMsg(CastMsgType type, object attach)
        {
            mMsgType = type;
            mAttach = attach;
        }

        public bool Suc
        {
            get { return CastMsgType.ok == mMsgType; }
        }

        public bool Error
        {
            get { return !Suc; }
        }
    }
}