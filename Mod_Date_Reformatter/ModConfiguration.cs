using System;
using System.IO;
using System.Xml.Serialization;

namespace Mod_Date_Reformatter
{
	public class ModConfiguration
	{
		public string DateFormat;
		public string TimeFormat;

		public ModConfiguration()
		{
			this.DateFormat = ModInfo.DateFormats[0];
			this.TimeFormat = ModInfo.TimeFormats[0];
		}

		public static bool Serialize(string filename, ModConfiguration config)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModConfiguration));
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(filename))
				{
					xmlSerializer.Serialize(streamWriter, config);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		public static ModConfiguration Deserialize(string filename)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModConfiguration));
			try
			{
				using (StreamReader streamReader = new StreamReader(filename))
				{
					ModConfiguration modConfiguration = (ModConfiguration)xmlSerializer.Deserialize(streamReader);
					if (Array.IndexOf<string>(ModInfo.DateFormats, modConfiguration.DateFormat) < 0)
					{
						modConfiguration.DateFormat = ModInfo.DateFormats[0];
					}
					if (Array.IndexOf<string>(ModInfo.TimeFormats, modConfiguration.TimeFormat) < 0)
					{
						modConfiguration.TimeFormat = ModInfo.TimeFormats[0];
					}
					return modConfiguration;
				}
			}
			catch
			{
			}
			return null;
		}
	}
}
