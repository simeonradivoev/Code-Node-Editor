using System;
using System.CodeDom;
using System.Collections.Generic;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using NodeEditor.Util;
using UnityEngine;

namespace NodeEditor.Nodes.Expressions
{
	[Title("References", "Expression: Field Reference")]
	public class FieldReferenceExpressionNode : ExpressionNode<CodeFieldReferenceExpression>
	{
		[SerializeField] private string m_RefName;

		[AutoCompleteTextControl("BuildAutocomplete")]
		public string fieldName { get { return m_RefName; } set { m_RefName = value; } }

		private EmptySlot<CodeExpression> m_Target;

		public FieldReferenceExpressionNode() : base("out", "Out (ref exp)")
		{
			name = "Field Reference";
			m_Target = CreateInputSlot<EmptySlot<CodeExpression>>("target","Target (exp)");
		}

		protected override void Build(CodeFieldReferenceExpression reference)
		{
			reference.FieldName = m_RefName;
			reference.TargetObject = GetSlotValue<CodeExpression>(m_Target);
		}

		private void BuildAutocomplete(string value, IList<string> values)
		{
			List<FieldNode> fieldNodes = ListPool<FieldNode>.Get();
			List<ParameterNode> parameters = ListPool<ParameterNode>.Get();
			this.FindParentsOrFirstChildren(fieldNodes);
			this.FindParentsOrFirstChildren(parameters);

			foreach (var fieldNode in fieldNodes)
			{
				if (!string.IsNullOrEmpty(fieldNode.memberName)
					&& !string.IsNullOrEmpty(value)
					&& fieldNode.memberName.StartsWith(value, StringComparison.InvariantCultureIgnoreCase))
				{
					values.Add(fieldNode.memberName);
				}
			}

			foreach (var parameterNode in parameters)
			{
				if (!string.IsNullOrEmpty(parameterNode.paramName)
					&& !string.IsNullOrEmpty(value)
					&& parameterNode.paramName.StartsWith(value, StringComparison.InvariantCultureIgnoreCase))
				{
					values.Add(parameterNode.paramName);
				}
			}

			ListPool<FieldNode>.Release(fieldNodes);
			ListPool<ParameterNode>.Release(parameters);
		}
	}
}