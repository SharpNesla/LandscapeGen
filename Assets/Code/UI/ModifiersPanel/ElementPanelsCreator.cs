using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.SimpleGenerator;
using Code.Core;
using Code.Core;
using Code.UI.Util;
using Code.Util;
using UnityEngine;
namespace Code.UI.ModifiersPanel
{
    public class ElementPanelsCreator : ListView<MonoBehaviour, ElementController>
    {
        public List<MonoBehaviour> Modifiers;
        private RectTransform _contentTransform;
        private void Start()
        {
            Modifiers = GameObject.FindGameObjectsWithTag("HoldElements")
                .SelectMany(x=>x.GetComponents<IInterfaceElement>())
                .Select(x=> x as MonoBehaviour)
                .ToList();
            Place(Modifiers);
        }

        public override ElementController Setup(MonoBehaviour source, ElementController element)
        {
            element.Modifier = source;
            return element;
        }

        public void Refresh()
        {
            foreach (var monoBehaviour in Modifiers)
            {
                var interfaceElement = monoBehaviour as IInterfaceElement;
                if (interfaceElement != null)
                {
                    interfaceElement.Refresh();
                }
            }
        }
    }
}