using Mod_Date_Reformatter;
using System.Collections.Generic;

public class CustomSavePanel : CustomLoadSavePanelBase<SaveGameMetaData>
{
	public static void RedirectCalls(List<RedirectCallsState> callStates)
	{
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "ClearListing", 0);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "AddToListing", 4);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingItems", 0);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingItem", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "FindIndexOf", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingName", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingData", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingMetaData", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingPath", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingPackageName", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(SavePanel), typeof(CustomSavePanel), "GetListingCount", 0);
	}
}
