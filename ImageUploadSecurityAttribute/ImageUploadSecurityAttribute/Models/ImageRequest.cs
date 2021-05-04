using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadSecurityAttribute.Models
{
    public class ImageRequest
    {
        /// <summary>
        /// Image base64 string with mime type
        /// </summary>
        public string Source { get; set; }
    }
}
