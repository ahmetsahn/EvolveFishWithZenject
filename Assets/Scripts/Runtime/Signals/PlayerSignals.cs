using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Signals
{
    public struct GetPlayerFishTypeSignal
    {
        public FishType FishType;
    }

    public struct EvolvePlayerSignal
    {
        public Sprite Sprite;
    }
}