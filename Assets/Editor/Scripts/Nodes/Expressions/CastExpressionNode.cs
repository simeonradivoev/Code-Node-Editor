using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Expressions
{
	[Title("Expression: Cast")]
	public class CastExpressionNode : ExpressionNode<CodeCastExpression>
	{
		private CodeTypeReferenceSlot m_Type;
		private EmptySlot<CodeExpression> m_Expression;

		public CastExpressionNode() : base("out", "Cast (exp)")
		{
			m_Type = CreateInputSlot<CodeTypeReferenceSlot>("type", "Type (ref)");
			m_Expression = CreateInputSlot<EmptySlot<CodeExpression>>("exp", "Cast (exp)");
		}

		protected override void Build(CodeCastExpression expression)
		{
			expression.TargetType = GetSlotValue<CodeTypeReference>(m_Type);
			expression.Expression = GetSlotValue<CodeExpression>(m_Expression);
		}
	}
}