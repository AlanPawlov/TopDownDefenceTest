using System.Collections.Generic;
using Models;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(AllCharacters),menuName = "Data/AllCharacters")]
    public class AllCharacters : ScriptableObject
    {
        public List<CharacterModel> Characters;
    }
}