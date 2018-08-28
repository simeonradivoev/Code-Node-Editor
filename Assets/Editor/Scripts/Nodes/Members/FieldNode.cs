using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes
{
	[Title("Member: Field")]
	public class FieldNode : MemberNode<CodeMemberField>
	{
		private GetterSlot<CodeMemberField> m_Output;
		private CodeTypeReferenceSlot m_VariableType;
		public EmptySlot<CodeExpression> m_InitExpresion;

		public FieldNode()
		{
			name = "Field Member";
			m_VariableType = CreateInputSlot<CodeTypeReferenceSlot>("type","Type (ref)");
			m_InitExpresion = CreateInputSlot<EmptySlot<CodeExpression>>("init","Init (exp)");
		}

		protected override void Build(CodeMemberField field)
		{
			field.Type = GetSlotValue<CodeTypeReference>(m_VariableType);
			field.InitExpression = GetSlotValue<CodeExpression>(m_InitExpresion);
		}
	}
}