using System.Collections.Generic;

namespace NodeEditor.Util
{
	public static class INodeExtensions
	{
		public static void FindParentsOrFirstChildren<T>(this INode from, IList<T> nodes) where T : INode
		{
			HashSet<INode> burned = new HashSet<INode>();
			Queue<INode> queue = new Queue<INode>();

			queue.Enqueue(from);

			List<ISlot> slots = ListPool<ISlot>.Get();
			List<INode> children = ListPool<INode>.Get();

			try
			{
				while (queue.Count > 0)
				{
					var head = queue.Dequeue();
					if (!burned.Contains(head))
					{
						if (head is T)
							nodes.Add((T)head);

						slots.Clear();
						head.GetInputSlots(slots);
						foreach (var inputSlot in slots)
						{
							children.Clear();
							GetChildren(from.owner,inputSlot.slotReference, children);
							foreach (var child in children)
							{
								if (child is T)
									nodes.Add((T)child);
							}
						}

						burned.Add(head);
						slots.Clear();
						head.GetOutputSlots(slots);
						foreach (var slot in slots)
						{
							GetParents(from.owner,slot.slotReference, queue);
						}
					}
				}
			}
			finally
			{
				ListPool<ISlot>.Release(slots);
				ListPool<INode>.Release(children);
			}
		}

		private static void GetChildren(IGraph graph,SlotReference slot, IList<INode> nodes)
		{
			var m_edges = graph.GetEdges(slot);
			foreach (var edge in m_edges)
			{
				var otherSlot = edge.inputSlot.Equals(slot) ? edge.outputSlot : edge.inputSlot;
				nodes.Add(graph.GetNodeFromGuid(otherSlot.nodeGuid));
			}
		}

		private static void GetParents(IGraph graph, SlotReference slot, Queue<INode> nodes)
		{
			var m_edges = graph.GetEdges(slot);
			foreach (var edge in m_edges)
			{
				var otherSlot = edge.inputSlot.Equals(slot) ? edge.outputSlot : edge.inputSlot;
				nodes.Enqueue(graph.GetNodeFromGuid(otherSlot.nodeGuid));
			}
		}
	}
}