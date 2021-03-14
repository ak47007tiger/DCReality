using System;
using DC.DCIMGUIBox;
using UnityEditor;
using UnityEngine;

namespace DC.ss.e
{
    /*
     * 绘制时间线
     * 在时间线上拖动触发不同的action
     * 增加节点
     *
     */
    public class TimeLineWindow : DCEWindow
    {
        [MenuItem("DC/IMGUI/TimeLineWindow")]
        public static void Open()
        {
            var window = GetWindow<TimeLineWindow>();
            window.minSize = new Vector2(800, 600);
        }

        public TimeLineDesc timeLineDesc;

        public float duration;

        protected override void OnSetupWidget()
        {
            var widget = Add(new TimeLineSlider(this));
            widget.SetArea(new Rect(0, 0, position.width, TimeLineSlider.HEIGHT));
        }

    }

    
}
