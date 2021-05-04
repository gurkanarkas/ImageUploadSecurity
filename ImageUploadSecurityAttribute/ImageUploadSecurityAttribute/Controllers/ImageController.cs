using ImageUploadSecurityAttribute.Attributes;
using ImageUploadSecurityAttribute.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadSecurityAttribute.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [CheckImageValidation(FileParam = "model.Source")]
        public string Upload(ImageRequest model)
        {
            return "OK";
        }
    }
}
