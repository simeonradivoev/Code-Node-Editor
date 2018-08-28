using System;
using System.CodeDom;
using NodeEditor;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Slots
{
	public class CodeTypeReferenceSlot : ValueSlot<string>, IHasValue<CodeTypeReference>
	{
		public override SerializedType valueType => typeof(CodeTypeReference);

		public CodeTypeReference value => new CodeTypeReference(base.value);

		public override VisualElement InstantiateControl()
		{
			VisualElement element = new VisualElement();
			element.AddToClassList("Container");

			element.AddStyleSheetPath("Styles/Controls/TypeControl");
			var icon = new VisualElement();
			icon.AddToClassList("warrning-field");
			var typeObject = string.IsNullOrEmpty(base.value) ? null : Type.GetType(base.value);
			icon.EnableInClassList("valid", typeObject != null);
			var textField = new TextField();
			textField.value = typeObject != null ? typeObject.FullName : "";
			textField.OnValueChanged(e =>
			{
				var type = Type.GetType(e.newValue);
				base.SetValue(e.newValue);
				icon.EnableInClassList("valid", type != null);
				owner.Dirty(ModificationScope.Node);
			});
			element.Add(icon);
			element.Add(textField);
			return element;
		}
	}
}