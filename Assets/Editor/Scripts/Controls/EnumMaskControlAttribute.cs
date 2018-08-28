using System;
using System.Reflection;
using NodeEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Controls
{
	public class EnumMaskControlAttribute : ControlAttribute
	{
		public override VisualElement InstantiateControl(AbstractNode node, PropertyInfo property)
		{
			var container = new VisualElement();
			container.AddToClassList("ControlField");
			AddLabel(container);
			var enumValue = property.GetValue(node);
			int propertyEnumValueInt = (int)enumValue;
			var values = Enum.GetValues(enumValue.GetType());
			var enumNames = Enum.GetNames(enumValue.GetType());
			var field = new Button(){name = "value-field" };
			field.clickable.clickedWithEventInfo += (e) =>
			{
				GenericMenu menu = new GenericMenu();
				for (int i = 0; i < enumNames.Length; i++)
				{
					var enumValueInt = (int)values.GetValue(i);
					bool isSelected = (propertyEnumValueInt & enumValueInt) == enumValueInt;
					menu.AddItem(new GUIContent(enumNames[i]), isSelected, () =>
					{
						int finalMask = isSelected ? propertyEnumValueInt & ~enumValueInt : propertyEnumValueInt | enumValueInt;
						property.SetValue(node, Enum.ToObject(enumValue.GetType(),finalMask));
					});
				}
				menu.DropDown(new Rect(e.originalMousePosition,Vector2.zero));
			};
			container.Add(field);
			return container;
		}
	}
}