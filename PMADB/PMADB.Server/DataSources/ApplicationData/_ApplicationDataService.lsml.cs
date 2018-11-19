using Microsoft.LightSwitch.Security.Server;
using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class ApplicationDataService
    {
        //partial void remainingProducts_PreprocessQuery(int? applicationID, ref IQueryable<si_lu_product> query)
        //{

        //    query = from prod in query
        //            where !prod.si_application_product.Any(a => a.si_applications.Id == applicationID)
        //            select prod;
        //}

        partial void remainingMediaTypes_PreprocessQuery(int? applicationID, ref IQueryable<si_lu_media> query)
        {
            query = from media in query
                    where !media.si_application_mediaCollection.Any(a => a.si_application.Id == applicationID)
                    select media;
        }

        partial void remainingApplications_PreprocessQuery(int? meetingID, ref IQueryable<si_application> query)
        {
            query = from app in query
                    where !app.si_meeting_decision.Any(b => b.si_meeting.Id == meetingID)
                    select app;
        }

        partial void si_lu_products_CanDelete(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.DelLookupData);
        }

        partial void si_lu_products_CanInsert(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.AddLookupData);
        }

        partial void si_lu_products_CanUpdate(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.EditLookupData);
        }

        partial void si_lu_mediaSet_CanDelete(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.DelLookupData);
        }

        partial void si_lu_mediaSet_CanInsert(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.AddLookupData);
        }

        partial void si_lu_mediaSet_CanUpdate(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.EditLookupData);
        }

        partial void si_lu_adv_purposes_CanDelete(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.DelLookupData);
        }

        partial void si_lu_adv_purposes_CanInsert(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.AddLookupData);
        }

        partial void si_lu_adv_purposes_CanRead(ref bool result)
        {
            result = Application.User.HasPermission(Permissions.EditLookupData);
        }

        partial void si_meeting_decisions_Inserting(si_meeting_decision entity)
        {
            entity.decision = "Pending";
        }

        partial void si_meeting_decisions_Validate(si_meeting_decision entity, EntitySetValidationResultsBuilder results)
        {
            ///Not using because unable to differentiate between update and insert event
            ///

            //var ds = applicationExists(entity.si_applications.Id, entity.si_meeting.Id);
            
            //if(ds.Count()>0)
            //{
            //    results.AddPropertyError(entity.ApplicationID + " is a Duplicate Entry", entity.Details.Properties.si_applications);
            //}
        }

        partial void applicationExists_PreprocessQuery(int? appID, int? meetingID, ref IQueryable<si_meeting_decision> query)
        {
            query = query.Where(x => x.si_meeting.Id == meetingID && x.si_applications.Id == appID);
        }

        partial void si_applicants_CanDelete(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.DelAppData);
        }

        partial void si_applicants_CanInsert(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.AddAppData);
        }

        partial void si_applicants_CanUpdate(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.EditAppData);
        }

        partial void si_application_adv_details_CanDelete(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.DelAppData);
        }

        partial void si_application_adv_details_CanInsert(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.AddAppData);
        }

        partial void si_application_adv_details_CanUpdate(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.EditAppData);
        }

        partial void si_application_products_CanDelete(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.DelAppData);
        }

        partial void si_application_products_CanInsert(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.AddAppData);
        }

        partial void si_application_products_CanUpdate(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.EditAppData);
        }

        partial void si_applications_CanDelete(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.DelAppData);
        }

        partial void si_applications_CanInsert(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.AddAppData);
        }

        partial void si_applications_CanUpdate(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.EditAppData);
        }

        partial void si_meeting_decisions_CanDelete(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.DelAppData);
        }

        partial void si_meeting_decisions_CanInsert(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.AddAppData);
        }

        partial void si_meeting_decisions_CanUpdate(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.EditAppData);
        }

        partial void si_applicants_Deleting(si_applicant entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_applicants_Updating(si_applicant entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_application_adv_details_Deleting(si_application_adv_detail entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_application_adv_details_Updating(si_application_adv_detail entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_application_products_Deleting(si_application_product entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_application_products_Updating(si_application_product entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_applications_Deleting(si_application entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_applications_Updating(si_application entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_lu_adv_purposes_Deleting(si_lu_adv_purpose entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_lu_adv_purposes_Updating(si_lu_adv_purpose entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_lu_mediaSet_Deleting(si_lu_media entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_lu_mediaSet_Updating(si_lu_media entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_lu_products_Deleting(si_lu_product entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_lu_products_Updating(si_lu_product entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_meeting_decisions_Deleting(si_meeting_decision entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_meeting_decisions_Updating(si_meeting_decision entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }

        partial void si_meetings_Deleting(si_meeting entity)
        {
            AuditHelper.CreateAuditTrailForDelete(entity, this);
        }

        partial void si_meetings_Updating(si_meeting entity)
        {
            AuditHelper.CreateAuditTrailForUpdate(entity, this);
        }
    }
}