using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.SimpleGenerator;
using Code.Core;
using Code.Core;
using Code.UI.Util;
using UnityEngine;
namespace Code.UI.ModifiersPanel
{
    public class ModifierPanelsCreator : ListView<MonoBehaviour, ModifierController>
    {
        public List<MonoBehaviour> Modifiers;
        private RectTransform _contentTransform;
        private void Start()
        {
            Modifiers = FindObjectOfType<UnityChunkedGenerator>()
                .gameObject
                .GetComponents<IModifier<CellImpl>>()
                .Select(x=>x as MonoBehaviour).ToList();
            Place(Modifiers, 3f);
        }

        public override void Setup(MonoBehaviour modifier, ModifierController controller)
        {
            var panelName = modifier.GetType().Name;
            controller.Modifier = modifier;
            controller.PanelTitle = panelName;
        }
    }
}