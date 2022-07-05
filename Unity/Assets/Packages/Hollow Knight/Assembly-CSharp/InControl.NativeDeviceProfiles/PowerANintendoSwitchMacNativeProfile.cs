namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class PowerANintendoSwitchMacNativeProfile : InputDeviceProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PowerA Nintendo Switch Controller";
			base.DeviceNotes = "PowerA Nintendo Switch Controller on Mac";
			base.DeviceClass = InputDeviceClass.Controller;
			base.DeviceStyle = InputDeviceStyle.NintendoSwitch;
			base.IncludePlatforms = new string[1] { "OS X" };
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)8406,
					ProductID = (ushort)42769
				}
			};
			base.ButtonMappings = new InputControlMapping[18]
			{
				new InputControlMapping
				{
					Name = "Action3",
					Target = InputControlType.Action3,
					Source = InputDeviceProfile.Button(0)
				},
				new InputControlMapping
				{
					Name = "Action1",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(1)
				},
				new InputControlMapping
				{
					Name = "Action2",
					Target = InputControlType.Action2,
					Source = InputDeviceProfile.Button(2)
				},
				new InputControlMapping
				{
					Name = "Action4",
					Target = InputControlType.Action4,
					Source = InputDeviceProfile.Button(3)
				},
				new InputControlMapping
				{
					Name = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = InputDeviceProfile.Button(4)
				},
				new InputControlMapping
				{
					Name = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = InputDeviceProfile.Button(5)
				},
				new InputControlMapping
				{
					Name = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					Source = InputDeviceProfile.Button(6)
				},
				new InputControlMapping
				{
					Name = "Right Trigger",
					Target = InputControlType.RightTrigger,
					Source = InputDeviceProfile.Button(7)
				},
				new InputControlMapping
				{
					Name = "Minus",
					Target = InputControlType.Minus,
					Source = InputDeviceProfile.Button(8)
				},
				new InputControlMapping
				{
					Name = "Plus",
					Target = InputControlType.Plus,
					Source = InputDeviceProfile.Button(9)
				},
				new InputControlMapping
				{
					Name = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = InputDeviceProfile.Button(10)
				},
				new InputControlMapping
				{
					Name = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = InputDeviceProfile.Button(11)
				},
				new InputControlMapping
				{
					Name = "Home",
					Target = InputControlType.Home,
					Source = InputDeviceProfile.Button(12)
				},
				new InputControlMapping
				{
					Name = "Capture",
					Target = InputControlType.Capture,
					Source = InputDeviceProfile.Button(13)
				},
				new InputControlMapping
				{
					Name = "DPad Up",
					Target = InputControlType.DPadUp,
					Source = InputDeviceProfile.Button(14)
				},
				new InputControlMapping
				{
					Name = "DPad Down",
					Target = InputControlType.DPadDown,
					Source = InputDeviceProfile.Button(15)
				},
				new InputControlMapping
				{
					Name = "DPad Left",
					Target = InputControlType.DPadLeft,
					Source = InputDeviceProfile.Button(16)
				},
				new InputControlMapping
				{
					Name = "DPad Right",
					Target = InputControlType.DPadRight,
					Source = InputDeviceProfile.Button(17)
				}
			};
			base.AnalogMappings = new InputControlMapping[8]
			{
				new InputControlMapping
				{
					Name = "Left Stick Left",
					Target = InputControlType.LeftStickLeft,
					Source = InputDeviceProfile.Analog(0),
					SourceRange = InputRangeType.ZeroToMinusOne,
					TargetRange = InputRangeType.ZeroToOne
				},
				new InputControlMapping
				{
					Name = "Left Stick Right",
					Target = InputControlType.LeftStickRight,
					Source = InputDeviceProfile.Analog(0),
					SourceRange = InputRangeType.ZeroToOne,
					TargetRange = InputRangeType.ZeroToOne
				},
				new InputControlMapping
				{
					Name = "Left Stick Up",
					Target = InputControlType.LeftStickUp,
					Source = InputDeviceProfile.Analog(1),
					SourceRange = InputRangeType.ZeroToMinusOne,
					TargetRange = InputRangeType.ZeroToOne
				},
				new InputControlMapping
				{
					Name = "Left Stick Down",
					Target = InputControlType.LeftStickDown,
					Source = InputDeviceProfile.Analog(1),
					SourceRange = InputRangeType.ZeroToOne,
					TargetRange = InputRangeType.ZeroToOne
				},
				new InputControlMapping
				{
					Name = "Right Stick Left",
					Target = InputControlType.RightStickLeft,
					Source = InputDeviceProfile.Analog(2),
					SourceRange = InputRangeType.ZeroToMinusOne,
					TargetRange = InputRangeType.ZeroToOne
				},
				new InputControlMapping
				{
					Name = "Right Stick Right",
					Target = InputControlType.RightStickRight,
					Source = InputDeviceProfile.Analog(2),
					SourceRange = InputRangeType.ZeroToOne,
					TargetRange = InputRangeType.ZeroToOne
				},
				new InputControlMapping
				{
					Name = "Right Stick Up",
					Target = InputControlType.RightStickUp,
					Source = InputDeviceProfile.Analog(3),
					SourceRange = InputRangeType.ZeroToMinusOne,
					TargetRange = InputRangeType.ZeroToOne
				},
				new InputControlMapping
				{
					Name = "Right Stick Down",
					Target = InputControlType.RightStickDown,
					Source = InputDeviceProfile.Analog(3),
					SourceRange = InputRangeType.ZeroToOne,
					TargetRange = InputRangeType.ZeroToOne
				}
			};
		}
	}
}