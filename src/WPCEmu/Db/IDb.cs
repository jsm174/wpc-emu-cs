namespace WPCEmu.Db
{
	public interface IDb
	{
		string name { get; }
		string version { get; }
		Pinmame? pinmame { get; }
		RomFile? rom { get; }
		SwitchMapping[] switchMapping { get; }
		FliptronicsMapping[] fliptronicsMappings { get; }
		SolenoidMapping[] solenoidMapping { get; }
		Playfield? playfield { get; }
		bool skipWpcRomCheck { get; }
		string[] features { get; }
		string[] cabinetColors { get;  }
		Initialise? initialise { get; }
		MemoryPosition? memoryPosition { get; }
		string[] testErrors { get; }
	}
}