<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationCtrl.ascx.cs" Inherits="KMS_Copy.CONTROLTEMPLATES.KMS_Copy.NavigationCtrl" %>
<link href="/_layouts/15/KMS_Copy/Styles/header-default.css" rel="stylesheet" />

<div style="float: left; padding: 0px 40px; margin: 10px 0px; cursor: pointer" data-bind="click: Navigate2Root">
    <img src="/_layouts/15/KMS_Copy/Images/logo_envision.png" />
</div>

<div class="navbar-collapse mega-menu">
    <div>
        <ul class="nav navbar-nav" id="rootnav" style="margin-top: 32px"></ul>
    </div>
</div>
