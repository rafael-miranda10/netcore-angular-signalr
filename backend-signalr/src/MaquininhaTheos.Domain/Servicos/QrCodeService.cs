using MaquininhaTheos.CrossCuting.Resources;
using MaquininhaTheos.Domain.Entidades;
using MaquininhaTheos.Domain.Interfaces.Notificacoes;
using MaquininhaTheos.Domain.Interfaces.Repository;
using MaquininhaTheos.Domain.Interfaces.Servicos;
using MaquininhaTheos.Domain.Shared;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

namespace MaquininhaTheos.Domain.Servicos
{
    public class QrCodeService : IQrCodeService
    {
       // private readonly IQrCodeRepositorio _qrCodeRepositorio;
        public QrCodeService(
          //  IQrCodeRepositorio qrCodeRepositorio,
           // INotificationHandler notificacaoDeDominio
            )
        {
          //  _qrCodeRepositorio = qrCodeRepositorio;
        }

        public async Task<string> CreateQRCode(string content)
        {
            //await ValidarConteudoQrCode(content);

            //if (_notificacaoDeDominio.HasNotification())
            //    return string.Empty;

            var qrCodeWriter = CriarBarcoderWriter();
            var pixelData = qrCodeWriter.Write(content);
            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
            string qrCode = CodeQrGenerator(pixelData, bitmap);
            //await Salvar(new QrCode(qrCode));
            return qrCode;

        }

        private string CodeQrGenerator(PixelData pixelData, Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                        pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG   
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()));
            }
        }

        private async Task ValidarConteudoQrCode(string content)
        {
            //if (string.IsNullOrEmpty(content))
            //    await NotificarValidacoesDeServico(Resource.ConteudoQrCodeVazio);
        }

        private BarcodeWriterPixelData CriarBarcoderWriter()
        {
            return new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 250,
                    Width = 250,
                    DisableECI = true,
                    CharacterSet = "UTF-8"
                }
            };
        }

        public Task Salvar(QrCode qrCode)
        {
           // _qrCodeRepositorio.AdicionarAsync(qrCode);
            return Task.FromResult(true);
        }
    }
}
