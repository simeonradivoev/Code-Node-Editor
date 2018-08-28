using System;
using System.Reflection;
using UnityEngine.Experimental.UIElements;

namespace NodeEditor
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public abstract class ControlAttribute : Attribute, IControlAttribute
	{
		public string label;
		public abstract VisualElement InstantiateControl(AbstractNode node, PropertyInfo property);

		public void AddLabel(VisualElement container)
		{
			if (!string.IsNullOrEmpty(label))
			{
				var labelField = new Label(label){name = "control-label" };
				container.Add(labelField);
			}
		}
	}
}