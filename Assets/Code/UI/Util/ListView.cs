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
        public TElement ElementExample;
        public Transform ElementParent;
        public void Place(IEnumerable<TSource> list)
        {
            foreach (var source in list)
            {
                var element = Instantiate(Setup(source)).GetComponent<TElement>();
                var pos = element.gameObject.GetComponent<RectTransform>();
                pos.parent = ElementParent;
            }
        }
        public abstract TElement Setup(TSource source);
    }


}