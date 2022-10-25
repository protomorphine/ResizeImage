using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ResizeImage.API.Controllers;

[ApiController]
[Route("api/ImageSharp/image")]
public class ImageSharpController : ControllerBase
{
    /// <summary>
    /// Изменение размера изображения с помощью ImageSharp
    /// </summary>
    /// <param name="newWidth">новая ширина изображения</param>
    /// <param name="newHeight">новая высота изображения</param>
    /// <param name="encodedImage">изображение, закодированное в base64</param>
    /// <returns>Изображение с измененным размером</returns>
    [HttpPost("resize/{newWidth}/{newHeight}")]
    public async Task<IActionResult> ResizeImage(int newWidth, int newHeight, [FromBody] string encodedImage)
    {
        await using var stream = new MemoryStream(Convert.FromBase64String(encodedImage));
        using var image = await Image.LoadAsync(stream);

        image.Mutate(img => img.Resize(new Size(newWidth, newHeight)));

        var resizedImageBase64String = image.ToBase64String(JpegFormat.Instance)
            .Replace("data:image/jpeg;base64,", "");

        return File(Convert.FromBase64String(resizedImageBase64String), "image/jpg");
    }

    /// <summary>
    /// Изменнеие размера изображения с помощью ImageSharp
    /// </summary>
    /// <param name="scaleRatio">значение изменнеия размера изображения в процентах</param>
    /// <param name="encodedImage">изображение, закодированное в base64</param>
    /// <returns>Изображение с измененным размером</returns>
    [HttpPost("scale/{scaleRatio}")]
    public async Task<IActionResult> ScaleImage(int scaleRatio, [FromBody] string encodedImage)
    {
        await using var stream = new MemoryStream(Convert.FromBase64String(encodedImage));
        using var image = await Image.LoadAsync(stream);

        var size = new Size(
            (image.Width * scaleRatio)/100,
            (image.Height * scaleRatio)/100
            );

        image.Mutate(op => op.Resize(size));

        var resizedImageBase64String = image.ToBase64String(JpegFormat.Instance).Replace("data:image/jpeg;base64,", "");

        return File(Convert.FromBase64String(resizedImageBase64String), "image/jpg");
    }
}