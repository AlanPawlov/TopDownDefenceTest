using System;
using CommonTemplate.Data;

namespace Game.Models
{
    [Serializable]
    public class CharacterModel : BaseModel
    {
        public int Health;
        public float MaxSpeed;
        public float MinSpeed;
        public string WeaponId;
        public string CharacterPath;
        public string CharacterView;
        public string AnimatorController;
    }
}