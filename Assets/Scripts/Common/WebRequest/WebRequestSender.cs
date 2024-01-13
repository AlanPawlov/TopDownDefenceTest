using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Common.WebRequest
{
    public class WebRequestSender
    {
        public Action<FileLoadingProgress> DownloadProgress;
        private FileLoadingProgress fileProgress = new FileLoadingProgress(0);

        public async UniTask<string> DownloadText(string name, string address)
        {
            var url = $"{address}{name}.json";
            fileProgress.SetNameFile(name);
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
            fileProgress.Progress = progress;
            DownloadProgress?.Invoke(fileProgress);
            Debug.Log($"DownloadJsonData {fileProgress.FileName} {progress}%");
        }
    }
}