using System;
using System.Linq;
using System.Reflection;
using Assets.SimpleGenerator;
using Code.Core;
using Code.Core;
using UnityEngine;
namespace Code.UI.ModifiersPanel
{
    public class ModifierPanelsCreator : MonoBehaviour
    {
        public MonoBehaviour[] Modifiers;
        public GameObject PanelExample, NumberBar, SlideredNumberBar;
        private RectTransform _contentTransform;
        private void Start()
        {
            Modifiers = FindObjectOfType<UnityChunkedGenerator>()
                .gameObject
                .GetComponents<IModifier<CellImpl>>()
                .Select(x=>x as MonoBehaviour)
                .ToArray();
            MakePanels();
        }

        private void MakePanels()
        {
            foreach (var modifier in Modifiers)
            {
                var panel = Instantiate(PanelExample).GetComponent<ModifierController>();
                panel.Modifier = modifier;
            }
        }

        private void MakePanel(ModifierController controller,MonoBehaviour modifier)
        {

            var fields = modifier
                .GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        }
    }
}