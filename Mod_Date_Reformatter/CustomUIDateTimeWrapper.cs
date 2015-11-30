using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mod_Date_Reformatter
{
	public class CustomUIDateTimeWrapper : UIWrapper<DateTime>
	{
		public CustomUIDateTimeWrapper(DateTime def) : base(def)
		{
		}

		public override void Check(DateTime newVal)
		{
			if (this.m_Value.Year != newVal.Year || this.m_Value.Month != newVal.Month || this.m_Value.Day != newVal.Day)
			{
				this.m_Value = newVal;
				String format = ModInfo.ModConf.DateFormat;
                if (ModInfo.ModConf.DateFormat == ModInfo.DEFAULT)
				{
					format = "dd/MM/yyyy";
				}
				this.m_String = this.m_Value.Date.ToString(format);
			}
		}

		public static void RedirectCalls(List<RedirectCallsState> callStates)
		{
			RedirectionHelper.RedirectCalls(callStates, typeof(UIDateTimeWrapper), typeof(CustomUIDateTimeWrapper), "Check", 1);
		}
	}
}