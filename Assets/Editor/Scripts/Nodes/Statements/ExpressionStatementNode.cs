using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Statements
{
	[Title("Statement: Expression")]
	public class ExpressionStatementNode : StatementNode<CodeExpressionStatement>
	{
		private EmptySlot<CodeExpression> m_Expression;

		public ExpressionStatementNode() : base("out", "This (stm)")
		{
			name = "Expression Statement";
			m_Expression = CreateInputSlot<EmptySlot<CodeExpression>>("expression","Exp (exp)");
		}

		protected override void Build(CodeExpressionStatement statement)
		{
			statement.Expression = GetSlotValue<CodeExpression>(m_Expression);
		}
	}
}