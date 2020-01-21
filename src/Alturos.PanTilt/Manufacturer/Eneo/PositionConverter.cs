using System;

namespace Alturos.PanTilt.Manufacturer.Eneo
{
    public class PositionConverter
    {
        public void ConvertPositionData(double position, out byte data1, out byte data2)
        {
            //To be able to encode two decimal points
            var targetPosition = (int)(position * 100);

            if (targetPosition == 0)
            {
                //Set angle position zero
                data1 = 0xDA;
                data2 = 0xAA;
                return;
            }

            if (targetPosition > 0)
            {
                data1 = (byte)(targetPosition >> 8);
                data2 = (byte)(targetPosition & 0x00FF);

                if (data1 == 0x00)
                {
                    data1 = 0x50;
                }

                if (data2 == 0x00)
                {
                    //Dont move the order it's important first data2
                    data2 = data1;
                    data1 = 0x51;
                }

                return;
            }

            //position < 0
            data1 = (byte)(Math.Abs(targetPosition) >> 8);
            data1 |= 0x80;

            data2 = (byte)(Math.Abs(targetPosition) & 0x00FF);

            if (data1 == 0x00)
            {
                data1 = 0xD0;
            }

            if (data2 == 0x00)
            {
                //Dont move the order it's important first data2
                data2 = data1;
                data1 = 0xD1;
            }
        }
    }
}