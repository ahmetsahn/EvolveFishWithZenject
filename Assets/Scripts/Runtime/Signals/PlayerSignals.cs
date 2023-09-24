using Runtime.Enums;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Signals
{
    public class PlayerSignals
    {
        public Func<FishType> GetPlayerFishTypeSignal;
    }

    public struct EvolvePlayerSignal
    {
        public Sprite Sprite;
    }
}