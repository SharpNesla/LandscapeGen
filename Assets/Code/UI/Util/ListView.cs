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
        public TElement[] ElementExamples;
        public Transform ElementParent;
        public void Place(IEnumerable<TSource> list)
        {
            foreach (var source in list)
            {
                var prototype = Instantiate(SelectElement(source).gameObject);
                var element = Setup(source ,prototype.GetComponent<TElement>());
                var pos = element.gameObject.GetComponent<RectTransform>();
                pos.parent = ElementParent;
            }
        }
        public abstract TElement Setup(TSource source, TElement element);

        public virtual TElement SelectElement(TSource source)
        {
            return ElementExamples[0];
        }
    }


}