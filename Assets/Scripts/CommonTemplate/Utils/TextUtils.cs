using System.Collections.Generic;
using System.IO;
using CommonTemplate.Data;
using Newtonsoft.Json;
using UnityEngine;


namespace CommonTemplate.Utils
{
    public static class TextUtils
    {
        public static readonly JsonSerializerSettings SerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        public static string DictionariesPath
        {
            get
            {
#if UNITY_EDITOR
                 return "Assets/GameData";
#elif UNITY_ANDROID
                 return Application.persistentDataPath;
#endif
            }
        }

        public static string GetTextFromLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
                File.Create(path).Close();

            var text = File.ReadAllText(path);
            return text;
        }

        public static string GetConfigPath<T>()
        {
            var path = Path.Combine(DictionariesPath, $"{typeof(T).Name}.json");
            return path;
        }
        
        public static bool IsLoadedToLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            return File.Exists(path);
        }

        public static Dictionary<string, T> FillDictionary<T>(string jsonData) where T : BaseModel
        {
            var result = new Dictionary<string, T>();
            if (string.IsNullOrEmpty(jsonData))
                return result;

            var json = JsonConvert.DeserializeObject<List<T>>(jsonData);
            foreach (var item in json) result.Add(item.Id, item);

            return result;
        }

        public static T FillModel<T>(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<T>(jsonData, SerializerSettings);
            return data;
        }

        public static void Save<T>(string jsonData)
        {
            File.WriteAllText(GetConfigPath<T>(), jsonData);
        }
        
        public static string CollapseAddressablePath(this string str)
        {
            var strWithoutPath = str.Substring(str.LastIndexOf("/") + 1);
            var strWithoutExtension =
                strWithoutPath.Replace(strWithoutPath.Substring(strWithoutPath.LastIndexOf(".")), "");
            return strWithoutExtension;
        }
    }
}