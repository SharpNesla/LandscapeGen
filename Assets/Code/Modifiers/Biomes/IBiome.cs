using Assets.SimpleGenerator;
using Code.Core;
using UnityEngine;

namespace Code.Modifiers.Biomes
{

    public abstract class Biome<T> : MonoBehaviour, IModifier<T> where T : Cell
    {
        public Color TextureColor;
        public abstract void Apply(T current,TerrainStorage storage);
        public abstract void Callback(T current);
        public virtual void Start() {}
    }
}