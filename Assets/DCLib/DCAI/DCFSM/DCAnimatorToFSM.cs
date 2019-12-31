using System;
using System.Collections.Generic;
using SimpleJSON;
using TreeEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace DC.AI
{
/*
状态机转换
defaultState
states
    [1,2,3...]
relations
    [state:[{trans,state}]...]
*/

    public class DCAnimatorToFSM : Singleton<DCAnimatorToFSM>
    {
        public DCFSM Convert(string jsonCfg, Convert<DCFSMState, int> StateIdToState)
        {
            var json = JSON.Parse(jsonCfg);

            var stateIds = json["states"].AsArray;
            var stateIdToStateDic = new Dictionary<int, DCFSMState>();
            for (var i = 0; i < stateIds.Count; i++)
            {
                var stateId = stateIds[i].AsInt;
                var dcState = StateIdToState(stateId);
                dcState.ID = stateId;
                stateIdToStateDic.Add(stateId, dcState);
            }

            var relations = json["relations"].AsObject;
            for (var i = 0; i < stateIds.Count; i++)
            {
                var stateId = stateIds[i].AsInt;
                var srcState = stateIdToStateDic[stateId];
                var relation = relations[stateId.ToString()].AsArray;
                for (var j = 0; j < relation.Count; j++)
                {
                    var relationItem = relation[j];
                    var trans = relationItem["trans"].AsInt;
                    var dstStateId = relationItem["state"];
                    srcState.AddTransition(trans, dstStateId);
                }
            }

            var fsm = new DCFSM();
            var defaultStateId = json["defaultState"].AsInt;
            fsm.AddState(stateIdToStateDic[defaultStateId]);
            for (var i = 0; i < stateIds.Count; i++)
            {
                var srcStateId = stateIds[i].AsInt;

                if (defaultStateId == srcStateId)
                {
                    continue;
                }

                var srcState = stateIdToStateDic[srcStateId];
                fsm.AddState(srcState);
            }

            return fsm;
        }
    }
}