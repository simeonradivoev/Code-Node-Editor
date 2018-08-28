using System;
using System.Reflection;
using NodeEditor;
using UnityEditor.Experimental.UIElements;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Controls
{
	public class EnumControlAttribute : ControlAttribute
	{
		public override VisualElement InstantiateControl(AbstractNode node, PropertyInfo property)
		{
			var container = new VisualElement();
			container.AddToClassList("ControlField");
			AddLabel(container);
			var enumField = new EnumField((Enum) property.GetValue(node)){name = "value-field" };
			enumField.OnValueChanged(e =>
			{
				node.owner.owner.RegisterCompleteObjectUndo(property.Name + " Changed");
				property.SetValue(node,e.newValue);
				node.Dirty(ModificationScope.Node);
			});
			container.Add(enumField);
			return container;
		}
	}
}