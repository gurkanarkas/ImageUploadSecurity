using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace ImageUploadSecurityAttribute.Helpers
{
    public class ImageFilterAttributeExecuter
    {
        public void Execute(List<KeyValuePair<string, object>> pData, string fileParam)
        {
            var _params = fileParam.Contains(".") ? fileParam.Split('.') : null;
            var _helper = new ImageHelper();
            if (pData != null && pData.Count > 0)
            {
                for (int i = 0; i < pData.Count; i++)
                {
                    if (_params[0] == pData[i].Key)
                    {
                        var type = pData[i].Value.GetType();
                        var parameters = type.GetProperties();
                        if (parameters != null)
                        {
                            var subParam = parameters.FirstOrDefault(x => x.Name == _params[1]);
                            if (subParam != null)
                            {
                                dynamic model = subParam.GetValue(pData[i].Value, null);
                                if (!_helper.CheckImageSign(model.ToString()))
                                {
                                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                                }
                            }
                        }
                    }
                    else if (_params != null)
                    {
                        var type = pData[i].Value.GetType();
                        var parameters = type.GetProperties();
                        if (parameters != null)
                        {
                            var subParam = parameters.FirstOrDefault(x => x.Name == _params[0]);

                            if (subParam != null)
                            {
                                dynamic model = subParam.GetValue(pData[i].Value, null);
                                if (model != null)
                                {
                                    var mType = model[0].GetType();
                                    var mParams = (PropertyInfo[])mType.GetProperties();
                                    for (int m = 0; m < mParams.Length; m++)
                                    {
                                        string src = string.Empty;
                                        if (mParams[m].Name == _params[1])
                                        {
                                            var source = mParams[m].GetValue(model[0], null);
                                            if (!_helper.CheckImageSign(source))
                                            {
                                                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                                            }
                                        }
                                        else
                                        {
                                            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                                        }
                                    }
                                }
                                else
                                {
                                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                                }
                            }
                            else
                            {
                                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                            }
                        }
                        else
                        {
                            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                        }
                    }
                    else
                    {
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
                    }
                }
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
            }

        }
    }
}
