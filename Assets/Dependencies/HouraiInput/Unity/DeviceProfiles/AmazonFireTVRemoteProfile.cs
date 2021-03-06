using UnityEngine;

namespace HouraiTeahouse.HouraiInput {

    public class AmazonFireTVRemote : UnityInputDeviceProfile {

        public AmazonFireTVRemote() {
            Name = "Amazon Fire TV Remote";
            Meta = "Amazon Fire TV Remote on Amazon Fire TV";

            SupportedPlatforms = new[] {"Amazon AFTB", "Amazon AFTM"};

            JoystickNames = new[] {"", "Amazon Fire TV Remote"};

            ButtonMappings = new[] {
                new InputMapping {Handle = "A", Target = InputTarget.Action1, Source = Button0},
                new InputMapping {Handle = "Back", Target = InputTarget.Select, Source = KeyCodeButton(KeyCode.Escape)}
            };

            AnalogMappings = new[] {
                new InputMapping {
                    Handle = "DPad Left",
                    Target = InputTarget.DPadLeft,
                    Source = Analog4,
                    SourceRange = InputMapping.Negative,
                    TargetRange = InputMapping.Negative,
                    Invert = true
                },
                new InputMapping {
                    Handle = "DPad Right",
                    Target = InputTarget.DPadRight,
                    Source = Analog4,
                    SourceRange = InputMapping.Positive,
                    TargetRange = InputMapping.Positive
                },
                new InputMapping {
                    Handle = "DPad Up",
                    Target = InputTarget.DPadUp,
                    Source = Analog5,
                    SourceRange = InputMapping.Negative,
                    TargetRange = InputMapping.Negative,
                    Invert = true
                },
                new InputMapping {
                    Handle = "DPad Down",
                    Target = InputTarget.DPadDown,
                    Source = Analog5,
                    SourceRange = InputMapping.Positive,
                    TargetRange = InputMapping.Positive,
                }
            };
        }

    }

}