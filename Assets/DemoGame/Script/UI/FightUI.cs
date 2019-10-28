using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DC
{
    public class FightUI : BaseMonoBehaviour
    {
        public Image mMousePoint;

        void Awake()
        {
            
        }

        void Update()
        {
            CacheRectTransform.anchoredPosition = Input.mousePosition;
        }
        public Texture2D cursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;
        void OnMouseEnter()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
