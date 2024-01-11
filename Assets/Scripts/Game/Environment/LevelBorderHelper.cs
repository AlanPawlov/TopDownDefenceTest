using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Resource;
using UnityEngine;

namespace Game.Environment
{
    public class LevelBorderHelper
    {
        private readonly IResourceLoader _resourceLoader;
        private List<GameObject> _borders;
        private GameObject _prefab;

        public LevelBorderHelper(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
            _borders = new List<GameObject>(4);
        }

        public async Task SetupBorders()
        {
            if (_prefab != null)
                return;

            var camera = UnityEngine.Camera.main;
            _prefab = await _resourceLoader.Load<GameObject>("Border");
            var leftBorderMax = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            var rightBorderMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            var topBorderMax = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            var bottomBorderMax = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            var horizontalBorderSize = new Vector2(rightBorderMax - leftBorderMax, 1);
            var verticalBorderSize = new Vector2(1, topBorderMax - bottomBorderMax);

            var leftBorder = Object.Instantiate(_prefab).GetComponent<BoxCollider2D>();
            leftBorder.size = verticalBorderSize;
            leftBorder.transform.position = new Vector3(leftBorderMax - leftBorder.size.x / 2, 0, 0);
            var rightBorder = Object.Instantiate(_prefab).GetComponent<BoxCollider2D>();
            rightBorder.size = verticalBorderSize;
            rightBorder.transform.position = new Vector3(rightBorderMax + leftBorder.size.x / 2, 0, 0);
            var topBorder = Object.Instantiate(_prefab).GetComponent<BoxCollider2D>();
            topBorder.size = horizontalBorderSize;
            topBorder.transform.position = new Vector3(0, topBorderMax + topBorder.size.y / 2, 0);
            var bottomBorder = Object.Instantiate(_prefab).GetComponent<BoxCollider2D>();
            bottomBorder.size = horizontalBorderSize;
            bottomBorder.transform.position = new Vector3(0, bottomBorderMax - bottomBorder.size.y / 2, 0);

            _borders.Add(leftBorder.gameObject);
            _borders.Add(rightBorder.gameObject);
            _borders.Add(topBorder.gameObject);
            _borders.Add(bottomBorder.gameObject);
        }
    }
}