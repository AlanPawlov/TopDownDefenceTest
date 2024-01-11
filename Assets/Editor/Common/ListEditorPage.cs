using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Editor.Utils;
using Editor.Windows;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor.Common
{
    public class ListEditorPage<TEditor, TModel> : BaseEditorPage
        where TModel : BaseModel, new() where TEditor : BaseModelEditor<TModel>, new()
    {
        [ShowInInspector]
        [ListDrawerSettings(HideRemoveButton = false, DraggableItems = false, Expanded = true,
            CustomAddFunction = nameof(AddElement), CustomRemoveElementFunction = nameof(RemoveElement))]
        [DynamicNumberOfItemsPerPage("ItemsInPage")]
        [ShowIf(nameof(DataExist))]
        [HorizontalGroup("2")]
        [PropertyOrder(2)]
        [Searchable(FilterOptions = SearchFilterOptions.ValueToString)]
        public List<TEditor> ChildElements;

        protected virtual int ItemsInPage => 5;
        protected GameData GameData;

        protected virtual Dictionary<string, TModel> Models
        {
            get
            {
                Debug.LogError($"Warning: {typeof(TModel)} models is empty");
                return new Dictionary<string, TModel>();
            }
        }

        protected List<TModel> GetModels()
        {
            var models = ChildElements.Select(t => t.GetModel()).ToList();
            return models;
        }

        public override void SaveData()
        {
            var models = GetModels();
            EditorUtils.Save(models);
        }

        public override void LoadData()
        {
            try
            {
                var constructor = typeof(TEditor).GetConstructor(new Type[] { typeof(TModel), GameData.GetType() });
                ChildElements = Models.Select(f => (TEditor)constructor.Invoke(new object[] { f.Value, GameData }))
                    .ToList();
                base.LoadData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected virtual TModel AddElement()
        {
            var id = EditorUtils.GetRandomStringId();
            var newModel = new TModel() { Id = id };
            Models.Add(id, newModel);
            var constructor = typeof(TEditor).GetConstructor(new Type[] { typeof(TModel), GameData.GetType() });
            var newEditor = (TEditor)constructor.Invoke(new object[] { newModel, GameData });

            ChildElements.Add(newEditor);
            return newModel;
        }

        protected virtual void RemoveElement(TEditor item, object b, List<TEditor> items)
        {
            var targetElement = ChildElements.First(e => e.GetModel().Id == item.GetModel().Id);
            var id = targetElement.GetModel().Id;
            Models.Remove(id);
            ChildElements.Remove(targetElement);
        }
    }
}