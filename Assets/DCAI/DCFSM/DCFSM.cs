using System.Collections.Generic;
using UnityEngine;

namespace DC.AI
{
    public abstract class DCFSMState
    {
        public const int common_none_trans = 0;
        public const int common_none_state = 0;

        protected Dictionary<int, int> map = new Dictionary<int, int>();

        protected int stateID;
        public int ID { get { return stateID; } }

        public void AddTransition(int trans, int id)
        {
            // Check if anyone of the args is invalid
            if (trans == common_none_trans)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
                return;
            }

            if (id == common_none_state)
            {
                Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
                return;
            }

            // Since this is a Deterministic FSM,
            //   check if the current transition was already inside the map
            if (map.ContainsKey(trans))
            {
                Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                               "Impossible to assign to another state");
                return;
            }

            map.Add(trans, id);
        }

        /// <summary>
        /// This method deletes a pair transition-state from this state's map.
        /// If the transition was not inside the state's map, an ERROR message is printed.
        /// </summary>
        public void DeleteTransition(int trans)
        {
            // Check for NullTransition
            if (trans == common_none_trans)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return;
            }

            // Check if the pair is inside the map before deleting
            if (map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }
            Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                           " was not on the state's transition list");
        }

        /// <summary>
        /// This method returns the new state the FSM should be if
        ///    this state receives a transition and 
        /// </summary>
        public int GetOutputState(int trans)
        {
            // Check if the map has this transition
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }
            return common_none_state;
        }

        /// <summary>
        /// This method is used to set up the State condition before entering it.
        /// It is called automatically by the FSMSystem class before assigning it
        /// to the current state.
        /// </summary>
        public virtual void DoBeforeEntering() { }

        /// <summary>
        /// This method is used to make anything necessary, as reseting variables
        /// before the FSMSystem changes to another one. It is called automatically
        /// by the FSMSystem before changing to a new state.
        /// </summary>
        public virtual void DoBeforeLeaving() { }

        /// <summary>
        /// This method decides if the state should transition to another on its list
        /// NPC is a reference to the object that is controlled by this class
        /// </summary>
        public abstract void Reason(object data);

        /// <summary>
        /// This method controls the behavior of the NPC in the game World.
        /// Every action, movement or communication the NPC does should be placed here
        /// NPC is a reference to the object that is controlled by this class
        /// </summary>
        public abstract void Act(object data);
    }

    public class DCFSM
    {
        Dictionary<int, DCFSMState> states;

        /// <summary>
        /// 栈顶始终是当前的状态
        /// </summary>
        Stack<DCFSMState> mStateStack;

        // The only way one can change the state of the FSM is by performing a transition
        // Don't change the CurrentState directly
        private int currentStateID;
        public int CurrentStateID { get { return currentStateID; } }
        private DCFSMState currentState;
        public DCFSMState CurrentState { get { return currentState; } }

        public DCFSM()
        {
            states = new Dictionary<int, DCFSMState>();
            mStateStack = new Stack<DCFSMState>();
        }

        /// <summary>
        /// This method places new states inside the FSM,
        /// or prints an ERROR message if the state was already inside the List.
        /// First state added is also the initial state.
        /// </summary>
        public void AddState(DCFSMState s)
        {
            // Check for Null reference before deleting
            if (s == null)
            {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
            }

            // First State inserted is also the Initial state,
            //   the state the machine is in when the simulation begins
            if (states.Count == 0)
            {
                states.Add(s.ID, s);
                currentState = s;
                currentStateID = s.ID;
                return;
            }

            // Add the state to the List if it's not inside it
            if (states.ContainsKey(s.ID))
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                                   " because state has already been added");
                return;
            }
            states.Add(s.ID, s);
        }

        /// <summary>
        /// This method delete a state from the FSM List if it exists, 
        ///   or prints an ERROR message if the state was not on the List.
        /// </summary>
        public void DeleteState(int id)
        {
            // Check for NullState before deleting
            if (id == DCFSMState.common_none_state)
            {
                Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
                return;
            }

            // Search the List and delete the state if it's inside it
            if (states.Remove(id))
            {
                return;
            }
            Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                           ". It was not on the list of states");
        }

        /// <summary>
        /// This method tries to change the state the FSM is in based on
        /// the current state and the transition passed. If current state
        ///  doesn't have a target state for the transition passed, 
        /// an ERROR message is printed.
        /// </summary>
        public void PerformTransition(int trans)
        {
            // Check for NullTransition before changing the current state
            if (trans == DCFSMState.common_none_trans)
            {
                Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
                return;
            }

            // Check if the currentState has the transition passed as argument
            var id = currentState.GetOutputState(trans);
            if (id == DCFSMState.common_none_state)
            {
                Debug.LogError("FSM ERROR: State " + currentStateID.ToString() + " does not have a target state " +
                               " for transition " + trans.ToString());
                return;
            }

            // Update the currentStateID and currentState		
            currentStateID = id;
            if(states.TryGetValue(id, out var state))
            {
                // Do the post processing of the state before setting the new one
                currentState.DoBeforeLeaving();

                currentState = state;

                //如果栈里面已经有这个状态，删除这个状态
                Toolkit.RemoveFromStack(mStateStack, currentState);
                //加到状态栈
                mStateStack.Push(currentState);

                // Reset the state to its desired condition before it can reason or act
                currentState.DoBeforeEntering();
            }
        }

        public void BackToPreviousState()
        {
            if(mStateStack.Count > 1)
            {
                var top = mStateStack.Pop();
                top.DoBeforeLeaving();

                var previous = mStateStack.Pop();
                currentStateID = previous.ID;
                currentState = previous;

                previous.DoBeforeEntering();
            }
        }
    }

}
