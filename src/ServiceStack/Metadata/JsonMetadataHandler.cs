using System;
using System.Web.UI;
using ServiceStack.Host;
using ServiceStack.Serialization;
using ServiceStack.Server;
using ServiceStack.Text;
using ServiceStack.Utils;

namespace ServiceStack.Metadata
{
    public class JsonMetadataHandler : BaseMetadataHandler
    {
        public override Format Format { get { return Format.Json; } }
		
		protected override string CreateMessage(Type dtoType)
        {
            var requestObj = ReflectionUtils.PopulateObject(dtoType.CreateInstance());
            return JsonDataContractSerializer.Instance.SerializeToString(requestObj);
        }

        protected override void RenderOperations(HtmlTextWriter writer, IHttpRequest httpReq, ServiceMetadata metadata)
        {
            var defaultPage = new OperationsControl
            {
				Title = EndpointHost.Config.ServiceName,
                OperationNames = metadata.GetOperationNamesForMetadata(httpReq, Format),
                MetadataOperationPageBodyHtml = EndpointHost.Config.MetadataOperationPageBodyHtml,
            };

            defaultPage.RenderControl(writer);
        }
    }
}