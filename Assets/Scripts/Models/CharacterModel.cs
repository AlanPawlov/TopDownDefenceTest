﻿using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class CharacterModel : BaseModel
    {
        public int Health;
        public float MaxSpeed;
        public float MinSpeed;
        public string WeaponId;
        public string CharacterPath;
        public Sprite CharacterView;
        public string AnimatorController;
    }
}