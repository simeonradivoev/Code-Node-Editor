using System;
using UnityEngine;

namespace NodeEditor
{
	public class GraphObject : ScriptableObject, ISerializationCallbackReceiver, IGraphObject
	{
		[SerializeField, HideInInspector]
		bool m_IsDirty;

		[SerializeField,HideInInspector]
		SerializationHelper.JSONSerializedElement m_SerializedGraph;

		public virtual Type GraphType => typeof(NodeGraph);
		private bool m_IsEnabled;

		IGraph m_DeserializedGraph;
		IGraph m_Graph;

		public IGraph graph
		{
			get { return m_Graph; }
			set
			{
				if (m_Graph != null)
				{
					m_Graph.owner = null;
					m_Graph.onNodeAdded += OnNodeAdded;
				}

				m_Graph = value;
				if (m_Graph != null)
				{
					m_Graph.owner = this;
					m_Graph.onNodeAdded -= OnNodeAdded;
				}
			}
		}

		public virtual void OnBeforeSerialize()
		{
			if (graph != null)
				m_SerializedGraph = SerializationHelper.Serialize(graph,false);
		}

		public virtual void OnAfterDeserialize()
		{
			try
			{
				var deserializedGraph = SerializationHelper.Deserialize<IGraph>(m_SerializedGraph, null);
				if (graph == null)
					graph = deserializedGraph;
				else
					m_DeserializedGraph = deserializedGraph;
			}
			catch (Exception e)
			{
				// ignored
			}
		}

		public void RegisterCompleteObjectUndo(string name)
		{
#if UNITY_EDITOR
			UnityEditor.Undo.RegisterCompleteObjectUndo(this, name);
#endif
		}

		private void OnNodeAdded(INode node)
		{
			if (!m_IsEnabled && Application.isPlaying) return;
			var onEnableNode = node as IOnAssetEnabled;
			onEnableNode?.OnEnable();
		}

		void ValidateInternal()
		{
			if (graph != null)
			{
				graph.owner = this;
				graph.OnEnable();
				graph.ValidateGraph();
				graph.onNodeAdded += OnNodeAdded;
			}
		}

		void OnEnable()
		{
			m_IsEnabled = true;
			ValidateInternal();

#if UNITY_EDITOR
			UnityEditor.Undo.undoRedoPerformed += UndoRedoPerformed;
#endif
			UndoRedoPerformed();
		}

		void OnDisable()
		{
#if UNITY_EDITOR
			UnityEditor.Undo.undoRedoPerformed -= UndoRedoPerformed;
#endif
			(graph as IDisposable)?.Dispose();
			if (graph != null) graph.onNodeAdded -= OnNodeAdded;
		}

		protected void UndoRedoPerformed()
		{
			if (m_DeserializedGraph != null)
			{
				graph.ReplaceWith(m_DeserializedGraph);
				m_DeserializedGraph = null;
			}
		}

		public void SetDirty(bool dirty)
		{
			m_IsDirty = dirty;
		}

		public bool isDirty
		{
			get { return m_IsDirty; }
		}
	}
}