using System.Reflection;
using NodeEditor;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Controls
{
	public class ToggleControlAttribute : ControlAttribute
	{
		public override VisualElement InstantiateControl(AbstractNode node, PropertyInfo property)
		{
			var container = new VisualElement();
			container.AddToClassList("ControlField");
			AddLabel(container);

			var toggleField = new Toggle(){name = "value-field" };
			toggleField.value = (bool)property.GetValue(node);
			toggleField.OnToggle(() =>
			{
				node.owner.owner.RegisterCompleteObjectUndo(property.Name + " Changed");
				property.SetValue(node,toggleField.value);
				node.Dirty(ModificationScope.Node);
			});
			container.Add(toggleField);
			return container;
		}
	}
}