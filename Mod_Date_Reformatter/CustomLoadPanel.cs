using Mod_Date_Reformatter;
using System.Collections.Generic;

public class CustomLoadPanel : CustomLoadSavePanelBase<SaveGameMetaData>
{
	public static void RedirectCalls(List<RedirectCallsState> callStates)
	{
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "ClearListing", 0);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "AddToListing", 4);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingItems", 0);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingItem", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "FindIndexOf", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingName", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingData", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingMetaData", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingPath", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingPackageName", 1);
		RedirectionHelper.RedirectCalls(callStates, typeof(LoadPanel), typeof(CustomLoadPanel), "GetListingCount", 0);
	}
}
