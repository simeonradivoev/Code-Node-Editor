using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Statements
{
	[Title("Statements","Statement: Method Return")]
	public class MethodReturnStatementNode : StatementNode<CodeMethodReturnStatement>
	{
		private EmptySlot<CodeExpression> m_Expresion;

		public MethodReturnStatementNode() : base("out", "Out (stm)")
		{
			name = "Method Return";
			m_Expresion = CreateInputSlot<EmptySlot<CodeExpression>>("expression","Exp (exp)");
		}

		protected override void Build(CodeMethodReturnStatement statement)
		{
			statement.Expression = GetSlotValue<CodeExpression>(m_Expresion);
		}
	}
}