using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes.Expressions
{
	[Title("Expresssion: Create Object")]
	public class ObjectCreateExpressionNode : ExpressionNode<CodeObjectCreateExpression>
	{
		private CodeTypeReferenceSlot m_Type;
		private EmptySlot<CodeExpression> m_Parameters;

		public ObjectCreateExpressionNode() : base("out", "Object (exp)")
		{
			m_Type = CreateInputSlot<CodeTypeReferenceSlot>("type", "Type");
			m_Parameters = CreateInputSlot<EmptySlot<CodeExpression>>("parameters", "Parameters");
			m_Parameters.allowMultipleConnections = true;
		}

		protected override void Build(CodeObjectCreateExpression expression)
		{
			expression.CreateType = GetSlotValue<CodeTypeReference>(m_Type);
			var parameters = GetSlotValues<CodeExpression>(m_Parameters);
			foreach (var parameter in parameters)
			{
				expression.Parameters.Add(parameter);
			}
		}
	}
}