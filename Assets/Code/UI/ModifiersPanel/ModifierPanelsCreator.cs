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
            Place(Modifiers);
        }

        public override ModifierController Setup(MonoBehaviour modifier)
        {
            ElementExample.Modifier = modifier;
            return ElementExample;
        }
    }
}