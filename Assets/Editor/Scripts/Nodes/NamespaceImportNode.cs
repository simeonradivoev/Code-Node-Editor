using System.CodeDom;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using UnityEngine;

namespace NodeEditor.Nodes
{
	[Title("Declarations", "Declare: Namespace Import")]
	public class NamespaceImportNode : CodeNode<CodeNamespaceImport>
	{
		[SerializeField] private string m_ImportName;

		[TextFieldControl]
		public string importName { get { return m_ImportName; } set { m_ImportName = value; }}

		public NamespaceImportNode()
		{
			name = "Namespace Import";
			CreateOutputSlot<GetterSlot<CodeNamespaceImport>>("import", "Import").SetGetter(Build);
		}

		private CodeNamespaceImport Build()
		{
			var import = new CodeNamespaceImport();
			import.Namespace = m_ImportName;
			return import;
		}
	}
}