<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManualHtmlDetailCtrl.ascx.cs" Inherits="Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Manual.ManualHtmlDetailCtrl" %>
<link href="/_layouts/15/Envision.KMS.Solution/Styles/Site.css" rel="stylesheet" />
<div class="w1200 hz focuswin-detail" id="manualdetail">
    <div class="focuswin-detail-head">
        <div class="focuswin-detail-title"><span data-bind="text: Title"></span></div>
    </div>
    <div class="focuswin-detail-content ms-rtestate-field">
        <div data-bind="html: ContentHtml">
        </div>
    </div>
</div>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/HomePage/ManualDetail.js"></script>
