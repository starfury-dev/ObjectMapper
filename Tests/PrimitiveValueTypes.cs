using StarFuryDev.ObjectMapper.Attributes;

namespace StarFuryDev.ObjectMapper.Tests;

internal class ValueTypesSource
{
	public bool BoolProp { get; set; }
	public byte ByteProp { get; set; }
	public sbyte SByteProp { get; set; }
	public char CharProp { get; set; }
	public decimal DecimalProp { get; set; }
	public double DoubleProp { get; set; }
	public float FloatProp { get; set; }
	public int IntProp { get; set; }
	public uint UIntProp { get; set; }
	public nint NintProp { get; set; }
	public nuint UuintProp { get; set; }
	public long LongProp { get; set; }
	public ulong UlongProp { get; set; }
	public short ShortProp { get; set; }
	public ushort UshortProp { get; set; }
}

[MapFrom(nameof(ValueTypesSource))]
internal class ValueTypesDestination
{
	public bool BoolProp { get; set; }
	public byte ByteProp { get; set; }
	public sbyte SByteProp { get; set; }
	public char CharProp { get; set; }
	public decimal DecimalProp { get; set; }
	public double DoubleProp { get; set; }
	public float FloatProp { get; set; }
	public int IntProp { get; set; }
	public uint UIntProp { get; set; }
	public nint NintProp { get; set; }
	public nuint UuintProp { get; set; }
	public long LongProp { get; set; }
	public ulong UlongProp { get; set; }
	public short ShortProp { get; set; }
	public ushort UshortProp { get; set; }
}
