using QRCoder;
using System;
using System.Drawing;
using System.IO;

namespace AvaliadorPI.API
{
    public static class Util
    {
        public static byte[] GenerateQRCode(Guid id)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(id.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap img = qrCode.GetGraphic(20);

            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
