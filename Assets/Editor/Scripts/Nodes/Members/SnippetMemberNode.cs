using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using UnityEngine;

namespace NodeEditor.Nodes
{
	[Title("Members","Member: Snippet")]
	public class SnippetMemberNode : CodeNode<CodeSnippetTypeMember>
	{
		[SerializeField] private string m_Snippet;

		[TextFieldControl(multiline = true)]
		public string snippet { get { return m_Snippet; } set { m_Snippet = value; } }

		public SnippetMemberNode()
		{
			CreateOutputSlot<GetterSlot<CodeSnippetTypeMember>>("out", "Out (mem)").SetGetter(BuildInternal);
		}

		private CodeSnippetTypeMember BuildInternal()
		{
			CodeSnippetTypeMember member = new CodeSnippetTypeMember();
			member.Text = m_Snippet;
			return member;
		}
	}
}