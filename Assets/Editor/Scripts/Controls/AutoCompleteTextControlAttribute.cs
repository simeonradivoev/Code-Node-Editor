using System;
using System.Collections.Generic;
using System.Reflection;
using NodeEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Assets.Editor.Scripts.Controls
{
	public class AutoCompleteTextControlAttribute : ControlAttribute
	{
		public string autoCompleteMethodName;

		public AutoCompleteTextControlAttribute(string autoCompleteMethodName)
		{
			this.autoCompleteMethodName = autoCompleteMethodName;
		}

		public override VisualElement InstantiateControl(AbstractNode node, PropertyInfo property)
		{
			return new AutoCompleteView(label,node,node.GetType().GetMethod(autoCompleteMethodName,BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),property);
		}

		public class AutoCompleteView : VisualElement
		{
			protected VisualElement m_FieldContainer;
			private List<Label> m_Labels;
			private TextField m_TextField;
			private MethodInfo m_AutoCompleteMethod;
			protected PropertyInfo m_Property;
			protected AbstractNode m_Node;
			private List<string> m_Options;
			protected Action m_OnTextSet;

			public AutoCompleteView(string label, AbstractNode node, MethodInfo autoCompleteMethod, PropertyInfo property)
			{
				AddStyleSheetPath("Styles/Controls/AutoCompleteControlView");
				m_FieldContainer = new VisualElement();
				m_FieldContainer.AddToClassList("Container");

				if (!string.IsNullOrEmpty(label))
					Add(new Label(label));

				m_Options = new List<string>();
				m_Node = node;
				m_AutoCompleteMethod = autoCompleteMethod;
				m_Property = property;
				m_Labels = new List<Label>();
				m_TextField = new TextField();
				m_TextField.RegisterCallback<FocusOutEvent>(OnFocusOutEvent);
				m_TextField.RegisterCallback<KeyDownEvent>(OnKeyDown);
				m_TextField.RegisterCallback<ExecuteCommandEvent>(e => Debug.Log(e.commandName));
				m_TextField.value = property.GetValue(node) as string;
				m_TextField.OnValueChanged(OnTextChange);
				m_FieldContainer.Add(m_TextField);
				Add(m_FieldContainer);
			}

			protected virtual void BuildList(string value, List<string> values)
			{
				m_AutoCompleteMethod.Invoke(m_Node, new object[] { value, values });
			}

			protected virtual void OnTextChange(ChangeEvent<string> e)
			{
				m_Options.Clear();
				BuildList(e.newValue, m_Options);

				int optionsCount = Mathf.Min(m_Options.Count,4);

				if (m_Labels.Count > optionsCount)
				{
					for (int i = optionsCount; i < m_Labels.Count; i++)
					{
						Remove(m_Labels[i]);
						m_Labels[i] = null;
					}
				}
				else if (m_Labels.Count < optionsCount)
				{
					for (int i = m_Labels.Count; i < optionsCount; i++)
					{
						var label = new Label();
						label.AddToClassList("auto-complete");
						int index = i;
						label.RegisterCallback<MouseDownEvent>(evt =>
						{
							SetValue(index);
							e.StopPropagation();
						});
						m_Labels.Add(label);
						Add(label);
					}
				}

				m_Labels.RemoveAll(l => l == null);

				for (int i = 0; i < m_Labels.Count; i++)
				{
					m_Labels[i].text = m_Options[i];
				}
			}

			private void OnKeyDown(KeyDownEvent e)
			{
				if (e.keyCode == KeyCode.Space)
				{
					if (m_Options.Count == 1)
					{
						SetValue(0);
						e.StopPropagation();
					}
				}
				else if (e.keyCode == KeyCode.Return)
				{
					m_Property.SetValue(m_Node, m_TextField.value);
					m_FieldContainer.Focus();
					m_OnTextSet?.Invoke();
					m_Node.Dirty(ModificationScope.Node);
				}
			}

			private void SetValue(int index)
			{
				m_Node.owner.owner.RegisterCompleteObjectUndo("Type Modified");
				m_Property.SetValue(m_Node, m_Options[index]);
				m_OnTextSet?.Invoke();
				m_TextField.value = m_Options[index];
				m_TextField.SelectAll();
				m_Node.Dirty(ModificationScope.Node);
			}

			private void OnFocusOutEvent(FocusOutEvent e)
			{
				ClearLabels();
				m_TextField.value = (string)m_Property.GetValue(m_Node);
			}

			private void ClearLabels()
			{
				foreach (var label in m_Labels)
				{
					Remove(label);
				}

				m_Labels.Clear();
				m_Options.Clear();
			}
		}
	}
}