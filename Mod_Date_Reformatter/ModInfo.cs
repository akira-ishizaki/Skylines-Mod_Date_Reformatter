using ICities;
using System;
using System.Collections.Generic;
using ColossalFramework.Plugins;
using ColossalFramework;
using UnityEngine;
using System.IO;

namespace Mod_Date_Reformatter
{
	public class ModInfo : IUserMod
	{
		public const string SETTINGFILENAME = "DateReformatter.xml";
		public string MODNAME = "Date Reformatter";
		public const string DEFAULT = "(Default)";

		public static string configPath;

		List<RedirectCallsState> m_redirectionStates = new List<RedirectCallsState>();

		public RedirectCallsState[] revertMethods = new RedirectCallsState[30];

		public bool Inited = false;

		private PluginManager.PluginInfo pluginInfo;

		public static string[] DateFormats = new string[]
		{
			DEFAULT,
			"dd/MM/yyyy",
			"dd.MM.yyyy",
            "dd/MM/yy",
            "dd-MM-yy",
			"dd MM yy",
            "dd.MM.yy",
            "dd.M.yy",
            "d/MM/yyyy",
            "d/MM/yy",
            "d-MM-yy",
            "d.MM.yy",
            "d/M/yyyy",
            "d.M.yyyy",
            "d/M/yy",
            "d.M.yy",
            "dd/MMM/yyyy",
			"dd-MMM-yyyy",
			"dd MMM yyyy",
            "dd MMM yy",
            "d-MMM-yyyy",
			"d MMM yyyy",
            "d.MMM yyyy",
            "d-MMM-yy",
            "d/MMM/yy",
            "d MMM yy",
            "d.MMM.yy",
            "MMMM dd",
            "MMMM d",
            "MMM dd, yyyy",
			"MMM dd yyyy",
			"MM/dd/yyyy",
            "MM/dd/yy",
            "M/d/yyyy",
            "M/d/yy",
            "yyyy/MM/dd",
			"yyyy-MM-dd",
			"yyyy MM dd",
			"yyyy MMM d",
			"yyyy/M/d",
            "yy/MM/dd (ddd)",
            "yy/MM/dd",
            "yy-MM-dd",
            "yy MM dd",
            "yy/M/d (ddd)",
            "yy/M/d",
            "y/M/d (ddd)",
            "y/M/d",
            "y/M/d (ddd)",
        };

		public static string[] TimeFormats = new string[]
		{
			DEFAULT,
			"h:mm:ss tt",
			"hh:mm:ss tt",
			"tt h:mm:ss",
			"tt hh:mm:ss",
			"H:mm:ss",
			"HH:mm:ss",
			"h:mm tt",
			"hh:mm tt",
			"tt h:mm",
			"tt hh:mm",
			"H:mm",
			"HH:mm",
		};

		public string Name
		{
			get
			{
				Setup();
				return MODNAME;
			}
		}

		public static ModConfiguration ModConf;

		private void Setup()
		{
			try
			{
				PluginsChanged();
				PluginManager.instance.eventPluginsChanged += PluginsChanged;
				PluginManager.instance.eventPluginsStateChanged += PluginsChanged;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning, "[" + MODNAME + "] " + e.GetType() + ": " + e.Message);
			}
		}

		public string Description
		{
			get { return "This changes the date and time format."; }
		}

		public ModInfo()
		{
			this.pluginInfo = getPluginInfo();
		}

		private void PluginsChanged()
		{
			if (isActive())
			{
				if (!Inited)
				{
					this.InitConfigFile();
					CustomUIDateTimeWrapper.RedirectCalls(m_redirectionStates);
					CustomSavePanel.RedirectCalls(m_redirectionStates);
					CustomLoadPanel.RedirectCalls(m_redirectionStates);
					Inited = true;
				}
            }
			else
			{
				if (Inited)
				{
					ModConf.DateFormat = DEFAULT;
					ModConf.TimeFormat = DEFAULT;
					foreach (RedirectCallsState rcs in m_redirectionStates)
					{
						RedirectionHelper.RevertRedirect(rcs);
					}
					Inited = false;
				}
			}
		}

		public bool isActive()
		{
			if (this.pluginInfo == null)
			{
				return true;
			}
			return this.pluginInfo.isEnabled;
		}

		private PluginManager.PluginInfo getPluginInfo()
		{
			foreach (PluginManager.PluginInfo current in Singleton<PluginManager>.instance.GetPluginsInfo())
			{
				if (current.name == "565071445" && current.publishedFileID.ToString() == "565071445")
				{
					return current;
				}
			}
			return null;
		}

		public void OnSettingsUI(UIHelperBase helper)
		{
			if (!isActive())
			{
				return;
			}
			this.InitConfigFile();
			UIHelperBase group = helper.AddGroup("Format Settings");
			int num = Array.IndexOf<string>(ModInfo.DateFormats, ModInfo.ModConf.DateFormat);
			if (num < 0)
			{
				num = 0;
			}
			group.AddDropdown("Date format", ModInfo.DateFormats, num, delegate (int c)
			{
				ModInfo.ModConf.DateFormat = ModInfo.DateFormats[c];
				ModConfiguration.Serialize(ModInfo.configPath, ModInfo.ModConf);
			});
			int num2 = Array.IndexOf<string>(ModInfo.TimeFormats, ModInfo.ModConf.TimeFormat);
			if (num2 < 0)
			{
				num2 = 0;
			}
			group.AddDropdown("Time format", ModInfo.TimeFormats, num2, delegate (int c)
			{
				ModInfo.ModConf.TimeFormat = ModInfo.TimeFormats[c];
				ModConfiguration.Serialize(ModInfo.configPath, ModInfo.ModConf);
			});
		}

		private void InitConfigFile()
		{
			try
			{
				string pathName = GameSettings.FindSettingsFileByName("gameSettings").pathName;
				string str = "";
				if (pathName != "")
				{
					str = Path.GetDirectoryName(pathName) + Path.DirectorySeparatorChar;
				}
				ModInfo.configPath = str + SETTINGFILENAME;
				ModInfo.ModConf = ModConfiguration.Deserialize(ModInfo.configPath);
				if (ModInfo.ModConf == null)
				{
					ModInfo.ModConf = ModConfiguration.Deserialize(SETTINGFILENAME);
					if (ModInfo.ModConf != null && ModConfiguration.Serialize(str + SETTINGFILENAME, ModInfo.ModConf))
					{
						try
						{
							File.Delete(SETTINGFILENAME);
						}
						catch
						{
						}
					}
				}
				if (ModInfo.ModConf == null)
				{
					ModInfo.ModConf = new ModConfiguration();
					if (!ModConfiguration.Serialize(ModInfo.configPath, ModInfo.ModConf))
					{
						ModInfo.configPath = SETTINGFILENAME;
						ModConfiguration.Serialize(ModInfo.configPath, ModInfo.ModConf);
					}
				}
			}
			catch
			{
			}
		}
	}
}