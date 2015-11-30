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
			MethodInfo originalMethod = typeof(UIDateTimeWrapper).GetMethods().FirstOrDefault(m => m.Name == "Check" && m.GetParameters().Length == 1);
			MethodInfo replacementMethod = typeof(CustomUIDateTimeWrapper).GetMethods().FirstOrDefault(m => m.Name == "Check" && m.GetParameters().Length == 1);

			if (originalMethod != null && replacementMethod != null)
			{
				callStates.Add(RedirectionHelper.RedirectCalls(originalMethod, replacementMethod));
			}
		}
	}
}