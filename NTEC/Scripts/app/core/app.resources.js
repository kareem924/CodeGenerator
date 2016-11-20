
SampleNS.namespace("Resources");

SampleNS.Resources = (function () {
    "use strict";

    var RoleType = {
        None : 0,
    }

    return {
        Messages: {
            Welcome: "Welcome",
            Msg_Category_Saved: "Category Has Saved Successfully.",
            Msg_Ext_Parties_Saved: "External Parties Has Saved Successfully.",
            Msg_Select_File_Up: "Please select a file to upload!",
            Msg_File_Up_Suc: "File Uploaded Successfully.",
            Cancel: "Cancel",
            AdvanceSearch: "Advance Search",
            Subject: "Subject",
            FromTo: "From To",
            status: "status",
            FromDate: "From Date",
            ToDate: "To Date",
            Type: "Type",
            InternalRef: "Internal Ref",
            ExternalRef: "External Ref",
            Search: "Search",
            Ext_Party_Mas: "External Party Master",
            ID: "ID",
            PartName: "Party Name",
            Orgainzation: "Orgainzation",
            Save: "Save",
            Category: "Category",
            CategoreyName: "Categorey Name",

            Any: "Any",
            None: "None",
            Saved: "Saved",
            Forwarded: "Forwarded",
            Expired: "Expired",
            Closed: "Closed",
            Correspondence: "Correspondence",
            DraftSaved: "Draft saved.",
            Announcements: "Announcements",
            InternalMemo: "Internal Memo",
            Msg_Record_Delete: "Record Deleted Successfully",
            Msg_Record_Saved: "Record Saved Successfully",
            Title: "Title",
            Reference: "Reference",
            Msg_Record_Selection_Delete: "Please select a record to delete",

            lblInbox: "",
            lblSent: "",
            lblInternal: "",
            lblIntMemo: "",
            lblCirc: "",
            lblAncnmnt: "",
            lblDraft: "",
            lblDeleted: "",


            Msg_Confirm_Template_Change: "Changing template will erase your current data, are you sure you want to proceed?",
            Msg_Select_Category: "Please select a Category",
            Msg_Select_Date: "Please enter Date of Reply",
            Msg_Select_Subject: "Please enter subject",
            Msg_Select_From: "Please enter From",
            Msg_Select_To: "Please enter To",

            Msg_Record_Not_Saved: "Record did not Save Successfully",
            Msg_Record_Delete_Corr: "Correspondence deleted successfully",
            Msg_Record_Delete_Attachment: "Attachment deleted successfully",
            Msg_Record_Not_Delete_Attachment: "Attachment did not deleted successfully",
            Msg_Sure_Close_Window : "",
            Msg_Sure_Cnfrm_Delete: "",
            Msg_No_File_Selected: "",
            Msg_CorrSaved: "",

            Msg_CORR_Sent:"",
            AddNewCorr: "",
            AddNewCir: "",
            AddNewIM: "",
            AddNewAnn: "",
            AddInBound:"",
            AddNewInternal: "",
            
            Inbound: "",
            Outbound: "",
            Internal:"",
            
            Msg_CorrFwded: "",
            Msg_FwdBy: "",

            Archived: "",
            Archive: "",

            CategoreyID: "Category Id",
            CreatedDate: "Created Date",
            ExternalPartyId: "External Party Id",
            ExternalPartyName: "External Party Name",
            OrganizationName: "Organization Name",
            ContactName: "Contact Name",
            ContactNumber: "Contact Number",
            Email: "Email",
            Address: "Address",
            Msg_User_Name: "You must enter a user name.",
            Msg_Password: "You must enter a password.",

            Of: "Of",
            Page: "Page",
            View: "View",
            SendItTo: "Send it to",
            File: "File",
            Print: "Print",

            Forward: "Forward",
            Circular: "Circular",
            Reply: "Reply",
            Accountant: "Accountant",
            AbuAlHamd: "Abu Al Hamd",
            
            AlignColumn: "left",
            UserManagement: "User Management",
            DefaultUser: "Default User",
            ActionSelect: "Action",
            MessageSelect: "Please select corresspondence first",
           

            LoggedInUserId:0,
            UserId: 0,
            LanguageId: 2
        },
        Views: {
            LoginURL: SampleNS.Globals.baseURL + "Login",
            FileOpenURL: SampleNS.Globals.baseURL + "Home/CorrespondenceAddEdit" ,
            SetCulture: SampleNS.Globals.baseURL + "LandingPanel/SetCulture",
            SetCultureAtLogin: SampleNS.Globals.baseURL + "Login/SetCulture",
            AuditTrail: SampleNS.Globals.baseURL + "AuditTrailReport/Index"
        },
        Images: {
            CloseImageURL: SampleNS.Globals.baseURL + "content/autoComplete/images/close_icon.gif",
            ExpImageURL: SampleNS.Globals.baseURL + "content/images/img-expand.png",
            ColapseImageURL: SampleNS.Globals.baseURL + "content/images/img-collapse.png",
            ImageURL: SampleNS.Globals.baseURL + "Images/IconsAuxCodes/",
            ContentFolderImages: SampleNS.Globals.baseURL + "Images/Content/images/"
        },
        FilePath:{
            
        },
        TemplatePath:{
           
        },
        CurrentUserInfo: {
                LoginId: ''
        }
     };
} ());

