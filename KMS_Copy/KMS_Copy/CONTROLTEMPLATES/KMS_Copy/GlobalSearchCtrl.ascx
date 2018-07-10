<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GlobalSearchCtrl.ascx.cs" Inherits="KMS_Copy.CONTROLTEMPLATES.KMS_Copy.GlobalSearchCtrl" %>

<div class="search-open">
    <div class="input-group">
        <input type="text" class="form-control search-input-height" placeholder="Search" data-bind="value: SearchText" style="width: 210px; height: 30px; line-height: 30px; padding: 0px; margin: 0px;" />
        <span class="input-group-btn">
            <button class="btn-u" type="button" style="width: 58px; height: 32px; margin: 0px;" data-bind="click: Search, text: GoText"></button>
        </span>
    </div>
</div>

<div style="float: left; margin-left: 20px;">
    <button class="btn-s" type="button" style="min-width: 20px;" data-bind="click: switchLang.bind($data, '2025'), css: IsEnglish() ? 'btn-s' : 'btn-s active'">中文</button>
    <button class="btn-s" type="button" style="min-width: 20px;" data-bind="click: switchLang.bind($data, '1033'), css: IsEnglish() ? 'btn-s active' : 'btn-s'">English</button>
</div>

<script src="/_layouts/15/KMS_Copy/Scripts/HomePage/Header.js"></script>
