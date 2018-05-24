using Alturos.PanTilt.Eneo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading;

namespace Alturos.PanTilt.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("192.168.184.105");
            using (ICommunication communication = new UdpNetworkCommunication(ipAddress, 4003, 4003))
            using (IPanTiltControl control = new EneoPanTiltControl(communication))
            {
                control.Start();
                Thread.Sleep(200);
                var currentPosition = control.GetPosition();
                control.Stop();
            }


            //var ipAddress = IPAddress.Parse("192.168.184.105");
            //ICommunication communication = new TcpNetworkCommunication(new IPEndPoint(ipAddress, 4003));
            //var communication = new SerialPortCommunication("COM1");
            //ICommunication communication = new UdpNetworkCommunication(ipAddress, 4003, 4003);

            ////var reader = new FirmwareReader(communication);
            ////reader.Dispose();

            //IPanTiltControl panTiltControl = new EneoPanTiltControl(communication);
            //Thread.Sleep(1000);

            ////panTiltControl.ReinitializePosition();
            ////panTiltControl.ReinitializePosition(); //2x ???? Warum
            ////Console.WriteLine("Press enter for next command");
            ////Console.ReadLine();


            ////DriveRandomPositions(communication, false);
            //CircleMovementWithAbsolutePosition(communication, false);

            //communication.Dispose();
        }

        private static void PanTiltControl_OnPositionChanged(PanTiltPosition position)
        {
            Console.WriteLine("{0} - {1}", DateTime.Now, position.ToString());
        }

        private static void BasicMovementLogic(ICommunication communication)
        {
            while (true)
            {
                Console.WriteLine("Start");
                var panTiltControl = new EneoPanTiltControl(communication);
                panTiltControl.PositionChanged += PanTiltControl_OnPositionChanged;
                Console.WriteLine("Pan 80");
                panTiltControl.MovePanAbsolute(80);
                panTiltControl.MoveTiltAbsolute(0);
                Thread.Sleep(4000);
                Console.WriteLine("Pan 20");
                panTiltControl.MovePanAbsolute(20);
                panTiltControl.MoveTiltAbsolute(0);
                Thread.Sleep(4000);
                Console.WriteLine("Pan 80");
                panTiltControl.MovePanAbsolute(80);
                panTiltControl.MoveTiltAbsolute(0);
                Thread.Sleep(4000);
                Console.WriteLine("Pan 20");
                panTiltControl.MovePanAbsolute(20);
                panTiltControl.MoveTiltAbsolute(0);
                Thread.Sleep(4000);
                Console.WriteLine("Pan Relative 10");
                panTiltControl.PanRelative(10);
                Thread.Sleep(4000);
                Console.WriteLine("Stop Moving");
                panTiltControl.StopMoving();
                Thread.Sleep(1000);
                Console.WriteLine("Pan 20");
                panTiltControl.MovePanAbsolute(20);
                panTiltControl.MoveTiltAbsolute(0);
                Thread.Sleep(2000);

                panTiltControl.PositionChanged -= PanTiltControl_OnPositionChanged;
                panTiltControl.Dispose();
                Console.WriteLine("done");
                //Console.ReadLine();
            }
        }

        private static void TestPanMovementWithStartBeforeEndReached(ICommunication communication)
        {
            Console.WriteLine("Start");
            var panTiltControl = new EneoPanTiltControl(communication);
            panTiltControl.PositionChanged += PanTiltControl_OnPositionChanged;

            while (true)
            {
                panTiltControl.PanAbsolute(0);
                panTiltControl.ComparePosition(new PanTiltPosition(0, 0), 0.5, 20, 100);

                panTiltControl.PanAbsolute(40);
                panTiltControl.ComparePosition(new PanTiltPosition(40, 0), 0.5, 20, 100);

                panTiltControl.PanRelative(-40);
                Thread.Sleep(500);

                panTiltControl.PanRelative(40);
                Thread.Sleep(500);

                panTiltControl.PanAbsolute(0);

                Console.WriteLine("Loop done");
            }
        }

        private static void CheckSomeSpecialCommands(ICommunication communication)
        {
            var panTiltControl = new EneoPanTiltControl(communication);
            panTiltControl.PositionChanged += PanTiltControl_OnPositionChanged;
            panTiltControl.GetPanPotentiometer();
            panTiltControl.GetTiltPotentiometer();
            panTiltControl.GetTemperature();
            for (var i = 0; i < 1000; i++)
            {
                panTiltControl.MoveRandom();
                Thread.Sleep(500);
            }
            Console.ReadLine();

            panTiltControl.PositionChanged -= PanTiltControl_OnPositionChanged;
            panTiltControl.Dispose();
            Console.WriteLine("done");
        }

        private static void DriveRandomPositions(ICommunication communication, bool debug)
        {
            var panTiltControl = new EneoPanTiltControl(communication, debug);
            panTiltControl.Start();

            var random = new Random();

            while (true)
            {
                var panForward = true;
                var tiltForward = true;

                if (random.Next(0, 2) == 1)
                {
                    panForward = false;
                }

                if (random.Next(0, 2) == 1)
                {
                    tiltForward = false;
                }

                for (var i = 0; i < 1000; i++)
                {
                    var panDegreePerSecond = random.Next(1, 40);
                    var tiltDegreePerSecond = random.Next(1, 15);

                    if (!panForward)
                    {
                        panDegreePerSecond = -panDegreePerSecond;
                    }

                    if (!tiltForward)
                    {
                        tiltDegreePerSecond = -tiltDegreePerSecond;
                    }

                    panTiltControl.MoveRelative(panDegreePerSecond, tiltDegreePerSecond);

                    Thread.Sleep(40);
                }

                panTiltControl.PanTiltAbsolute(0, 0);
                panTiltControl.ComparePosition(new PanTiltPosition(0, 0), 0.5, 20, 200);
            }

        }

        private static void CircleMovementWithAbsolutePosition(ICommunication communication, bool debug)
        {
            var panTiltControl = new EneoPanTiltControl(communication, debug);
            panTiltControl.Start();
            panTiltControl.DisableLimit();

            //var random = new Random();

            for (var i = 0; i< 50; i++)
            {
                var positions = CalculatePolygon(i, new PointF(0, 0), 7);
                foreach (var position in positions)
                {

                    panTiltControl.MovePanAbsolute(position.X);
                    panTiltControl.MoveTiltAbsolute(position.Y);
                    Thread.Sleep(40);
                }

                //panTiltControl.PanTiltAbsolute(0, 0);
                //panTiltControl.ComparePosition(new PanTiltPosition(0, 0), 0.5, 20, 200);
            }

            panTiltControl.EnableLimit();
            panTiltControl.Dispose();
        }

        private static List<PointF> CalculatePolygon(float outerCircleRadius, PointF center, float deg)
        {
            var vertices = new List<PointF>();

            if (deg < 5)
            {
                return vertices;
            }

            var rU = outerCircleRadius;
            var z = deg / 180.0 * Math.PI;
            var pt1X = center.X + rU;
            var pt1Y = center.Y;

            vertices.Add(new PointF(Convert.ToSingle(pt1X), Convert.ToSingle(pt1Y)));

            var j = 1;
            for (var i = z; i <= Math.PI * 2.0; i += z)
            {
                var ptX = ((pt1X - center.X) * Math.Cos(i)) + ((pt1Y - center.Y) * -Math.Sin(i)) + center.X;
                var ptY = ((pt1X - center.X) * Math.Sin(i)) + ((pt1Y - center.Y) * Math.Cos(i)) + center.Y;
                vertices.Add(new PointF((float)Math.Round(ptX, 2), (float)Math.Round(ptY, 2)));
                j += 1;

                //Fallback
                if (j > 50)
                {
                    break;
                }
            }

            return vertices;
        }
    }
}
