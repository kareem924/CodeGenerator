/// <reference path="../../_references.js" />

function Enums() {
}


Enums.MessageType = { Error: 0, Success: 1, Loading: 2, Warning: 3, Info: 4 };

Enums.FormMode = { Add: 0, Edit: 1 };

Enums.CorrType = { None: 0, Correspondence: 1, Outbound: 2, InternalMemo: 3, Circular: 4, Internal: 5, CorrespondenceOnHold: 6, Archived: 10 };

Enums.ReceiptType = { None: 0, To: 1, CC: 2, BCC: 3, From: 4, CreatedBy: 5 }; // CreatedBy: 5 is also being used for forward

Enums.CToType = { OU: 1, Roles: 2, Users: 3, External: 4 };

Enums.Mode = { None: 0, Inbound: 1, InboundManual: 2, Outbound: 3 };

Enums.Status = { None: 0, Draft: 1, Sent: 2, Saved: 3, Forwarded: 4, Expired: 5, Closed: 6, Deleted: 7, Internal: 8, Archived: 10 };

Enums.Reply = { None: 0, Reply: 4, ReplyAll: 5, Forward: 6, ForwardToInternal: 7, ForwardToExternal: 8};

// 
