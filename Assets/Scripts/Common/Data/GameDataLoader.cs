using Common.Utils;
using Common.WebRequest;
using Cysharp.Threading.Tasks;
using Game.Models;
using UnityEngine;

namespace Common.Data
{
    public class GameDataLoader
    {
        private const string GAME_SERVER_ADDRESS = "alanpaxc.beget.tech";
        private string _address => $"http://{GAME_SERVER_ADDRESS}/OtherConfig/TopDown/";
        private readonly WebRequestSender _webRequestSender;

        private bool IsDownloadedInLocalStorage
        {
            get
            {
                var result = TextUtils.IsLoadedToLocalStorage<CharacterModel>();
                result &= TextUtils.IsLoadedToLocalStorage<EnemySpawnerModel>();
                result &= TextUtils.IsLoadedToLocalStorage<ProjectileModel>();
                result &= TextUtils.IsLoadedToLocalStorage<WallDefenceRulesModel>();
                result &= TextUtils.IsLoadedToLocalStorage<WallModel>();
                result &= TextUtils.IsLoadedToLocalStorage<WeaponModel>();
                return result;
            }
        }

        public GameDataLoader(WebRequestSender webRequestSender)
        {
            _webRequestSender = webRequestSender;
        }

        public async UniTask Init()
        {
            var needUpdateConfig = await IsNeedUpdateConfig();

            if (!IsDownloadedInLocalStorage || needUpdateConfig)
            {
                await LoadFromRemoteDirectory();
            }
        }

        private async UniTask<bool> IsNeedUpdateConfig()
        {
            var jsonData = string.Empty;
            if (TextUtils.IsLoadedToLocalStorage<ConfigVersion>())
                jsonData = TextUtils.GetTextFromLocalStorage<ConfigVersion>();
            else
            {
                jsonData = await _webRequestSender.DownloadText(nameof(ConfigVersion), _address);
                TextUtils.Save<ConfigVersion>(jsonData);
                return true;
            }

            var currentConfig = TextUtils.FillModel<ConfigVersion>(jsonData);
            var serverConfigJson = await _webRequestSender.DownloadText(nameof(ConfigVersion), _address);
            var serverConfig = TextUtils.FillModel<ConfigVersion>(serverConfigJson);
            Debug.Log($"current config version:{currentConfig.CurrentVersion}\n" +
                      $"server config version:{serverConfig.CurrentVersion}");

            if (currentConfig.CurrentVersion != serverConfig.CurrentVersion)
            {
                TextUtils.Save<ConfigVersion>(serverConfigJson);
                return true;
            }

            return false;
        }

        private async UniTask LoadFromRemoteDirectory()
        {
            await DownloadModels<CharacterModel>();
            await DownloadModels<EnemySpawnerModel>();
            await DownloadModels<ProjectileModel>();
            await DownloadModels<WallDefenceRulesModel>();
            await DownloadModels<WallModel>();
            await DownloadModels<WeaponModel>();
        }

        private async UniTask DownloadModels<T>() where T : BaseModel
        {
            var json = await _webRequestSender.DownloadText(typeof(T).Name, _address);
            TextUtils.Save<T>(json);
        }
    }
}