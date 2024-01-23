using System;
using System.Threading;
using CommonTemplate.UITemplate;
using CommonTemplate.UITemplate.DefaultWindows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CommonTemplate.Resource
{
    public class SceneLoader : IDisposable
    {
        private CancellationTokenSource _cancelationTokenSource;
        private readonly UIManager _uiManager;

        public SceneLoader(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public async UniTaskVoid Load(string name, Action onLoaded = null, Action<float> onUpdateProgress = null)
        {
            if (_cancelationTokenSource == null)
                _cancelationTokenSource = new CancellationTokenSource();

            Debug.Log($"Load scene: {name}");
            try
            {
                await LoadScene(name, onLoaded, onUpdateProgress);
            }
            catch (OperationCanceledException ex)
            {
                if (ex.CancellationToken == _cancelationTokenSource.Token)
                    Debug.Log($"Task cancelled {ex}");
            }
            finally
            {
                _cancelationTokenSource?.Cancel();
                _cancelationTokenSource = null;
            }
        }

        private async UniTask LoadScene(string nextScene, Action onLoaded, Action<float> onUpdateProgress = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                Debug.Log($"Load scene {nextScene} {waitNextScene.progress}");
                onUpdateProgress?.Invoke(waitNextScene.progress);
                await UniTask.Yield();
            }

            onLoaded?.Invoke();
        }

        public void Dispose()
        {
            _cancelationTokenSource.Cancel();
            _cancelationTokenSource = null;
        }
    }
}