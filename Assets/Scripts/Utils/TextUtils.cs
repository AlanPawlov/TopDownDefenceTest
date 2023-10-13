using System.Collections.Generic;
using System.IO;
using Models;
using Newtonsoft.Json;

namespace Utils
{
    public static class TextUtils
    {
        public static string DictionariesPath
        {
            get
            {
#if UNITY_EDITOR
                return "Assets/GameData";
#elif UNITY_STANDALONE || UNITY_SERVER
                return Application.streamingAssetsPath;
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

        public static void Save<T>(List<T> data)
        {
            var json = JsonConvert.SerializeObject(data, SerializerSettings);
            File.WriteAllText(GetConfigPath<T>(), json);
        }
        
        public static void Save<T>(T data)
        {
            File.WriteAllText(GetConfigPath<T>(),
                JsonConvert.SerializeObject(data, SerializerSettings));
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
            foreach (var item in json)
            {
                result.Add(item.Id, item);
            }

            return result;
        }

        public static T FillModel<T>(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<T>(jsonData, SerializerSettings);
            return data;
        }

        public static List<T> SaveAsList<T>(string jsonData) where T : BaseModel
        {
            var fromJson = JsonConvert.DeserializeObject<List<T>>(jsonData);
            Save(fromJson);
            return fromJson;
        }

        public static void Save<T>(string jsonData)
        {
            File.WriteAllText(GetConfigPath<T>(), jsonData);
        }

        public static StreamWriter GetFileWriterStream(string path, string fileName, bool append)
        {
            var filePath = Path.Combine(path, fileName);

            if (!File.Exists(filePath))
                if (!File.Exists(path))
                    Directory.CreateDirectory(path);

            return new StreamWriter(filePath, append: append);
        }

        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };
    }
}