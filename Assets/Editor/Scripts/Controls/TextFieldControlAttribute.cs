using System.Reflection;
using NodeEditor;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Controls
{
	public class TextFieldControlAttribute : ControlAttribute
	{
		public bool multiline;

		public override VisualElement InstantiateControl(AbstractNode node, PropertyInfo property)
		{
			var container = new VisualElement();
			container.AddToClassList("ControlField");
			AddLabel(container);
			var textField = new TextField(){name = "value-field" };
			textField.multiline = multiline;
			textField.value = (string)property.GetValue(node);
			textField.OnValueChanged(e =>
			{
				node.owner.owner.RegisterCompleteObjectUndo(property.Name + " Changed");
				property.SetValue(node,e.newValue);
				node.Dirty(ModificationScope.Node);
			});
			container.Add(textField);
			return container;
		}
	}
}