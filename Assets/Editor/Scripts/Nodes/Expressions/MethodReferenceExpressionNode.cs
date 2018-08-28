using System;
using System.CodeDom;
using System.Collections.Generic;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using NodeEditor.Util;
using UnityEngine;

namespace NodeEditor.Nodes.Expressions
{
	[Title("References", "Expression: Method Reference")]
	public class MethodReferenceExpressionNode : ExpressionNode<CodeMethodReferenceExpression>
	{
		[SerializeField] private string m_RefName;

		[AutoCompleteTextControl("BuildAutocomplete")]
		public string methodName { get { return m_RefName; } set { m_RefName = value; } }

		private EmptySlot<CodeExpression> m_Target;
		private EmptySlot<CodeTypeReference> m_TypeArguments;

		public MethodReferenceExpressionNode() : base("method", "Method (ref exp)")
		{
			name = "Method Reference";
			m_Target = CreateInputSlot<EmptySlot<CodeExpression>>("target","Target (exp)");
			m_TypeArguments = CreateInputSlot<EmptySlot<CodeTypeReference>>("typeArguments","Type Arguments (ref)");
		}

		protected override void Build(CodeMethodReferenceExpression reference)
		{
			reference.MethodName = methodName;
			reference.TargetObject = GetSlotValue<CodeExpression>(m_Target);

			var typeArguments = GetSlotValues<CodeTypeReference>(m_TypeArguments);
			foreach (var argument in typeArguments)
			{
				reference.TypeArguments.Add(argument);
			}
		}

		private void BuildAutocomplete(string value, IList<string> values)
		{
			List<MethodNode> methodNodes = ListPool<MethodNode>.Get();
			this.FindParentsOrFirstChildren(methodNodes);

			foreach (var fieldNode in methodNodes)
			{
				if (!string.IsNullOrEmpty(fieldNode.memberName)
					&& !string.IsNullOrEmpty(value)
					&& fieldNode.memberName.StartsWith(value, StringComparison.InvariantCultureIgnoreCase))
				{
					values.Add(fieldNode.memberName);
				}
			}

			ListPool<MethodNode>.Release(methodNodes);
		}
	}
}