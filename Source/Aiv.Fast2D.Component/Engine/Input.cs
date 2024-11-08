using System;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {

    public enum MouseButton {
        LeftMouse,
        RightMouse,
        MiddleMouse,
        Mouse1,
        Mouse2,
        Mouse3,
        Mouse4,
        Mouse5,
        Mouse6,
        Mouse7,
        Mouse8,
        Mouse9
    }

    public enum JoystickButton {
        ButtonA,
        ButtonB,
        ButtonX,
        ButtonY,
        DPadLeft,
        DPadRight,
        DPadUp,
        DPadDown,
        ShoulderLeft,
        ShoulderRight,
        None
    }

    public enum JoystickAxis {
        LeftStick_Horizontal,
        LeftStick_Vertical,
        RightStick_Horizontal,
        RightStick_Vertical,
        ShoulderTriggerLeft,
        ShoulderTriggerRight,
        None
    }

    public static class Input {

        private static Array keyCodeValues;
        private static Array mouseButtonValues;
        private static Array joystickButtonValues;

        private static Dictionary<KeyCode, bool> lastKeyValues;
        private static Dictionary<MouseButton, bool> lastMouseButtonValues;
        private static Dictionary<JoystickButton, bool>[] lastJoysticksValues;
        private static Dictionary<string, UserButton> userButtons;
        private static Dictionary<string, UserAxis> userAxies;

        static Input () {
            keyCodeValues = Enum.GetValues(typeof(KeyCode));
            mouseButtonValues = Enum.GetValues(typeof(MouseButton));
            joystickButtonValues = Enum.GetValues(typeof(JoystickButton));
            lastKeyValues = new Dictionary<KeyCode, bool>();
            lastMouseButtonValues = new Dictionary<MouseButton, bool>();
            userButtons = new Dictionary<string, UserButton>();
            userAxies = new Dictionary<string, UserAxis>();
            foreach (KeyCode key in keyCodeValues) {
                lastKeyValues.Add(key, false);
            }
            foreach (MouseButton btn in mouseButtonValues) {
                lastMouseButtonValues.Add(btn, false);
            }
            lastJoysticksValues = new Dictionary<JoystickButton, bool>[Window.Joysticks.Length];
            for (int i = 0; i < lastJoysticksValues.Length; i++) {
                lastJoysticksValues[i] = new Dictionary<JoystickButton, bool>();
                foreach (JoystickButton btn in joystickButtonValues) {
                    lastJoysticksValues[i].Add(btn, false);
                }
            }
            
        }

        public static void PerformLastKey () {
            foreach (KeyCode key in keyCodeValues) {
                lastKeyValues[key] = Game.Win.GetKey(key);
            }
            foreach (MouseButton btn in mouseButtonValues) {
                lastMouseButtonValues[btn] = FromMouseButtonToBool(btn);
            }
            for (int i = 0; i < lastJoysticksValues.Length; i++) {
                foreach (JoystickButton btn in joystickButtonValues) {
                    lastJoysticksValues[i][btn] = FromJoystickButtonToBool(btn, i);
                }
            }
        }

        public static bool GetKeyDown (KeyCode key) {
            return !lastKeyValues[key] && Game.Win.GetKey(key);
        }

        public static bool GetKey (KeyCode key) {
            return lastKeyValues[key] && Game.Win.GetKey(key);
        }

        public static bool GetKeyUp (KeyCode key) {
            return lastKeyValues[key] && !Game.Win.GetKey(key);
        }

        public static bool GetMouseButtonDown (MouseButton mouseButton) {
            return !lastMouseButtonValues[mouseButton] && FromMouseButtonToBool(mouseButton);
        }

        public static bool GetMouseButton (MouseButton mouseButton) {
            return lastMouseButtonValues[mouseButton] && FromMouseButtonToBool(mouseButton);
        }

        public static bool GetMouseButtonUp (MouseButton mouseButton) {
            return lastMouseButtonValues[mouseButton] && !FromMouseButtonToBool(mouseButton);
        }

        public static bool GetJoystickButtonDown (JoystickButton button, int index) {
            return !lastJoysticksValues[index][button] && FromJoystickButtonToBool(button, index);
        }

        public static bool GetJoystickButton (JoystickButton button, int index) {
            return lastJoysticksValues[index][button] && FromJoystickButtonToBool(button, index);
        }

        public static bool GetJoystickButtonUp (JoystickButton button, int index) {
            return lastJoysticksValues[index][button] && !FromJoystickButtonToBool(button, index);
        }

        public static float GetJoystickAxis (JoystickAxis axis, int index) {
            if (index >= Window.Joysticks.Length) {
                return 0;
            }
            switch (axis) {
                case JoystickAxis.LeftStick_Horizontal:
                    return Game.Win.JoystickAxisLeft(index).X;
                case JoystickAxis.LeftStick_Vertical:
                    return Game.Win.JoystickAxisLeft(index).Y;
                case JoystickAxis.RightStick_Horizontal:
                    return Game.Win.JoystickAxisRight(index).X;
                case JoystickAxis.RightStick_Vertical:
                    return Game.Win.JoystickAxisRight(index).Y;
                case JoystickAxis.ShoulderTriggerLeft:
                    return Game.Win.JoystickTriggerLeft(index);
                case JoystickAxis.ShoulderTriggerRight:
                    return Game.Win.JoystickTriggerRight(index);
                default:
                    return 0;
            }
        }

        public static void AddUserButton (string name, ButtonMatch[] buttonMatches) {
            userButtons.Add(name, new UserButton(buttonMatches));
        }

        public static bool GetButtonDown (string buttonName) {
            return userButtons[buttonName].GetButtonDown();
        }

        public static bool GetButton (string buttonName) {
            return userButtons[buttonName].GetButton();
        }

        public static bool GetButtonUp (string buttonName) {
            return userButtons[buttonName].GetButtonUp();
        }

        public static void AddUserAxis (string name, AxisMatch[] axisMatches) {
            userAxies.Add(name, new UserAxis(axisMatches));
        }

        public static float GetUserAxis (string name) {
            return userAxies[name].GetAxis();
        }

        private static bool FromMouseButtonToBool (MouseButton mouseButton) {
            switch (mouseButton) {
                case MouseButton.LeftMouse:
                    return Game.Win.MouseLeft;
                case MouseButton.MiddleMouse:
                    return Game.Win.MouseMiddle;
                case MouseButton.RightMouse:
                    return Game.Win.MouseRight;
                case MouseButton.Mouse1:
                    return Game.Win.MouseButton1;
                case MouseButton.Mouse2:
                    return Game.Win.MouseButton2;
                case MouseButton.Mouse3:
                    return Game.Win.MouseButton3;
                case MouseButton.Mouse4:
                    return Game.Win.MouseButton4;
                case MouseButton.Mouse5:
                    return Game.Win.MouseButton5;
                case MouseButton.Mouse6:
                    return Game.Win.MouseButton6;
                case MouseButton.Mouse7:
                    return Game.Win.MouseButton7;
                case MouseButton.Mouse8:
                    return Game.Win.MouseButton8;
                case MouseButton.Mouse9:
                    return Game.Win.MouseButton9;
                default:
                    return false;
            }
        }


        private static bool FromJoystickButtonToBool (JoystickButton button, int index) {
            if (index >= Window.Joysticks.Length) {
                return false;
            }
            switch (button) {
                case JoystickButton.ButtonA:
                    return Game.Win.JoystickA(index);
                case JoystickButton.ButtonB:
                    return Game.Win.JoystickB(index);
                case JoystickButton.ButtonX:
                    return Game.Win.JoystickX(index);
                case JoystickButton.ButtonY:
                    return Game.Win.JoystickY(index);
                case JoystickButton.DPadUp:
                    return Game.Win.JoystickUp(index);
                case JoystickButton.DPadDown:
                    return Game.Win.JoystickDown(index);
                case JoystickButton.DPadLeft:
                    return Game.Win.JoystickLeft(index);
                case JoystickButton.DPadRight:
                    return Game.Win.JoystickRight(index);
                case JoystickButton.ShoulderLeft:
                    return Game.Win.JoystickShoulderLeft(index);
                case JoystickButton.ShoulderRight:
                    return Game.Win.JoystickShoulderRight(index);
                default:
                    return false;

            }
        }

    }

    public abstract class ButtonMatch {
        public abstract bool GetButtonDown();
        public abstract bool GetButtonUp();
        public abstract bool GetButton();
    }

    public class KeyButtonMatch : ButtonMatch {

        private KeyCode key;

        public KeyButtonMatch (KeyCode code) {
            key = code;
        }

        public override bool GetButton() {
            return Input.GetKey(key);
        }

        public override bool GetButtonDown() {
            return Input.GetKeyDown(key);
        }

        public override bool GetButtonUp() {
            return Input.GetKeyUp(key);
        }
    }

    public class MouseButtonMatch : ButtonMatch{
        private MouseButton btn;

        public MouseButtonMatch (MouseButton code) {
            btn = code;
        }

        public override bool GetButton() {
            return Input.GetMouseButton(btn);
        }

        public override bool GetButtonDown() {
            return Input.GetMouseButtonDown(btn);
        }

        public override bool GetButtonUp() {
            return Input.GetMouseButtonUp(btn);
        }
    }

    public class JoystickButtonMatch : ButtonMatch {
        private int index;
        private JoystickButton btn;

        public JoystickButtonMatch (int index, JoystickButton btn) {
            this.index = index;
            this.btn = btn;
        }

        public override bool GetButton() {
            return Input.GetJoystickButton(btn, index);
        }

        public override bool GetButtonDown() {
            return Input.GetJoystickButtonDown(btn, index);
        }

        public override bool GetButtonUp() {
            return Input.GetJoystickButtonUp(btn, index);
        }
    }

    public class UserButton {

        public ButtonMatch[] bindedMatches;

        public UserButton (ButtonMatch[] bindedMatches) {
            this.bindedMatches = bindedMatches;
        }

        public bool GetButton () {
            bool value = false;

            for (int i = 0; i < bindedMatches.Length;i++) {
                value = value || bindedMatches[i].GetButton();
            }

            return value;
        }

        public bool GetButtonDown() {
            bool value = false;

            for (int i = 0; i < bindedMatches.Length; i++) {
                value = value || bindedMatches[i].GetButtonDown();
            }

            return value && !GetButton();
        }

        public bool GetButtonUp() {
            bool value = false;

            for (int i = 0; i < bindedMatches.Length; i++) {
                value = value || bindedMatches[i].GetButtonUp();
            }

            return value && !GetButton();
        }

    }

    public abstract class AxisMatch {
        public abstract float GetAxis();
    }

    public class JoystickAxisMatch: AxisMatch {

        private JoystickAxis axis;
        private int index;

        public JoystickAxisMatch(JoystickAxis axis, int index) {
            this.axis = axis;
            this.index = index;
        }

        public override float GetAxis () {
            return Input.GetJoystickAxis(axis, index);
        }

    }

    public class KeyAxisMatch : AxisMatch {

        private KeyCode negativeKeyCode;
        private KeyCode positiveKeyCode;

        public KeyAxisMatch (KeyCode negativeKeyCode, KeyCode positiveKeyCode) {
            this.negativeKeyCode = negativeKeyCode;
            this.positiveKeyCode = positiveKeyCode;
        }

        public override float GetAxis() {
            float value = 0;
            value -= Input.GetKey(negativeKeyCode) ? 1 : 0;
            value += Input.GetKey(positiveKeyCode) ? 1 : 0;
            return value;
        }
    }

    public class MouseAxisMatch : AxisMatch {
        private MouseButton negativeKeyCode;
        private MouseButton positiveKeyCode;

        public MouseAxisMatch (MouseButton negativeKeyCode, MouseButton positiveKeyCode) {
            this.negativeKeyCode = negativeKeyCode;
            this.positiveKeyCode = positiveKeyCode;
        }

        public override float GetAxis() {
            float value = 0;
            value -= Input.GetMouseButton(negativeKeyCode) ? 1 : 0;
            value += Input.GetMouseButton(positiveKeyCode) ? 1 : 0;
            return value;
        }
    }

    public class UserAxis {

        private AxisMatch[] axisMatches;

        public UserAxis (AxisMatch[] axisMatches) {
            this.axisMatches = axisMatches;
        }

        public float GetAxis () {
            float value = 0;
            for (int i = 0; i < axisMatches.Length; i++) {
                value += axisMatches[i].GetAxis();
            }
            value = value < -1 ? -1 : value > 1 ? 1 : value;
            return value;
        }

    }
}
