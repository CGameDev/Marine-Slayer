using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MarineSlayer.Tools
{
    public static class Xbox360RuntimeProbe
    {
        private const string XbdmPath = @"C:\Program Files (x86)\Microsoft Xbox 360 SDK\bin\x64\xbdm.dll";

        private const uint DmBreak = 1;
        private const uint DmDebugString = 2;
        private const uint DmExecution = 3;
        private const uint DmModuleLoad = 5;
        private const uint DmException = 9;
        private const uint DmAssert = 12;
        private const uint DmRip = 14;
        private const uint DmBugCheck = 20;
        private const uint DmAssertionFailure = 21;
        private const uint DmNotificationMask = 0x00ffffff;
        private const uint DmDebugSession = 0x00000002;
        private const uint DmAsyncSession = 0x00000004;
        private const uint DmPersistentSession = 0x00000001;
        private const int XbdmAlreadyExists = unchecked((int)0x82DA000A);
        private const int XbdmNotStopped = unchecked((int)0x82DA0008);
        private const int XbdmEndOfList = unchecked((int)0x82DA0104);

        private static readonly ConcurrentQueue<string> Events = new ConcurrentQueue<string>();
        private static readonly List<NotifyCallback> Callbacks = new List<NotifyCallback>();
        private static readonly Regex IPv4Pattern = new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b", RegexOptions.Compiled);
        private static readonly Regex MacPattern = new Regex(@"\b(?:[0-9A-Fa-f]{2}:){5}[0-9A-Fa-f]{2}\b|\b[0-9A-Fa-f]{12}\b", RegexOptions.Compiled);
        private static readonly Regex CredentialLinePattern = new Regex(@"(?im)(ApiKey|Username)\s*:\s*.*$", RegexOptions.Compiled);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate uint NotifyCallback(uint notification, UIntPtr parameter);

        [StructLayout(LayoutKind.Sequential)]
        private struct DebugStringNotification
        {
            public uint ThreadId;
            public uint Length;
            public IntPtr String;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ExceptionNotification
        {
            public uint ThreadId;
            public uint Code;
            public IntPtr Address;
            public uint Flags;
            public uint Information0;
            public uint Information1;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct ModuleLoadNotification
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string Name;
            public IntPtr BaseAddress;
            public uint Size;
            public uint TimeStamp;
            public uint CheckSum;
            public uint Flags;
            public IntPtr PDataAddress;
            public uint PDataSize;
            public uint ThreadId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct GamepadState
        {
            public ushort Buttons;
            public byte LeftTrigger;
            public byte RightTrigger;
            public short LeftThumbX;
            public short LeftThumbY;
            public short RightThumbX;
            public short RightThumbY;
        }

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int DmSetXboxNameNoRegister(string xboxName);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int DmGetDriveList(StringBuilder drives, ref uint count);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int DmMkdir(string directoryName);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "DmSendFileA")]
        private static extern int DmSendFile(string localName, string remoteName);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmWalkLoadedModules(ref IntPtr walk, out ModuleLoadNotification module);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmCloseLoadedModules(IntPtr walk);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmConnectDebugger([MarshalAs(UnmanagedType.Bool)] bool connect);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmOpenNotificationSession(uint flags, out IntPtr session);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmCloseNotificationSession(IntPtr session);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmNotify(IntPtr session, uint notification, NotifyCallback callback);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmGo();

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmGetPid(out uint processId);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int DmRebootEx(uint flags, string imagePath, string mediaPath, string debugCommandLine);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmAutomationBindController(uint userIndex, uint queueLength);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmAutomationUnbindController(uint userIndex);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmAutomationConnectController(uint userIndex);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmAutomationDisconnectController(uint userIndex);

        [DllImport(XbdmPath, CallingConvention = CallingConvention.StdCall)]
        private static extern int DmAutomationSetGamepadState(uint userIndex, ref GamepadState gamepad);

        public static string[] Run(string target, int captureSeconds)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("A console target is required.", "target");
            }

            if (captureSeconds < 1 || captureSeconds > 120)
            {
                throw new ArgumentOutOfRangeException("captureSeconds", "Capture duration must be between 1 and 120 seconds.");
            }

            ClearEvents();
            Callbacks.Clear();

            Check(DmSetXboxNameNoRegister(target), "select console");
            Check(DmConnectDebugger(true), "connect debugger");

            IntPtr session = IntPtr.Zero;
            try
            {
                Check(DmOpenNotificationSession(DmDebugSession | DmAsyncSession, out session), "open notification session");
                Register(session, DmBreak);
                Register(session, DmDebugString);
                Register(session, DmExecution);
                Register(session, DmModuleLoad);
                Register(session, DmException);
                Register(session, DmAssert);
                Register(session, DmRip);
                Register(session, DmBugCheck);
                Register(session, DmAssertionFailure);

                int goResult = DmGo();
                if (goResult < 0 && goResult != XbdmNotStopped)
                {
                    Check(goResult, "resume title");
                }
                Thread.Sleep(TimeSpan.FromSeconds(captureSeconds));

                uint processId;
                int pidResult = DmGetPid(out processId);
                Events.Enqueue(
                    pidResult < 0
                        ? "probe: no running title (HRESULT " + FormatResult(pidResult) + ")"
                        : "probe: running process id " + processId.ToString(CultureInfo.InvariantCulture));
            }
            finally
            {
                if (session != IntPtr.Zero)
                {
                    DmCloseNotificationSession(session);
                }

                DmConnectDebugger(false);
                Callbacks.Clear();
            }

            return Events.ToArray();
        }

        public static string[] GetDriveList(string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("A console target is required.", "target");
            }

            Check(DmSetXboxNameNoRegister(target), "select console");

            const int capacity = 1024;
            StringBuilder drives = new StringBuilder(capacity);
            uint count = capacity;
            Check(DmGetDriveList(drives, ref count), "enumerate console drives");

            return drives.ToString()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void EnsureDirectory(string target, string remotePath)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("A console target is required.", "target");
            }

            if (string.IsNullOrWhiteSpace(remotePath))
            {
                throw new ArgumentException("A remote directory path is required.", "remotePath");
            }

            Check(DmSetXboxNameNoRegister(target), "select console");
            int result = DmMkdir(remotePath);
            if (result < 0 && result != XbdmAlreadyExists)
            {
                Check(result, "create remote directory " + remotePath);
            }
        }

        public static void SendFile(string target, string localPath, string remotePath)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("A console target is required.", "target");
            }

            if (string.IsNullOrWhiteSpace(localPath))
            {
                throw new ArgumentException("A local file path is required.", "localPath");
            }

            if (string.IsNullOrWhiteSpace(remotePath))
            {
                throw new ArgumentException("A remote file path is required.", "remotePath");
            }

            Check(DmSetXboxNameNoRegister(target), "select console");
            Check(DmSendFile(localPath, remotePath), "send remote file " + remotePath);
        }

        public static string[] GetLoadedModules(string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("A console target is required.", "target");
            }

            Check(DmSetXboxNameNoRegister(target), "select console");
            List<string> modules = new List<string>();
            IntPtr walk = IntPtr.Zero;
            try
            {
                ModuleLoadNotification module;
                int result;
                while ((result = DmWalkLoadedModules(ref walk, out module)) >= 0)
                {
                    if (!string.IsNullOrEmpty(module.Name)) modules.Add(module.Name);
                }

                if (result != XbdmEndOfList)
                {
                    Check(result, "enumerate loaded modules");
                }
            }
            finally
            {
                if (walk != IntPtr.Zero) DmCloseLoadedModules(walk);
            }

            return modules.ToArray();
        }

        public static string[] LaunchAndCapture(string target, string imagePath, string mediaPath, int captureSeconds)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("A console target is required.", "target");
            }

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                throw new ArgumentException("An Xbox image path is required.", "imagePath");
            }

            if (captureSeconds < 1 || captureSeconds > 120)
            {
                throw new ArgumentOutOfRangeException("captureSeconds", "Capture duration must be between 1 and 120 seconds.");
            }

            ClearEvents();
            Callbacks.Clear();
            Check(DmSetXboxNameNoRegister(target), "select console");

            IntPtr session = IntPtr.Zero;
            try
            {
                Check(DmOpenNotificationSession(DmPersistentSession | DmAsyncSession, out session), "open persistent notification session");
                Register(session, DmBreak);
                Register(session, DmDebugString);
                Register(session, DmExecution);
                Register(session, DmModuleLoad);
                Register(session, DmException);
                Register(session, DmAssert);
                Register(session, DmRip);
                Register(session, DmBugCheck);
                Register(session, DmAssertionFailure);

                Check(DmRebootEx(0, imagePath, mediaPath, string.Empty), "launch title");
                Thread.Sleep(TimeSpan.FromSeconds(captureSeconds));

                uint processId;
                int pidResult = DmGetPid(out processId);
                Events.Enqueue(
                    pidResult < 0
                        ? "probe: no running title (HRESULT " + FormatResult(pidResult) + ")"
                        : "probe: running process id " + processId.ToString(CultureInfo.InvariantCulture));
            }
            finally
            {
                if (session != IntPtr.Zero)
                {
                    DmCloseNotificationSession(session);
                }

                Callbacks.Clear();
            }

            return Events.ToArray();
        }

        public static void PressButton(string target, ushort buttons, int holdMilliseconds)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("A console target is required.", "target");
            }

            if (holdMilliseconds < 30 || holdMilliseconds > 2000)
            {
                throw new ArgumentOutOfRangeException("holdMilliseconds", "Hold duration must be between 30 and 2000 milliseconds.");
            }

            Check(DmSetXboxNameNoRegister(target), "select console");
            Check(DmAutomationBindController(0, 0), "bind automation controller");
            try
            {
                Check(DmAutomationConnectController(0), "connect automation controller");
                try
                {
                    GamepadState pressed = new GamepadState { Buttons = buttons };
                    Check(DmAutomationSetGamepadState(0, ref pressed), "press automation controller button");
                    Thread.Sleep(holdMilliseconds);
                    GamepadState released = new GamepadState();
                    Check(DmAutomationSetGamepadState(0, ref released), "release automation controller button");
                    Thread.Sleep(150);
                }
                finally
                {
                    DmAutomationDisconnectController(0);
                }
            }
            finally
            {
                DmAutomationUnbindController(0);
            }
        }

        private static void Register(IntPtr session, uint notification)
        {
            NotifyCallback callback = HandleNotification;
            Callbacks.Add(callback);
            Check(DmNotify(session, notification, callback), "register notification " + notification.ToString(CultureInfo.InvariantCulture));
        }

        private static uint HandleNotification(uint notification, UIntPtr parameter)
        {
            try
            {
                uint type = notification & DmNotificationMask;
                switch (type)
                {
                    case DmDebugString:
                        DebugStringNotification debugString = Marshal.PtrToStructure<DebugStringNotification>((IntPtr)parameter);
                        string text = debugString.String == IntPtr.Zero
                            ? string.Empty
                            : Marshal.PtrToStringAnsi(debugString.String, checked((int)debugString.Length));
                        Events.Enqueue("debug: " + Sanitize((text ?? string.Empty).TrimEnd('\0', '\r', '\n')));
                        break;

                    case DmExecution:
                        Events.Enqueue("execution: " + ExecutionState(parameter.ToUInt64()));
                        break;

                    case DmModuleLoad:
                        ModuleLoadNotification module = Marshal.PtrToStructure<ModuleLoadNotification>((IntPtr)parameter);
                        Events.Enqueue("module: " + module.Name);
                        break;

                    case DmException:
                        ExceptionNotification exception = Marshal.PtrToStructure<ExceptionNotification>((IntPtr)parameter);
                        Events.Enqueue(
                            "exception: code=0x" + exception.Code.ToString("X8", CultureInfo.InvariantCulture) +
                            " address=0x" + exception.Address.ToInt64().ToString("X", CultureInfo.InvariantCulture) +
                            " thread=" + exception.ThreadId.ToString(CultureInfo.InvariantCulture) +
                            " flags=0x" + exception.Flags.ToString("X8", CultureInfo.InvariantCulture));
                        break;

                    case DmBreak:
                        Events.Enqueue("break: debugger break notification");
                        break;

                    case DmAssert:
                    case DmAssertionFailure:
                        Events.Enqueue("assertion: notification " + type.ToString(CultureInfo.InvariantCulture));
                        break;

                    case DmRip:
                        Events.Enqueue("rip: fatal title notification");
                        break;

                    case DmBugCheck:
                        Events.Enqueue("bugcheck: console notification");
                        break;
                }
            }
            catch (Exception error)
            {
                Events.Enqueue("probe callback error: " + error.Message);
            }

            return 0;
        }

        private static string ExecutionState(ulong state)
        {
            switch (state)
            {
                case 0: return "stopped";
                case 1: return "started";
                case 2: return "rebooting";
                case 3: return "pending";
                case 4: return "rebooting title";
                case 5: return "pending title";
                default: return "unknown (" + state.ToString(CultureInfo.InvariantCulture) + ")";
            }
        }

        private static string Sanitize(string value)
        {
            string sanitized = CredentialLinePattern.Replace(value, "$1: [redacted]");
            sanitized = IPv4Pattern.Replace(sanitized, "[redacted-ip]");
            return MacPattern.Replace(sanitized, "[redacted-mac]");
        }

        private static void ClearEvents()
        {
            string ignored;
            while (Events.TryDequeue(out ignored))
            {
            }
        }

        private static void Check(int result, string operation)
        {
            if (result < 0)
            {
                throw new InvalidOperationException(operation + " failed with HRESULT " + FormatResult(result));
            }
        }

        private static string FormatResult(int result)
        {
            return "0x" + unchecked((uint)result).ToString("X8", CultureInfo.InvariantCulture);
        }
    }
}
