using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace Code.UI.Util
{
    public abstract class ListView<TSource, TElement> : MonoBehaviour where TElement : MonoBehaviour
    {
        private RectTransform _backGround;

        public float Spacing;
        public RectTransform PlaceHolder;
        public TElement ElementExample;
        public Transform ElementParent;
        public float Place(IEnumerable<TSource> list,float startOffset)
        {
            var cursorX = -Spacing -startOffset;
            foreach (var source in list)
            {
                var element = Instantiate(ElementExample.gameObject).GetComponent<TElement>();
                Setup(source, element);
                var pos = element.gameObject.GetComponent<RectTransform>();
                pos.parent = ElementParent;
                pos.anchoredPosition3D = Vector3.zero;
                pos.anchoredPosition3D = new Vector3(0,cursorX,0);
                cursorX -= Spacing + pos.rect.height;
            }
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0,-cursorX);
            return -cursorX;

        }

        public abstract void Setup(TSource source, TElement element);
    }


}