﻿using Assets.SimpleGenerator;
using UnityEngine;

namespace Code.Modifiers.Biomes
{
    [RequireComponent(typeof(UnityChunkedGenerator))]
    public class RockSpawner : Biome<CellImpl>
    {
        [Range(25, 10000)] public int RockChance;


        public int MinTreeHeight, MaxTreeHeight;
        public void Start()
        {
        }

        public override void Callback(CellImpl current)
        {
            var value = current.Position.RandomFromPosition(0, RockChance, 54);
            if (value == 0)
            {
                current.Biomes.Add(this);
            }
        }

        public override void Apply(CellImpl current, TerrainStorage storage)
        {
            storage.Instances.Add(MakeRock(current, current.Core.Resolution));
        }

        private TreeInstance MakeRock(CellImpl current, int localScale)
        {
            TreeInstance instance = new TreeInstance
            {
                prototypeIndex = 1,
                rotation = current.Position.RandomFromPosition(0, 359,54),
                heightScale = (float) current.Position.RandomFromPosition(MinTreeHeight,MaxTreeHeight,54) / 10,
                position = new Vector3 ((float) current.LocalPosition.Y / localScale, current.Height,
                    (float) current.LocalPosition.X / localScale)
            };
            instance.widthScale = instance.heightScale;
            return instance;
        }
    }
}