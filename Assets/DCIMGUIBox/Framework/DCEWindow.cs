using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class DCEWindow : EditorWindow
    {
        protected bool hasSetupWidget = false;

        protected Rect previousWinSize;

        protected List<DCEWidget> children = new List<DCEWidget>();

        public virtual void Awake()
        {
            previousWinSize = position;
        }

        public virtual void OnGUI()
        {
            if (!hasSetupWidget)
            {
                OnSetupWidget();
                hasSetupWidget = true;
            }

            if (previousWinSize != position)
            {
                OnWindowSizeChange(previousWinSize, position);
                previousWinSize = position;
            }

            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnGUI();
            }
        }

        protected virtual void OnSetupWidget()
        {

        }

        public virtual void OnWindowSizeChange(Rect old, Rect cur)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnWindowSizeChange();
            }
        }

        public DCEWidget Add(DCEWidget widget)
        {
            if (children.Contains(widget))
            {
                return widget;
            }

            children.Add(widget);

            return widget;
        }

        public void Remove(DCEWidget widget)
        {
            children.Remove(widget);
        }

        public bool Has(DCEWidget widget)
        {
            return children.Contains(widget);
        }
    }
}