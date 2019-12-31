using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace DC.AI
{
    public class AnimatorToFSM : Singleton<AnimatorToFSM>
    {
        /// <summary>
        /// state
        /// translate
        /// state type
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
        public FSMSystem ConvertHero(AnimatorController animator, GameObject ctxObj)
        {
            var stateToHeroState = new Dictionary<AnimatorState, HeroBaseState>();
            var fsm = new FSMSystem();
            var controllerLayer = animator.layers[0];
            var stateMachine = controllerLayer.stateMachine;
            var machineStates = stateMachine.states;

            //所有状态
            foreach (var machineState in machineStates)
            {
                var animatorState = machineState.state;
                stateToHeroState.Add(animatorState, CreateHeroState(animatorState, ctxObj));
            }

            //所有明确的状态关系
            foreach (var machineState in machineStates)
            {
                var animatorState = machineState.state;
                var codeState = stateToHeroState[animatorState];

                var stateTransitions = animatorState.transitions;
                foreach (var transition in stateTransitions)
                {
                    var dstCodeState = AnimatorStateToCodeState(transition.destinationState);
                    var codeTrans = AnimatorTransitionToCodeTransition(transition);
//                    codeState.AddTransition(codeTrans, dstCodeState);
                }
            }

            //任意可进入的
            var anyStateTransitions = stateMachine.anyStateTransitions;
            foreach (var transition in anyStateTransitions)
            {
                var dstCodeState = AnimatorStateToCodeState(transition.destinationState);
                var codeTrans = AnimatorTransitionToCodeTransition(transition);

                foreach (var machineState in machineStates)
                {
                    var animatorState = machineState.state;
                    var codeState = stateToHeroState[animatorState];
//                    codeState.AddTransition(codeTrans, dstCodeState);
                }
            }

            foreach (var item in stateToHeroState)
            {
//                fsm.AddState(item.Value);
            }

            return fsm;
        }

        public Transition AnimatorTransitionToCodeTransition(AnimatorStateTransition animatorStateTransition)
        {
            var dstAnimatorState = animatorStateTransition.destinationState;
            var transition = (Transition) Enum.Parse(typeof(Transition), string.Format("To{0}", dstAnimatorState.name));
            return transition;
        }

        public StateID AnimatorStateToCodeState(string animatorState)
        {
            return (StateID)Enum.Parse(typeof(StateID), animatorState);
        }

        public StateID AnimatorStateToCodeState(AnimatorState animatorState)
        {
            return AnimatorStateToCodeState(animatorState.name);
        }

        public HeroBaseState CreateHeroState(AnimatorState animatorState, GameObject cxtObj)
        {
            var type = AnimatorStateToCodeStateType(animatorState.name, "Hero");

            var stateInst = (HeroBaseState) Activator.CreateInstance(type);
            var stateID = AnimatorStateToCodeState(animatorState);
            stateInst.SetUp(cxtObj);

            return stateInst;
        }

        public Type AnimatorStateToCodeStateType(string animatorState, string roleType)
        {
            var typeName = string.Format("DC.AI.{0}{1}State", roleType, animatorState);
            return Type.GetType(typeName);
        }
    }
}