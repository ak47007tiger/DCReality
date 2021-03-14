using System;
using DC.DCIMGUIBox;
using UnityEditor;
using UnityEngine;

namespace DC.ss.e
{
    //横条，拖拽后调整当前进度
    public class TimeLineSlider : DCEWidget
    {
        public static float HEIGHT = 40;

        public static float SLIDE_POINT_WIDTH = 20;

        public Rect areaRect;

        public Rect sizeRect;

        public Rect sliderPointSize;

        public Color sliderPointColor;

        public float value;

        private EventType previousEvtType = EventType.MouseUp;

        public TimeLineSlider(DCEWindow hostWindow):base(hostWindow)
        {
            sliderPointColor = new Color(0, 0, 0, 0.5f);

            OnWindowSizeChange();
        }

        public override void OnWindowSizeChange()
        {
            var position = hostWindow.position;
            areaRect = new Rect(0, 0, position.width, HEIGHT);
            sizeRect = new Rect(0, 0, areaRect.width, areaRect.height);
            sliderPointSize = new Rect(0, 0, SLIDE_POINT_WIDTH, sizeRect.height);
        }

        public override void BeforeChildrenOnGUI()
        {
            EditorGUI.DrawRect(sizeRect, new Color(0, 0, 0, 0.2f));

            var evt = Event.current;

//            Debug.Log(string.Format("{0}, {1}", evt.type, evt.mousePosition));

            if (previousEvtType == EventType.MouseDown)
            {
//                Debug.Log(evt.mousePosition);
                if (evt.type == EventType.MouseDrag)
                {
                    UpdateValue();
                }
            }

            if (evt.type == EventType.MouseDown)
            {
                previousEvtType = evt.type;
                UpdateValue();
            }

            if (evt.type == EventType.MouseUp)
            {
                previousEvtType = evt.type;
                UpdateValue();
            }

            var slidePointRect = new Rect(value * (sizeRect.width - sliderPointSize.width), 0, sliderPointSize.width, sliderPointSize.height);
//            Debug.Log(string.Format("slidePointRect: {0}, {1}, {2}", value, sizeRect.width, sliderPointSize.width));
            EditorGUI.DrawRect(slidePointRect, sliderPointColor);
        }

        private void UpdateValue()
        {
            var evt = Event.current;
            var value = evt.mousePosition.x / sizeRect.width;
//            Debug.Log(string.Format("UpdateValue: {0}, {1}, {2}", evt.mousePosition, sizeRect.width, value));
            SetValue(value);
        }

        public void SetValue(float value)
        {
//            Debug.Log("set value " + value);
            this.value = Math.Min(1, Math.Max(0, value));
            hostWindow.Repaint();
        }
    }
}