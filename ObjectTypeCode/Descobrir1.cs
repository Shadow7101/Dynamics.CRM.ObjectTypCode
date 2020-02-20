using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTypeCode
{
    public class Descobrir1
    {
        private readonly IOrganizationService service;
        public Descobrir1()
        {
            this.service = CrmConnect365.CreateConnection();
        }

        public  string Reterieve(string entitylogicalname)
        {
            Entity entity = new Entity(entitylogicalname);
            RetrieveEntityRequest EntityRequest = new RetrieveEntityRequest();
            EntityRequest.LogicalName = entity.LogicalName;
            EntityRequest.EntityFilters = EntityFilters.All;
            RetrieveEntityResponse responseent = (RetrieveEntityResponse)service.Execute(EntityRequest);
            EntityMetadata ent = (EntityMetadata)responseent.EntityMetadata;
            string ObjectTypeCode = ent.ObjectTypeCode.ToString();
            return ObjectTypeCode;
        }
        public string GetEntityLogicalName(int EntityTypeCode)
        {
            var entityFilter = new MetadataFilterExpression(LogicalOperator.And);
            entityFilter.Conditions.Add(new MetadataConditionExpression("ObjectTypeCode ", MetadataConditionOperator.Equals, EntityTypeCode));
            var propertyExpression = new MetadataPropertiesExpression { AllProperties = false };
            propertyExpression.PropertyNames.Add("LogicalName");
            var entityQueryExpression = new EntityQueryExpression()
            {
                Criteria = entityFilter,
                Properties = propertyExpression
            };

            var retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest()
            {
                Query = entityQueryExpression
            };

            var response = (RetrieveMetadataChangesResponse)service.Execute(retrieveMetadataChangesRequest);

            if (response.EntityMetadata.Count == 1)
            {
                return response.EntityMetadata[0].LogicalName;
            }
            return null;
        }

    }
}
