using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NodeEditor;
using NodeEditor.Nodes;
using NodeEditor.Util;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Controls
{
	public class TypeControlAttribute : ControlAttribute
	{
		public override VisualElement InstantiateControl(AbstractNode node, PropertyInfo property)
		{
			return new View(label,node,null,property);
		}

		public class View : AutoCompleteTextControlAttribute.AutoCompleteView
		{
			private VisualElement m_Icon;

			public View(string label, AbstractNode node, MethodInfo autoCompleteMethod, PropertyInfo property) : base(label, node, autoCompleteMethod, property)
			{
				AddStyleSheetPath("Styles/Controls/TypeControl");
				m_Icon = new VisualElement();
				m_Icon.AddToClassList("warrning-field");
				m_FieldContainer.Insert(0, m_Icon);
				UpdateIcon();
				m_OnTextSet = UpdateIcon;
			}

			private void UpdateIcon()
			{
				string typeName = m_Property.GetValue(m_Node) as string;
				if (string.IsNullOrEmpty(typeName))
				{
					m_Icon.EnableInClassList("valid", false);
					return;
				}

				List<NamespaceImportNode> imports = ListPool<NamespaceImportNode>.Get();
				List<string> finalNames = ListPool<string>.Get();
				m_Node.FindParentsOrFirstChildren(imports);
				foreach (var import in imports)
				{
					finalNames.Add(import.importName + "." + typeName);
				}

				finalNames.Add(typeName);
				m_Icon.EnableInClassList("valid", CheckTypes(finalNames));

				ListPool<NamespaceImportNode>.Release(imports);
				ListPool<string>.Release(finalNames);
			}

			private bool CheckTypes(List<string> types)
			{
				return AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()).Any(t => types.Contains(t.FullName));
			}

			protected override void BuildList(string value, List<string> values)
			{
				if(string.IsNullOrEmpty(value)) return;

				var types = AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(a => a.GetTypes())
					.Where(t => t.Name.StartsWith(value,StringComparison.CurrentCultureIgnoreCase))
					.Select(t => t.FullName).OrderBy(t => t.LevenshteinDistance(value))
					.Take(4);

				foreach (var type in types)
				{
					values.Add(type);
				}
			}
		}
	}
}