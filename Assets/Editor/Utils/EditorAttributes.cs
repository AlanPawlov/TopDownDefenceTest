using System;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using Editor.Utils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;

[assembly: RegisterStateUpdater(typeof(DynamicNumberOfItemsPerPageAttributeStateUpdater))]
#endif

namespace Editor.Utils
{
    public class ReadOnlyDictionaryKeysAttribute : Attribute { }

    public class ReadOnlyDictionaryKeysAttributeProcessor<T1, T2> : OdinAttributeProcessor<EditableKeyValuePair<T1, T2>>
    {
        public override bool CanProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member)
        {
            return parentProperty.Attributes.HasAttribute<ReadOnlyDictionaryKeysAttribute>();
        }

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
        {
            if (member.Name == "Key")
            {
                attributes.Add(new ReadOnlyAttribute());
            }
        }
    }

    public class DynamicNumberOfItemsPerPageAttribute : Attribute
    {
        public string ItemsPerPage;

        public DynamicNumberOfItemsPerPageAttribute(string itemsPerPage) => ItemsPerPage = itemsPerPage;
    }

    public class DynamicNumberOfItemsPerPageAttributeProcessor : OdinAttributeProcessor
    {
        public override bool CanProcessSelfAttributes(InspectorProperty property)
        {
            return property.Attributes.HasAttribute<DynamicNumberOfItemsPerPageAttribute>()
                   && property.Attributes.HasAttribute<ListDrawerSettingsAttribute>();
        }

        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            var listDrawerSettingsAttribute = attributes.GetAttribute<ListDrawerSettingsAttribute>();
            var dynamicNumberOfItemsPerPageAttribute = attributes.GetAttribute<DynamicNumberOfItemsPerPageAttribute>();
            var itemsPerPageResolver = ValueResolver.Get<int>(property, dynamicNumberOfItemsPerPageAttribute.ItemsPerPage);
            listDrawerSettingsAttribute.NumberOfItemsPerPage = itemsPerPageResolver.GetValue();
        }
    }

    public class DynamicNumberOfItemsPerPageAttributeStateUpdater : AttributeStateUpdater<DynamicNumberOfItemsPerPageAttribute>
    {
        private int previousItemsPerPage;
        private ValueResolver<int> itemsPerPageResolver;

        protected override void Initialize()
        {
            itemsPerPageResolver = ValueResolver.Get<int>(Property, Attribute.ItemsPerPage);
            previousItemsPerPage = itemsPerPageResolver.GetValue();
        }

        public override void OnStateUpdate()
        {
            var itemsPerPage = itemsPerPageResolver.GetValue();

            if (previousItemsPerPage != itemsPerPage)
            {
                Property.RefreshSetup();
            }
        }
    }

}