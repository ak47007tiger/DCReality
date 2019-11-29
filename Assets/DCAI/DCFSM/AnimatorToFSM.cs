using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace DC.AI
{
    public delegate int AnimatorStateToCodeState(AnimatorState state);
    public delegate int AnimatorTransToCodeTrans(AnimatorStateTransition state);
    public delegate DCFSMState CreateDCFSMState(AnimatorState state, GameObject ctxObj);

    public class DCAnimatorToFSM : Singleton<DCAnimatorToFSM>
    {
        public AnimatorStateToCodeState mAStateToCState;
        public AnimatorTransToCodeTrans mATransToCTrans;
        public CreateDCFSMState mCreateState;

        /// <summary>
        /// state
        /// translate
        /// state type
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
        public DCFSM ConvertHero(AnimatorController animator, GameObject ctxObj)
        {
            var aStateToCStateDic = new Dictionary<AnimatorState, DCFSMState>();
            var fsm = new DCFSM();
            var controllerLayer = animator.layers[0];
            var stateMachine = controllerLayer.stateMachine;
            var machineStates = stateMachine.states;

            //所有状态
            foreach (var machineState in machineStates)
            {
                var animatorState = machineState.state;
                aStateToCStateDic.Add(animatorState, mCreateState(animatorState, ctxObj));
            }

            //所有明确的状态关系
            foreach (var machineState in machineStates)
            {
                var animatorState = machineState.state;
                var codeState = aStateToCStateDic[animatorState];

                var stateTransitions = animatorState.transitions;
                foreach (var transition in stateTransitions)
                {
                    var dstCodeState = mAStateToCState(transition.destinationState);
                    var codeTrans = mATransToCTrans(transition);
                    codeState.AddTransition(codeTrans, dstCodeState);
                }
            }

            //任意可进入的
            var anyStateTransitions = stateMachine.anyStateTransitions;
            foreach (var transition in anyStateTransitions)
            {
                var dstCodeState = mAStateToCState(transition.destinationState);
                var codeTrans = mATransToCTrans(transition);

                foreach (var machineState in machineStates)
                {
                    var animatorState = machineState.state;
                    var codeState = aStateToCStateDic[animatorState];
                    codeState.AddTransition(codeTrans, dstCodeState);
                }
            }

            foreach (var item in aStateToCStateDic)
            {
                fsm.AddState(item.Value);
            }

            return fsm;
        }
    }
}