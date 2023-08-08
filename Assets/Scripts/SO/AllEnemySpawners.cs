using System.Collections.Generic;
using Models;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(AllEnemySpawners),menuName = "Data/AllEnemySpawners")]
    public class AllEnemySpawners : ScriptableObject
    {
        public List<EnemySpawnerModel> EnemySpawners;
    }
}