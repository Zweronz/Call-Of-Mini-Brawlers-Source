using System.Collections.Generic;

public class ZS_PublishAllMapBoxInfo
{
	public delegate void GetAllMapBoxList(List<ZS_MapBoxInfo> list);

	public GetAllMapBoxList mapBoxList { get; private set; }

	public ZS_PublishAllMapBoxInfo(GetAllMapBoxList list)
	{
		mapBoxList = list;
	}
}
