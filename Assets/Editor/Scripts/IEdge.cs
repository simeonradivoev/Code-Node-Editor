﻿namespace NodeEditor
{
	public interface IEdge
	{
		SlotReference outputSlot { get; }
		SlotReference inputSlot { get; }
	}
}