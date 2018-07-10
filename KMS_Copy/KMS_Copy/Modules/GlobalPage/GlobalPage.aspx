<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" MasterPageFile="~sitecollection/_catalogs/masterpage/Envision.KMS.Global.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" meta:webpartpageexpansion="full" %>

<asp:content id="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
</asp:content>

<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <script type="text/javascript">
        document.title = "Global";
    </script>
    <WebPartPages:SPProxyWebPartManager runat="server" ID="spproxywebpartmanager"></WebPartPages:SPProxyWebPartManager>
    <WebPartPages:WebPartZone ID="WikipediaZone" runat="server" Title="百科">
    </WebPartPages:WebPartZone>
</asp:content>

<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
</asp:content>

<asp:content id="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:content>
