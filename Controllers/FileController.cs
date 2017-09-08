using DropzoneJS.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DropzoneJS.Controllers
{
    public class FileController : ApiController
    {
        [RoutePrefix("api/file")]
        public class FileUploadController : ApiController
        {

            private static readonly string folderLocation = "C:\\Temp";

            [Route("upload")]
            [HttpPost]
            [MimeContentFilter]
            public async Task<bool> UpoadFile()
            {
                try
                {
                    var streamProvider = new OwnMultipartFormDatastreamProvider(folderLocation);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);

                    return await Task.FromResult(true);
                }
                catch
                {
                    return false;
                }
            }

            public class OwnMultipartFormDatastreamProvider: MultipartFormDataStreamProvider
            {
                public OwnMultipartFormDatastreamProvider(string path) : base(path)
                { }

                public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
                {
                    // override the filename which is stored by the provider (by default is bodypart_x)
                    string originalFileName = headers.ContentDisposition.FileName.Trim('\"');

                    return originalFileName;
                }
            }
        }
    }
}
