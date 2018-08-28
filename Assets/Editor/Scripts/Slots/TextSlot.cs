using NodeEditor;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Slots
{
	public class TextSlot : ValueSlot<string>
	{
		public override VisualElement InstantiateControl()
		{
			var textField = new TextField();
			textField.value = value;
			textField.OnValueChanged(e =>
			{
				owner.owner.owner.RegisterCompleteObjectUndo("Slot " + displayName + " Changed");
				SetValue(e.newValue);
				owner.Dirty(ModificationScope.Node);
			});
			return textField;
		}
	}
}