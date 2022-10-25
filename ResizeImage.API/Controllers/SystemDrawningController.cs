using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Mvc;

namespace ResizeImage.API.Controllers;

[ApiController]
[Route("api/SystemDrawning/image")]
public class SystemDrawningController : ControllerBase
{
    /// <summary>
    /// Изменение размера изображения с помощью System.Drawning
    /// </summary>
    /// <param name="newWidth">новая ширина изображения</param>
    /// <param name="newHeight">новая высота изображения</param>
    /// <param name="encodedImage">изображение, закодированное в base64</param>
    /// <returns>Изображение с измененным размером</returns>
    [HttpPost("resize/{newWight}/{newHeight}")]
    public IActionResult ResizeImage(int newWidth, int newHeight, [FromBody] string encodedImage)
    {
        using var stream = new MemoryStream(Convert.FromBase64String(encodedImage));

        using var image = Image.FromStream(stream);
        using var destinationBitmap = new Bitmap(newWidth, newHeight);
        using var destinationGraphics = Graphics.FromImage((System.Drawing.Image) destinationBitmap);
        destinationGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        destinationGraphics.DrawImage(image, 0,0,newWidth, newHeight);

        var converter = new ImageConverter();
        var byteArrayImage = (byte[]) converter.ConvertTo(destinationBitmap, typeof(byte[]));

        return File(byteArrayImage, "image/jpeg");
    }

    /// <summary>
    /// Изменнеие размера изображения с помощью System.Drawning
    /// </summary>
    /// <param name="scaleRatio">значение изменнеия размера изображения в процентах</param>
    /// <param name="encodedImage">изображение, закодированное в base64</param>
    /// <returns>Изображение с измененным размером</returns>
    [HttpPost("scale/{scaleRatio}")]
    public IActionResult ScaleImage(int scaleRatio, [FromBody] string encodedImage)
    {
        using var stream = new MemoryStream(Convert.FromBase64String(encodedImage));

        using var image = Image.FromStream(stream);

        var destinationWight = (image.Width * scaleRatio) / 100;
        var destinationHeight = (image.Height * scaleRatio) / 100;

        using var destinationBitmap = new Bitmap(destinationWight, destinationHeight);
        using var destinationGraphics = Graphics.FromImage((System.Drawing.Image) destinationBitmap);
        destinationGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        destinationGraphics.DrawImage(image, 0,0,destinationWight, destinationHeight);

        var converter = new ImageConverter();
        var byteArrayImage = (byte[]) converter.ConvertTo(destinationBitmap, typeof(byte[]));

        return File(byteArrayImage, "image/jpeg");
    }
}