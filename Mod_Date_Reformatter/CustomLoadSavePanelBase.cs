using ColossalFramework.Packaging;
using System;
using System.Collections.Generic;

namespace Mod_Date_Reformatter
{
	public abstract class CustomLoadSavePanelBase<T> : ToolsModifierControl where T : MetaData
	{
		private List<MetaData> m_MetaDataList;

		private List<string> m_StringsList;

		private List<Package.Asset> m_AssetsList;

		protected void ClearListing()
		{
			if (this.m_StringsList != null)
			{
				this.m_StringsList.Clear();
			}
			if (this.m_AssetsList != null)
			{
				this.m_AssetsList.Clear();
			}
			if (this.m_MetaDataList != null)
			{
				this.m_MetaDataList.Clear();
			}
		}

		protected void AddToListing(string name, string timeStamp, Package.Asset asset, MetaData mmd)
		{
			if (this.m_StringsList == null)
			{
				this.m_StringsList = new List<string>();
			}
			if (this.m_AssetsList == null)
			{
				this.m_AssetsList = new List<Package.Asset>();
			}
			//CODebugBase<LogChannel>.Log(LogChannel.Core, "mmd:"+mmd.ToString());
			if (this.m_MetaDataList == null)
			{
				this.m_MetaDataList = new List<MetaData>();
			}
			string text = string.Empty;
			if (asset.isCloudAsset)
			{
				text += "<sprite SteamCloud> ";
			}
			if (asset.isWorkshopAsset)
			{
				text += "<sprite SteamWorkshop> ";
			}
			text += name;
			if (timeStamp != null)
			{
				text += Environment.NewLine;


				String dateformat = ModInfo.ModConf.DateFormat;
				if (ModInfo.ModConf.DateFormat == ModInfo.DEFAULT)
				{
					dateformat = "d/MM/yyyy";
				}
				String timeformat = ModInfo.ModConf.TimeFormat;
				if (ModInfo.ModConf.TimeFormat == ModInfo.DEFAULT)
				{
					timeformat = "h:mm:ss tt";
				}
				String datetimeformat = dateformat + " " + timeformat;

				if (asset.isCloudAsset || asset.isWorkshopAsset)
				{
					text = text + "        <color #50869a>" + DateTime.Parse(timeStamp).ToString(datetimeformat) + "</color>";
				}
				else
				{
					text = text + "<color #50869a>" + DateTime.Parse(timeStamp).ToString(datetimeformat) + "</color>";
				}
			}
			this.m_StringsList.Add(text);
			this.m_AssetsList.Add(asset);
			this.m_MetaDataList.Add(mmd);
		}

		protected string[] GetListingItems()
		{
			if (this.m_StringsList == null)
			{
				return new string[0];
			}
			return this.m_StringsList.ToArray();
		}

		protected string GetListingItem(int index)
		{
			return this.m_StringsList[index];
		}

		protected int FindIndexOf(string text)
		{
			if (this.m_AssetsList != null)
			{
				for (int i = 0; i < this.m_AssetsList.Count; i++)
				{
					if (string.Compare(this.m_AssetsList[i].name, text, true) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		protected string GetListingName(int index)
		{
			return this.m_AssetsList[index].name;
		}

		protected Package.Asset GetListingData(int index)
		{
			if (this.m_MetaDataList != null && this.m_MetaDataList.Count > 0)
			{
				return this.m_MetaDataList[index].assetRef;
			}
			return null;
		}

		protected MetaData GetListingMetaData(int index)
		{
			return this.m_MetaDataList[index];
		}

		protected string GetListingPath(int index)
		{
			return this.m_AssetsList[index].package.packagePath;
		}

		protected string GetListingPackageName(int index)
		{
			if (index < 0 || index >= this.m_AssetsList.Count)
			{
				return null;
			}
			return this.m_AssetsList[index].package.packageName;
		}

		protected int GetListingCount()
		{
			if (this.m_AssetsList != null)
			{
				return this.m_AssetsList.Count;
			}
			return 0;
		}
	}
}