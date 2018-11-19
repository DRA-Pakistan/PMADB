using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LightSwitchApplication
{
    public static class AuditHelper
    {
        public static void CreateAuditTrailForUpdate<E>(E entity, ApplicationData app) where E : IEntityObject
        {

            //StringBuilder newValues = new StringBuilder("New Values :" + Environment.NewLine);
            //StringBuilder oldValues = new StringBuilder("Original Values :" + Environment.NewLine);
            foreach (var prop in entity.Details.Properties.All().OfType<IEntityStorageProperty>())
            {
                if (prop.Name != "Id" && prop.Name != "RowVersion" && prop.Name != "Modified" && prop.Name != "ModifiedBy" && prop.Name != "Created" && prop.Name != "CreatedBy" && prop.Name != "isVerifyOnly")
                {
                    if (!(Object.Equals(prop.Value, prop.OriginalValue)))
                    {
                        AuditTrail auditRecord = app.AuditTrails.AddNew();
                        auditRecord.ChangeType = "Updated";
                        auditRecord.ReferenceId = (int)entity.Details.Properties["Id"].Value;
                        auditRecord.ReferenceType = entity.Details.EntitySet.Details.Name;
                        auditRecord.Updated = DateTime.Now;
                        auditRecord.ChangedBy = Application.Current.User.FullName;
                        //oldValues.AppendLine(string.Format("{0}: {1}", prop.Name, prop.OriginalValue));
                        //newValues.AppendLine(string.Format("{0}: {1}", prop.Name, prop.Value));
                        auditRecord.ReferenceProp = prop.Name;
                        // Convert.Tostring() returns empty string if value is null
                        auditRecord.OriginalValues = Convert.ToString(prop.OriginalValue);//prop.OriginalValue.ToString() ?? " ";
                        auditRecord.NewValues = prop.Value.ToString();
                    }
                }
            }

            foreach (var prop in entity.Details.Properties.All().OfType<IEntityReferenceProperty>())
            {
                if (prop.Name != "Id" && prop.Name != "RowVersion" && prop.Name != "Modified" && prop.Name != "ModifiedBy" && prop.Name != "Created" && prop.Name != "CreatedBy" && prop.Name != "isVerifyOnly")
                {
                    if (!(Object.Equals(prop.Value, prop.OriginalValue)))
                    {
                        AuditTrail auditRecord = app.AuditTrails.AddNew();
                        auditRecord.ChangeType = "Updated";
                        auditRecord.ReferenceId = (int)entity.Details.Properties["Id"].Value;
                        auditRecord.ReferenceType = entity.Details.EntitySet.Details.Name;
                        auditRecord.Updated = DateTime.Now;
                        auditRecord.ChangedBy = Application.Current.User.FullName;
                        //oldValues.AppendLine(string.Format("{0}: {1}", prop.Name, prop.OriginalValue));
                        //newValues.AppendLine(string.Format("{0}: {1}", prop.Name, prop.Value));
                        auditRecord.ReferenceProp = prop.Name;
                        auditRecord.OriginalValues = prop.OriginalValue.ToString() ?? " ";
                        auditRecord.NewValues = prop.Value.ToString();
                    }
                }
            }
        }

        public static void CreateAuditTrailForInsert<E>(E entity, ApplicationData app) where E : IEntityObject
        {

            StringBuilder newValues = new StringBuilder("Inserted Values :" + Environment.NewLine);

            foreach (var prop in entity.Details.Properties.All().OfType<IEntityStorageProperty>())
            {
                if (prop.Name != "Id" && prop.Name != "RowVersion" && prop.Name != "Modified" && prop.Name != "ModifiedBy" && prop.Name != "Created" && prop.Name != "CreatedBy" && prop.Name != "isVerifyOnly")
                {
                    AuditTrail auditRecord = app.AuditTrails.AddNew();
                    auditRecord.ChangeType = "Inserted";
                    auditRecord.ReferenceId = (int)entity.Details.Properties["Id"].Value;
                    auditRecord.ReferenceType = entity.Details.EntitySet.Details.Name;
                    auditRecord.Updated = DateTime.Now;
                    auditRecord.ChangedBy = Application.Current.User.FullName;
                    auditRecord.ReferenceProp = prop.Name;
                    auditRecord.NewValues = prop.Value.ToString();
                }
            }

            foreach (var prop in entity.Details.Properties.All().OfType<IEntityReferenceProperty>())
            {
                if (prop.Name != "Id" && prop.Name != "RowVersion" && prop.Name != "Modified" && prop.Name != "ModifiedBy" && prop.Name != "Created" && prop.Name != "CreatedBy" && prop.Name != "isVerifyOnly")
                {
                    AuditTrail auditRecord = app.AuditTrails.AddNew();
                    auditRecord.ChangeType = "Inserted";
                    auditRecord.ReferenceId = (int)entity.Details.Properties["Id"].Value;
                    auditRecord.ReferenceType = entity.Details.EntitySet.Details.Name;
                    auditRecord.Updated = DateTime.Now;
                    auditRecord.ChangedBy = Application.Current.User.FullName;
                    auditRecord.ReferenceProp = prop.Name;
                    auditRecord.NewValues = prop.Value.ToString();
                }
            }
        }

        public static void CreateAuditTrailForDelete<E>(E entity, ApplicationData app) where E : IEntityObject
        {

            StringBuilder oldValues = new StringBuilder("Deleted Values :" + Environment.NewLine);

            foreach (var prop in entity.Details.Properties.All().OfType<IEntityStorageProperty>())
            {
                if (prop.Name != "Id" && prop.Name != "RowVersion" && prop.Name != "Modified" && prop.Name != "ModifiedBy" && prop.Name != "Created" && prop.Name != "CreatedBy" && prop.Name != "isVerifyOnly")
                {
                    AuditTrail auditRecord = app.AuditTrails.AddNew();
                    auditRecord.ChangeType = "Deleted";
                    auditRecord.Updated = DateTime.Now;
                    auditRecord.ChangedBy = Application.Current.User.FullName;
                    auditRecord.ReferenceId = (int)entity.Details.Properties["Id"].Value;
                    auditRecord.ReferenceType = entity.Details.EntitySet.Details.Name;
                    auditRecord.ReferenceProp = prop.Name;
                    auditRecord.OriginalValues = prop.OriginalValue.ToString();
                }
            }

            foreach (var prop in entity.Details.Properties.All().OfType<IEntityReferenceProperty>())
            {
                if (prop.Name != "Id" && prop.Name != "RowVersion" && prop.Name != "Modified" && prop.Name != "ModifiedBy" && prop.Name != "Created" && prop.Name != "CreatedBy" && prop.Name != "isVerifyOnly")
                {
                    AuditTrail auditRecord = app.AuditTrails.AddNew();
                    auditRecord.ChangeType = "Deleted";
                    auditRecord.Updated = DateTime.Now;
                    auditRecord.ChangedBy = Application.Current.User.FullName;
                    auditRecord.ReferenceId = (int)entity.Details.Properties["Id"].Value;
                    auditRecord.ReferenceType = entity.Details.EntitySet.Details.Name;
                    auditRecord.ReferenceProp = prop.Name;
                    auditRecord.OriginalValues = prop.OriginalValue.ToString();
                }
            }
        }
    }
}