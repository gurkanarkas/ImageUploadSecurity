using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadSecurityAttribute.Helpers
{
    public class ImageHelper
    {
        public bool CheckImageSign(string data)
        {
            var base64 = data;
            try
            {
                var base64array = data.Split(',');
                if (base64array.Length > 1)
                    base64 = base64array[1];

                var byteArray = Convert.FromBase64String(base64);
                using (var stream = new MemoryStream(byteArray))
                {
                    var buffer = stream.ToArray();
                    if (buffer.Length != stream.Length)
                    {
                        return false;
                    }

                    ImageType key = (ImageType)(buffer[1] << 8) + buffer[0];

                    if (key == ImageType.JPG || key == ImageType.PNG)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
          

            return false;
        }
    }
}
