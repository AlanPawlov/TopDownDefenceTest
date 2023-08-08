using System.Collections.Generic;
using Models;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(AllWalls),menuName = "Data/AllWalls")]
    public class AllWalls : ScriptableObject
    {
        public List<WallModel> Walls;
    }
}