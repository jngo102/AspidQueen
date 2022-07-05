using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential)]
	internal class CCallbackBaseVTable
	{
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void RunCBDel(IntPtr thisptr, IntPtr pvParam);
	
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void RunCRDel(IntPtr thisptr, IntPtr pvParam, [MarshalAs(UnmanagedType.I1)] bool bIOFailure, ulong hSteamAPICall);
	
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate int GetCallbackSizeBytesDel(IntPtr thisptr);
	
		private const CallingConvention cc = CallingConvention.ThisCall;
	
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public RunCRDel m_RunCallResult;
	
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public RunCBDel m_RunCallback;
	
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public GetCallbackSizeBytesDel m_GetCallbackSizeBytes;
	}
}