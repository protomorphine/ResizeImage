using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

namespace ResizeImage.API.Controllers;

[ApiController]
[Route("api/SkiaSharp/image")]
public class SkiaSharpController : ControllerBase
{
    /// <summary>
    /// Изменение размера изображения с помощью SkiaSharp
    /// </summary>
    /// <param name="newWidth">новая ширина изображения</param>
    /// <param name="newHeight">новая высота изображения</param>
    /// <param name="encodedImage">изображение, закодированное в base64</param>
    /// <returns>Изображение с измененным размером</returns>
    [HttpPost("resize/{newWight}/{newHeight}")]
    public IActionResult ResizeImage(int newWidth, int newHeight, [FromBody] string encodedImage)
    {
        using var stream = new MemoryStream(Convert.FromBase64String(encodedImage));

        using var bitmap = SKBitmap.Decode(stream);

        var resizedBitmap = bitmap.Resize(new SKSizeI(newWidth, newHeight), SKFilterQuality.High);
        var resizedImg = SKImage.FromBitmap(resizedBitmap);

        return File(resizedImg.Encode().ToArray(), "image/jpeg");
    }

    /// <summary>
    /// Изменнеие размера изображения с помощью SkiaSharp
    /// </summary>
    /// <param name="scaleRatio">значение изменнеия размера изображения в процентах</param>
    /// <param name="encodedImage">изображение, закодированное в base64</param>
    /// <returns>Изображение с измененным размером</returns>
    [HttpPost("scale/{scaleRatio}")]
    public IActionResult ScaleImage(int scaleRatio, [FromBody] string encodedImage)
    {
        using var stream = new MemoryStream(Convert.FromBase64String(encodedImage));
        using var bitmap = SKBitmap.Decode(stream);

        var size = new SKSizeI(
            (bitmap.Width * scaleRatio)/100,
            (bitmap.Height * scaleRatio)/100
            );

        var scaledBitmap = bitmap.Resize(size, SKFilterQuality.High);
        var scaledImage = SKImage.FromBitmap(scaledBitmap);

        return File(scaledImage.Encode().ToArray(), "image/jpeg");
    }
}