using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Editor.Utils
{
    public static class EditorUtils
    {
        public static string GetRandomStringId() => Random.Range(0, 99999).ToString();

        public static List<T> LoadList<T>()
        {
            var text = GetTextFromLocalStorage<T>();
            var data = JsonConvert.DeserializeObject<List<T>>(text, SerializerSettings);
            if (data == null || data.Count == 0)
            {
                data = new List<T>();
            }

            return data;
        }

        public static T Load<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
                File.Create(path);

            var text = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<T>(text, SerializerSettings);
            return data;
        }

        public static void Save<T>(List<T> data) where T : BaseModel
        {
            try
            {
                foreach (var model in data)
                {
                    var duplicates = data.Where(h => h.Id == model.Id);
                    if (duplicates.Count() > 1)
                        throw new ArgumentException($"Duplicates item {model.Id} in {data.GetType()}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }

            File.WriteAllText(GetConfigPath<T>(), JsonConvert.SerializeObject(data, SerializerSettings));
        }

        public static void Save<T>(T data)
        {
            File.WriteAllText(GetConfigPath<T>(), JsonConvert.SerializeObject(data, SerializerSettings));
        }

        public static string GetConfigPath<T>()
        {
            var path = Path.Combine(DictionariesPath, $"{typeof(T).Name}.json");
            return path;
        }

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

        private static string GetTextFromLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
                File.Create(path);

            var text = File.ReadAllText(path);
            return text;
        }

        public static void GetArrowPoints(Vector3 startPoint, Vector3 endPoint, out Vector3 headRight,
            out Vector3 headLeft, out Vector3 headUp, out Vector3 headDown)
        {
            var direction = (endPoint - startPoint).normalized;
            var arrowLenghtPercent = 0.2f;
            var arrowHeadLength = Vector3.Distance(startPoint, endPoint) * arrowLenghtPercent;
            var arrowHeadAngle = 20.0f;

            headRight = endPoint + Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) *
                Vector3.back * arrowHeadLength;
            headLeft = endPoint + Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) *
                Vector3.back * arrowHeadLength;
            headUp = endPoint + Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) *
                Vector3.back * arrowHeadLength;
            headDown = endPoint + Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) *
                Vector3.back * arrowHeadLength;
        }

        public static void DrawArrow(Vector3 startPoint, Vector3 endPoint, Color color)
        {
            GetArrowPoints(startPoint, endPoint, out Vector3 headRight, out Vector3 headLeft, out Vector3 headUp,
                out Vector3 headDown);

            Handles.BeginGUI();
            Handles.color = color;
            Handles.DrawLine(startPoint, endPoint);
            Handles.DrawLine(endPoint, headRight);
            Handles.DrawLine(endPoint, headLeft);
            Handles.DrawLine(endPoint, headUp);
            Handles.DrawLine(endPoint, headDown);
            Handles.EndGUI();
        }

        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        public static string CollapseAddressablePath(this string str)
        {
            var strWithoutPath = str.Substring(str.LastIndexOf("/") + 1);
            var strWithoutExtension = strWithoutPath.Replace(strWithoutPath.Substring(strWithoutPath.LastIndexOf(".")), "");
            return strWithoutExtension;
        }

        public static string AddAsAddresables(this string str)
        {
            var address = str.CollapseAddressablePath();
            AddToAddressablesGroup(str, address);
            return address;
        }

        
        
        public static void AddToAddressablesGroup(string path, string address)
        {
            var guiID = AssetDatabase.AssetPathToGUID(path);
            AddressableAssetSettingsDefaultObject.Settings.CreateOrMoveEntry(guiID,
                AddressableAssetSettingsDefaultObject.Settings.DefaultGroup);
            AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(guiID).address = address;
        }

    }
}