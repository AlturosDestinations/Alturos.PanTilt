using Alturos.PanTilt.Communication;
using Alturos.PanTilt.Eneo;
using Alturos.PanTilt.Eneo.Response;
using Alturos.PanTilt.Tools;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alturos.PanTilt
{
    public class EneoPanTiltControl : IPanTiltControl, IFirmwareReader
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(EneoPanTiltControl));

        private PanTiltPosition _position;
        private readonly PanTiltLimit _limits;
        private readonly FeedbackHandler _handler;
        private readonly ManualResetEvent _resetEvent;
        private readonly PositionConverter _positionConverter;
        private bool _stopHeartbeat;
        private readonly int _dipReceiverPort = 1;
        private int _receiveCount;
        private int _sendCount;
        private readonly bool _debug;
        private readonly ICommunication _communication;
        private readonly Dictionary<double, byte> _speed = new Dictionary<double, byte>();

        public event Action<PanTiltPosition> PositionChanged;
        public event Action LimitOverrun;
        public event Action LimitChanged;

        public EneoPanTiltControl(ICommunication communication, bool debug = false)
        {
            this._communication = communication;
            this._communication.ReceiveData += PackageReceived;

            this._debug = debug;

            this._positionConverter = new PositionConverter();

            this._position = null;
            this._limits = new PanTiltLimit();
            this._handler = new FeedbackHandler();

            this._resetEvent = new ManualResetEvent(false);
            this._stopHeartbeat = true;

            this.InitializeSpeedTranslationTable();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._communication.ReceiveData -= PackageReceived;
        }

        private void InitializeSpeedTranslationTable()
        {
            this._speed.Add(0.138357516814, 1);
            this._speed.Add(0.211443667312, 2);
            this._speed.Add(0.284872336578, 3);
            this._speed.Add(0.358669399696, 4);
            this._speed.Add(0.43286073175, 5);
            this._speed.Add(0.507472207824, 6);
            this._speed.Add(0.582529703002, 7);
            this._speed.Add(0.658059092368, 8);
            this._speed.Add(0.734086251006, 9);
            this._speed.Add(0.810637054, 10);
            this._speed.Add(0.887737376434, 11);
            this._speed.Add(0.965413093392, 12);
            this._speed.Add(1.043690079958, 13);
            this._speed.Add(1.122594211216, 14);
            this._speed.Add(1.20215136225, 15);
            this._speed.Add(1.282387408144, 16);
            this._speed.Add(1.363328223982, 17);
            this._speed.Add(1.444999684848, 18);
            this._speed.Add(1.527427665826, 19);
            this._speed.Add(1.610638042, 20);
            this._speed.Add(1.694656688454, 21);
            this._speed.Add(1.779509480272, 22);
            this._speed.Add(1.865222292538, 23);
            this._speed.Add(1.951821000336, 24);
            this._speed.Add(2.03933147875, 25);
            this._speed.Add(2.127779602864, 26);
            this._speed.Add(2.217191247762, 27);
            this._speed.Add(2.307592288528, 28);
            this._speed.Add(2.399008600246, 29);
            this._speed.Add(2.491466058, 30);
            this._speed.Add(2.584990536874, 31);
            this._speed.Add(2.679607911952, 32);
            this._speed.Add(2.775344058318, 33);
            this._speed.Add(2.872224851056, 34);
            this._speed.Add(2.97027616525, 35);
            this._speed.Add(3.069523875984, 36);
            this._speed.Add(3.169993858342, 37);
            this._speed.Add(3.271711987408, 38);
            this._speed.Add(3.374704138266, 39);
            this._speed.Add(3.478996186, 40);
            this._speed.Add(3.584614005694, 41);
            this._speed.Add(3.691583472432, 42);
            this._speed.Add(3.799930461298, 43);
            this._speed.Add(3.909680847376, 44);
            this._speed.Add(4.02086050575, 45);
            this._speed.Add(4.133495311504, 46);
            this._speed.Add(4.247611139722, 47);
            this._speed.Add(4.363233865488, 48);
            this._speed.Add(4.480389363886, 49);
            this._speed.Add(4.59910351, 50);
            this._speed.Add(4.719402178914, 51);
            this._speed.Add(4.841311245712, 52);
            this._speed.Add(4.964856585478, 53);
            this._speed.Add(5.090064073296, 54);
            this._speed.Add(5.21695958425, 55);
            this._speed.Add(5.345568993424, 56);
            this._speed.Add(5.475918175902, 57);
            this._speed.Add(5.608033006768, 58);
            this._speed.Add(5.741939361106, 59);
            this._speed.Add(5.877663114, 60);
            this._speed.Add(6.015230140534, 61);
            this._speed.Add(6.154666315792, 62);
            this._speed.Add(6.295997514858, 63);
            this._speed.Add(6.439249612816, 64);
            this._speed.Add(6.58444848475, 65);
            this._speed.Add(6.731620005744, 66);
            this._speed.Add(6.880790050882, 67);
            this._speed.Add(7.031984495248, 68);
            this._speed.Add(7.185229213926, 69);
            this._speed.Add(7.340550082, 70);
            this._speed.Add(7.497972974554, 71);
            this._speed.Add(7.657523766672, 72);
            this._speed.Add(7.819228333438, 73);
            this._speed.Add(7.983112549936, 74);
            this._speed.Add(8.14920229125, 75);
            this._speed.Add(8.317523432464, 76);
            this._speed.Add(8.488101848662, 77);
            this._speed.Add(8.660963414928, 78);
            this._speed.Add(8.836134006346, 79);
            this._speed.Add(9.013639498, 80);
            this._speed.Add(9.193505764974, 81);
            this._speed.Add(9.375758682352, 82);
            this._speed.Add(9.560424125218, 83);
            this._speed.Add(9.747527968656, 84);
            this._speed.Add(9.93709608775, 85);
            this._speed.Add(10.129154357584, 86);
            this._speed.Add(10.323728653242, 87);
            this._speed.Add(10.520844849808, 88);
            this._speed.Add(10.720528822366, 89);
            this._speed.Add(10.922806446, 90);
            this._speed.Add(11.127703595794, 91);
            this._speed.Add(11.335246146832, 92);
            this._speed.Add(11.545459974198, 93);
            this._speed.Add(11.758370952976, 94);
            this._speed.Add(11.97400495825, 95);
            this._speed.Add(12.192387865104, 96);
            this._speed.Add(12.413545548622, 97);
            this._speed.Add(12.637503883888, 98);
            this._speed.Add(12.864288745986, 99);
            this._speed.Add(13.09392601, 100);
            this._speed.Add(13.326441551014, 101);
            this._speed.Add(13.561861244112, 102);
            this._speed.Add(13.800210964378, 103);
            this._speed.Add(14.041516586896, 104);
            this._speed.Add(14.28580398675, 105);
            this._speed.Add(14.533099039024, 106);
            this._speed.Add(14.783427618802, 107);
            this._speed.Add(15.036815601168, 108);
            this._speed.Add(15.293288861206, 109);
            this._speed.Add(15.552873274, 110);
            this._speed.Add(15.815594714634, 111);
            this._speed.Add(16.081479058192, 112);
            this._speed.Add(16.350552179758, 113);
            this._speed.Add(16.622839954416, 114);
            this._speed.Add(16.89836825725, 115);
            this._speed.Add(17.177162963344, 116);
            this._speed.Add(17.459249947782, 117);
            this._speed.Add(17.744655085648, 118);
            this._speed.Add(18.033404252026, 119);
            this._speed.Add(18.325523322, 120);
            this._speed.Add(18.621038170654, 121);
            this._speed.Add(18.919974673072, 122);
            this._speed.Add(19.222358704338, 123);
            this._speed.Add(19.528216139536, 124);
            this._speed.Add(19.83757285375, 125);
            this._speed.Add(20.150454722064, 126);
            this._speed.Add(20.466887619562, 127);
            this._speed.Add(20.786897421328, 128);
            this._speed.Add(21.110510002446, 129);
            this._speed.Add(21.437751238, 130);
            this._speed.Add(21.768647003074, 131);
            this._speed.Add(22.103223172752, 132);
            this._speed.Add(22.441505622118, 133);
            this._speed.Add(22.783520226256, 134);
            this._speed.Add(23.12929286025, 135);
            this._speed.Add(23.478849399184, 136);
            this._speed.Add(23.832215718142, 137);
            this._speed.Add(24.189417692208, 138);
            this._speed.Add(24.550481196466, 139);
            this._speed.Add(24.915432106, 140);
            this._speed.Add(25.284296295894, 141);
            this._speed.Add(25.657099641232, 142);
            this._speed.Add(26.033868017098, 143);
            this._speed.Add(26.414627298576, 144);
            this._speed.Add(26.79940336075, 145);
            this._speed.Add(27.188222078704, 146);
            this._speed.Add(27.581109327522, 147);
            this._speed.Add(27.978090982288, 148);
            this._speed.Add(28.379192918086, 149);
            this._speed.Add(28.78444101, 150);
            this._speed.Add(29.193861133114, 151);
            this._speed.Add(29.607479162512, 152);
            this._speed.Add(30.025320973278, 153);
            this._speed.Add(30.447412440496, 154);
            this._speed.Add(30.87377943925, 155);
            this._speed.Add(31.304447844624, 156);
            this._speed.Add(31.739443531702, 157);
            this._speed.Add(32.178792375568, 158);
            this._speed.Add(32.622520251306, 159);
            this._speed.Add(33.070653034, 160);
            this._speed.Add(33.523216598734, 161);
            this._speed.Add(33.980236820592, 162);
            this._speed.Add(34.441739574658, 163);
            this._speed.Add(34.907750736016, 164);
            this._speed.Add(35.37829617975, 165);
            this._speed.Add(35.853401780944, 166);
            this._speed.Add(36.333093414682, 167);
            this._speed.Add(36.817396956048, 168);
            this._speed.Add(37.306338280126, 169);
            this._speed.Add(37.799943262, 170);
            this._speed.Add(38.298237776754, 171);
            this._speed.Add(38.801247699472, 172);
            this._speed.Add(39.308998905238, 173);
            this._speed.Add(39.821517269136, 174);
            this._speed.Add(40.33882866625, 175);
            this._speed.Add(40.860958971664, 176);
            this._speed.Add(41.387934060462, 177);
            this._speed.Add(41.919779807728, 178);
            this._speed.Add(42.456522088546, 179);
            this._speed.Add(42.998186778, 180);
            this._speed.Add(43.544799751174, 181);
            this._speed.Add(44.096386883152, 182);
            this._speed.Add(44.652974049018, 183);
            this._speed.Add(45.214587123856, 184);
            this._speed.Add(45.78125198275, 185);
            this._speed.Add(46.352994500784, 186);
            this._speed.Add(46.929840553042, 187);
            this._speed.Add(47.511816014608, 188);
            this._speed.Add(48.098946760566, 189);
            this._speed.Add(48.691258666, 190);
            this._speed.Add(49.288777605994, 191);
            this._speed.Add(49.891529455632, 192);
            this._speed.Add(50.499540089998, 193);
            this._speed.Add(51.112835384176, 194);
            this._speed.Add(51.73144121325, 195);
            this._speed.Add(52.355383452304, 196);
            this._speed.Add(52.984687976422, 197);
            this._speed.Add(53.619380660688, 198);
            this._speed.Add(54.259487380186, 199);
            this._speed.Add(54.90503401, 200);
            this._speed.Add(55.556046425214, 201);
            this._speed.Add(56.212550500912, 202);
            this._speed.Add(56.874572112178, 203);
            this._speed.Add(57.542137134096, 204);
            this._speed.Add(58.21527144175, 205);
            this._speed.Add(58.894000910224, 206);
            this._speed.Add(59.578351414602, 207);
            this._speed.Add(60.268348829968, 208);
            this._speed.Add(60.964019031406, 209);
            this._speed.Add(61.665387894, 210);
            this._speed.Add(62.372481292834, 211);
            this._speed.Add(63.085325102992, 212);
            this._speed.Add(63.803945199558, 213);
            this._speed.Add(64.528367457616, 214);
            this._speed.Add(65.25861775225, 215);
            this._speed.Add(65.994721958544, 216);
            this._speed.Add(66.736705951582, 217);
            this._speed.Add(67.484595606448, 218);
            this._speed.Add(68.238416798226, 219);
            this._speed.Add(68.998195402, 220);
            this._speed.Add(69.763957292854, 221);
            this._speed.Add(70.535728345872, 222);
            this._speed.Add(71.313534436138, 223);
            this._speed.Add(72.097401438736, 224);
            this._speed.Add(72.88735522875, 225);
            this._speed.Add(73.683421681264, 226);
            this._speed.Add(74.485626671362, 227);
            this._speed.Add(75.293996074128, 228);
            this._speed.Add(76.108555764646, 229);
            this._speed.Add(76.929331618, 230);
            this._speed.Add(77.756349509274, 231);
            this._speed.Add(78.589635313552, 232);
            this._speed.Add(79.429214905918, 233);
            this._speed.Add(80.275114161456, 234);
            this._speed.Add(81.12735895525, 235);
            this._speed.Add(81.985975162384, 236);
            this._speed.Add(82.850988657942, 237);
            this._speed.Add(83.722425317008, 238);
            this._speed.Add(84.600311014666, 239);
            this._speed.Add(85.484671626, 240);
            this._speed.Add(86.375533026094, 241);
            this._speed.Add(87.272921090032, 242);
            this._speed.Add(88.176861692898, 243);
            this._speed.Add(89.087380709776, 244);
            this._speed.Add(90.00450401575, 245);
            this._speed.Add(90.928257485904, 246);
            this._speed.Add(91.858666995322, 247);
            this._speed.Add(92.795758419088, 248);
            this._speed.Add(93.739557632286, 249);
            this._speed.Add(94.69009051, 250);
            this._speed.Add(95.647382927314, 251);
            this._speed.Add(96.611460759312, 252);
            this._speed.Add(97.582349881078, 253);
            this._speed.Add(98.560076167696, 254);
        }

        public bool Start()
        {
            if (!this._stopHeartbeat)
            {
                return false;
            }
            this._stopHeartbeat = false;

            this.SetSmoothing();
            this.QueryLimits();
            
            Task.Run(() => this.Heartbeat());

            return true;
        }

        public bool Stop()
        {
            this._stopHeartbeat = true;
            this._resetEvent.Set();
            Thread.Sleep(100);

            return true;
        }

        private void PackageReceived(byte[] data)
        {
            if (this._debug)
            {
                var hex = BitConverter.ToString(data);
                Log.Debug($"{nameof(PackageReceived)} - {hex}");
            }

            Interlocked.Increment(ref this._receiveCount);
            var responses = this._handler.HandleResponse(data);

            #region Pan Tilt Position

            //Set last position is possible only tilt or pan information is receive
            PanTiltPosition position;
            if (this._position == null)
            {
                position = new PanTiltPosition(0, 0);
            }
            else
            {
                position = new PanTiltPosition(this._position.Pan, this._position.Tilt);
            }

            var groupResponses = responses.GroupBy(o => o.ResponseType).ToDictionary(o => o.Key, o => o.ToList());

            if (groupResponses.ContainsKey(ResponseType.PanInfo))
            {
                position.Pan = groupResponses[ResponseType.PanInfo].Select(o => ((PanInfoResponse)o).Pan).FirstOrDefault();
            }

            if (groupResponses.ContainsKey(ResponseType.TiltInfo))
            {
                position.Tilt = groupResponses[ResponseType.TiltInfo].Select(o => ((TiltInfoResponse)o).Tilt).FirstOrDefault();
            }

            #endregion

            #region Pan Tilt Limit

            if (groupResponses.ContainsKey(ResponseType.PanLimit))
            {
                var limits = groupResponses[ResponseType.PanLimit].Select(o => ((PanLimitResponse)o)).ToList();
                var minLimit = limits.FirstOrDefault(o => o.Type == LimitType.Min)?.Limit;
                var maxLimit = limits.FirstOrDefault(o => o.Type == LimitType.Max)?.Limit;

                if (minLimit.HasValue)
                {
                    this._limits.PanMin = minLimit.Value;
                }

                if (maxLimit.HasValue)
                {
                    this._limits.PanMax = maxLimit.Value;
                }
            }

            if (groupResponses.ContainsKey(ResponseType.TiltLimit))
            {
                var limits = groupResponses[ResponseType.TiltLimit].Select(o => ((TiltLimitResponse)o)).ToList();
                var minLimit = limits.FirstOrDefault(o => o.Type == LimitType.Min)?.Limit;
                var maxLimit = limits.FirstOrDefault(o => o.Type == LimitType.Max)?.Limit;

                if (minLimit.HasValue)
                {
                    this._limits.TiltMin = minLimit.Value;
                }

                if (maxLimit.HasValue)
                {
                    this._limits.TiltMax = maxLimit.Value;
                }
            }

            if (groupResponses.ContainsKey(ResponseType.LimitOverrun))
            {
                var limitOverrun = groupResponses[ResponseType.LimitOverrun].Select(o => ((LimitOverrunResponse)o)).FirstOrDefault();
                Log.Warn($"{nameof(PackageReceived)} - LimitOverrun Type:{limitOverrun.LimitOverrunType}");
                this.LimitOverrun?.Invoke();
            }

            #endregion

            if (groupResponses.ContainsKey(ResponseType.Temperature))
            {
                var temperature = groupResponses[ResponseType.Temperature].Select(o => ((TemperatureResponse)o).Temperature).FirstOrDefault();
                Log.Info($"{nameof(PackageReceived)} - Temperature:{temperature}");
            }

            if (groupResponses.ContainsKey(ResponseType.Potentiometer))
            {
                var potentiometers = groupResponses[ResponseType.Potentiometer].Select(o => ((PotentiometerResponse)o)).ToList();
                foreach (var potentiometer in potentiometers)
                {
                    Log.Info($"{nameof(PackageReceived)} - Potentiometer (Source:{potentiometer.Source} Voltage:{potentiometer.Voltage})");
                }
            }

            if (this._position == null || !this._position.Equals(position))
            {
                try
                {
                    this.PositionChanged?.Invoke(this._position);
                }
                catch (Exception exception)
                {
                    Log.Error($"{nameof(PackageReceived)}", exception);
                }
            }
            this._position = position;
        }

        private void Heartbeat()
        {
            var timeout = 10 * 1000; //10 seconds

            while (!this._stopHeartbeat)
            {
                this.ActivateFeedback();
                this._resetEvent.WaitOne(timeout);
            }

            this.DeactivateFeedback();
        }

        public bool ReinitializePtHead()
        {
            //Firmware request command need to commands for start the reinitialize

            var data = new byte[1];
            data[0] = 0x69;

            var command = this.CreateCommand(data);

            this.Send(command, "GetFirmware-Reinitialize1");
            Thread.Sleep(50);
            return this.Send(command, "GetFirmware-Reinitialize2");
        }

        #region Absolute

        public bool SetSmoothing(byte acceleration = 100, byte gain = 50)
        {
            gain = Math.Max((byte)0x01, gain);
            var data = new byte[3];
            data[0] = 0xD2;
            data[1] = acceleration; // or speed
            data[2] = gain; // or constant

            var command = this.CreateCommand(data);
            return this.Send(command, $"SetSmoothing Acceleration:{acceleration} Gain:{gain}");
        }

        public bool MovePanAbsolute(double panPosition)
        {
            var data = new byte[3];
            data[0] = 0xDC;

            byte panData1;
            byte panData2;

            this._positionConverter.ConvertPositionData(panPosition, out panData1, out panData2);

            data[1] = panData1;
            data[2] = panData2;

            var command = this.CreateCommand(data);
            return this.Send(command, $"PanAbsolute {panPosition}");
        }

        public bool MoveTiltAbsolute(double tiltPosition)
        {
            var data = new byte[3];
            data[0] = 0xDD;

            byte tiltData1;
            byte tiltData2;

            this._positionConverter.ConvertPositionData(tiltPosition, out tiltData1, out tiltData2);

            data[1] = tiltData1;
            data[2] = tiltData2;

            var command = this.CreateCommand(data);
            return this.Send(command, $"TiltAbsolute {tiltPosition}");
        }

        #endregion

        #region Relative

        /// <summary>
        /// MoveRelative
        /// </summary>
        /// <param name="panSpeed">degree per second</param>
        /// <param name="tiltSpeed">degree per second</param>
        public bool MoveRelative(double panSpeed, double tiltSpeed)
        {
            if (panSpeed == 0 && tiltSpeed == 0)
            {
                return this.StopMoving();
            }

            #region Get Eneo Speed value for degree per second value

            byte eneoPanSpeed = 0;
            byte eneoTiltSpeed = 0;

            if (panSpeed != 0)
            {
                eneoPanSpeed = this._speed.OrderBy(x => Math.Abs(x.Key - Math.Abs(panSpeed))).Select(o => o.Value).First();
            }

            if (tiltSpeed != 0)
            {
                eneoTiltSpeed = this._speed.OrderBy(x => Math.Abs(x.Key - (Math.Abs(tiltSpeed) * 2))).Select(o => o.Value).First();
            }

            #endregion

            byte[] data;

            if (Math.Abs(panSpeed) < 0.01)
            {
                this.StopPanMovement();
                data = new byte[2];

                if (tiltSpeed > 0.01)
                {
                    // move up
                    data[0] = 0x55;
                }
                else
                {
                    // move down
                    data[0] = 0x44;
                }

                data[1] = eneoTiltSpeed;
            }
            else if (Math.Abs(tiltSpeed) < 0.01)
            {
                this.StopTiltMovement();
                data = new byte[2];

                if (panSpeed > 0.01)
                {
                    // move right
                    data[0] = 0x52;
                }
                else
                {
                    // move left
                    data[0] = 0x4C;
                }

                data[1] = eneoPanSpeed;
            }
            else
            {
                data = new byte[3];

                // get direction byte
                if (panSpeed > 0 && tiltSpeed > 0)
                {
                    data[0] = 0x53;
                }
                else if (panSpeed > 0 && tiltSpeed < 0)
                {
                    data[0] = 0X4D;
                }
                else if (panSpeed < 0 && tiltSpeed < 0)
                {
                    data[0] = 0x45;
                }
                else
                {
                    data[0] = 0x56;
                }

                data[1] = eneoPanSpeed;
                data[2] = eneoTiltSpeed;
            }

            var command = this.CreateCommand(data);
            return this.Send(command, $"MoveRelative Tilt:{tiltSpeed}  Pan:{panSpeed}");
        }

        public bool StopMoving()
        {
            var data = new byte[1];
            data[0] = 0x58;

            var command = this.CreateCommand(data);
            this.Send(command, "StopMoving");
            return true;
        }

        public bool StopPanMovement()
        {
            var data = new byte[1];
            data[0] = 0x49;

            var command = this.CreateCommand(data);
            return this.Send(command, "StopPanMovement");
        }

        public bool StopTiltMovement()
        {
            var data = new byte[1];
            data[0] = 0x4A;

            var command = this.CreateCommand(data);
            return this.Send(command, "StopTiltMovement");
        }

        #endregion

        #region Other

        public void GetPanPotentiometer()
        {
            var data = new byte[1];
            data[0] = 0xA0;

            var command = this.CreateCommand(data);
            this.Send(command, "GetPanPotentiometer");
        }

        public void GetTiltPotentiometer()
        {
            var data = new byte[1];
            data[0] = 0xA2;

            var command = this.CreateCommand(data);
            this.Send(command, "GetTiltPotentiometer");
        }

        public void GetTemperature()
        {
            var data = new byte[1];
            data[0] = 0xA5;

            var command = this.CreateCommand(data);
            this.Send(command, "GetTemperature");
        }

        public void MoveRandom()
        {
            var data = new byte[1];
            //data[0] = 0x72;
            data[0] = 0x69;

            var command = this.CreateCommand(data);
            this.Send(command, "MoveRandom");
        }

        #endregion

        #region Limits

        public void SetLimitRigth()
        {
            var data = new byte[1];
            data[0] = 0x6C;

            var command = this.CreateCommand(data);
            this.Send(command, "SetLimitRigth");
            this.LimitChanged?.Invoke();
        }

        public void SetLimitLeft()
        {
            var data = new byte[1];
            data[0] = 0x77;

            var command = this.CreateCommand(data);
            this.Send(command, "SetLimitLeft");
            this.LimitChanged?.Invoke();
        }

        public void SetLimitUp()
        {
            var data = new byte[1];
            data[0] = 0x6B;

            var command = this.CreateCommand(data);
            this.Send(command, "SetLimitUp");
            this.LimitChanged?.Invoke();
        }

        public void SetLimitDown()
        {
            var data = new byte[1];
            data[0] = 0x70;

            var command = this.CreateCommand(data);
            this.Send(command, "SetLimitDown");
            this.LimitChanged?.Invoke();
        }

        public void EnableLimit()
        {
            var data = new byte[1];
            data[0] = 0x6A;

            var command = this.CreateCommand(data);
            this.Send(command, "EnableLimit");
        }

        public void DisableLimit()
        {
            var data = new byte[1];
            data[0] = 0x6F;

            var command = this.CreateCommand(data);
            this.Send(command, "DisableLimit");
        }

        public PanTiltLimit GetLimits()
        {
            return this._limits;
        }

        public bool SetLimits(PanTiltLimit panTiltLimit)
        {
            Task.Run(() =>
            {
                var positionChecker = new PositionChecker(this);

                //Disable limits
                this.DisableLimit();

                //Move to zero position
                this.PanTiltAbsolute(0, 0);
                positionChecker.ComparePosition(new PanTiltPosition(0, 0));

                //PanMin
                this.PanRelative(-30);
                positionChecker.ComparePosition(new PanTiltPosition(panTiltLimit.PanMin - 10, 0), tolerance: 5, timeout: 50, retry: 200);
                this.SetLimitLeft();
                this.PanAbsolute(panTiltLimit.PanMin);
                positionChecker.ComparePosition(new PanTiltPosition(panTiltLimit.PanMin, 0));
                this.SetLimitLeft();

                //PanMax
                this.PanRelative(30);
                positionChecker.ComparePosition(new PanTiltPosition(panTiltLimit.PanMax + 10, 0), tolerance: 5, timeout: 50, retry: 200);
                this.SetLimitRigth();
                this.PanAbsolute(panTiltLimit.PanMax);
                positionChecker.ComparePosition(new PanTiltPosition(panTiltLimit.PanMax, 0));
                this.SetLimitRigth();

                //Move to zero position
                this.PanTiltAbsolute(0, 0);
                positionChecker.ComparePosition(new PanTiltPosition(0, 0));

                //TiltMin
                this.TiltRelative(-20);
                positionChecker.ComparePosition(new PanTiltPosition(0, panTiltLimit.TiltMin - 10), tolerance: 5, timeout: 50, retry: 200);
                this.SetLimitDown();
                this.TiltAbsolute(panTiltLimit.TiltMin);
                positionChecker.ComparePosition(new PanTiltPosition(0, panTiltLimit.TiltMin));
                this.SetLimitDown();

                //TiltMax
                this.TiltRelative(20);
                positionChecker.ComparePosition(new PanTiltPosition(0, panTiltLimit.TiltMax + 10), tolerance: 5, timeout: 50, retry: 200);
                this.SetLimitUp();
                this.TiltAbsolute(panTiltLimit.TiltMax);
                positionChecker.ComparePosition(new PanTiltPosition(0, panTiltLimit.TiltMax));
                this.SetLimitUp();

                //Enable limits
                this.EnableLimit();

                //Move to zero position
                this.PanTiltAbsolute(0, 0);
                positionChecker.ComparePosition(new PanTiltPosition(0, 0));
            });

            return true;
        }

        #endregion

        #region Relay

        public void SetRelay(int index, bool active)
        {
            if (index <= 0 || index > 8)
            {
                return;
            }

            //the first relay is on 10 in hex (0x10 is 16 in ascii)
            var relayCommand = 16;
            //Every relay index is on a 10 in hex
            relayCommand += (index - 1) * 16;

            if (active)
            {
                relayCommand++;
            }

            var data = new byte[2];
            data[0] = 0x41;
            data[1] = (byte)relayCommand;

            var command = this.CreateCommand(data);
            this.Send(command, $"SetRelay Index:{index} Active:{active}");
        }

        #endregion

        #region Feedback

        public void ActivateFeedback()
        {
            //0x7A = 25ms (z)
            //0xEA = 40ms
            //0xEB = 65ms

            var data = new byte[1];
            data[0] = (byte)'z';

            var command = this.CreateCommand(data);
            this.Send(command, "ActivateFeedback");
        }

        public void DeactivateFeedback()
        {
            var data = new byte[1];
            data[0] = (byte)'Z';

            var command = this.CreateCommand(data);
            this.Send(command, "DeactivateFeedback");
        }

        public void QueryLimits()
        {
            var firstPanLimitCmd = new byte[1];
            firstPanLimitCmd[0] = 0xAA;

            var command = this.CreateCommand(firstPanLimitCmd);
            this.Send(command, "GetLimitPanMin");

            var secondPanLimitCmd = new byte[1];
            secondPanLimitCmd[0] = 0xAB;

            command = this.CreateCommand(secondPanLimitCmd);
            this.Send(command, "GetLimitPanMax");

            var firstTiltLimitCmd = new byte[1];
            firstTiltLimitCmd[0] = 0xAC;

            command = this.CreateCommand(firstTiltLimitCmd);
            this.Send(command, "GetLimitTiltMin");

            var secondTiltLimitCmd = new byte[1];
            secondTiltLimitCmd[0] = 0xAD;

            command = this.CreateCommand(secondTiltLimitCmd);
            this.Send(command, "GetLimitTiltMax");

            this.LimitChanged?.Invoke();
        }

        #endregion

        #region Communication

        private byte[] CreateCommand(byte[] data)
        {
            var controlBytes = new byte[3];
            controlBytes[0] = (byte)'P';
            controlBytes[1] = (byte)(this._dipReceiverPort >> 7);
            controlBytes[1] |= 0x80;
            controlBytes[2] = (byte)this._dipReceiverPort;
            controlBytes[2] |= 0x80;

            var finishByte = new byte[1] { 0x00 };

            return controlBytes.Concat(data).Concat(finishByte).ToArray();
        }

        private bool Send(byte[] command, string description)
        {
            if (this._debug)
            {
                var hex = BitConverter.ToString(command);
                Log.Debug($"{nameof(Send)} - {hex}");
            }

            Interlocked.Increment(ref this._sendCount);

            try
            {
                return this._communication.Send(command, description);
            }
            catch (ObjectDisposedException exception)
            {
                Log.Debug(nameof(Send), exception);
            }
            catch (Exception exception)
            {
                Log.Error(nameof(Send), exception);
            }

            return false;
        }

        #endregion

        #region IPanTiltControl

        public bool PanAbsolute(double degree)
        {
            return this.MovePanAbsolute(degree);
        }

        public bool TiltAbsolute(double degree)
        {
            return this.MoveTiltAbsolute(degree);
        }

        public bool PanTiltAbsolute(double panDegree, double tiltDegree)
        {
            if (this.MovePanAbsolute(panDegree) && this.MoveTiltAbsolute(tiltDegree))
            {
                return true;
            }

            return false;
        }

        public bool PanRelative(double degreePerSecond)
        {
            return this.MoveRelative(degreePerSecond, 0);
        }

        public bool TiltRelative(double degreePerSecond)
        {
            return this.MoveRelative(0, degreePerSecond);
        }

        public bool PanTiltRelative(double panDegreePerSecond, double tiltDegreePerSecond)
        {
            return this.MoveRelative(panDegreePerSecond, tiltDegreePerSecond);
        }

        public PanTiltPosition GetPosition()
        {
            return this._position;
        }

        #endregion

        public async Task<string> GetFirmwareAsync()
        {
            this.DeactivateFeedback();

            await Task.Delay(100);

            var firmware = string.Empty;
            var receiveData = new Action<byte[]>(delegate (byte[] data) { firmware += Encoding.ASCII.GetString(data); });

            this._communication.ReceiveData += receiveData;
            var firmwareCommand = new byte[] { 0x50, 0x80, 0x81, 0x69, 0x00 };
            this._communication.Send(firmwareCommand, "GetFirmware");
            await Task.Delay(500);
            this._communication.ReceiveData -= receiveData;

            this.ActivateFeedback();

            return firmware;
        }
    }
}
