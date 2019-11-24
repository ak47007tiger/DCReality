using System.Collections.Generic;
using UnityEngine;

namespace DC.GameLogic
{

    public class GameInput : SingletonMono<GameInput>
    {
        private HashSet<KeyCode> mDownKeyCodes = new HashSet<KeyCode>();

        void Update()
        {
            Reset();
        }

        public void Reset()
        {
            mDownKeyCodes.Clear();
        }

        public void AddDownKeyCode(KeyCode downKey)
        {
            mDownKeyCodes.Add(downKey);
        }

        public bool GetKeyDown(KeyCode code)
        {
            return Input.GetKeyDown(code) || mDownKeyCodes.Contains(code);
        }

    }

}