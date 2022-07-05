using System;

namespace InControl
{
	
	public enum InputControlType
	{
		None = 0,
		LeftStickUp = 1,
		LeftStickDown = 2,
		LeftStickLeft = 3,
		LeftStickRight = 4,
		LeftStickButton = 5,
		RightStickUp = 6,
		RightStickDown = 7,
		RightStickLeft = 8,
		RightStickRight = 9,
		RightStickButton = 10,
		DPadUp = 11,
		DPadDown = 12,
		DPadLeft = 13,
		DPadRight = 14,
		LeftTrigger = 15,
		RightTrigger = 16,
		LeftBumper = 17,
		RightBumper = 18,
		Action1 = 19,
		Action2 = 20,
		Action3 = 21,
		Action4 = 22,
		Action5 = 23,
		Action6 = 24,
		Action7 = 25,
		Action8 = 26,
		Action9 = 27,
		Action10 = 28,
		Action11 = 29,
		Action12 = 30,
		Back = 100,
		Start = 101,
		Select = 102,
		System = 103,
		Options = 104,
		Pause = 105,
		Menu = 106,
		Share = 107,
		Home = 108,
		View = 109,
		Power = 110,
		Capture = 111,
		Assistant = 112,
		Plus = 113,
		Minus = 114,
		Create = 115,
		Mute = 116,
		PedalLeft = 150,
		PedalRight = 151,
		PedalMiddle = 152,
		GearUp = 153,
		GearDown = 154,
		Pitch = 200,
		Roll = 201,
		Yaw = 202,
		ThrottleUp = 203,
		ThrottleDown = 204,
		ThrottleLeft = 205,
		ThrottleRight = 206,
		POVUp = 207,
		POVDown = 208,
		POVLeft = 209,
		POVRight = 210,
		TiltX = 250,
		TiltY = 251,
		TiltZ = 252,
		ScrollWheel = 253,
		[Obsolete("Use InputControlType.TouchPadButton instead.", true)]
		TouchPadTap = 254,
		TouchPadButton = 255,
		TouchPadXAxis = 256,
		TouchPadYAxis = 257,
		LeftSL = 258,
		LeftSR = 259,
		RightSL = 260,
		RightSR = 261,
		Command = 300,
		LeftStickX = 301,
		LeftStickY = 302,
		RightStickX = 303,
		RightStickY = 304,
		DPadX = 305,
		DPadY = 306,
		LeftCommand = 307,
		RightCommand = 308,
		Analog0 = 400,
		Analog1 = 401,
		Analog2 = 402,
		Analog3 = 403,
		Analog4 = 404,
		Analog5 = 405,
		Analog6 = 406,
		Analog7 = 407,
		Analog8 = 408,
		Analog9 = 409,
		Analog10 = 410,
		Analog11 = 411,
		Analog12 = 412,
		Analog13 = 413,
		Analog14 = 414,
		Analog15 = 415,
		Analog16 = 416,
		Analog17 = 417,
		Analog18 = 418,
		Analog19 = 419,
		Button0 = 500,
		Button1 = 501,
		Button2 = 502,
		Button3 = 503,
		Button4 = 504,
		Button5 = 505,
		Button6 = 506,
		Button7 = 507,
		Button8 = 508,
		Button9 = 509,
		Button10 = 510,
		Button11 = 511,
		Button12 = 512,
		Button13 = 513,
		Button14 = 514,
		Button15 = 515,
		Button16 = 516,
		Button17 = 517,
		Button18 = 518,
		Button19 = 519,
		Count = 520
	}
}