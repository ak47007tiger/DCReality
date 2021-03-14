using System.Collections.Generic;
using UnityEngine;

namespace DC.DCIMGUIBox
{
    public class DCEWidget
    {
        protected List<DCEWidget> children = new List<DCEWidget>();

        protected DCEWindow hostWindow;

        protected DCEWidget parent;

        protected Rect area;

        public DCEWidget(DCEWindow hostWindow)
        {
            this.hostWindow = hostWindow;
        }

        public DCEWidget(DCEWidget parent)
        {
            this.parent = parent;
            this.hostWindow = parent.hostWindow;
        }

        public DCEWidget(DCEWindow hostWindow, DCEWidget parent)
        {
            this.hostWindow = hostWindow;
            this.parent = parent;
        }

        public void SetArea(Rect area)
        {
            this.area = area;
        }

        public bool HasArea()
        {
            return area != default;
        }

        public float GetFinalX()
        {
            if (parent == null)
            {
                return area.x;
            }

            return parent.GetFinalX() + area.x;
        }

        public float GetFinalY()
        {
            if (parent == null)
            {
                return area.x;
            }

            return parent.GetFinalY() + area.x;
        }

        public Rect GetFinalArea()
        {
            return new Rect(GetFinalX(), GetFinalY(), area.width, area.height);
        }

        public bool IsRoot()
        {
            return parent == null;
        }

        public virtual void OnWindowSizeChange()
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

        public virtual void OnGUI()
        {
            var finalArea = GetFinalArea();
//            Debug.Log(finalArea);
            GUILayout.BeginArea(finalArea);
            BeforeChildrenOnGUI();
            GUILayout.EndArea();

            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnGUI();
            }

            GUILayout.BeginArea(finalArea);
            AfterChildrenOnGUI();
            GUILayout.EndArea();
        }

        public virtual void BeforeChildrenOnGUI()
        {
            
        }

        public virtual void AfterChildrenOnGUI()
        {

        }

        public virtual void OnDestroy()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnDestroy();
            }
        }
    }

}
