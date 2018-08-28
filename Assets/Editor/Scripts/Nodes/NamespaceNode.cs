using System.CodeDom;
using System.Collections.Generic;
using Assets.Editor.Scripts.Controls;
using Assets.Editor.Scripts.Slots;
using UnityEngine;

namespace NodeEditor.Nodes
{
	[Title("Declarations", "Declare: Namespace")]
	public class NamespaceNode : CodeNode<CodeNamespace>
	{
		[SerializeField] private string m_NamespaceName;

		[TextFieldControl]
		public string namespaceName
		{
			get { return m_NamespaceName; }
			set { m_NamespaceName = value; }
		}

		private EmptySlot<CodeTypeDeclaration> m_Types;
		private EmptySlot<CodeNamespaceImport> m_Imports;

		public NamespaceNode()
		{
			name = "Namespace Declaration";
			m_Types = CreateInputSlot<EmptySlot<CodeTypeDeclaration>>("types","Types (dec)");
			m_Types.allowMultipleConnections = true;
			m_Imports = CreateInputSlot<EmptySlot<CodeNamespaceImport>>("imports", "Imports");
			m_Imports.allowMultipleConnections = true;
		}

		public CodeNamespace BuildNamespace()
		{
			CodeNamespace space = new CodeNamespace(m_NamespaceName);
			var types = GetSlotValues<CodeTypeDeclaration>(m_Types);
			foreach (var type in types)
			{
				space.Types.Add(type);
			}

			var imports = GetSlotValues<CodeNamespaceImport>(m_Imports);
			foreach (var import in imports)
			{
				space.Imports.Add(import);
			}

			return space;
		}

		public void GetTypeBuilders(IList<CodeTypeDeclaration> builders)
		{
			GetSlotValues(m_Types,builders);
		}
	}
}