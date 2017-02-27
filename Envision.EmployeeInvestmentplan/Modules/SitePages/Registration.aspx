<%@ Assembly Name="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WikiEditPage" MasterPageFile="~masterurl/default.master" meta:webpartpageexpansion="full" meta:progid="SharePoint.WebPartPage.Document" %>

<%@ Import Namespace="Microsoft.SharePoint.WebPartPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    员工身份
                </td>
                <td>
                    <input type="text" value="" id="employee" />
                </td>

            </tr>
             <tr>
                <td>
                    风收宝可购额度
                </td>
                <td>
                    <input type="text" value="" id="amountF" />
                </td>

            </tr>
             <tr>
                <td>
                    菠萝蜜可购额度

                </td>
                <td>
                    <input type="text" value="" id="amountB" />
                </td>

            </tr>
            <tr>
                <td>
                    <input type="button" value="Submit" id="btnSubmit" />
                </td>
            </tr>
        </table>
    </div>
    <script src="../_layouts/15/Investment/Scripts/jquery-1.11.3.min.js"></script>
    <script type="text/javascript">

        $(function () {
            var employee = $("#employee").val();
            var amountF = $("#amountF").val();
            var amountB = $("#amountB").val();
            $("#btnSubmit").click(function () {
                $.ajax({
                    type: "POST",
                    url: "/_layouts/15/Investment/Handlers/AjaxHandlers.ashx?MethodName=" + url,
                    async: false,
                    contentType: "application/json; charset=utf-8",

                    success: function (msg) {

                        if (msg == "OK") {
                            alert("The file has been successfully transferred !");
                            window.location.href = targetUrl;
                            //window.navigate("AllItems.aspx");
                        }
                        else if (msg == "Fail") {
                            alert("The file has NOT been successfully transferred... Please try again !");
                            window.location.reload();
                        }

                    },
                    error: function (msg) {
                        alert("error");
                    }
                });

            });
        });
    </script>

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Registration
    <link href="../_layouts/15/Investment/Scripts/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
</asp:Content>



