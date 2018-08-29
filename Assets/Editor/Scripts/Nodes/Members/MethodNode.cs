using System.CodeDom;
using Assets.Editor.Scripts.Slots;

namespace NodeEditor.Nodes
{
	[Title("Member: Method")]
	public class MethodNode : MemberNode<CodeMemberMethod>
	{
		private CodeTypeReferenceSlot m_ReturnType;
		private EmptySlot<CodeParameterDeclarationExpression> m_Parameters;
		private EmptySlot<CodeStatement> m_Statements;

		public MethodNode()
		{
			name = "Method Member";
			m_ReturnType = CreateInputSlot<CodeTypeReferenceSlot>("returnType","ReturnType (ref)");
			m_ReturnType.SetValue("System.Void");
			m_Parameters = CreateInputSlot<EmptySlot<CodeParameterDeclarationExpression>>("parameters","Parameters (dec exp)");
			m_Parameters.allowMultipleConnections = true;
			m_Statements = CreateInputSlot<EmptySlot<CodeStatement>>("statements","Statements (stm)");
			m_Statements.allowMultipleConnections = true;
		}

		protected override void Build(CodeMemberMethod method)
		{
			var returnType = GetSlotValue<CodeTypeReference>(m_ReturnType);
			if (returnType != null)
				method.ReturnType = returnType;

			var parameters = GetSlotValues<CodeParameterDeclarationExpression>(m_Parameters);
			foreach (var param in parameters)
			{
				method.Parameters.Add(param);
			}

			var statements = GetSlotValues<CodeStatement>(m_Statements);
			foreach (var statement in statements)
			{
				method.Statements.Add(statement);
			}
		}
	}
}