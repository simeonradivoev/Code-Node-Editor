using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using UnityEngine;

namespace NodeEditor.Nodes.Expressions
{
	[Title("Expressions", "Primitive")]
	public class PrimitiveExpressionNode<T> : ExpressionNode<CodePrimitiveExpression>
	{
		[SerializeField] protected T m_Value;

		public PrimitiveExpressionNode() : base("out", "This (exp)")
		{
			name = "Primitive";
		}

		protected override void Build(CodePrimitiveExpression primitive)
		{
			primitive.Value = m_Value;
		}
	}

	[Title("String")]
	public class PrimitiveExpressionNodeString : PrimitiveExpressionNode<string>
	{
		[TextFieldControl]
		public string value { get { return m_Value; } set { m_Value = value; } }
	}
}