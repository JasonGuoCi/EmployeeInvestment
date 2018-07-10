using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Xml.Linq;

namespace Envision.KMS.Model.Features.Envision.KMS.Model_Web
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("b6fb61ee-a405-4636-950e-39b3fef2125b")]
    public class EnvisionKMSEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb targetWeb = properties.Feature.Parent as SPWeb;

            using (SPSite site = new SPSite(targetWeb.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    BindLookUpField(web, "Field_KMS_ForumType", "ForumType");
                    BindLookUpField(web, "Field_KMS_Knowledge", "DefinitionLibrary");
                    BindLookUpField(web, "Field_Case_Type", "CaseType");
                }
            }
        }

        public void BindLookUpField(SPWeb web, string fieldName, string listname)
        {
            SPField lookupField = web.Fields.TryGetFieldByStaticName(fieldName);

            if (lookupField != null)
            {
                // Getting Schema of field
                XDocument fieldSchema = XDocument.Parse(lookupField.SchemaXml);

                // Get the root element of the field definition
                XElement root = fieldSchema.Root;

                // Check if list definition exits exists
                if (root.Attribute("List") != null)
                {
                    // Getting value of list url
                    SPList list = web.Lists[listname];

                    // Get the correct folder for the list
                    if (list != null)
                    {
                        // Setting the list id of the schema
                        XAttribute attrList = root.Attribute("List");
                        if (attrList != null)
                        {
                            // Replace the url wit the id
                            attrList.Value = list.ID.ToString();
                        }

                        // Setting the souce id of the schema
                        XAttribute attrWeb = root.Attribute("SourceID");
                        if (attrWeb != null)
                        {
                            // Replace the sourceid with the correct webid
                            attrWeb.Value = web.ID.ToString();
                        }

                        // Update field with new schema
                        lookupField.SchemaXml = fieldSchema.ToString();
                    }
                }
            }
        }

        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
