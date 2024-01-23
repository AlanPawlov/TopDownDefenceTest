using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CommonTemplate.WebRequest
{
    public class WebRequestSender
    {
        public Action<FileLoadingProgress> DownloadProgress;
        private FileLoadingProgress _fileProgress = new FileLoadingProgress(0);

        public async UniTask<string> DownloadText(string name, string address)
        {
            var url = $"{address}{name}.json";
            _fileProgress.SetNameFile(name);
            var progress = Progress.CreateOnlyValueChanged<float>(ReportProgress);
            var op = await UnityWebRequest.Get(url).SendWebRequest().ToUniTask(progress);
            return op.downloadHandler.text;
        }
        
        //Вообще тут все операции надо бы раскидать но мне сейчас нужно только скачать конфиги с сервера
        public async UniTask<string> Get(string url)
        {
            var op = await UnityWebRequest.Get(url).SendWebRequest();
            return op.downloadHandler.text;
        }

        private void ReportProgress(float progress)
        {
            _fileProgress.Progress = progress;
            DownloadProgress?.Invoke(_fileProgress);
            Debug.Log($"Download JSON data {_fileProgress.FileName} {progress}%");
        }
    }
}