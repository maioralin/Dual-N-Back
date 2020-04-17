package crc641a72e2c85b3880cc;


public class PlatformSpecificFunctions
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DualNBack.Droid.PlatformSpecificFunctions, DualNBack.Android", PlatformSpecificFunctions.class, __md_methods);
	}


	public PlatformSpecificFunctions ()
	{
		super ();
		if (getClass () == PlatformSpecificFunctions.class)
			mono.android.TypeManager.Activate ("DualNBack.Droid.PlatformSpecificFunctions, DualNBack.Android", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
